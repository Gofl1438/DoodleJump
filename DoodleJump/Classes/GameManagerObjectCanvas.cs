using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DoodleJump.Classes.GameConfig;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Менеджер игровых объектов на холсте.
    /// </summary>
    public static class GameManagerObjectCanvas 
    {
        private static Random _random = new Random();
        private static readonly int _canvasWidth = CanvasParameters.Width;
        private static readonly int _сanvasHeight = CanvasParameters.Height;
        private static readonly int _springHeight = GameConfig.Spring.Height;
        public static List<ObjectObstacles> Objects { get; } = new List<ObjectObstacles>();
        public static List<Ball> ActiveBalls { get; } = new List<Ball>();

        /// <summary>
        /// Полностью очищает все игровые объекты.
        /// </summary>
        public static void AllClearObject()
        {
            Objects.Clear();
            ActiveBalls.Clear();
        }

        /// <summary>
        /// Удаляет объекты, с которыми игрок уже взаимодействовал, если это предусмотрено.
        /// </summary>
        public static void DeleteTouchObject()
        {
            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                if (Objects[i].IsTouchedByPlayer)
                {
                    Objects.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Очищает объекты, выходящие за границы холста.
        /// </summary>
        public static void RemoveOffScreenObjects()
        {
            for (int i = Objects.Count - 2; i >= 0; i--)
            {
                if (Objects[i].Transform.Position.Y >= _сanvasHeight)
                {
                    if (Objects[i + 1] is Spring)
                    {
                        Objects.RemoveAt(i + 1);
                    }
                    Objects.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Удаляет мячи, которые вышли за границы экрана или попали в цель.
        /// </summary>
        public static void ClearBall()
        {
            for (int i = 0; i < ActiveBalls.Count; i++)
            {
                if (ActiveBalls[i].Physics.Transform.Position.Y <= 0 || ActiveBalls[i].Physics.IsWasHit)
                {
                    ActiveBalls.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Поддерживает необходимое количество платформ на игровом поле
        /// </summary>
        public static void MaintainPlatformsCount()
        {
            if (Objects.Count == 0)
                return;

            var lastObj = Objects.Last();
            float lastPlatformY = lastObj.Transform.Position.Y;

            if (lastPlatformY > GameConfig.PlatformConfig.PositionLast)
            {
                GameState.CurrentMinPlatformBlock = Math.Clamp(GameConfig.PlatformConfig.MaxQuantityInBlock - (GameState.Score / DifficultyCoefficient), 1, GameConfig.PlatformConfig.MaxQuantityInBlock);
                AddBlockPlatforms();
                AddPlatformSpring();
                AddPlatformMonstrum();
            }
        }

        /// <summary>
        /// Добавляет пружинку на платформу, если это возможно.
        /// </summary>
        public static void AddPlatformSpring()
        {
            if (_random.NextDouble() > GameConfig.Probabilities.Spring)
                return;

            if (Objects.Last() is Platform platform)
            {
                if (platform.Type == GameConfig.PlatformType.Green)
                {
                    var platformPos = platform.Transform.Position;
                    int springX = _random.Next((int)platformPos.X, (int)platformPos.X + GameConfig.PlatformConfig.Width - GameConfig.Spring.Width);

                    Objects.Add(new Spring(new Point(springX, (int)platformPos.Y - _springHeight)));
                }
            }
        }

        /// <summary>
        /// Добавляет монстра с учетом вероятности.
        /// </summary>
        public static void AddPlatformMonstrum()
        {
            if (_random.NextDouble() > GameConfig.Probabilities.Monstrum)
                return;

            var lastPlatform = Objects.Last();
            float lastPlatformY = lastPlatform.Transform.Position.Y;

            int minY = (int)(lastPlatformY - GameConfig.Player.JumpHeight);
            int maxY = (int)(lastPlatformY - PaddingCanvas - GameConfig.Monstrum.HeightRed);

            var point = new Point(_random.Next(PaddingCanvas, _canvasWidth - GameConfig.Monstrum.HeightRed), _random.Next(minY, maxY));

            Objects.Add(new Monstrum(point, GameConfig.MonstrumType.Red, GameConfig.Monstrum.healthPointsRed));
        }

        /// <summary>
        /// Добавляет блок платформ с помощью генератора.
        /// </summary>
        public static void AddBlockPlatforms()
        {
            List<Platform> blockPlatform = PlatformGenerator.BlockPlatform(Objects);
            foreach (Platform platform in blockPlatform)
            {
                Objects.Add(platform);
            }
        }

        /// <summary>
        /// Применяет физику движения по горизонтали ко всем объектам.
        /// </summary>
        public static void ApplyPhysicsObject()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Physics.MoveObjectOx();
            }
        }

        /// <summary>
        /// Применяет физику движения мячей и проверяет столкновения.
        /// </summary>
        public static void ApplyPhysicsBall()
        {
            for (int i = 0; i < ActiveBalls.Count; i++)
            {
                ActiveBalls[i].Physics.MoveBallOy();
                ActiveBalls[i].Physics.CollideWithMonstrum();
            }
        }

        /// <summary>
        /// Генерирует начальную последовательность платформ.
        /// </summary>
        public static void GenerateStartSequence()
        {
            do
            {
                AddBlockPlatforms();
            }
            while (Objects[Objects.Count - 1].Transform.Position.Y > GameConfig.PlatformConfig.PositionLast);
        }
    }
}
