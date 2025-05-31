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
            int LengthTrunk = GameConfig.Player.Dimensions.LengthTrunk;
            float OxPlayer = transform.Position.X;
            float OyPlayer = transform.Position.Y;
            int WidthPlayer = transform.Size.Width;
            int HeightPlayer = transform.Size.Height;

            foreach (var obj in GameManagerObjectCanvas.objectCanvas)
            {
                float OxObj = obj.Transform.Position.X;
                float OyObj = obj.Transform.Position.Y;
                int WidthObj = obj.Transform.Size.Width;
                int HeightObj = obj.Transform.Size.Height;

                bool isColliding = OxPlayer + LengthTrunk <= OxObj + WidthObj && OxPlayer + WidthPlayer - LengthTrunk >= OxObj;
                if (!isColliding) continue;

                switch (obj)
                {
                    case Platform platform when gravity > 0:
                        if (OyPlayer + HeightPlayer >= OyObj && OyPlayer + HeightPlayer <= OyObj + HeightObj / 2)
                        {
                            switch (platform.Type)
                            {
                                case GameConfig.PlatformType.White:
                                    AddForce();
                                    platform.IsTouchedByPlayer = true;
                                    break;
                                case GameConfig.PlatformType.Brown:
                                    platform.IsTouchedByPlayer = true;
                                    break;
                                default:
                                    AddForce();
                                    break;
                            }
                        }
                        break;
                    case Monstrum monstrum:
                        if (gravity > 0)
                        {
                            if (OyPlayer + HeightPlayer > OyObj)
                            {
                                if (OyPlayer + HeightPlayer < OyObj + HeightObj / 2)
                                {
                                    obj.IsTouchedByPlayer = true;
                                    AddForce();
                                }
                                else if (OyPlayer + HeightPlayer < OyObj + HeightObj)
                                {
                                    //пока заглушка
                                }
                            }
                        }
                        else if (gravity < 0)
                        {
                            if ((OyPlayer + LengthTrunk <= OyObj + HeightObj) && (OyPlayer + LengthTrunk >= OyObj))
                            {
                                //пока заглушка
                            }
                        }
                        break;
                    case Spring spring when gravity > 0:
                        if (OyPlayer + HeightPlayer >= OyObj && OyPlayer + HeightPlayer <= OyObj + HeightObj / 2)
                        {
                            gravity = GameConfig.Spring.Gravity;
                        }
                        break;
                }
            }
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
