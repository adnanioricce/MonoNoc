using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lib.Entities.Particles
{
    public record Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        private Texture2D Texture;
        public float Lifespan;
        public Particle(Texture2D texture,Vector2 position,float lifespan)
        {
            Position = position;
            Acceleration = new Vector2();
            Velocity = new Vector2();
            Texture = texture;
            Lifespan = lifespan;
        }
        public bool IsDead() => Lifespan < 0.0;
        public void Update()
        {
            Velocity += Acceleration;
            Position += Velocity;
            Lifespan -= 0.8f;            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Position,Color.Black * (Lifespan / 255));
        }
    }
}
