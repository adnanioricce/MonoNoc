using Microsoft.Xna.Framework;
using System;

namespace Lib.Components
{
    /// <summary>
    /// Defines information to handle transform operations on 2D worlds
    /// </summary>
    public struct Transform2D
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;        
        public Transform2D(Vector2 position, Vector2 velocity, Vector2 acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;            
        }

        /// <summary>
        /// Accelerates <see cref="Velocity"/> based on <see cref="Acceleration"/>
        /// </summary>
        public void Accelerate() => Velocity += Acceleration;
        /// <summary>
        /// Updates <see cref="Position"/> based on <see cref="Velocity"/>
        /// </summary>
        public void Move() => Position += Velocity;
        public void Edges(int width,int height,Rectangle target)
        {            
            if(Position.X + target.Width >= width){                
                Velocity.X *= -1;
                this.Position.X = width - target.Width;
            }
            if(Position.X < 0){                
                Velocity.X *= -1;
                this.Position.X = 0;
                
            }
            if(Position.Y + target.Height >= height){                
                Velocity.Y *= Velocity.Y < 0 ? 1 : -1;
                this.Position.Y = height - target.Height;
            }
            if(Position.Y < 0){                
                Velocity.Y *= Velocity.Y + target.Height > height ? -1 : 1;
                this.Position.Y = 0;
            }
        }
        public void ApplyForce(Func<Vector2> force)
        {            
            this.Acceleration += force();
        }
    }    
}
