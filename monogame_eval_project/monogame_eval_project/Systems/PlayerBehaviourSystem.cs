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
        private ComponentMapper<Components.Player> _playerMapper;
        private ComponentMapper<Components.Moveable> _moveableMapper;

        SceneGraph _sceneGraph;

        private ContentManager _content;

        //Ability Textures
        Texture2D bulletTexture;

        public PlayerBehaviourSystem(SceneGraph sceneGraph, ContentManager content)
        : base(Aspect.All(typeof(Components.Player), typeof(Components.Moveable), typeof(SceneNode)))
        {
            _sceneGraph = sceneGraph;
            _content = content;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _moveableMapper = mapperService.GetMapper<Components.Moveable>();
            _playerMapper = mapperService.GetMapper<Components.Player>();
        }

        public override void Update(GameTime gameTime)
        {
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

            if (true) //MAKE IT SO THAT THIS CONDITION IS IF SOMETHIGN HAS ACTUALLY BEEN PRESSED - POSSIBLE FIRST STEP TO ALLOW FOR ACCELERATION
            {
                Vector2.Normalize(movementVector);

                foreach (var entity in ActiveEntities)
                {
                    _moveableMapper.Get(entity).Velocity = movementVector * (float)(_playerMapper.Get(entity)._WalkSpeed);

                    if (_playerMapper.Get(entity)._Health <= 0)
                    {
                        Die();
                    }

                    //To test out spawning the abilities
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {

                    }
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
