using System;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath
{
    public class AccelerationGame : ICustomGame
    {        
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly ContentManager _content;
        private readonly Mover[] _balls = new Mover[10];
        private readonly Random rng = new Random();
        public AccelerationGame(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            _content = content;
        }        

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            for(int i = 0; i < _balls.Length;++i){
                var (x,y) = (rng.Next(1,1280),
                            rng.Next(1,720 / 2));
                _balls[i] = new Mover(_content.Load<Texture2D>("ball"),
                new Vector2(x,y),
                (float)rng.NextDouble());
            }
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            for(int i = 0;i < _balls.Length;++i){
                _balls[i].ApplyForce(new Vector2(0.0f,0.5f));
                if(mouseState.LeftButton == ButtonState.Pressed){
                    _balls[i].ApplyForce(new Vector2(0.5f,0.0f));
                }
                _balls[i].Update();
                _balls[i].Edges(Globals.ScreenSizeToVector);
            }
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            for(int i = 0;i < _balls.Length;++i){
                _balls[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();                     
        }
    }
}