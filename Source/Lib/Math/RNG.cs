using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Math
{
    public static class RNG
    {
        private static readonly Random rng = new Random();
        private static readonly object _lock = new object();

        public static int GetRandomInt(int min,int max)
        {
            lock(_lock){
                return rng.Next(min,max);
            }
        }
        public static float GetRandomFloat()
        {
            lock(_lock){
                return (float)rng.NextDouble();
            }
        }
        public static float GetRandomFloat(float min,float max){
            lock (_lock) {
                return rng.GetRandomNumber(min,max);
            }
        }
    }
}
