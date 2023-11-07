using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using monogame_eval_project.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
                //Check solid object collision
                if (_colliderMapper.Get(entityID)._CollisionLayer == Components.Collider.CollisionLayer.Solid)
                {
                    foreach (var eID in ActiveEntities)
                    {
                        //Check for collision with ANYTHING and push them back
                    }

                    continue; //The players and enemies are not marked as solid. Only one item in the collision has to be solid
                }

                //Check specifically enemy to enemy collision
                if (_colliderMapper.Get(entityID)._CollisionLayer == Components.Collider.CollisionLayer.Solid)
                {
                    foreach (var eID in ActiveEntities)
                    {
                       
                    }

                    continue; //Can only be one collision type
                }

                //Check specifically player collision
                if (_colliderMapper.Get(entityID)._CollisionLayer == Components.Collider.CollisionLayer.Player)
                {
                    foreach (var eID in ActiveEntities)
                    {
                        //Chek for enemy and enemy projectile collision with the player
                        switch(_colliderMapper.Get(eID)._CollisionLayer)
                        {
                            case Components.Collider.CollisionLayer.Enemy:
                                {
                                    if (CheckCollision(_colliderMapper.Get(entityID), _sceneNodeMapper.Get(entityID), 
                                        _colliderMapper.Get(eID), _sceneNodeMapper.Get(eID)))
                                    {
                                        //Deal the damage of the Enemy to the player
                                        Enemy enemyComponent = GetEntity(eID).Get<Enemy>();
                                        Player playerComponent = GetEntity(entityID).Get<Player>();

                                        playerComponent._Health -= enemyComponent._MeleeDamage;

                                        //playerComponent._Health = -1;
                                    }

                                    break;
                                }
                            case Components.Collider.CollisionLayer.EnemyProjectile:
                                {
                                    if (CheckCollision(_colliderMapper.Get(entityID), _sceneNodeMapper.Get(entityID),
                                        _colliderMapper.Get(eID), _sceneNodeMapper.Get(eID)))
                                    {
                                        //Deal the damage of the Projectile to the player
                                        Projectile projectileComponent = GetEntity(eID).Get<Projectile>(); //Projectile components are identical for enemies and players
                                        Player playerComponent = GetEntity(entityID).Get<Player>();

                                        playerComponent._Health -= projectileComponent._Damage;
                                    }

                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }
        }

        bool CheckCollision(Collider collider1, SceneNode sceneNode1, Collider collider2, SceneNode sceneNode2) //Scene nodes for position
        {
            if (sceneNode1.Position.X + (collider1._Width/2) > sceneNode2.Position.X - (collider2._Width / 2) && //Right side of object 1 > Left side of object 2
                sceneNode2.Position.X + (collider2._Width/2) > sceneNode1.Position.X  - (collider1._Width / 2) && //Right side of object 2 > left side of object 1
                //"Top" really means bottom because the y axis is flipped but its fine
                sceneNode1.Position.Y + (collider1._Height / 2) > sceneNode2.Position.Y - (collider2._Height / 2) && //Top side of object 1 > Bottom side of object 2
                sceneNode2.Position.Y + (collider2._Height / 2) > sceneNode1.Position.Y - (collider1._Height / 2)) //Top side of object 2 > Bottom side of object 1
            {
                return true;
            }

            return false;
        }
    }
}
