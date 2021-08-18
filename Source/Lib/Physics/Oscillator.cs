using System;
using Lib.Delegates;
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
        public UpdateByVelocity2 OnUpdatePosition;
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
            OnUpdatePosition = 
                (_angle,_amplitude) => new Vector2(MathF.Sin(_angle.X) * _amplitude.X,MathF.Cos(_angle.Y) * _amplitude.Y);
        }
        public void Oscillate()
        {
            angle += velocity;
        }
        public void Update(GameTime gameTime)
        {     
            position = centerScreen + OnUpdatePosition(angle,amplitude) * (gameTime.ElapsedGameTime.Milliseconds * 0.16f);
            Oscillate();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(this.centerScreen,position + texture.Bounds.Center.ToVector2(),Color.Black);
            spriteBatch.Draw(texture,position,Color.White);
        }
    }
}