using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Platform : ObjectObstacles
    {
        public GameConfig.PlatformType Type { get; private set; }
        public Platform(Point position, GameConfig.PlatformType typePlatform)
        {
            Transform = new Transform(position, new Size(GameConfig.PlatformConfig.Width, GameConfig.PlatformConfig.Height));
            Type = typePlatform;
            IsTouchedByPlayer = false;
            SetPlatformAppearance(Type);
        }

        private void SetPlatformAppearance(GameConfig.PlatformType typePlatform)
        {
            switch (typePlatform)
            {
                case GameConfig.PlatformType.Green:
                    Sprite = GameConfig.PlatformConfig.Sprites.Green;
                    Physics = new PhysicsObstacles(Transform);
                    break;
                case GameConfig.PlatformType.Brown:
                    Sprite = GameConfig.PlatformConfig.Sprites.Brown;
                    Physics = new PhysicsObstacles(Transform);
                    break;
                case GameConfig.PlatformType.Blue:
                    Sprite = GameConfig.PlatformConfig.Sprites.Blue;
                    Physics = new PhysicsObstacles(Transform, GameConfig.PlatformConfig.SpeedBlue);
                    break;
                case GameConfig.PlatformType.White:
                    Sprite = GameConfig.PlatformConfig.Sprites.White;
                    Physics = new PhysicsObstacles(Transform);
                    break;
            }
        }
    }
}
