using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lib.Extensions
{
    [Obsolete("Use PrimitivesExtensions instead")]
    public static class SpriteBatchExtensions
    {
        public static void DrawLineAtAngle(this SpriteBatch spriteBatch,Vector2 pos1,Vector2 pos2,Color color,float angle = 1f,float scale = 1f,float thickness = 1f)
        {
            var distance = Vector2.Distance(pos1,pos2);
            DrawLine(spriteBatch,pos1,distance * scale, angle, color, thickness);
        }
        public static void DrawLine(this SpriteBatch spriteBatch,Vector2 pos1,Vector2 pos2,Color color,float scale = 1f,float thickness = 1f)
        {
            var distance = Vector2.Distance(pos1,pos2);
            var angle = MathF.Atan2(pos2.Y - pos1.Y,pos2.X - pos1.X);
            DrawLine(spriteBatch,pos1,distance * scale,angle,color,thickness);
        }
        public static void DrawLine(this SpriteBatch spriteBatch,Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var tex = new Texture2D(spriteBatch.GraphicsDevice,1,1);
            tex.SetData(new Color[] { color });
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(tex, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
    
}