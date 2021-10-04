using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Linq;

namespace Lib.Factories
{
    public class TextureFactory
    {
        //public static Textu
        public static Texture2D CreateSolidTexture(GraphicsDevice graphicsDevice,Color color,int width,int height)
        {
            var colors = Enumerable.Repeat(color,width * height).ToArray();
            var texture = new Texture2D(graphicsDevice,width,height);
            texture.SetData(colors);
            return texture;
        }
        public static Texture2D CreateSolidCircleTexture(GraphicsDevice graphicsDevice,float radius,Color color)
        {
            radius = radius / 2;
            int width = (int)radius * 2;
            int height = width;
            var center = new Vector2(radius,radius);
            CircleF circle = new CircleF(center.ToPoint(),radius);

            Color[] dataColors = new Color[width * height];
            int row = -1;
            int column = 0;
            for (int i = 0; i < dataColors.Length; ++i)
            {
                column++;
                if(i % width == 0)
                {
                    row++;
                    column = 0;
                }
                Vector2 point = new Vector2(row, column);
                dataColors[i] = circle.Contains(point) ? color : Color.Transparent;
            }
            var texture = new Texture2D(graphicsDevice,width,height);
            texture.SetData(0,new Rectangle(0,0,width,height), dataColors,0, width * height);
            return texture;
        }
    }
}
