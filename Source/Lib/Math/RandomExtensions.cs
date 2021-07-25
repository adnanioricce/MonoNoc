using System;

namespace Lib.Math
{
    public static class RandomExtensions
    {
        public static float GetRandomNumber(this Random rng,float min,float max) => (float)(rng.NextDouble() * (max - min) + min);
        public static float GetRandomNumber(this Random rng,double min,double max) => GetRandomNumber(rng,(float)min,(float)max);
        public static float GetRandomNumber(this Random rng) => (float)rng.NextDouble();
    }
}