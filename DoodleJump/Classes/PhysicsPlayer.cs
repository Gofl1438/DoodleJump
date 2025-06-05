using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using static DoodleJump.Classes.GameConfig;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Класс, реализующий физику игрока.
    /// </summary>
    public class PhysicsPlayer
    {
        private Transform _transform;
        private float _gravity;
        private float _acceleration;
        private float _dx;

        /// <summary>
        /// Трансформация игрока (позиция и размер).
        /// </summary>
        public Transform Transform
        {
            get => _transform;
            set => _transform = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Текущее значение гравитации (скорость по оси Y).
        /// </summary>
        public float Gravity
        {
            get => _gravity;
            set => _gravity = value;
        }

        /// <summary>
        /// Скорость перемещения по оси X.
        /// </summary>
        public float Dx
        {
            get => _dx;
            set => _dx = value;
        }

        /// <summary>
        /// Конструктор класса PhysicsPlayer.
        /// </summary>
        /// <param name="transform">Трансформация игрока</param>
        public PhysicsPlayer(Transform transform)
        {
            Transform = transform;
            Gravity = GameConfig.Player.DefaultGravity;
            _acceleration = GameConfig.Player.Acceleration;
            Dx = 0;
        }

        /// <summary>
        /// Применяет всю физику игрока за один кадр.
        /// </summary>
        public void ApplyPhysics()
        {
            MoveOx();
            ApplyGravity();
            CollideWithObject();
        }


        /// <summary>
        /// Перемещает игрока по горизонтали.
        /// </summary>
        public void MoveOx()
        {
            Transform.Position.X += Dx;
        }

        /// <summary>
        /// Применяет гравитацию к игроку.
        /// </summary>
        private void ApplyGravity()
        {
            Transform.Position.Y += Gravity;
            Gravity += _acceleration;
        }

        /// <summary>
        /// Обрабатывает столкновения с игровыми объектами.
        /// </summary>
        public void CollideWithObject()
        {
            CheckFallDeath();

            foreach (var obj in GameManagerObjectCanvas.Objects)
            {
                ProcessCollisionWithObject(obj);
            }
        }

        /// <summary>
        /// Проверяет выход за нижнюю границу экрана.
        /// </summary>
        private void CheckFallDeath()
        {
            if (Transform.Position.Y > GameConfig.CanvasParameters.Height)
            {
                GameState.IsFallDeath = true;
            }
        }


        /// <summary>
        /// Обрабатывает столкновение с конкретным объектом.
        /// </summary>
        /// <param name="obj">Игровой объект</param>
        private void ProcessCollisionWithObject(ObjectGame obj)
        {
            switch (obj)
            {
                case Platform platform when Gravity > 0:
                    if (CheckHorizontalCollisionTotal(platform.Transform, GameConfig.Player.Dimensions.LengthTrunk))
                        HandlePlatformCollision(platform);
                    break;

                case Monstrum monstrum:
                    if (CheckHorizontalCollisionMonstrum(monstrum.Transform, GameConfig.Player.Dimensions.LengthTrunk))
                        HandleMonsterCollision(monstrum);
                    break;

                case Spring spring when Gravity > 0:
                    if (CheckHorizontalCollisionTotal(spring.Transform, GameConfig.Player.Dimensions.LengthTrunk))
                        HandleSpringCollision(spring);
                    break;
            }
        }


        /// <summary>
        /// Обрабатывает столкновение с платформой.
        /// </summary>
        private void HandlePlatformCollision(Platform platform)
        {
            if (IsPlayerOnTop(platform.Transform))
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
        }

        /// <summary>
        /// Обрабатывает столкновение с монстром.
        /// </summary>
        private void HandleMonsterCollision(Monstrum monstrum)
        {
            if (Gravity > 0)
            {
                if (IsPlayerOnTop(monstrum.Transform, 0.5f))
                {
                    monstrum.IsTouchedByPlayer = true;
                    AddForce();
                }
                else if (IsPlayerInsideMonster(monstrum.Transform))
                {
                    GameState.IsMonsterDeath = true;
                }
            }
            else if (Gravity < 0)
            {
                if (IsPlayerHittingFromBelow(monstrum.Transform, GameConfig.Player.Dimensions.LengthTrunk))
                {
                    GameState.IsMonsterDeath = true;
                }
            }
        }

        // <summary>
        /// Обрабатывает столкновение с пружиной.
        /// </summary>
        private void HandleSpringCollision(Spring spring)
        {
            if (IsPlayerOnTop(spring.Transform))
            {
                Gravity = GameConfig.Spring.Gravity;
            }
        }

        /// <summary>
        /// Сбрасывает гравитацию до начального значения.
        /// </summary>
        public void AddForce()
        {
            Gravity = GameConfig.Player.DefaultGravity;
        }


        /// <summary>
        /// Обеспечивает "заворачивание" игрока при выходе за горизонтальные границы.
        /// </summary>
        /// <param name="doodleCanvas">Canvas, на котором отрисовывается игрок</param>
        public void WrapHorizontalPosition(PictureBox doodleCanvas)
        {
            float halfWidth = Transform.Size.Width * 0.5f;
            float canvasWidth = doodleCanvas.Width;
            float currentX = Transform.Position.X;

            if (currentX + halfWidth > canvasWidth)
            {
                Transform.Position.X = -halfWidth;
            }
            else if (currentX < -halfWidth)
            {
                Transform.Position.X = canvasWidth - halfWidth;
            }
        }


        private bool CheckHorizontalCollisionMonstrum(Transform obj, int lengthTrunk)
        {
            return (Transform.Position.X + Transform.Size.Width - lengthTrunk >= obj.Position.X) &&
                (Transform.Position.X + lengthTrunk <= obj.Position.X + obj.Size.Width);
        }

        private bool CheckHorizontalCollisionTotal(Transform obj, int lengthTrunk)
        {
            return (Transform.Position.X + lengthTrunk <= obj.Position.X + obj.Size.Width &&
                Transform.Position.X + Transform.Size.Width - lengthTrunk >= obj.Position.X);
        }


        private bool IsPlayerOnTop(Transform objTransform, float heightFactor = 1f)
        {
            return Transform.Position.Y + Transform.Size.Height >= objTransform.Position.Y &&
                   Transform.Position.Y + Transform.Size.Height <= objTransform.Position.Y + objTransform.Size.Height * heightFactor;
        }

        private bool IsPlayerInsideMonster(Transform objTransform)
        {
            return Transform.Position.Y + Transform.Size.Height >= objTransform.Position.Y &&
                   Transform.Position.Y + Transform.Size.Height <= objTransform.Position.Y + objTransform.Size.Height;
        }

        private bool IsPlayerHittingFromBelow(Transform monsterTransform, int lengthTrunk)
        {
            return (Transform.Position.Y + lengthTrunk <= monsterTransform.Position.Y + monsterTransform.Size.Height) &&
                (Transform.Position.Y + lengthTrunk >= monsterTransform.Position.Y);
        }
    }
}
