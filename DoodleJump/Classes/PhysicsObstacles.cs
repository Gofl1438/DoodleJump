using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class PhysicsObstacles
    {
        public Transform Transform { get; set; }
        public int Speed { get; set; }
        private bool isMovingRight;
        private int rightBoundary;
        private int leftBoundary;

        public PhysicsObstacles(Transform transform, int speed = 0)
        {
            this.Transform = transform;
            this.Speed = speed;
            isMovingRight = true;
            leftBoundary = GameConfig.PaddingCanvas;
            rightBoundary = GameConfig.CanvasParameters.Width - GameConfig.PaddingCanvas - transform.Size.Width;
        }

        /// <summary>
        /// Перемещение объектов по Ox
        /// </summary>
        public void MoveObjectOx()
        {
            float newX = Transform.Position.X + (isMovingRight ? Speed : -Speed);

            if (isMovingRight && newX > rightBoundary)
            {
                newX = rightBoundary;
                isMovingRight = false;
            }
            else if (!isMovingRight && newX < leftBoundary)
            {
                newX = leftBoundary;
                isMovingRight = true;
            }
            Transform.Position.X = newX;
        }
    }
}
