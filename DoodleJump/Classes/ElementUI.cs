using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Базовый класс для UI-элементов в игре.
    /// </summary>
    public class ElementUI : ObjectGame
    {
        /// <summary>
        /// Создает новый UI-элемент с указанными параметрами.
        /// </summary>
        /// <param name="sprite">Изображение элемента.</param>
        /// <param name="sizeObject">Размер элемента в пикселях.</param>
        /// <param name="pointObject">Позиция элемента на экране (верхний левый угол).</param>
        public ElementUI(Bitmap sprite, Size sizeObject, Point pointObject)
        {
            this.Sprite = sprite;
            Size size = sizeObject;
            Point point = pointObject;
            Transform = new Transform(point, size);
        }
    }
}
