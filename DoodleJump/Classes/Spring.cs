using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Spring : ObjectObstacles
    {
        public Spring(PointF position, Bitmap springSprite = null)
        {
            Sprite = springSprite ?? GameConfig.Spring.Sprite;
            Transform = new Transform(position,
                new Size(GameConfig.Spring.Width, GameConfig.Spring.Height
                ));
            IsTouchedByPlayer = false;
            Physics = new PhysicsObstacles(Transform);
        }
    }
}
