using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Класс, реализующий физику перемещающихся препятствий.
    /// </summary>
    public class PhysicsObstacles
    {
        private Transform _transform;
        private int _speed;
        private bool _isMovingRight;
        private readonly int _rightBoundary;
        private readonly int _leftBoundary;

        /// <summary>
        /// Трансформация препятствия (позиция и размер).
        /// </summary>
        public Transform Transform
        {
            get => _transform;
            set => _transform = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Скорость перемещения препятствия.
        /// </summary>
        public int Speed
        {
            get => _speed;
            set => _speed = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), "Скорость не может быть отрицательной");
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="transform">Трансформация препятствия.</param>
        /// <param name="speed">Скорость перемещения (по умолчанию 0).</param>
        public PhysicsObstacles(Transform transform, int speed = 0)
        {
            Transform = transform;
            Speed = speed;
            _isMovingRight = true;
            _leftBoundary = GameConfig.PaddingCanvas;
            _rightBoundary = GameConfig.CanvasParameters.Width - GameConfig.PaddingCanvas - transform.Size.Width;
        }

        /// <summary>
        /// Перемещает объект по горизонтали между границами
        /// </summary>
        public void MoveObjectOx()
        {
            if (Speed == 0) return;

            float newX = CalculateNewPosition();

            if (NeedToChangeDirection(newX))
            {
                newX = GetBoundaryPosition();
                _isMovingRight = !_isMovingRight;
            }

            Transform.Position.X = newX;
        }

        /// <summary>
        /// Сбрасывает движение препятствия в начальное состояние
        /// </summary>
        public void ResetMovement()
        {
            _isMovingRight = true;
        }

        private float CalculateNewPosition()
        {
            return Transform.Position.X + (_isMovingRight ? Speed : -Speed);
        }

        private bool NeedToChangeDirection(float newX)
        {
            return (_isMovingRight && newX > _rightBoundary) ||
                   (!_isMovingRight && newX < _leftBoundary);
        }

        private float GetBoundaryPosition()
        {
            return _isMovingRight ? _rightBoundary : _leftBoundary;
        }
    }
}
