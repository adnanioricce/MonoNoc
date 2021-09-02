using Lib;
using Lib.Entities.Particles;
using Lib.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using VectorsMath;

namespace Playground.Games.Particles
{
    public class HelloParticles : ICustomGame
    {
        private readonly List<ParticleSystem> particleSystems = new List<ParticleSystem>();        
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        public HelloParticles(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
        }
        Particle ParticleFactory(ParticleSystem system) {
            var particle = new Particle(TextureFactory.CreateSolidTexture(_graphicsDevice,Color.Black,16,16),system.Origin,Globals.GetRandomInt(50,255));
            particle.Acceleration = new Vector2(0, 0.05f);
            particle.Velocity = new Vector2(Globals.GetRandomFloat(-1.0f, 1.0f), Globals.GetRandomFloat(-2, 0));
            return particle;
        }
        public void LoadContent()
        {            
            particleSystems.AddRange(
                Enumerable.Range(0,50).Select(i => new ParticleSystem(25,new Vector2(Globals.GetRandomInt(0,1200),Globals.GetRandomInt(0,700)),this.ParticleFactory))
            );
        }
        public void Initialize()
        {            
        }
        public void Update(GameTime gameTime)
        {                        
            particleSystems.ForEach(system => system.Update(gameTime));
        }
        public void Draw(GameTime gameTime)
        {                        
            _spriteBatch.Begin();
            particleSystems.ForEach(system => system.Draw(_spriteBatch,gameTime));
            _spriteBatch.End();
        }
    }
}
