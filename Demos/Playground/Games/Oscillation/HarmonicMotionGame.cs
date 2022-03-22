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
        private const float amplitude = 798.9825f;
        private Mover _mover;
        
        public HarmonicMotionGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }

        public void LoadContent()
        {
            _mover = new Mover(_content.Load<Texture2D>("ball"),new Vector2(0,Globals.CenterScreen.Y));
            _mover.angle = 0.0f;
            _mover.angularAcceleration = 0.05f;
            _mover.angularVelocity = 0.05f;
        }
        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            float x = amplitude * MathF.Cos(_mover.angle);
            Console.WriteLine($"X:{x}");
            var transform = _mover.transform;
            transform.Position.X = amplitude + x;            
            _mover.angle += _mover.angularVelocity;
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _mover.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}