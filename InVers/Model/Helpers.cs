using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Model
{
    public static class Helpers
    {
        static Random random = new Random();
        public static List<int> GenerateRandom(int count, int min, int max)
        {
            var candidates = new HashSet<int>();

            for(; min <= max; min++)
            {
                candidates.Add(min);
            }

            var result = new List<int>();
            result.AddRange(candidates);

            var i = result.Count;
            while (i > 1)
            {
                i--;
                var k = random.Next(i + 1);
                var value = result[k];
                result[k] = result[i];
                result[i] = value;
            }
            return result;
        }
    }
}
