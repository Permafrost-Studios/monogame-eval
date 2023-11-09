using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Sprites;
using monogame_eval_project.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Systems.Abilities
{
    public class StraightProjectileAbilitySystem : EntityUpdateSystem
    {
        private ComponentMapper<Components.Abilities.StraightProjectileAbility> _straightProjectileAbilityMapper;

        private ContentManager _content;
        private SceneGraph _sceneGraph;

        float abilityCoolDown;
        float abilityRemainingCoolDown;


        public StraightProjectileAbilitySystem(SceneGraph sceneGraph, ContentManager content)
        : base(Aspect.All(typeof(Components.Abilities.StraightProjectileAbility)))
        {
            _content = content;
            _sceneGraph = sceneGraph;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _straightProjectileAbilityMapper = mapperService.GetMapper<Components.Abilities.StraightProjectileAbility>();

            abilityCoolDown = 3f;
            abilityRemainingCoolDown = -1f;
        }

        bool temp = false;

        public override void Update(GameTime gameTime)
        {
            abilityRemainingCoolDown -= (float)gameTime.ElapsedGameTime.TotalSeconds; //Universal cooldown for all of them

            foreach (var entityID in ActiveEntities)
            {
                if (abilityRemainingCoolDown <= 0)
                {
                    //Activate Ability
                    ActivateAbility(entityID);

                    abilityRemainingCoolDown = abilityCoolDown;
                }
            }
        }

        void ActivateAbility(int entityID)
        {
            Entity straightProjectileEntity = CreateEntity();
            straightProjectileEntity.Attach(new Damager(100f));
            straightProjectileEntity.Attach(new Collider(Collider.CollisionLayer.PlayerAttack, 50f, 50f));

            //Create Sprite and Transform (SceneNode : Transform2)
            var _abilityTexture = _content.Load<Texture2D>("PrototypeArt/tile_0000");

            SceneNode abilityNode = new SceneNode("Projectile", GetEntity(entityID).Get<SceneNode>().Position - ( new Vector2(20, 0)));
            Sprite abilitySprite = new Sprite(_abilityTexture);

            abilityNode.Entities.Add(new SpriteEntity(abilitySprite));

            _sceneGraph.RootNode.Children.Add(abilityNode);

            straightProjectileEntity.Attach(abilityNode);
            straightProjectileEntity.Attach(new Moveable { Velocity = new Vector2(-100f, 0f) });
        }
    }
}
