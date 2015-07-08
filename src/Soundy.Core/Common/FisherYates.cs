using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundy.Core.Common
{
    public static class FisherYates
    {
        public static void Shuffle<T>(ref T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + (int)(_random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }

        public static IEnumerable<T> Shuffle<T>(T[] array)
        {
            Shuffle<T>(ref array);
            return array;
        }
        private static readonly Random _random = new Random();
    }
}
