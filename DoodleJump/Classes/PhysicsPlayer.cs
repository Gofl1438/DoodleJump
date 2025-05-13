using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class PhysicsPlayer
    {
        public Transform transform;
        public float gravity;
        private float acceleration;
        public float dx;
        public PhysicsPlayer(Transform transform)
        {
            this.transform = transform;
            gravity = GameConfig.Player.DefaultGrafity;
            acceleration = GameConfig.Player.Acceleration;
            dx = 0;
        }

        /// <summary>
        /// Применение всей описанной физики
        /// </summary>
        public void ApplyPhysics()
        {
            MoveOx();
            transform.Position.Y += gravity;
            gravity += acceleration;
            CollideWithObject();
        }

        /// <summary>
        /// Перемещение по Ox
        /// </summary>
        public void MoveOx()
        {
            transform.Position.X += dx;
        }

        /// <summary>
        /// Расчёт столковений с объектами
        /// </summary>
        public void CollideWithObject()
        {
            //дописать коллизию
        }

        /// <summary>
        /// Сброс грвитации до дефолтного значения
        /// </summary>
        public void AddForce()
        {
            gravity = GameConfig.Player.DefaultGrafity;
        }
    }
}
