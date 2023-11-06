using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;

namespace monogame_eval_project.Systems
{
    public class EnemyBehaviourSystem :EntityUpdateSystem
    {
        private ComponentMapper<SceneNode> _sceneNodeMapper;
        private ComponentMapper<Components.Player> _playerMapper;
        private ComponentMapper<Components.Enemy> _enemyMapper;

        Transform2 targetPosition;

        Vector2 movementVector;

        public EnemyBehaviourSystem()
        : base(Aspect.One(typeof(Components.Enemy), typeof(Components.Player)).All(typeof(SceneNode)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            targetPosition = new Transform2();

            _sceneNodeMapper = mapperService.GetMapper<SceneNode>();
            _playerMapper = mapperService.GetMapper<Components.Player>();
            _enemyMapper = mapperService.GetMapper<Components.Enemy>();
        }

        public override void Update(GameTime gameTime)
        {


            foreach (var entityID in ActiveEntities)
            {
                if (_playerMapper.Has(entityID)) //Checking if it is a player entity
                {
                    targetPosition = (Transform2)_sceneNodeMapper.Get(entityID); //SceneNode derives from Transform2
                }
                else if (_enemyMapper.Has(entityID))
                {
                    movementVector = Vector2.Normalize(targetPosition.Position - _sceneNodeMapper.Get(entityID).Position);

                    _sceneNodeMapper.Get(entityID).Position += movementVector * (float)(_enemyMapper.Get(entityID)._WalkSpeed);
                }
            }
        }
    }
}
