using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using monogame_eval_project.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Systems
{
    public class CollisionSystem: EntityUpdateSystem
    {
        private ComponentMapper<Components.Collider> _colliderMapper; 
        private ComponentMapper<SceneNode> _sceneNodeMapper; //Needed for position

        public CollisionSystem()
        : base(Aspect.All(typeof(Components.Collider), typeof(SceneNode))) 
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _colliderMapper = mapperService.GetMapper<Components.Collider>();
            _sceneNodeMapper = mapperService.GetMapper<SceneNode>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entityID in ActiveEntities)
            {
                if (_colliderMapper.Get(entityID)._CollisionLayer == Components.Collider.CollisionLayer.Solid) 
                {
                    foreach (var eID in ActiveEntities)
                    {

                    }
                }
            }
        }

        bool CheckCollision(Collider collider1, SceneNode sceneNode1, Collider collider2, SceneNode sceneNode2) //Scene nodes for position
        {
            if (sceneNode1.Position.X < sceneNode1.Position.X + collider2._Width &&
                sceneNode1.Position.X + collider1._Width > sceneNode2.Position.X &&
                sceneNode1.Position.Y < sceneNode1.Position.Y + collider2._Height &&
                sceneNode1.Position.Y + collider1._Height > sceneNode2.Position.Y)
            {
                return true;
            }

            return false;
        }
    }
}
