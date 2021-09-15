using Lib.Components;
using Lib.Math.Noises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lib.Entities.Particles
{
    /// <summary>
    /// ???
    /// </summary>
    // Noc probably has a good definition
    public record Particle
    {
        private Texture2D Texture;
        public Transform2D Location;
        public PhysicForce2D Physics;        
        public float Lifespan;        
        public Particle(Texture2D texture,
            Transform2D position,
            PhysicForce2D physicsForce,            
            float lifespan = 255f)
        {
            Location = position;            
            Texture = texture;
            Physics = physicsForce;
            Lifespan = lifespan;            
        }
        public bool IsDead() => Lifespan < 0.0;
        public void Update()
        {
            Location.Accelerate();
            Location.Move();
            Location.Acceleration *= 0f;
            Lifespan -= 0.8f;
        }        
        public void Draw(SpriteBatch spriteBatch)
        {            
            //var color = new Color(Location.Position.Y / 720f,Location.Position.X / 1280f,Perlin.Noise(Location.Position.X,Location.Position.Y));
            var color = Color.White;
            spriteBatch.Draw(Texture,Location.Position,null,color * (Lifespan / 255),0f,Vector2.Zero,0.05f,SpriteEffects.None,1f);
        }
    }
}
