using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Noises 
{
    public static class Perlin {
        public static float Interpolate(float a0,float a1, float w)
        {
            return (a1 - a0) * w + a0;
        }
        public static Vector2 RandomGradient(int ix,int iy)
        {
            float random = 2920f * MathF.Sin(ix * 21942f + iy * 171324.0f + 8912f) * MathF.Cos(ix * 23157f * iy * 217832f + 9758f);
            return new Vector2(MathF.Cos(random),MathF.Sin(random));
        }
        public static float DotGridGradient(int ix,int iy, float x,float y)
        {
            Vector2 gradient = RandomGradient(ix,iy);

            float dx = x - (float)ix;
            float dy = y - (float)iy;

            return dx * gradient.X + dy * gradient.Y;
        }
        public static float Noise(float x,float y)
        {
            int x0 = (int)x;
            int x1 = x0 + 1;
            int y0 = (int)y;
            int y1 = y0 + 1;

            float sx = x - (float)x0;
            float sy = y - (float)y0;

            float n0, n1, ix0, ix1;

            n0 = DotGridGradient(x0, y0, x, y);
            n1 = DotGridGradient(x1, y0, x, y);
            ix0 = Interpolate(n0,n1,sx);

            n0 = DotGridGradient(x0, y1, x, y);
            n1 = DotGridGradient(x1, y1, x, y);
            ix1 = Interpolate(n0, n1, sx);

            var value = Interpolate(ix0, ix1, sy);
            return value;
        }
        public static float SmoothNoise(float x,float y,int width,int height,float[,] noiseMap){
            //get fractional part of x and y
            double fractX = x - (int)x;
            double fractY = y - (int)y;

            // wrap around
            int x1 = ((int)x + width) % width;
            int y1 = ((int)y + height) % height;

            //neighbor values
            int x2 = (x1 + width - 1) % width;
            int y2 = (y1 + height - 1) % height;

            //smooth the noise with bilinear interpolation
            double value = 0.0f;
            value += fractX * fractY * noiseMap[y1,x1];
            value += (1 - fractX) * fractY * noiseMap[y1,x2];
            value += fractX * (1 - fractY) * noiseMap[y2,x1];
            value += (1 - fractX) * (1 - fractY) * noiseMap[y2,x2];
            return (float)value;
        }
        public static float Turbulence(float x,float y,float size,(int Width,int Height) windowSize,float[,] map){
            float value = 0f;
            float initialSize = size;
            while (size >= 1)
            {
                value += SmoothNoise(x / size,y / size,windowSize.Width,windowSize.Height,map) * size;
                size /= 2.0f;
            }
            return (value / initialSize);
        }
        public static Color[] SineMap((int Width,int Height) size,float[,] map)
        {
            var colors = new List<Color>(size.Height * size.Width);
            for (int y = 0; y < size.Height; y++){
                for (int x = 0; x < size.Width; x++){
                    var value = SineNoise(x,y,size,map);
                    var color = new Color(value,value,value);
                    colors.Add(color);
                }
            }
            return colors.ToArray();
        }
        public static float SineNoise(int x,int y,(int width,int height) size,float[,] map){
            const float xPeriod = 5.0f;
            const float yPeriod = 10.0f;
            const float turbPower = 256.0f;
            const float turbSize = 32.0f;
            var (width,height) = size;
            var xyValue = x * xPeriod / width + y * yPeriod / height + turbPower * Turbulence(x,y,turbSize,size,map) / 256f;
            var sineValue = MathF.Abs(MathF.Sin(xyValue * 3.14159f));
            return sineValue;
        }
        public static float[,] GenerateWhiteNoise(int initialX,int initialY,int randomSeed = 1)
        {
            int defaultXWidth = 360;
            int defaultYWidth = 360;
            float[,] noiseFieldToReturn = new float[defaultXWidth,defaultYWidth];
            for(int x = initialX;x < defaultXWidth + initialX;++x){
                for(int y = initialY;y < defaultYWidth + initialY;++y){
                    noiseFieldToReturn[x,y] = Perlin.Noise(CRandom.Gen(x,y),y);
                }
            }
            return noiseFieldToReturn;
        }        
    }
}