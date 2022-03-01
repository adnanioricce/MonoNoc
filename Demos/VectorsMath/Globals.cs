using System;
using Lib.Math;
using Microsoft.Xna.Framework;

namespace VectorsMath
{
    public static class Globals
    {
        private static Vector2 _screenSize;
        private static Vector2 _centerScreen;
        private static (int Width,int Height) screenSize;
        public static (int Width,int Height) ScreenSize
        {
            get => screenSize;
            set {
                var (width,height) = value;
                _screenSize = new Vector2(width,height);
                screenSize = (width,height);
                _centerScreen = new Vector2(width / 2,height / 2);
            }
        }
        public static Vector2 ScreenSizeToVector => _screenSize;        
        public static Vector2 CenterScreen => _centerScreen;
    }
}