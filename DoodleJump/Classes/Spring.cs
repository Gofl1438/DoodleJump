using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Класс, представляющий пружину в игре.
    /// </summary>
    public class Spring : ObjectObstacles
    {
        /// <summary>
        /// Создает новую пружину в указанной позиции.
        /// </summary>
        /// <param name="position">Позиция пружины на игровом поле.</param>
        /// <param name="springSprite">Пользовательский спрайт пружины.</param>
        public Spring(PointF position, Bitmap springSprite = null)
        {
            InitializeSprite(springSprite);
            InitializeTransform(position);
            InitializePhysics();
            ResetState();
        }

        /// <summary>
        /// Сбрасывает состояние пружины.
        /// </summary>
        public void ResetState()
        {
            IsTouchedByPlayer = false;
        }

        private void InitializeSprite(Bitmap customSprite)
        {
            Sprite = customSprite ?? GameConfig.Spring.Sprite;
        }

        private void InitializeTransform(PointF position)
        {
            Transform = new Transform(position, new Size(GameConfig.Spring.Width, GameConfig.Spring.Height));
        }

        private void InitializePhysics()
        {
            Physics = new PhysicsObstacles(Transform);
        }
    }
}
