using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Класс, представляющий монстра в игре.
    /// </summary>
    public class Monstrum : ObjectObstacles
    {
        /// <summary>
        /// Тип монстра, определяющий его характеристики и поведение.
        /// </summary>
        public GameConfig.MonstrumType Type { get; private set; }

        /// <summary>
        /// Текущее количество очков здоровья монстра.
        /// </summary>
        public int HealthPoints { get; set; }

        /// <summary>
        /// Создает нового монстра с заданными параметрами.
        /// </summary>
        /// <param name="position">Начальное количество здоровья.</param>
        /// <param name="typeMonstrum">Тип монстра</param>
        /// <param name="healthPoints">Начальное количество здоровья.</param>
        public Monstrum(Point position, GameConfig.MonstrumType typeMonstrum, int healthPoints)
        {
            if (healthPoints <= 0)
                throw new ArgumentOutOfRangeException(nameof(healthPoints), "Здоровье должно быть положительным числом");

            Type = typeMonstrum;
            HealthPoints = healthPoints;
            IsTouchedByPlayer = false;
            InitializeMonster(typeMonstrum, position);
        }

        /// <summary>
        /// Инициализирует характеристики монстра в зависимости от его типа.
        /// </summary>
        private void InitializeMonster(GameConfig.MonstrumType typeMonstrum, Point position)
        {
            switch (typeMonstrum)
            {
                case GameConfig.MonstrumType.Red:
                    ConfigureRedMonster(position);
                    break;
                default:
                    throw new ArgumentException($"Неизвестный тип монстра: {typeMonstrum}");
            }
        }

        /// <summary>
        /// Настраивает параметры красного монстра.
        /// </summary>
        private void ConfigureRedMonster(Point position)
        {
            Sprite = GameConfig.Monstrum.SpriteRed;
            Transform = new Transform(position, new Size(GameConfig.Monstrum.WidthRed, GameConfig.Monstrum.HeightRed));
            Physics = new PhysicsObstacles(Transform, GameConfig.Monstrum.SpeedRed);
        }
    }
}
