using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public abstract class ObjectGame
    {
        public Transform Transform { get; set; }
        public Bitmap Sprite { get; set; }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(Sprite, Transform.Position.X, Transform.Position.Y, Transform.Size.Width, Transform.Size.Height);
        }
    }
}
