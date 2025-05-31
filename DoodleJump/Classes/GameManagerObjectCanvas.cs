using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DoodleJump.Classes.GameConfig;

namespace DoodleJump.Classes
{
    public static class GameManagerObjectCanvas
    {
        private static Random rand = new Random();
        private static readonly int CanvasWidth = CanvasParameters.Width;
        private static readonly int CanvasHeight = CanvasParameters.Height;
        private static readonly int SpringHeight = GameConfig.Spring.Height;
        public static List<ObjectObstacles> objectCanvas = new List<ObjectObstacles>();
        public static List<Ball> ball = new List<Ball>();
        public static void AllClearObject()
        {
            objectCanvas.Clear();
            ball.Clear();
        }
        public static void DeleteTouchObject()
        {
            for (int i = objectCanvas.Count - 1; i >= 0; i--)
            {
                if (objectCanvas[i].IsTouchedByPlayer)
                {
                    objectCanvas.RemoveAt(i);
                }
            }
        }

        public static void ClearObjectCanvas()
        {
            for (int i = objectCanvas.Count - 2; i >= 0; i--)
            {
                if (objectCanvas[i].Transform.Position.Y >= CanvasHeight)
                {
                    if (objectCanvas[i + 1] is Spring spring)
                    {
                        objectCanvas.RemoveAt(i + 1);
                    }
                    objectCanvas.RemoveAt(i);
                }
            }
        }

        public static void ClearBall()
        {
            for (int i = 0; i < ball.Count; i++)
            {
                if (ball[i].physics.transform.Position.Y <= 0 || ball[i].physics.IsWasHit)
                {
                    ball.RemoveAt(i);
                }
            }
        }

        public static void MaintainPlatformsCount()
        {
            if (objectCanvas.Count == 0)
                return;

            var lastObj = objectCanvas.Last();
            float lastPlatformY = lastObj.Transform.Position.Y;

            if (lastPlatformY > GameConfig.PlatformConfig.PositionLast)
            {
                GameState.CurrentMinPlatformBlock = Math.Clamp(GameConfig.PlatformConfig.MaxQuantityInBlock - (GameState.Score / DifficultyCoefficient), 1, GameConfig.PlatformConfig.MaxQuantityInBlock);
                AddBlockPlatforms();
                AddPlatformSpring();
                AddPlatformMonstrum();
            }
        }

        public static void AddPlatformSpring()
        {
            if (rand.NextDouble() > GameConfig.Probabilities.Spring)
                return;

            if (objectCanvas.Last() is Platform platform)
            {
                if (platform.Type == GameConfig.PlatformType.Green)
                {
                    var platformPos = platform.Transform.Position;
                    int springX = rand.Next((int)platformPos.X, (int)platformPos.X + GameConfig.PlatformConfig.Width - GameConfig.Spring.Width);

                    objectCanvas.Add(new Spring(new Point(springX, (int)platformPos.Y - SpringHeight)));
                }
            }
        }

        public static void AddPlatformMonstrum()
        {
            if (rand.NextDouble() > GameConfig.Probabilities.Monstrum)
                return;

            var lastPlatform = objectCanvas.Last();
            float lastPlatformY = lastPlatform.Transform.Position.Y;

            int minY = (int)(lastPlatformY - GameConfig.Player.JumpHeight);
            int maxY = (int)(lastPlatformY - PaddingCanvas - GameConfig.Monstrum.HeightRed);

            var point = new Point(rand.Next(PaddingCanvas, CanvasWidth - GameConfig.Monstrum.HeightRed), rand.Next(minY, maxY));

            objectCanvas.Add(new Monstrum(point, GameConfig.MonstrumType.Red, GameConfig.Monstrum.healthPointsRed));
        }

        public static void AddBlockPlatforms()
        {
            List<Platform> blockPlatform = PlatformGenerator.BlockPlatform(objectCanvas);
            foreach (Platform platform in blockPlatform)
            {
                objectCanvas.Add(platform);
            }
        }

        public static void ApplyPhysicsObject()
        {
            for (int i = 0; i < objectCanvas.Count; i++)
            {
                objectCanvas[i].Physics.MoveObjectOx();
            }
        }

        public static void ApplyPhysicsBall()
        {
            for (int i = 0; i < ball.Count; i++)
            {
                ball[i].physics.MoveBallOy();
                ball[i].physics.CollideWithMonstrum();
            }
        }

        public static void GenerateStartSequence()
        {
            do
            {
                AddBlockPlatforms();
            }
            while (objectCanvas[objectCanvas.Count - 1].Transform.Position.Y > GameConfig.PlatformConfig.PositionLast);
        }
    }
}
