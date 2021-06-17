using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Noises
{
    public static class ColorExtensions
    {
        public static Color RGBtoHSV(this Color color){
            var outColor = new Vector3();
            var values = new float[] {color.R,color.G,color.B};
            var min = values.Min<float>();
            var max = values.Max<float>();
            outColor.Z = max;
            var delta = max - min;
            if(max != 0){
                outColor.Y = delta / max;                
            } else {
                outColor.Y = 0;
                outColor.X = -1;
                return new Color(outColor);
            }
            if(color.R == max){
                outColor.X = (color.G - color.B) / delta;                
            }
            else if(color.G == max){
                outColor.X = 2 + (color.B - color.R) / delta;                
            } 
            else {
                outColor.X = 4 + (color.R - color.G) / delta;
            }

            outColor.X *= 60;
            if(outColor.X < 0){
                outColor.X += 360;
            }
            return new Color(outColor);
        }
        public static Color HSVtoRGB(this Vector3 hsvColor)
        {
            var h = hsvColor.X;
            var s = hsvColor.Y;
            var v = hsvColor.Z;
            var outColor = new Vector3();
            int i = 0;
            if(outColor.Y == 0){
                return new Color(hsvColor.Z,hsvColor.Z,hsvColor.Z);
            }
            h /= 60;
            i = (int)MathF.Floor(h);
            var f = h - i;
            var p = v * (1 - s);
            var q = v * (1 - s * f);
            var t = v * (1 - s * (1 - f));
            return i switch
            {
                0 => new Color(v, t, p),
                1 => new Color(q, v, p),
                2 => new Color(p, v, t),
                3 => new Color(p, q, v),
                4 => new Color(t, p, v),
                _ => new Color(v, p, q),
            };
        }
    }
}