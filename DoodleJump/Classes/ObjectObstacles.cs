using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public abstract class ObjectObstacles : ObjectGame
    {
        public PhysicsObstacles Physics { get; set; }
        public bool IsTouchedByPlayer { get; set; }
    }
}
