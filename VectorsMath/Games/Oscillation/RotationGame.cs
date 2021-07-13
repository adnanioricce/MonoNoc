using System;
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
        
        public RotationGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }
        
        public void LoadContent()
        {
            var screenVector = Globals.ScreenSizeToVector;
            mover = new Mover(_content.Load<Texture2D>("rectangle"),new Vector2(screenVector.X * 0.5f,screenVector.Y * 0.5f),1f);                        
        }
        public void Initialize()
        {
            
        }        

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mousePos = mouseState.Position.ToVector2();
            var moverPos = mover.position;
            var acceleration = MathHelper.Clamp(MathF.Sin(Vector2.Distance(mouseState.Position.ToVector2(),moverPos)),0.5f,1f) / 100f;            
            mover.angularAcceleration = acceleration;
            mover.UpdateAngularVelocity();
            if(mousePos.X >= Globals.ScreenSize.Width / 2){
                mover.angularAcceleration = acceleration * -1;
            }
            else if (mousePos.X <= Globals.ScreenSize.Width / 2){
                mover.angularAcceleration = acceleration * -1;
            }                                    
            mover.Update();
            mover.Edges(Globals.ScreenSize.Width,Globals.ScreenSize.Height);
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();            
            mover.Draw(_spriteBatch,mover.angle);
            _spriteBatch.End();
        }
    }
}