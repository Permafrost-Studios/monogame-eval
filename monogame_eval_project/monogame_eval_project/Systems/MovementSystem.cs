using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Systems
{
    public class MovementSystem : EntityUpdateSystem
    {
        private ComponentMapper<SceneNode> _sceneNodeMapper;
        private ComponentMapper<Components.Moveable> _moveableMapper;

        public MovementSystem()
        : base(Aspect.All(typeof(Components.Moveable), typeof(SceneNode)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _moveableMapper = mapperService.GetMapper<Components.Moveable>();
            _sceneNodeMapper = mapperService.GetMapper<SceneNode>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entityID in ActiveEntities)
            {
                _sceneNodeMapper.Get(entityID).Position += _moveableMapper.Get(entityID).Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                _moveableMapper.Get(entityID).Velocity += _moveableMapper.Get(entityID).Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
