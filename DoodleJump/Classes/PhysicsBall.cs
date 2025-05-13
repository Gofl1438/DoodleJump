using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class PhysicsBall
    {
        public Transform transform { get; set; }
        public bool IsWasHit { get; set; }
        public PhysicsBall(Transform transform)
        {
            this.transform = transform;
            IsWasHit = false;
        }
        public void MoveBallOy()
        {
            transform.Position.Y -= GameConfig.Ball.Speed;
        }

        public void CollideWithMonstrum()
        {
            //дописать коллизию
        }
    }
}
