using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Класс, представляющий трансформацию игрового объекта (позицию и размер).
    /// </summary>
    public class Transform
    {
        /// <summary>
        /// Позиция объекта в игровом пространстве
        /// </summary>
        public PointF Position;

        /// <summary>
        /// Размер объекта
        /// </summary>
        public Size Size { get; private set; }

        /// <summary>
        /// Создает новую трансформацию с указанными параметрами.
        /// </summary>
        /// <param name="position">Начальная позиция объекта.</param>
        /// <param name="size">Размер объекта.</param>
        public Transform(PointF position, Size size)
        {
            Position = position;
            Size = size;
        }
    }
}
