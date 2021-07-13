using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Lib
{
    public class Attractor
    {
        private readonly Texture2D _texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
        public Rectangle rectangle;
        public float mass = 1f;
        public float gravity = 1f;
        public float angularVelocity = 0.0f;
        public float angularAcceleration = 0.0001f;
        public float angle = 0f;
        public Attractor(Texture2D texture,Vector2 position,float mass)
        {
            _texture = texture;
            velocity = new Vector2(0,0);
            acceleration = new Vector2(0.1f,0f);
            this.position = position;
            this.mass = mass;
            this.rectangle = new Rectangle((int)position.X,(int)position.Y,(int)(texture.Width * mass),(int)(texture.Height * mass));
        }        
        
        public void Drag(Vector2 position)
        {
            this.position = position;
        }        
        public Vector2 Attract(Mover other){
            var force = Vector2.Subtract(this.position,other.position);
            var radius = force.Length();
            float d =  MathHelper.Clamp(radius,0.1f,1f);
            force.Normalize();
            var r = gravity * mass * other.mass;
            float strength = MathF.Sqrt((r * r) + (d * d));
            return force * strength;
        }
        public void Update()
        {                                     
            velocity += acceleration;            
            position += velocity;            
            acceleration = Vector2.Multiply(acceleration,0f);
            this.rectangle.X = (int)position.X;
            this.rectangle.Y = (int)position.Y;
            angle += angularVelocity;
            angularVelocity += angularAcceleration;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,position,null,Color.White,0f,Vector2.Zero,mass,SpriteEffects.None,1f);
        }
    }
}