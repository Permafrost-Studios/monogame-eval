using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Sprites;
using monogame_eval_project.Components;
using monogame_eval_project.Components.Abilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Systems
{
    public class PlayerBehaviourSystem : EntityUpdateSystem
    {
        private ComponentMapper<Components.Moveable> _moveableMapper;
        private ComponentMapper<Components.Life> _healthMapper;
        private ComponentMapper<Components.Abilities.StraightProjectileAbility> _abilitiesMapper;
        private ComponentMapper<SceneNode> _sceneNodeMapper;

        ContentManager _content;
        SceneGraph _sceneGraph;

        //Ability Textures

        public float _playerWalkSpeed = 192f; //Roughly 2 pi - 1 rev per second - this was spin speed b4
        public float _walkAcceleration = 0f;

        bool facingRight;

        Entity PlayerEntity;
        SpriteEntity playerRightSpriteEntity;
        SpriteEntity playerLeftSpriteEntity;
        SceneNode _playerNode;

        public PlayerBehaviourSystem(SceneGraph sceneGraph, ContentManager content)
        : base(Aspect.All(typeof(Components.Player), typeof(Components.Moveable), typeof(SceneNode), typeof(Components.Life)))
        {
            _sceneGraph = sceneGraph;
            _content = content;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _moveableMapper = mapperService.GetMapper<Components.Moveable>();
            _healthMapper = mapperService.GetMapper<Components.Life>();
            _abilitiesMapper = mapperService.GetMapper<Components.Abilities.StraightProjectileAbility>();
            _sceneNodeMapper = mapperService.GetMapper<SceneNode>();

            facingRight = true;

            SpawnPlayer();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var KeyboardState = Keyboard.GetState();

                Vector2 movementVector = new Vector2(0, 0);

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    movementVector.X = 1;

                    if (facingRight == false)
                    {
                        FlipFacingDirection(entity);
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    movementVector.X = -1;

                    if (facingRight == true)
                    {
                        FlipFacingDirection(entity);
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    movementVector.Y = -1;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    movementVector.Y = 1;
                }
                Vector2.Normalize(movementVector);

                _moveableMapper.Get(entity).Velocity = movementVector * (float)(_playerWalkSpeed);

                if (_healthMapper.Get(entity)._Health <= 0)
                {
                    Die();
                }

                //To test out spawning the abilities
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    _abilitiesMapper.Put(entity, new Components.Abilities.StraightProjectileAbility());
                    //GetEntity(entity).Add(new StraightProjectileAbility());
                }
            }
        }

        void Die()
        {
            foreach (var entity in ActiveEntities)
            {
                //Delete all of the player's components
                //  1. Remove the sceneNode from the scene tree
                //  2. Might have to delete the other classes or something idk

                //1.
                _sceneGraph.RootNode.Children.Remove((GetEntity(entity).Get<SceneNode>()));

                DestroyEntity(entity); //This only destroys the entity, not everything else about it

                //System.Environment.Exit(1); - Just for testing, It works :)

                //Transition to the end game screen or something
            }
        }

        void FlipFacingDirection(int entityID)
        {
            switch (facingRight)
            {
                case true:                    
                    _playerNode.Entities.Remove(playerRightSpriteEntity);
                    _playerNode.Entities.Add(playerLeftSpriteEntity);
                    break;
                case false:
                    _playerNode.Entities.Remove(playerLeftSpriteEntity);
                    _playerNode.Entities.Add(playerRightSpriteEntity);
                    break;
            }

            facingRight = !facingRight;
        }

        void SpawnPlayer()
        {
            //Load Player START
            var playerTextureRight = _content.Load<Texture2D>("PrototypeArt/badbot-shot");
            var playerTextureLeft = _content.Load<Texture2D>("PrototypeArt/badbot-shot-left");

            _playerNode = new SceneNode("Player", new Vector2(960, 540));

            Sprite playerRightSprite = new Sprite(playerTextureRight);
            playerRightSpriteEntity = new SpriteEntity(playerRightSprite);
            Sprite playerLeftSprite = new Sprite(playerTextureLeft);
            playerLeftSpriteEntity = new SpriteEntity(playerLeftSprite);

            _playerNode.Entities.Add(playerRightSpriteEntity);

            _sceneGraph.RootNode.Children.Add(_playerNode);
            //Load Player END

            PlayerEntity = CreateEntity();
            PlayerEntity.Attach(new Player());
            PlayerEntity.Attach(_playerNode);
            PlayerEntity.Attach(new Life(10f));
            PlayerEntity.Attach(new Collider(Collider.CollisionLayer.Player, 250f, 250f));
            PlayerEntity.Attach(new Moveable());
        }
    }
}
