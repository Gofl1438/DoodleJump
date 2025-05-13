using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Transform
    {
        public PointF Position;
        public Size Size { get; private set; }
        public Transform(PointF position, Size size)
        {
            Position = position;
            Size = size;
        }
    }
}
