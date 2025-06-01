using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Класс, представляющий платформу в игре.
    /// </summary>
    public class Platform : ObjectObstacles
    {
        /// <summary>
        /// Тип платформы, определяющий её поведение и внешний вид
        /// </summary>
        public GameConfig.PlatformType Type { get; private set; }

        /// <summary>
        /// Создает новую платформу указанного типа в заданной позиции.
        /// </summary>
        /// <param name="position">Позиция платформы на игровом поле.</param>
        /// <param name="typePlatform">Тип создаваемой платформы.</param>
        public Platform(Point position, GameConfig.PlatformType typePlatform) : base()
        {
            Transform = new Transform(position, new Size(
                    GameConfig.PlatformConfig.Width,
                    GameConfig.PlatformConfig.Height
                )
            );
            Type = typePlatform;
            IsTouchedByPlayer = false;
            InitializePlatform();
        }

        /// <summary>
        /// Инициализирует внешний вид и физику платформы в зависимости от её типа.
        /// </summary>
        private void InitializePlatform()
        {
            SetPlatformAppearance();
            ConfigurePlatformPhysics();
        }

        /// <summary>
        /// Устанавливает спрайт платформы в соответствии с её типом.
        /// </summary>
        private void SetPlatformAppearance()
        {
            switch (Type)
            {
                case GameConfig.PlatformType.Green:
                    Sprite = GameConfig.PlatformConfig.Sprites.Green;
                    break;
                case GameConfig.PlatformType.Brown:
                    Sprite = GameConfig.PlatformConfig.Sprites.Brown;
                    break;
                case GameConfig.PlatformType.Blue:
                    Sprite = GameConfig.PlatformConfig.Sprites.Blue;
                    break;
                case GameConfig.PlatformType.White:
                    Sprite = GameConfig.PlatformConfig.Sprites.White;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), $"Неизвестный тип платформы: {Type}");
            }
        }

        /// <summary>
        /// Настраивает физическое поведение платформы в зависимости от её типа.
        /// </summary>
        private void ConfigurePlatformPhysics()
        {
            switch (Type)
            {
                case GameConfig.PlatformType.Blue:
                    Physics = new PhysicsObstacles(Transform, GameConfig.PlatformConfig.SpeedBlue);
                    break;
                default:
                    Physics = new PhysicsObstacles(Transform);
                    break;
            }
        }

        /// <summary>
        /// Сбрасывает состояние платформы.
        /// </summary>
        public void Reset()
        {
            IsTouchedByPlayer = false;
            if (Physics != null)
            {
                Physics.ResetMovement();
            }
        }
    }
}
