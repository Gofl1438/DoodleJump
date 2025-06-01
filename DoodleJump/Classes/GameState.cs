using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class GameState
    {
        public static bool IsDoodleFrozen;
        public static bool IsScenePaused;
        public static bool IsMonsterDeath;
        public static bool IsFallDeath;
        public static bool IsFallDeathSceneTwo;
        public static bool IsSceneGameOver;
        public static bool IsSceneMenu;
        public static bool IsWasShoot;
        public static int CurrentMinPlatformBlock;
        public static int Score;
        public GameState()
        {
            Score = 0;
            CurrentMinPlatformBlock = GameConfig.PlatformConfig.MinInitialQuantityInBlock;
            IsDoodleFrozen = false;
            IsScenePaused = false;
            IsMonsterDeath = false;
            IsFallDeath = false;
            IsFallDeathSceneTwo = false;
            IsSceneGameOver = false;
            IsWasShoot = false;
            IsSceneMenu = true;
        }
    }
}
