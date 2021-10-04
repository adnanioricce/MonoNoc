using Lib.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Entities.Particles
{
    public class ParticleSystem
    {        
        private readonly List<Particle> Particles = new List<Particle>();
        private readonly Func<ParticleSystem,Particle> ParticleFactory;
        public Vector2 Origin;
        public int ParticlesCount;
        public ParticleSystem(int particlesCount,Vector2 origin,Func<ParticleSystem,Particle> particleFactory)
        {            
            ParticlesCount = particlesCount;
            Origin = origin;
            ParticleFactory = particleFactory;
            Particles.AddRange(Enumerable.Repeat(ParticleFactory,particlesCount).Select(createParticle => createParticle(this)).ToList());
        }     
        public void AddParticle(Func<Particle> createParticle)
        {
            Particles.Add(createParticle());
        }
        public void AddParticle()
        {
            Particles.Add(ParticleFactory(this));
        }
        
        public void ApplyReppeler(RepellerObject2D reppeler)
        {
            Particles.ForEach(particle =>
            {
                var location = particle.Location;                
                var physics = particle.Physics;
                //TODO:It seems that this API is incompatible with what I needed. Refactor it to better apply here
                location.Acceleration += Vector2.Divide(reppeler.Repel(particle.Location.Position),physics.Mass);                
            });
        }
        public void ApplyForce()
        {
            Particles.ForEach(p =>
            {
                var ph = p.Physics;                
                ph.ApplyForce(p.Location);
            });
        }
        public void Update(GameTime gameTime)
        {

            foreach (var particle in Particles)
            {
                particle.Update(gameTime);
            }
            var newParticles = Particles
                                .Where(p => p.IsDead())
                                .Select(p => ParticleFactory(this))
                                .ToList();
            if(newParticles.Any()){
                Particles.RemoveAll(p => p.IsDead());                
            }
        }
        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            
            foreach(var particle in Particles)
            {
                particle.Draw(spriteBatch);
            }            
        }
    }
}
