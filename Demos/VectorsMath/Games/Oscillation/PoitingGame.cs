using System;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VectorsMath;

namespace VectorsGame
{
    public class PointingGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;
        readonly Vector2 _screenSize;
        Mover _mover;        
        public PointingGame(SpriteBatch spriteBatch,ContentManager content,Vector2 screenSize)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _screenSize = screenSize;
        }

        public void Initialize()
        {            
        }

        public void LoadContent()
        {
            _mover = new Mover(_content.Load<Texture2D>("rectangle"),_screenSize / 2f,0.5f);
            _mover.velocity = new Vector2(0.001f,0.001f);
        }

        public void Update(GameTime gameTime)
        {                    
            var mouse = Mouse.GetState();
            var mousePos = mouse.Position.ToVector2();
            var direction = mousePos - _mover.position;
            direction.Normalize();
            direction *= 0.5f;
            _mover.acceleration = direction;            
            _mover.angle = MathF.Atan2(_mover.velocity.Y, _mover.velocity.X);
            _mover.Update();
            _mover.velocity = Vector2.Min(_mover.velocity,_mover.topSpeed);
            _mover.Edges(_screenSize);
        }
        public void Draw(GameTime gameTime)
        {            
            _spriteBatch.Begin();            
            _mover.Draw(_spriteBatch,_mover.angle);
            _spriteBatch.End();
        }
    }
}