using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Systems
{
    public class PlayerBehaviourSystem : EntityUpdateSystem
    {
        private ComponentMapper<SceneNode> _sceneNodeMapper;
        private ComponentMapper<Components.Player> _playerMapper;

        public PlayerBehaviourSystem()
        : base(Aspect.All(typeof(Components.Player), typeof(SceneNode)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _sceneNodeMapper = mapperService.GetMapper<SceneNode>();
            _playerMapper = mapperService.GetMapper<Components.Player>();
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
            }
        }
    }
}
