using Lib.Components;
using Lib.Delegates;
using Lib.Extensions;
using Lib.Math.Noises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lib.Entities.Particles
{

    /// <summary>
    /// ???
    /// </summary>
    // Noc probably has a good definition
    public record Particle
    {
        Update<Particle> _updateAction;
        Texture2D Texture;

        public Transform2D Location;
        public PhysicForce2D Physics;        
        public float Lifespan;
        public Color Color;
        public Particle(Texture2D texture,
            Transform2D position,
            PhysicForce2D physicsForce,            
            Update<Particle> updateAction,
            float lifespan = 255f)
        {
            Location = position;            
            Texture = texture;
            Physics = physicsForce;
            Lifespan = lifespan;       
            _updateAction = updateAction;
        }
        public bool IsDead() => Lifespan < 0.0;
        public void Update(GameTime gameTime)
        {
            if(_updateAction is null)
            {
                DefaultUpdate();
                return;
            }
            _updateAction(this,gameTime);
        }
        private void DefaultUpdate()
        {
            Location.Accelerate();
            Location.Move();
            Location.Acceleration *= 0f;
            Lifespan -= 0.8f;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //var color = new Color(Location.Position.Y / 720f,Location.Position.X / 1280f,Perlin.Noise(Location.Position.X,Location.Position.Y));            
            spriteBatch.Draw(Texture, Location.Position, null, Color * (Lifespan / 255), 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            //spriteBatch.CreateCircle(Location.Position,4f,Color.White * (Lifespan / 255));
        }
    }
}
