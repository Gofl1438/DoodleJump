using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class GameState
    {
        public static int Score;
        public static int CurrentMinPlatformBlock;
        public GameState()
        {
            CurrentMinPlatformBlock = GameConfig.PlatformConfig.MinInitialQuantityInBlock;
            Score = 0;
        }
    }
}
