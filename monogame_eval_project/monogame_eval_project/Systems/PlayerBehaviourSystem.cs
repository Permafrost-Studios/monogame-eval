using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Systems
{
    public class PlayerBehaviourSystem : EntityUpdateSystem
    {
        private ComponentMapper<SceneNode> _sceneNodeMapper;
        private ComponentMapper<Components.Player> _playerMapper;

        SceneGraph _sceneGraph;

        private ContentManager _content;

        //Ability Textures
        Texture2D bulletTexture;

        public PlayerBehaviourSystem(SceneGraph sceneGraph, ContentManager content)
        : base(Aspect.All(typeof(Components.Player), typeof(SceneNode)))
        {
            _sceneGraph = sceneGraph;
            _content = content;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _sceneNodeMapper = mapperService.GetMapper<SceneNode>();
            _playerMapper = mapperService.GetMapper<Components.Player>();

            bulletTexture = _content.Load<Texture2D>("PrototypeArt/tile_0000");
        }

        public override void Update(GameTime gameTime)
        {
            //_sceneNodeMapper.Get(0).Rotation += (float)(_playerMapper.Get(0)._SpinSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            var KeyboardState = Keyboard.GetState();

            Vector2 movementVector = new  Vector2(0, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movementVector.X = 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movementVector.X = -1;
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

            foreach (var entity in ActiveEntities)
            {
                _sceneNodeMapper.Get(entity).Position += movementVector * (float)(_playerMapper.Get(entity)._WalkSpeed);

                if (_playerMapper.Get(entity)._Health <= 0)
                {
                    Die();
                }

                //To test out spawning the abilities
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    SceneNode abilityNode = new SceneNode("BulletAttack", _sceneNodeMapper.Get(entity).Position + new Vector2(-200, 0)); //Have an actual concrete direction later
                    Sprite abilitySprite = new Sprite(bulletTexture);

                    abilityNode.Entities.Add(new SpriteEntity(abilitySprite));

                    _sceneGraph.RootNode.Children.Add(abilityNode);


                    //Spawn Ability
                    Entity abilityEntity = CreateEntity();
                    abilityEntity.Attach(new Components.Abilities.ClawAbility { _Damage = 10f });
                    abilityEntity.Attach(abilityNode);
                    abilityEntity.Attach(new Components.Collider { _CollisionLayer = Components.Collider.CollisionLayer.PlayerProjectile, _Height = 25f, _Width = 25f });

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
    }
}
