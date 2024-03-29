using Lib;
using Lib.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath
{
    public class FrictionGame : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        private Mover _mover;        
        private Vector2 _gravity = new Vector2(0,0.3f);
        private Vector2 _wind = new Vector2(0.2f,0);
        public FrictionGame(SpriteBatch spriteBatch,ContentManager content)
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
            _gravity *= _mover.mass;
            var transform = _mover.transform;
            transform.ApplyForce(() => _gravity);
            transform.ApplyForce(() => _wind);
            if(Mouse.GetState().LeftButton == ButtonState.Pressed){
                
                var friction = new Vector2(transform.Velocity.X,transform.Velocity.Y);
                friction = Vector2.Normalize(friction);
                friction *= -1;
                float coefficient = 0.1f;
                friction *= coefficient;
                transform.ApplyForce(() => friction);
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
    }
}