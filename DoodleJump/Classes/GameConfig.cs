using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public static class GameConfig
    {
        public enum PlatformType { Blue, Brown, Green, White }
        public enum MonstrumType { Red }

        public static Size CanvasParameters { get; private set; }
        public static readonly int PaddingCanvas = 20;
        public static readonly int DifficultyCoefficient = 500;
        public static void Initialize(Size canvasSize)
        {
            CanvasParameters = canvasSize;
        }

        /// <summary>
        /// Информация об игроке
        /// </summary>
        public static class Player
        {
            public static readonly float Acceleration = 0.3f;
            public static readonly float MovingOx = 8;
            public static readonly int DefaultGrafity = -14;
            public static readonly int JumpHeight = 350;
            public static class Sprites
            {
                public static readonly Bitmap Right = Properties.Resources.DoodleRight;
                public static readonly Bitmap Left = Properties.Resources.DoodleLeft;
                public static readonly Bitmap Up = Properties.Resources.DoodleUp;
            }
            public static class Dimensions
            {
                public static readonly int UpWidth = 123;
                public static readonly int UpHeight = 118;
                public static readonly int DefaultWidth = 123;
                public static readonly int DefaultHeight = 118;
                public static readonly int LengthTrunk = 32;
            }
        }

        /// <summary>
        /// Информация об монстрах
        /// </summary>
        public static class Monstrum
        {
            public static readonly int WidthRed = 150;
            public static readonly int HeightRed = 100;
            public static readonly int SpeedRed = 4;
            public static readonly int healthPointsRed = 3;
            public static readonly Bitmap SpriteRed = Properties.Resources.RedMonster;
        }

        /// <summary>
        /// Информация об платформах
        /// </summary>
        public static class PlatformConfig
        {
            public static readonly int PositionLast = -100;
            public static int GameOverSpeed = 10;
            public static readonly int Height = 30;
            public static readonly int Width = 130;
            public static readonly int MaxQuantityInBlock = 6;
            public static readonly int MinQuantityInBlock = 1;
            public static readonly int MinInitialQuantityInBlock = 5;
            public static readonly int SpeedBlue = 5;
            public static class Sprites
            {
                public static readonly Bitmap Green = Properties.Resources.GreenPlatform;
                public static readonly Bitmap Brown = Properties.Resources.BrownPlatform;
                public static readonly Bitmap Blue = Properties.Resources.BluePlatform;
                public static readonly Bitmap White = Properties.Resources.WhitePlatform;
            }
        }

        /// <summary>
        /// Информация об пружине
        /// </summary>
        public static class Spring
        {
            public static readonly int Height = 30;
            public static readonly int Width = 40;
            public static readonly int Gravity = -22;
            public static readonly Bitmap Sprite = Properties.Resources.CompressedSpring;
        }

        /// <summary>
        /// Информация об мяче
        /// </summary>
        public static class Ball
        {
            public static readonly int Width = 20;
            public static readonly int Height = 20;
            public static readonly int Speed = 15;
            public static readonly Bitmap Sprite = Properties.Resources.Ball;
        }


        public static class Probabilities
        {
            public static float Spring = 0.6f;
            public static float Monstrum = 0.4f;

            public static float BlueStart = 0.1f;
            public static float BrownStart = 0.3f;
            public static float GreenStart = 0.6f;
            public static float WhiteStart = 0f;

            public static float BlueScoreFactor = 0.01f;
            public static float BrownScoreFactor = -0.01f;
            public static float GreenScoreFactor = -0.02f;
            public static float WhiteScoreFactor = 0.02f;

            public static float BlueMax = 0.3f;
            public static float BrownMin = 0.2f;
            public static float GreenMin = 0.2f;
            public static float WhiteMax = 0.2f;
        }
    }
}
