using System;
using Lib;
using Lib.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath.Games.Oscillation
{
    public class SpaceshipGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;
        Mover spaceship;
        float angularAccelaration = 0.01f;
        float angularVelocity = 0.0f;
        float angle = 90f;        
        public SpaceshipGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }        

        public void Initialize()
        {   
            var transform = spaceship.transform;
            transform.Position = Globals.CenterScreen;
        }

        public void LoadContent()
        {
            spaceship = new Mover(_content.Load<Texture2D>("spaceship"),Globals.CenterScreen);
        }

        public void Update(GameTime gameTime)
        {            
            
            var currentState = Keyboard.GetState();
            if(currentState.IsKeyDown(Keys.D)){
                angularVelocity += angularAccelaration;
                angle -= MathHelper.Clamp(angularVelocity,0.01f,0.15f);
                spaceship.direction = new Vector2(MathF.Cos(angle),MathF.Sin(angle));
            }
            if(currentState.IsKeyDown(Keys.A)){
                angularVelocity += angularAccelaration;
                angle += MathHelper.Clamp(angularVelocity,0.01f,0.15f);                
                spaceship.direction = new Vector2(MathF.Cos(angle),MathF.Sin(angle));
            }
            if(currentState.IsKeyDown(Keys.Space)){                
                spaceship.direction = new Vector2(MathF.Cos(angle),MathF.Sin(angle));                
            }
            var transform = spaceship.transform;
            transform.Position += spaceship.direction * 0.1f * gameTime.ElapsedGameTime.Milliseconds;
            spaceship.Update();
            spaceship.Edges(Globals.ScreenSizeToVector);            
        }
        public void Draw(GameTime gameTime)
        {            
            _spriteBatch.Begin();
            spaceship.Draw(_spriteBatch,angle);
            _spriteBatch.End();
        }
    }
}