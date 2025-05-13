using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Ball : ObjectGame
    {
        public PhysicsBall physics { get; set; }
        public Ball(int PlayerPositionX, int PlayerPositionY, Bitmap ballSprite = null)
        {
            Sprite = ballSprite ?? GameConfig.Ball.Sprite;
            Point point = new Point();
            point.X = PlayerPositionX + (GameConfig.Player.Dimensions.UpWidth - GameConfig.Ball.Width) / 2;
            point.Y = PlayerPositionY - GameConfig.Ball.Height;
            Transform = new Transform(point, new Size(GameConfig.Ball.Width, GameConfig.Ball.Height));
            physics = new PhysicsBall(Transform);
        }
    }
}
