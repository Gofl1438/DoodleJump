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
            float OxBall = transform.Position.X;
            float OyBall = transform.Position.Y;
            int WidthBall = transform.Size.Width;
            int HeightBall = transform.Size.Height;
            foreach (var obj in GameManagerObjectCanvas.objectCanvas)
            {
                if (obj is Monstrum monstrum)
                {
                    float OxMonstr = monstrum.Transform.Position.X;
                    float OyMonstr = monstrum.Transform.Position.Y;
                    int WidthMonstr = monstrum.Transform.Size.Width;
                    int HeightMonstr = monstrum.Transform.Size.Height;
                    if (OxBall <= OxMonstr + WidthMonstr && OxBall + WidthBall >= OxMonstr)
                    {
                        if (OyBall + HeightBall >= OyMonstr && OyBall + HeightBall <= OyMonstr + HeightMonstr)
                        {
                            IsWasHit = true;
                            if (monstrum.HealthPoints == 0)
                            {
                                monstrum.IsTouchedByPlayer = true;
                            }
                            monstrum.HealthPoints--;
                        }
                    }
                }
            }
        }
    }
}
