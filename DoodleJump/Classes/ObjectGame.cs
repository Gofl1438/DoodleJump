using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Абстрактный базовый класс для всех игровых объектов.
    /// Предоставляет базовую функциональность для отрисовки и позиционирования.
    /// </summary>
    public abstract class ObjectGame
    {
        /// <summary>
        /// Трансформация объекта (позиция и размер).
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// Графический спрайт объекта.
        /// </summary>
        public Bitmap Sprite { get; set; }

        /// <summary>
        /// Отрисовывает спрайт объекта на указанной поверхности.
        /// </summary>
        /// <param name="g">Графический контекст, в котором происходит отрисовка.</param>
        public void DrawSprite(Graphics g)
        {
            if (Sprite == null)
                throw new InvalidOperationException("Спрайт не задан.");

            if (Transform == null)
                throw new InvalidOperationException("Трансформация не задана.");

            g.DrawImage(Sprite, Transform.Position.X, Transform.Position.Y, Transform.Size.Width, Transform.Size.Height);
        }
    }
}
