using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Components
{
    public class Collider
    {
        public enum CollisionLayer 
        { 
            None,
            Solid,
            Player,
            Enemy,
            EnemyProjectile, //Attack types
            EnemyMelee,
            PlayerProjectile,
            PlayerMelee
        }

        //In pixels
        public float _Width;
        public float _Height;

        public CollisionLayer _CollisionLayer;
    }
}
