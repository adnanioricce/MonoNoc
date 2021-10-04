using System;
using System.Diagnostics;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath
{
    public class RotationGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;        
        Mover mover;                
        float direction = 1f;
        public RotationGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }
        
        public void LoadContent()
        {
            var screenVector = Globals.ScreenSizeToVector;
            mover = new Mover(_content.Load<Texture2D>("rectangle"),new Vector2(screenVector.X * 0.5f,screenVector.Y * 0.5f),1f);
            mover.angle = 0f;    
        }
        public void Initialize()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mousePos = mouseState.Position.ToVector2();
            //Debug.WriteLine($"Mouse position:{mousePos}");            
            var transform = mover.transform;
            var acceleration = MathHelper.Clamp(
                MathF.Sin(Vector2.Distance(mousePos,transform.Position)),
                0.0005f,
                1f) / 1000f;                        
            mover.angularAcceleration = acceleration;            
            mover.UpdateAngularVelocity();
            mover.angularAcceleration = acceleration;

        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();                        
            mover.Draw(_spriteBatch,mover.angle);
            _spriteBatch.End();
        }
    }
}