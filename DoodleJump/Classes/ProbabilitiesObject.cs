using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DoodleJump.Classes.GameConfig;

namespace DoodleJump.Classes
{
    /// <summary>
    /// Статический класс для управления вероятностями появления игровых объектов.
    /// </summary>
    public static class ProbabilitiesObject
    {
        private static Random rand = new Random();

        /// <summary>
        /// Получает случайный тип платформы на основе вероятностей.
        /// </summary>
        /// <param name="WithBrown"></param>
        /// <returns></returns>
        public static GameConfig.PlatformType GetTypePlatform(bool WithBrown = false)
        {
            Dictionary<GameConfig.PlatformType, float> probabilities;
            if (!WithBrown)
            {
                probabilities = GetCurrentProbabilities();
            }
            else
            {
                probabilities = GetPlatformProbabilitiesWithoutBrown();
            }
            double ran = rand.NextDouble();
            float cumulative = 0f;
            foreach (var kvp in probabilities)
            {
                cumulative += kvp.Value;
                if (ran <= cumulative)
                {
                    return kvp.Key;
                }
            }
            return GameConfig.PlatformType.Green;
        }

        /// <summary>
        /// Возвращает вероятности появления платформ без учета коричневых.
        /// </summary>
        private static Dictionary<GameConfig.PlatformType, float> GetPlatformProbabilitiesWithoutBrown()
        {
            Dictionary<GameConfig.PlatformType, float> probabilities = GetCurrentProbabilities();
            probabilities.Remove(GameConfig.PlatformType.Brown);
            float totalProbability = 0f;
            foreach (float probability in probabilities.Values)
            {
                totalProbability += probability;
            }
            Dictionary<GameConfig.PlatformType, float> normalizedProbabilities = new Dictionary<GameConfig.PlatformType, float>();
            foreach (KeyValuePair<GameConfig.PlatformType, float> pair in probabilities)
            {
                float normalizedValue = pair.Value / totalProbability;
                normalizedProbabilities.Add(pair.Key, normalizedValue);
            }
            return normalizedProbabilities;
        }
         
        /// <summary>
        /// Рассчитывает текущие вероятности появления платформ на основе счета игрока
        /// </summary>
        private static Dictionary<PlatformType, float> GetCurrentProbabilities()
        {
            float scoreMultiplier = GameState.Score / GameConfig.DifficultyCoefficient;
            return new Dictionary<PlatformType, float>
            {
                [PlatformType.Blue] = Math.Min(Probabilities.BlueStart + Probabilities.BlueScoreFactor * scoreMultiplier, Probabilities.BlueMax),
                [PlatformType.Brown] = Math.Max(Probabilities.BrownStart + Probabilities.BrownScoreFactor * scoreMultiplier, Probabilities.BrownMin),
                [PlatformType.Green] = Math.Max(Probabilities.GreenStart + Probabilities.GreenScoreFactor * scoreMultiplier, Probabilities.GreenMin),
                [PlatformType.White] = Math.Min(Probabilities.WhiteStart + Probabilities.WhiteScoreFactor * scoreMultiplier, Probabilities.WhiteMax),
            };
        }
    }
}
