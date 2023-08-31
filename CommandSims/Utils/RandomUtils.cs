using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Utils
{
    public class RandomUtils
    {
        private static Random random = new();

        public static void RefreshRandom(int seed)
        {
            random = new Random(seed);
        }

        public static int Next(int min = 0, int max = 10)
        {
            return random.Next(min, max);
        }

        public static int Next(int max)
        {
            return random.Next(0, max);
        }

        public static int GetNextWithWeight(List<int> weights)
        {
            int totalWeight = 0;
            List<int> requiredList = new();
            for (var i = 0; i < weights.Count; i++)
            {
                totalWeight += weights[i];
                if (weights[i] == 10000)
                {
                    requiredList.Add(i);
                }
            }
            if (requiredList.Count > 0)
            {
                var r = random.Next(requiredList.Count);
                return requiredList[r];
            }
            var nextRandom = random.Next(totalWeight);
            int weightSum = 0;
            for (var i = 0; i < weights.Count; i++)
            {
                weightSum += weights[i];
                if (nextRandom < weightSum)
                {
                    return i;
                }
            }
            return -1;
        }


        public static List<int> GetNextListWithWeight(List<int> weights, int count)
        {
            List<int> result = new();
            if (weights.Count <= count)
            {
                for (int i = 0; i < weights.Count; i++)
                {
                    result.Add(i);
                }
                return result;
            }
            while (result.Count < count)
            {
                var index = GetNextWithWeight(weights);
                if (!result.Any(x => x == index))
                {
                    result.Add(index);
                }
            }
            return result;
        }

    }
}
