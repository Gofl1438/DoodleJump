using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Класс, представляющий игрока в игре.
    /// </summary>
    public class Player : ObjectGame
    {
        /// <summary>
        /// Физическая модель игрока, отвечающая за движение и взаимодействия.
        /// </summary>
        public PhysicsPlayer Physics { get; private set; }

        /// <summary>
        /// Создает нового игрока с указанным спрайтом или спрайтом по умолчанию.
        /// </summary>
        /// <param name="playerSprite">Пользовательский спрайт игрока.</param>
        public Player(Bitmap playerSprite = null)
        {
            InitializeSprite(playerSprite);
            InitializeTransform();
            InitializePhysics();
        }

        private void InitializeSprite(Bitmap customSprite)
        {
            Sprite = customSprite ?? GameConfig.Player.Sprites.Right;
        }

        private void InitializeTransform()
        {
            var position = CalculateStartPosition();
            var size = GetPlayerSize();
            Transform = new Transform(position, size);
        }

        private Point CalculateStartPosition()
        {
            return new Point(
                (GameConfig.CanvasParameters.Width - GameConfig.Player.Dimensions.DefaultWidth) / 2,
                GameConfig.CanvasParameters.Height / 2
            );
        }

        private Size GetPlayerSize()
        {
            return new Size(
                GameConfig.Player.Dimensions.DefaultWidth,
                GameConfig.Player.Dimensions.DefaultHeight
            );
        }

        private void InitializePhysics()
        {
            Physics = new PhysicsPlayer(Transform);
        }
    }
}
