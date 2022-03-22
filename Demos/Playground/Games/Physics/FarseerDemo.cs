using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Playground.Games.Physics
{
    public class FarseerDemo : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        
        public FarseerDemo(SpriteBatch spriteBatch,
            ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }
        public void LoadContent()
        {
            
        }
        public void Initialize()
        {
            
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(GameTime gameTime)
        {
            //_spriteBatch.Begin();
            //_world.BodyList.ForEach(body => _spriteBatch.Draw(ballTexture, body.Position, Color.White));
            //_spriteBatch.Draw(rectangleTexture, rectangleBody.Position, Color.White);
            //_spriteBatch.End();
        }
    }
}
