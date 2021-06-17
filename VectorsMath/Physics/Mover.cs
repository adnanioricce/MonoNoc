using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath
{
    public class Mover
    {
        private readonly Texture2D _texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
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
            velocity = new Vector2(0,0);
            acceleration = new Vector2(0.0f,0f);
            this.position = position;
            this.mass = mass;
            this.rectangle = new Rectangle((int)position.X,(int)position.Y,(int)(ballTexture.Width * mass),(int)(ballTexture.Height * mass));
        }
        public Mover(Texture2D ballTexture,Vector2 position,float mass,float gravity) : this(ballTexture,position,mass)
        {
            this.gravity = gravity;
        }        
        public void Edges(Vector2 screenSize)
        {
            Edges((int)screenSize.X,(int)screenSize.Y);
        }
        public void Edges(int width,int height)
        {            
            if(position.X + rectangle.Width >= width){                
                velocity.X *= -1;
                this.position.X = width - rectangle.Width;
            }
            if(position.X < 0){                
                velocity.X *= -1;
                this.position.X = 0;
                
            }
            if(position.Y + rectangle.Height >= height){                
                velocity.Y *= velocity.Y < 0 ? 1 : -1;
                this.position.Y = height - rectangle.Height;
            }
            if(position.Y < 0){                
                velocity.Y *= velocity.Y + rectangle.Height > height ? -1 : 1;
                this.position.Y = 0;
            }
        }
        public void Drag(Vector2 position)
        {
            this.position = position;
        }
        public void ApplyForce(Vector2 force)
        {
            acceleration += Vector2.Divide(force,mass);
        }
        public Vector2 Attract(Mover other){
            var force = Vector2.Subtract(this.position,other.position);
            var radius = force.Length();            
            force.Normalize();
            float strength = (gravity * mass * other.mass) / radius;
            force *= strength;
            return force;
        }
        public void Update()
        {                                     
            velocity += acceleration;
            position += velocity;            
            acceleration = Vector2.Multiply(acceleration,0f);            
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;                        
        }
        public void UpdateAngularVelocity()
        {
            angularVelocity += angularAcceleration;
            angle += angularVelocity;
            angularAcceleration *= 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,position,null,Color.White,0f,Vector2.Zero,mass,SpriteEffects.None,1f);
        }
        public void Draw(SpriteBatch spriteBatch,float angle){
            
            var center = new Vector2(_texture.Width / 2,_texture.Height / 2);
            spriteBatch.Draw(_texture,position,null,Color.White,angle,center,mass,SpriteEffects.None,1f);
        }
    }
}