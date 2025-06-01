using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Представляет мяч в игре.
    /// </summary>
    public class Ball : ObjectGame
    {
        /// <summary>
        /// Физическая модель мяча.
        /// </summary>
        public PhysicsBall Physics { get; set; }

        /// <summary>
        /// Создает новый экземпляр мяча в позиции над игроком.
        /// </summary>
        /// <param name="PlayerPositionX">X-координата позиции игрока.</param>
        /// <param name="PlayerPositionY">Y-координата позиции игрока.</param>
        /// <param name="ballSprite">Изображение мяча.</param>
        public Ball(int playerPositionX, int playerPositionY, Bitmap ballSprite = null)
        {
            InitializeSprite(ballSprite);
            InitializeTransform(playerPositionX, playerPositionY);
            InitializePhysics();
        }

        private void InitializeSprite(Bitmap customSprite)
        {
            Sprite = customSprite ?? GameConfig.Ball.Sprite;
        }

        private void InitializeTransform(int playerX, int playerY)
        {
            var position = CalculateStartPosition(playerX, playerY);
            var size = new Size(GameConfig.Ball.Width, GameConfig.Ball.Height);
            Transform = new Transform(position, size);
        }

        private Point CalculateStartPosition(int playerX, int playerY)
        {
            return new Point(
                playerX + (GameConfig.Player.Dimensions.UpWidth - GameConfig.Ball.Width) / 2,
                playerY - GameConfig.Ball.Height
            );
        }

        private void InitializePhysics()
        {
            Physics = new PhysicsBall(Transform);
        }

    }
}
