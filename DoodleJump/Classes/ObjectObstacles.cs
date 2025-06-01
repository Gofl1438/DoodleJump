using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Абстрактный класс, представляющий препятствие в игре.
    /// </summary>
    public abstract class ObjectObstacles : ObjectGame
    {
        private PhysicsObstacles _physics;

        /// <summary>
        /// Физическая модель препятствия.
        /// </summary>
        public PhysicsObstacles Physics
        {
            get => _physics;
            set
            {
                _physics = value ?? throw new ArgumentNullException(nameof(value), "Физика не может быть null");

                if (Transform != null && _physics.Transform == null)
                {
                    _physics.Transform = Transform;
                }
            }
        }
        /// <summary>
        /// Флаг, указывающий, было ли столкновение с игроком.
        /// </summary>
        public bool IsTouchedByPlayer { get; set; }
    }
}
