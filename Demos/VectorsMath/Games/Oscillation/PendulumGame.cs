using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Playground.Entities;
using System;
using System.Diagnostics;
using VectorsMath;

namespace Playground.Games.Oscillation
{
    public class PendulumGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;                
        Pendulum _pendulum;

        public PendulumGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }
        
        public void LoadContent()
        {
            var screenSize = Globals.ScreenSizeToVector;
            _pendulum = new Pendulum(_content.Load<Texture2D>("ball"),new Vector2(screenSize.X / 2,0f),screenSize / 2f);
            _pendulum.Angle = MathF.PI / 4;
            _pendulum.Radius = 250f;
        }
        public void Initialize()
        {            
        }        

        public void Update(GameTime gameTime)
        {
            _pendulum.Update(gameTime);
            _pendulum.AngularVelocity *= 0.99f;
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _pendulum.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}