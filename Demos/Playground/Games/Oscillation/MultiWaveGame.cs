using Lib;
using Lib.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Playground.Games.Oscillation
{
    public class MultiWaveGame : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        private readonly List<Wave> _waves = new List<Wave>();
        public MultiWaveGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }
        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            var firstWave = new Wave(12,30,Vector2.One);
            firstWave.OnOscillate = (point,time) =>
            {
                   
            };
            _waves.Add(firstWave);
            _waves.Add(new Wave(12,30,Vector2.One * 2));            
        }

        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(GameTime gameTime)
        {
            
        }
    }
}
