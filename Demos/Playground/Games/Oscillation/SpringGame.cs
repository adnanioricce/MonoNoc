using System;
using Lib;
using Lib.Components;
using Lib.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Playground.Entities;
using VectorsMath;

namespace Playground.Games.Oscillation
{
    public class SpringGame : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        Spring _spring;
        Mover _mover;
        Vector2 textureOffset;
        Vector2 ScreenSize;
        const int millisecondsPerFrame = 48;
        int FrameCount = 0;
        public SpringGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            ScreenSize = Globals.ScreenSizeToVector;            
        }
        public void Initialize()
        {

        }
        public void LoadContent()
        {
            var ballTexture = _content.Load<Texture2D>("ball");
            textureOffset = ballTexture.Bounds.Size.ToVector2() / 2;
            
            _mover = new Mover(ballTexture,ScreenSize / 2f);
            _spring = new Spring(ballTexture,new Vector2(ScreenSize.X / 2f,0f),125);
            var transform = _mover.transform;
            transform.Acceleration = new Vector2(2f,1f);
            transform.Velocity = new Vector2(0.002f,0.002f);
            _mover.gravity = 1f;
        }
        public void Update(GameTime gameTime)
        {            
            FrameCount++;
            if(FrameCount >= 5){
                // _mover.ApplyGravity();
                _spring.Connect(_mover);            
                Console.WriteLine("Milliseconds {0}",((float)gameTime.ElapsedGameTime.Milliseconds));
                var transform = _mover.transform;
                transform.Accelerate();
                transform.Move();                
                _mover.Edges(ScreenSize);
                // _mover.velocity += Vector2.One / ((float)gameTime.ElapsedGameTime.Milliseconds);            
                // _mover.position += _mover.velocity;                
                Console.WriteLine($"Mover position {transform.Position}");
                FrameCount = 0;
            }
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            // _spriteBatch.DrawLine(_spring.Anchor,_mover.position,Color.Black);
            var transform = _mover.transform;
            _spriteBatch.DrawLine(_spring.Anchor + textureOffset,transform.Position + textureOffset,Color.Black);
            _spring.Draw(_spriteBatch);
            _mover.Draw(_spriteBatch);            
            _spriteBatch.End();
        }
    }
}