using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DoodleJump.Classes.GameConfig;

namespace DoodleJump.Classes
{
    public static class PlatformGenerator
    {
        private static Random rand = new Random();
        private static int heightBlock = GameConfig.Player.JumpHeight - PaddingCanvas - GameConfig.PlatformConfig.Height;

        public static List<Platform> BlockPlatform(List<ObjectObstacles> objectCanvas)
        {
            List<Platform> blockPlatforms = new List<Platform>();
            List<Point> point = GeneratePlatformPositions(objectCanvas);
            GameConfig.PlatformType type;
            for (int i = 0; i < point.Count; i++)
            {
                Point p = point[i];
                double r = rand.NextDouble();
                if (i == 0 || i == point.Count - 1)
                {
                    type = ProbabilitiesObject.GetTypePlatform(true);
                    Platform platform = new Platform(p, type);
                    blockPlatforms.Add(platform);
                }
                else
                {
                    type = ProbabilitiesObject.GetTypePlatform();
                    Platform platform = new Platform(p, type);
                    blockPlatforms.Add(platform);
                }
            }
            return blockPlatforms;
        }

        private static List<Point> GeneratePlatformPositions(List<ObjectObstacles> objectCanvas)
        {
            List<Point> points = new List<Point>();
            int posLast;
            if (objectCanvas.Count == 0)
            {
                posLast = GameConfig.CanvasParameters.Height - PaddingCanvas - GameConfig.PlatformConfig.Height;
            }
            else
            {
                var lastPlatform = objectCanvas[objectCanvas.Count - 1];
                posLast = (int)lastPlatform.Transform.Position.Y - GameConfig.PlatformConfig.Height - PaddingCanvas;
            }
            int countPlatform = rand.Next(GameState.CurrentMinPlatformBlock, GameConfig.PlatformConfig.MaxQuantityInBlock);
            int heightplatforms;
            int backlash = 0;
            if (countPlatform > 1)
            {
                int k = countPlatform - 1;
                heightplatforms = GameConfig.PlatformConfig.Height * k;
                backlash = (heightBlock - heightplatforms - (PaddingCanvas * k)) / (k * 2);
            }
            for (int i = 0; i < countPlatform; i++)
            {
                Point point = new Point();
                if (countPlatform == 1)
                {
                    if (objectCanvas.Count == 0)
                    {
                        point.Y = posLast;
                    }
                    else
                    {
                        point.Y = rand.Next(posLast - heightBlock, posLast);
                    }
                }
                else
                {
                    if (objectCanvas.Count == 0)
                    {
                        point.Y = posLast;
                        posLast -= (backlash + GameConfig.PlatformConfig.Height + PaddingCanvas);
                    }
                    else
                    {
                        if (i == countPlatform - 1)
                        {
                            point.Y = posLast;
                        }
                        else
                        {
                            int lightpos = posLast - backlash * 2;
                            point.Y = rand.Next(lightpos, posLast);
                            posLast = lightpos - (GameConfig.PlatformConfig.Height + PaddingCanvas);
                        }
                    }

                }
                point.X = rand.Next(PaddingCanvas, GameConfig.CanvasParameters.Width - GameConfig.PlatformConfig.Width - PaddingCanvas);
                points.Add(point);
            }
            return points;
        }
    }
}
