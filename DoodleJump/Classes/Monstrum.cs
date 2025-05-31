using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Monstrum : ObjectObstacles
    {
        public GameConfig.MonstrumType Type { get; private set; }
        public int HealthPoints { get; set; }
        public Monstrum(Point position, GameConfig.MonstrumType typeMonstrum, int healthPoints)
        {
            Type = typeMonstrum;
            HealthPoints = healthPoints;
            IsTouchedByPlayer = false;
            BitmapTypeMonstrum(Type, position);
        }

        public void BitmapTypeMonstrum(GameConfig.MonstrumType typeMonstrum, Point position)
        {
            switch (typeMonstrum)
            {
                case GameConfig.MonstrumType.Red:
                    Sprite = GameConfig.Monstrum.SpriteRed;
                    Transform = new Transform(position, new Size(GameConfig.Monstrum.WidthRed, GameConfig.Monstrum.HeightRed));
                    Physics = new PhysicsObstacles(Transform, GameConfig.Monstrum.SpeedRed);
                    break;
            }
        }
    }
}
