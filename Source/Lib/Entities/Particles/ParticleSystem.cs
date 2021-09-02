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
        public void Update(GameTime gameTime)
        {
            //var enumerator = Particles.GetEnumerator();
            //while (enumerator.MoveNext())
            
            foreach(var particle in Particles)
            {
                //var particle = enumerator.Current;                
                particle.Update();
            }
            var newParticles = Particles
                                .Where(p => p.IsDead())
                                .Select(p => ParticleFactory(this))
                                .ToList();
            if(newParticles.Any()){
                Particles.RemoveAll(p => p.IsDead());
                Particles.AddRange(newParticles);
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
