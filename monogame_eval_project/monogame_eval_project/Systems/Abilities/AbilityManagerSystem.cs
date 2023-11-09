using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Systems.Abilities
{
    public class AbilityManagerSystem : EntityUpdateSystem //Abilities are entities
    {
        private ComponentMapper<Components.Abilities.AbilityHolder> _abilityHolderMapper;

        public AbilityManagerSystem()
        : base(Aspect.All(typeof(Components.Abilities.AbilityHolder)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _abilityHolderMapper = mapperService.GetMapper<Components.Abilities.AbilityHolder>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entityID in ActiveEntities)
            {
                foreach (Components.Abilities.Ability ability in _abilityHolderMapper.Get(entityID).Abilities)
                {
                    //NVM DONT DO THIS
                }
            }
        }
    }
}
