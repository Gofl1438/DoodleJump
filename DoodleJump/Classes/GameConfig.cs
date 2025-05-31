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


        /// <summary>
        /// Игровой интерфейс
        /// </summary>
        public static class GameUiConfig
        {
            public static readonly Bitmap Panel = Properties.Resources.PanelElement;
            public static readonly Bitmap PauseButton = Properties.Resources.ButtonPause;
            public const string FamilyNameScore = "Mistral";
            public static readonly SolidBrush CustomBrush = new SolidBrush(Color.FromArgb(123, 88, 50));
            public static readonly FontStyle StyleScore = FontStyle.Bold;
            public const float SizeScore = 50;
            private const int WidthPanel = 600;
            private const int HeightPanel = 100;
            private const int OxPanel = 0;
            private const int OyPanel = 0;
            private const int WidthButtonPause = 60;
            private const int HeightButtonPause = 60;
            private const int OxPauseButton = 500;
            private const int OyPauseButton = 15;
            private const int OxScore = 25;
            private const int OyScore = 10;
            public static class Dimensions
            {
                public static readonly Size Panel = new Size(WidthPanel, HeightPanel);
                public static readonly Size PauseButton = new Size(WidthButtonPause, HeightButtonPause);
                public static readonly Font ScoreFont = new Font(FamilyNameScore, SizeScore, StyleScore);
            }
            public static class Positions
            {
                public static readonly Point Panel = new Point(OxPanel, OyPanel);
                public static readonly Point PauseButton = new Point(OxPauseButton, OyPauseButton);
                public static readonly Point Score = new Point(OxScore, OyScore);
            }
        }

        /// <summary>
        /// Меню паузы
        /// </summary>
        public static class PauseMenuConfig
        {
            public static readonly Bitmap ButtonResume = Properties.Resources.ButtonResume;
            public static readonly Bitmap BackgroundPause = Properties.Resources.BackgroundPause;
            public static int WidthBackground { get; set; }
            public static int HeightBackground { get; set; }
            private const int OxBackground = 0;
            private const int OyBackground = 0;
            public const int HeightButtonResume = 80;
            public const int WidthButtonResume = 222;
            public static int OxButtonResume { get; set; }
            public static int OyButtonResume { get; set; }
            public const int OffsetDownButtonResume = 200;

            public static class Dimensions
            {
                public static readonly Size Background = new Size(WidthBackground, HeightBackground);
                public static readonly Size ButtonResume = new Size(WidthButtonResume, HeightButtonResume);
            }
            public static class Positions
            {
                public static readonly Point Background = new Point(OxBackground, OyBackground);
                public static readonly Point ButtonResume = new Point(OxButtonResume, OyButtonResume);
            }
        }

        /// <summary>
        /// Главное меню 
        /// </summary>
        public static class MainMenuConfig
        {
            public static readonly Bitmap ButtonStartPlay = Properties.Resources.ButtonStartPlay;
            public static readonly Bitmap BackgroundError = Properties.Resources.BackgroundError;
            public static readonly Bitmap TitleDoodleJump = Properties.Resources.TitleDoodleJump;

            public static int OxTitleDoodleJump { get; set; }
            private const int HeightTitleDoodleJump = 200;
            public const int WidthTitleDoodleJump = 600;
            private const int OyTitleDoodleJump = 300;

            public static int OyBackgroundError { get; set; }
            public const int HeightBackgroundError = 150;
            private const int WidthBackgroundError = 600;
            private const int OxBackgroundError = 0;

            public static int OxButtonStartPlay { get; set; }
            public static int OyButtonStartPlay { get; set; }
            private const int HeightButtonStartPlay = 80;
            public const int WidthButtonStartPlay = 222;
            public const int OffsetDownButtonStartPlay = 300;

            public static class Dimensions
            {
                public static readonly Size TitleDoodleJump = new Size(WidthTitleDoodleJump, HeightTitleDoodleJump);
                public static readonly Size BackgroundError = new Size(WidthBackgroundError, HeightBackgroundError);
                public static readonly Size ButtonStartPlay = new Size(WidthButtonStartPlay, HeightButtonStartPlay);
            }
            public static class Positions
            {
                public static readonly Point TitleDoodleJump = new Point(OxTitleDoodleJump, OyTitleDoodleJump);
                public static readonly Point BackgroundError = new Point(OxBackgroundError, OyBackgroundError);
                public static readonly Point ButtonStartPlay = new Point(OxButtonStartPlay, OyButtonStartPlay);
            }
        }

        /// <summary>
        /// Сцена завершения игры
        /// </summary>
        public static class GameOverConfig
        {
            public static readonly Bitmap TitleGameOver = Properties.Resources.TitleGameOver;
            public static readonly Bitmap ButtonMenu = Properties.Resources.ButtonMenu;
            public static readonly Bitmap ButtonPlayAgain = Properties.Resources.ButtonPlayAgain;
            public static int OxTitleGameOver { get; set; }
            private const int OyTitleGameOver = 400;
            private const int HeightTitleGameOver = 200;
            public const int WidthTitleGameOver = 500;

            private const int OxButtonMenu = 350;
            public static int OyButtonMenu { get; set; }
            private const int HeightButtonMenu = 80;
            private const int WidthButtonMenu = 222;
            public static int OxButtonAgainPlay { get; set; }
            public static int OyButtonAgainPlay { get; set; }
            private const int HeightButtonAgainPlay = 80;
            private const int WidthButtonAgainPlay = 222;
            public const int OffsetDownButtonMenu = 50;
            public const int OffsetDownButtonAgainPlay = 150;
            public static class Dimensions
            {
                public static readonly Size TitleGameOver = new Size(WidthTitleGameOver, HeightTitleGameOver);
                public static readonly Size ButtonMenu = new Size(WidthButtonMenu, HeightButtonMenu);
                public static readonly Size ButtonPlayAgain = new Size(WidthButtonAgainPlay, HeightButtonAgainPlay);
            }
            public static class Positions
            {
                public static readonly Point TitleGameOver = new Point(OxTitleGameOver, OyTitleGameOver);
                public static readonly Point ButtonMenu = new Point(OxButtonMenu, OyButtonMenu);
                public static readonly Point ButtonPlayAgain = new Point(OxButtonAgainPlay, OyButtonAgainPlay);
            }
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
