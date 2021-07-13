namespace Lib.Math
{
    public static class CRandom
    {
        public static float Gen(long x,long y){
            x = x * 3266489917 + 374761393;
            x = (x << 17) | (x >> 15);
            x += y * 3266489917;

            x *= 668265263;
            x ^= x >> 15;
            x *= 2246822519;
            x ^= x >> 13;
            x *= 3266489917;
            x ^= x >> 16;

            return (x & 0x00ffffff) * (1.0f / 0x1000000);
        }
    }
}