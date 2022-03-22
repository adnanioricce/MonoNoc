using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using Lib.Math.Noises;
using Lib.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VectorsMath.Games.Oscillation
{
    public class WaveGame : ICustomGame
    {    
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;        
        readonly List<Oscillator> _oscillators = new();
        float amplitude = 200f;
        float angle = 1f;
        float angularVelocity = 0.2f;
        int waveWidth = Globals.ScreenSize.Width / 24;
        const int millisecondsPerFrame = 64;
        int millisecondsSinceLastUpdate = 0;
        public WaveGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;            
        }    
        public void Initialize()
        {

        }

        public void LoadContent()
        {
            var oscillators = Enumerable.Range(1,waveWidth).Select(x => {
                var oscillator = new Oscillator(
                    texture:_content.Load<Texture2D>("ball"),
                    angle:new Vector2(x * 24,0),
                    velocity:Vector2.Zero,
                    amplitude:Vector2.Zero,
                    acceleration:Vector2.Zero,
                    centerScreen:Globals.CenterScreen
                );
                oscillator.position.X = x * 24;
                return oscillator;
            }).ToList();
            _oscillators.AddRange(oscillators);
        }

        public void Update(GameTime gameTime)
        {            
            millisecondsSinceLastUpdate += (int)gameTime.ElapsedGameTime.TotalMilliseconds;            
            if(millisecondsSinceLastUpdate >= millisecondsPerFrame){               
                Console.WriteLine($"totalMilliseconds:{millisecondsSinceLastUpdate}"); 
                millisecondsSinceLastUpdate = 0;
                _oscillators.ForEach(o => {

                    float y = amplitude * MathF.Sin(angle) * (gameTime.ElapsedGameTime.Milliseconds / 16);
                    o.position.Y = y + Globals.CenterScreen.Y;
                    angle += angularVelocity;
                });
            }                                    
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _oscillators.ForEach(oscillator => oscillator.Draw(_spriteBatch));
            _spriteBatch.End();
        }
    }
}