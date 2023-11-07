using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Components
{
    public class Moveable
    {
        public Vector2 Velocity;

        public Vector2 Acceleration = Vector2.Zero; //Explicity setting it to zero by default just in case
    }
}
