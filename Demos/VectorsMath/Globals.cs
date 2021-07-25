using System;
using Lib.Math;
using Microsoft.Xna.Framework;

namespace VectorsMath
{
    public static class Globals
    {
        public static (int Width,int Height) ScreenSize = (0,0);
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
        public static Vector2 ScreenSizeToVector => new Vector2(Globals.ScreenSize.Width,Globals.ScreenSize.Height);        
        public static Vector2 CenterScreen => new Vector2(ScreenSize.Width / 2,ScreenSize.Height / 2);
    }
}