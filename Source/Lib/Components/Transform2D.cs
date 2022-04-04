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
    public static partial class TransformExtensions
    {
        public static Vector2 Limit(this Vector2 vec, float max)
        {
            if (vec.LengthSquared() > max * max)
            {
                //vec.Normalize();
                //vec *= max;
                vec.SetMagnitude(max);
            }
            return vec;
        }
        //TODO: Check if this already exists in MonoGame library
        public static Vector2 SetMagnitude(this Vector2 vec,float length)
        {
            vec.Normalize();
            vec *= length;
            return vec;
        }
        // A method that calculates a steering force towards a target
        // STEER = DESIRED MINUS VELOCITY
        public static Vector2 GetDirection(this Vector2 initial, Vector2 target,float max = 1f)
        {
            var desired = target - initial;  // A vector pointing from the position to the target

            // Scale to maximum speed            
            return desired.SetMagnitude(max);                        
        }
        // A method that calculates a steering force towards a target
        // STEER = DESIRED MINUS VELOCITY
        public static Vector2 Seek(this Vector2 desired,Vector2 velocity,float max = 1f)
        {
            // Steering = Desired minus velocity
            var steer = desired - velocity;
            return steer.Limit(max);  // Limit to maximum steering force            
        }
        public static float GetHeadingDirection(this Vector2 vec)
        {
            return MathF.Atan2(vec.Y, vec.X);
        }
    }
}
