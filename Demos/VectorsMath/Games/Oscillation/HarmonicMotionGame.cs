using System;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VectorsMath.Games.Oscillation
{
    public class HarmonicMotionGame : ICustomGame
    {    
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        private Mover _mover;
        private float amplitude = 80f;
        private float period = 120f;
        private float frameCount = 1f;
        private float xVelocity = 1f;
        public HarmonicMotionGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }

        public void LoadContent()
        {
            _mover = new Mover(_content.Load<Texture2D>("ball"),Globals.CenterScreen);
        }
        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            frameCount += xVelocity;            
            if(frameCount > period){
                xVelocity *= -1f;                
            }
            float x = amplitude * MathF.Cos((2 * MathF.PI) * frameCount / period);
            _mover.position.X += x / 2f;
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _mover.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}