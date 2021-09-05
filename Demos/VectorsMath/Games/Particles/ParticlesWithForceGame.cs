using Lib;
using Lib.Components;
using Lib.Entities.Particles;
using Lib.Factories;
using Lib.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using VectorsMath;

namespace Playground.Games.Particles
{
    public class ParticlesWithForceGame : ICustomGame
    {
        private ParticleSystem particleSystem;
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        KeyboardState lastState;
        InputHelper inputHelper = new InputHelper();
        private Func<Vector2> _force = () => Vector2.Zero;   
        private RepellerObject2D _repeller;
        public ParticlesWithForceGame(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
        }
        Particle ParticleFactory(ParticleSystem system) {
            var transform = new Transform2D();
            transform.Position = system.Origin;
            transform.Acceleration = new Vector2(Globals.GetRandomFloat(-.05f, .05f), Globals.GetRandomFloat(-.05f, .05f));
            transform.Velocity = new Vector2(Globals.GetRandomFloat(-1.0f, 1.0f), Globals.GetRandomFloat(-2, 2));
            var physics = new PhysicForce2D();            
            physics.Force = _force();
            var particle = new Particle(TextureFactory.CreateSolidTexture(_graphicsDevice,Color.Black,16,16),transform,physics);
            return particle;
        }
        public void LoadContent()
        {           
            var radius = 12f;
            _repeller = new RepellerObject2D(TextureFactory.CreateSolidTexture(_graphicsDevice,Color.White,(int)radius * 2,(int)radius * 2),
                new Vector2(600,450),
                radius,
                2.5f);
            particleSystem = new ParticleSystem(1,new Vector2(600,310),this.ParticleFactory);            
        }

        public void Initialize()
        {
            
        }        
        
        public void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
            if(inputHelper.IsNewKeyPress(currentState,lastState,Keys.Space)){
                _force = () =>
                {
                    return new Vector2(Globals.GetRandomFloat(-0.05f,0.05f),Globals.GetRandomFloat(-0.05f,0.05f));
                };
            }
            particleSystem.ApplyForce();
            particleSystem.ApplyReppeler(_repeller);
            particleSystem.Update(gameTime);
            particleSystem.AddParticle();
            
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _repeller.Draw(_spriteBatch);
            particleSystem.Draw(_spriteBatch,gameTime);
            _spriteBatch.End();
        }        

    }
}
