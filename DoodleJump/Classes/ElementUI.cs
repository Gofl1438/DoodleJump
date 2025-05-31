using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class ElementUI : ObjectGame
    {
        public ElementUI(Bitmap sprite, Size sizeObject, Point pointObject)
        {
            this.Sprite = sprite;
            Size size = sizeObject;
            Point point = pointObject;
            Transform = new Transform(point, size);
        }
    }
}
