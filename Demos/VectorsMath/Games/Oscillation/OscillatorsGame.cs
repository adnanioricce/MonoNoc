using System.Collections.Generic;
using System.Linq;
using Lib;
using Lib.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VectorsMath.Games.Oscillation
{
    public class OscillatorsGame : ICustomGame
    {    
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _contentManager;
        private List<Oscillator> oscillators = new List<Oscillator>();
        public OscillatorsGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _contentManager = content;
        }
        public void Initialize()
        {
        }
        public void LoadContent()
        {
            var initialOscillators = Enumerable.Range(1,15).Select(i => {
                var pos = new Vector2(i % 2 == 0 ? 180 : 360,i % 2 == 0 ? 180 : 360);
                var velocity = new Vector2(
                    i * 0.005f,
                    i * 0.005f
                );
                var amplitude = new Vector2(-128,80);
                var oscillator = new Oscillator(
                    texture:_contentManager.Load<Texture2D>("ball"),
                    angle:pos,
                    velocity:velocity,
                    amplitude:amplitude,
                    acceleration:new Vector2(0,i * 0.0001f),
                    Globals.CenterScreen);
                return oscillator;
            }).ToList();
            oscillators.AddRange(initialOscillators);
            // oscillator = 
        }
        public void Update(GameTime gameTime)
        {
            oscillators.ForEach(oscillator => {                
                oscillator.Update(gameTime);
            });
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            oscillators.ForEach(oscillator => oscillator.Draw(_spriteBatch));
            _spriteBatch.End();
        }
    }
}