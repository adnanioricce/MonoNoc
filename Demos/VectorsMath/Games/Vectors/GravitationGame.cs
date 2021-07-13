using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath
{
    public class GravitationGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;
        Attractor _centerBall;
        readonly List<Mover> _orbitingBalls = new List<Mover>();
        readonly float _gravity = 1f;
        bool isClicked = false;
        public GravitationGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }
        public void Initialize(){}

        public void LoadContent()
        {
            var ballTexture = _content.Load<Texture2D>("ball");
            _centerBall = new Attractor(ballTexture,
            new Vector2(Globals.ScreenSize.Width / 2, Globals.ScreenSize.Height / 2),
            2f)
            {
                velocity = new Vector2(1f, 0.0f),
                acceleration = new Vector2(0.001f, 0.0f)
            };
            _orbitingBalls.AddRange(Enumerable.Range(0,5).Select(i => {
                var ball = new Mover(ballTexture,new Vector2(Globals.ScreenSize.Width / (i + 1),
                Globals.ScreenSize.Height / 8),
                1f / (i + 1f),_gravity);
                return ball;
            }));            
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();            
            if(isClicked){
                _centerBall.Drag(mouseState.Position.ToVector2());
            }            
            if(mouseState.LeftButton == ButtonState.Pressed){
                Console.WriteLine($"X:{mouseState.Position.X},Y:{mouseState.Position.Y}");
                if(_centerBall.rectangle.Contains(mouseState.Position)){
                    isClicked = !isClicked;                                                            
                }

            }
            
            
            foreach (var orbitingBall in _orbitingBalls)
            {
                var force = _centerBall.Attract(orbitingBall);
                orbitingBall.ApplyForce(force);
                orbitingBall.Update();
                orbitingBall.Edges(Globals.ScreenSize.Width,Globals.ScreenSize.Height);
            }
            
            // _centerBall.Update();            
            // _centerBall.Edges(Globals.ScreenSize.Width,Globals.ScreenSize.Height);
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _centerBall.Draw(_spriteBatch);
            foreach (var orbitingBalls in _orbitingBalls)
            {
                orbitingBalls.Draw(_spriteBatch);
            }
            _spriteBatch.End();
        }
    }
}