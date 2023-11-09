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
        private ComponentMapper<Components.Moveable> _moveableMapper;

        Transform2 targetPosition;

        Vector2 movementVector;

        private float _enemyWalkSpeed = 50f;

        public EnemyBehaviourSystem()
        : base(Aspect.One(typeof(Components.Enemy), typeof(Components.Player)).All(typeof(SceneNode), typeof(Components.Moveable)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            targetPosition = new Transform2();

            _sceneNodeMapper = mapperService.GetMapper<SceneNode>();
            _playerMapper = mapperService.GetMapper<Components.Player>();
            _enemyMapper = mapperService.GetMapper<Components.Enemy>();
            _moveableMapper = mapperService.GetMapper<Components.Moveable>();
        }

        bool targetChanged = false;

        public override void Update(GameTime gameTime)
        {
            foreach (var entityID in ActiveEntities)
            {
                if (_playerMapper.Has(entityID)) //Checking if it is a player entity
                {
                    if ((Transform2)_sceneNodeMapper.Get(entityID) != targetPosition)
                    {
                        targetPosition = (Transform2)_sceneNodeMapper.Get(entityID); //SceneNode derives from Transform2

                        targetChanged = true;
                    } else
                    {
                        targetChanged = false;
                    }
                    
                }
                else if (targetChanged = true && _enemyMapper.Has(entityID))
                {
                    movementVector = Vector2.Normalize(targetPosition.Position - _sceneNodeMapper.Get(entityID).Position);

                    //_sceneNodeMapper.Get(entityID).Position += movementVector * (float)(_enemyMapper.Get(entityID)._WalkSpeed);

                    _moveableMapper.Get(entityID).Velocity = movementVector * (float)(_enemyWalkSpeed);

                    //targetChanged = false;
                }
            }
        }
    }
}
