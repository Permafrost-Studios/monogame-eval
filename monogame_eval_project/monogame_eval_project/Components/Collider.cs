using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Components
{
    public class Collider
    {
        public Collider(CollisionLayer layer, float width, float height) 
        { 
            _CollisionLayer = layer;
            _Width = width;
            _Height = height;
        }

        public enum CollisionLayer 
        { 
            None,
            Solid,
            Player,
            Enemy,
            EnemyAttack, //Attack types
            PlayerAttack
        }

        //In pixels
        public float _Width;
        public float _Height;

        public CollisionLayer _CollisionLayer;
    }
}
