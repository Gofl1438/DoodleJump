using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Класс, реализующий физику мяча в игре.
    /// </summary>
    public class PhysicsBall
    {
        private Transform _transform;

        /// <summary>
        /// Трансформация мяча (позиция и размер).
        /// </summary>
        public Transform Transform
        {
            get => _transform;
            set => _transform = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Флаг, указывающий было ли попадание по монстру.
        /// </summary>
        public bool IsWasHit { get; set; }

        /// <summary>
        /// Конструктор класса PhysicsBall.
        /// </summary>
        /// <param name="transform">Трансформация мяча.</param>
        public PhysicsBall(Transform transform)
        {
            Transform = transform;
            IsWasHit = false;
        }

        /// <summary>
        /// Перемещает мяч вверх по оси Y
        /// </summary>
        public void MoveBallOy()
        {
            if (Transform == null)
                throw new InvalidOperationException("Transform не задан");

            Transform.Position.Y -= GameConfig.Ball.Speed;
        }

        /// <summary>
        /// Проверяет столкновение мяча с монстрами и наносит урон при попадании
        /// </summary>
        public void CollideWithMonstrum()
        {
            if (Transform == null)
                throw new InvalidOperationException("Transform не задан");

            var ballRect = GetBoundingBox(Transform);

            foreach (var obj in GameManagerObjectCanvas.Objects)
            {
                if (obj is Monstrum monstrum)
                {
                    var monsterRect = GetBoundingBox(monstrum.Transform);

                    if (ballRect.IntersectsWith(monsterRect))
                    {
                        HandleMonsterHit(monstrum);
                    }
                }
            }
        }

        /// <summary>
        /// Обрабатывает попадание по монстру.
        /// </summary>
        /// <param name="monstrum">Монстр, по которому попали.</param>
        private void HandleMonsterHit(Monstrum monstrum)
        {
            IsWasHit = true;
            monstrum.HealthPoints--;

            if (monstrum.HealthPoints <= 0)
            {
                monstrum.IsTouchedByPlayer = true;
                monstrum.HealthPoints = 0;
            }
        }

        /// <summary>
        /// Создает прямоугольник для проверки столкновений.
        /// </summary>
        /// <param name="transform">Трансформация объекта.</param>
        /// <returns></returns>
        private RectangleF GetBoundingBox(Transform transform)
        {
            return new RectangleF(
                transform.Position.X,
                transform.Position.Y,
                transform.Size.Width,
                transform.Size.Height);
        }
    }
}
