using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    }
}
