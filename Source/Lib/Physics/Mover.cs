using Lib.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lib
{
    public class Mover
    {
        private readonly Texture2D _texture;
        public Transform2D transform;        
        public Vector2 direction;
        public Rectangle rectangle;
        public float mass = 2f;
        public float gravity = 1f;
        public float angularVelocity = 0.0f;
        public float angularAcceleration = 0.0001f;
        public float angle = 90f;
        public Vector2 topSpeed = Vector2.One * 4f;
        public Mover(Texture2D ballTexture,Vector2 position,float mass = 1f)
        {
            _texture = ballTexture;
            transform = new Transform2D(position ,new Vector2(0,0),new Vector2(0.0f,0.0f));
            this.mass = mass;
            this.rectangle = new Rectangle((int)position.X,(int)position.Y,(int)(ballTexture.Width * mass),(int)(ballTexture.Height * mass));
        }
        public Mover(Texture2D ballTexture,Vector2 position,float mass,float gravity) : this(ballTexture,position,mass)
        {
            this.gravity = gravity;
        }
        public void Edges(Vector2 screenSize)
        {
            transform.Edges((int)screenSize.X,(int)screenSize.Y,rectangle);
        }        
        public void ApplyGravity()
        {
            this.transform.Acceleration += Vector2.Divide(new Vector2(0f,gravity),mass);
        }
        public Vector2 Attract(Mover other){
            var position = transform.Position;
            var force = Vector2.Subtract(position,other.transform.Position);
            var radius = force.Length();            
            force.Normalize();
            float strength = (gravity * mass * other.mass) / radius;
            force *= strength;
            return force;
        }
        public void Update()
        {          
            transform.Accelerate();
            transform.Move();
            transform.Acceleration = Vector2.Multiply(transform.Acceleration,0f); 
            var position = transform.Position;
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;            
            //var velocity = transform.Velocity;
            //if(velocity.X > 0 && velocity.Y > 0){
            //    velocity -= new Vector2(velocity.X * 0.01f,velocity.Y * 0.01f);
            //    if(velocity.X < 0){
            //        velocity.X = 0;
            //    }
            //    if(velocity.Y < 0){
            //        velocity.Y = 0;
            //    }
            //}
            //transform.Velocity = velocity;
        }
        public void UpdateAngularVelocity()
        {
            angularVelocity += angularAcceleration;
            angle += angularVelocity;            
            angularAcceleration *= 0;
            angle = MathHelper.Clamp(angle,1f,360f);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            var position = transform.Position;
            spriteBatch.Draw(_texture,position,null,Color.White,0f,Vector2.Zero,mass,SpriteEffects.None,1f);
        }
        public void Draw(SpriteBatch spriteBatch,float angle){
                        
            var center = new Vector2(_texture.Width / 2,_texture.Height / 2);
            var position = transform.Position;
            spriteBatch.Draw(_texture,position,null,Color.White,angle,center,mass,SpriteEffects.None,1f);
        }
    }
}