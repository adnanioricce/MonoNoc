using System;
using Lib.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lib.Physics
{
    public class Oscillator
    {
        public Vector2 angle;
        public Vector2 velocity;
        public Vector2 amplitude;
        public Vector2 acceleration;
        public Vector2 position = Vector2.Zero;
        readonly Vector2 centerScreen;
        readonly Texture2D texture;        
        
        
        public Oscillator(Texture2D texture,
            Vector2 angle,
            Vector2 velocity,
            Vector2 amplitude,
            Vector2 acceleration,
            Vector2 centerScreen)
        {
            this.angle = angle;
            this.velocity = velocity;
            this.amplitude = amplitude;
            this.acceleration = acceleration;
            this.texture = texture;
            this.centerScreen = centerScreen;
        }        
        public void Oscillate()
        {
            angle += velocity;
        }
        public void Update(GameTime gameTime)
        {            
            position.X = centerScreen.X + MathF.Sin(angle.X) * amplitude.X * (gameTime.ElapsedGameTime.Milliseconds * 0.16f);
            position.Y = centerScreen.Y + MathF.Cos(angle.Y) * amplitude.Y * (gameTime.ElapsedGameTime.Milliseconds * 0.16f);   
            Oscillate();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(this.centerScreen,position + texture.Bounds.Center.ToVector2(),Color.Black);
            spriteBatch.Draw(texture,position,Color.White);
        }
    }
}