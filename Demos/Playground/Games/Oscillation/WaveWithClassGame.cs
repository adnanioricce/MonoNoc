using Lib;
using Lib.Entities;
using Lib.Math.Noises;
using Lib.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace VectorsMath.Games.Oscillation
{
    public class WaveWithClassGame : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        Wave wave;        
        float anglePeriod = 6f;
        float angularVelocity = 0.02f;
        const int millisecondsPerFrame = 64;
        int millisecondsSinceLastUpdate = 0;
        public WaveWithClassGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }        
        public void Initialize()
        {
            
        }
        public void LoadContent()
        {
            wave = new Wave(64,200,Vector2.One);
            wave.OnOscillate = (oscillator,time) =>
            {
                var y = wave.Amplitude * MathF.Sin(wave.Angle * oscillator.angle.X) * (time.ElapsedGameTime.Milliseconds / 16);
                oscillator.position.Y = y + Globals.CenterScreen.Y;                                
            };
            Func<int,Oscillator> createOscillator = (index) =>
            {
                var oscillator = new Oscillator(
                    texture:_content.Load<Texture2D>("ball"),
                    angle:new Vector2(index * 0.1f,0f),
                    velocity:new Vector2(0.02f,0.0f),
                    amplitude:new Vector2(0f,200f),
                    acceleration:Vector2.Zero,
                    centerScreen:Globals.CenterScreen);
                oscillator.position = new Vector2(24 * index,0f);
                return oscillator;
            };
            for (int i = 1; i <= wave.WaveWidth; ++i) {
                var oscillator = createOscillator(i);                
                wave.Oscillators.Add(oscillator);
            }            
            
        }
        public void Update(GameTime gameTime)
        {
            millisecondsSinceLastUpdate += millisecondsPerFrame;
            if(millisecondsSinceLastUpdate >= millisecondsPerFrame){
                millisecondsSinceLastUpdate = 0;
                wave.Update(gameTime);                
                wave.Angle += angularVelocity;
                if (wave.Angle > anglePeriod)
                {
                    angularVelocity *= -1;
                }
                else if(Math.Abs(wave.Angle) > anglePeriod)
                {
                    angularVelocity *= -1;
                }
                
                
            }
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            wave.Draw(_spriteBatch);
            //secondWave.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
