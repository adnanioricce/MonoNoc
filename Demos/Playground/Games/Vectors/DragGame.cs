using Lib;
using Lib.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath
{
    public class DragGame : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        private Mover _mover;
        private Vector2 _gravity = new Vector2(0.0f,0.4f);
        public DragGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }        
        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            _mover = new Mover(_content.Load<Texture2D>("ball"),new Vector2(Globals.ScreenSize.Width / 2,Globals.ScreenSize.Height / 2),0.5f);
        }

        public void Update(GameTime gameTime)
        {       
            var transform = _mover.transform;
            if(MousePressed()){                
                var dragForce = new Vector2(transform.Velocity.X,transform.Velocity.Y);
                dragForce.Normalize();
                var coefficient = -0.03f;
                var speed = transform.Velocity.LengthSquared();
                dragForce *= speed * coefficient;                
                transform.ApplyForce(() => dragForce);
                
            } else {
                transform.ApplyForce(() => _gravity);                
            }            
            _mover.Update();
            _mover.Edges(Globals.ScreenSizeToVector);
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _mover.Draw(_spriteBatch);
            _spriteBatch.End();
        }
        private bool MousePressed()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
    }
}