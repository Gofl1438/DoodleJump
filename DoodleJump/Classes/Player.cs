using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Player : ObjectGame
    {
        public PhysicsPlayer Physics { get; private set; }
        public Player(Bitmap playerSprite = null)
        {
            Sprite = playerSprite ?? GameConfig.Player.Sprites.Right;
            Point point = new Point(
                (GameConfig.CanvasParameters.Width - GameConfig.Player.Dimensions.DefaultWidth) / 2,
                GameConfig.CanvasParameters.Height / 2
                );
            Size size = new Size(
                GameConfig.Player.Dimensions.DefaultWidth,
                GameConfig.Player.Dimensions.DefaultHeight
                );
            Transform = new Transform(point, size);
            Physics = new PhysicsPlayer(Transform);
        }
    }
}
