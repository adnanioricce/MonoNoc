using Microsoft.Xna.Framework;
using System;

namespace Lib.Components
{
    public static partial class TransformExtensions
    {
        /// <summary>
        /// Limit the length of the vector to the <paramref name="max"/> value
        /// </summary>
        /// <param name="vec">the vector being limited</param>
        /// <param name="max">the limit value</param>
        /// <returns>a vector with it's length limited to <paramref name="max"/></returns>
        public static Vector2 Limit(this Vector2 vec, float max)
        {
            if (vec.LengthSquared() > max * max)
            {
                vec.ToLength(max);
            }
            return vec;
        }
        //TODO: Check if this already exists in MonoGame library
        public static Vector2 ToLength(this Vector2 vec,float length)
        {
            vec.Normalize();
            vec *= length;
            return vec;
        }               
        public static Vector2 GetDirection(this Vector2 vec, Vector2 target,float max = 1f)
        {
            // A vector pointing from the position to the target
            var desired = target - vec;  
            // Scale to maximum speed            
            return desired.ToLength(max);                        
        }        
        public static Vector2 Seek(this Vector2 desired,Vector2 velocity,float max = 1f)
        {
            // Steering = Desired minus velocity
            var steer = desired - velocity;
            // Limit to maximum steering force
            return steer.Limit(max);  
        }
        /// <summary>
        /// Get the angle of rotation for <paramref name="vec"/>
        /// </summary>
        /// <param name="vec">the current <paramref name="vec"/> to calculate the angle rotation</param>
        /// <returns>the angle of rotation for <paramref name="vec"/></returns>
        public static float GetHeadingDirection(this Vector2 vec)
            => MathF.Atan2(vec.Y, vec.X);
        public static Transform2D Edges(this Transform2D t,int width, int height, Rectangle target)
        {
            if (t.Position.X + target.Width >= width)
            {
                t.Velocity.X *= -1;
                t.Position.X = width - target.Width;
            }
            if (t.Position.X < 0)
            {
                t.Velocity.X *= -1;
                t.Position.X = 0;

            }
            if (t.Position.Y + target.Height >= height)
            {
                t.Velocity.Y *= t.Velocity.Y < 0 ? 1 : -1;
                t.Position.Y = height - target.Height;
            }
            if (t.Position.Y < 0)
            {
                t.Velocity.Y *= t.Velocity.Y + target.Height > height ? -1 : 1;
                t.Position.Y = 0;
            }
            return t;
        }
        /// <summary>
        /// Accelerates <see cref="Velocity"/> based on <see cref="Acceleration"/>
        /// </summary>
        public static Transform2D Accelerate(this Transform2D t)
        {
            t.Velocity += t.Acceleration;
            return t;
        }
        /// <summary>
        /// Updates <see cref="Position"/> based on <see cref="Velocity"/>
        /// </summary>
        public static Transform2D Move(this Transform2D t)
        {
            t.Position += t.Velocity;
            return t;
        }
        public static Transform2D ApplyForce(this Transform2D t,Func<Vector2> force)
        {
            t.Acceleration += force();
            return t;
        }
        public static Transform2D ApplyForce(this Transform2D t, Vector2 force)
        {
            t.Acceleration += force;
            return t;
        }
        //public static Transform2D Map()
        //{
        //    float outgoing = start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
        //    String badness = null;
        //    if (outgoing != outgoing)
        //    {
        //        badness = "NaN (not a number)";

        //    }
        //    else if (outgoing == Float.NEGATIVE_INFINITY ||
        //             outgoing == Float.POSITIVE_INFINITY)
        //    {
        //        badness = "infinity";
        //    }
        //    if (badness != null)
        //    {
        //        final String msg =
        //          String.format("map(%s, %s, %s, %s, %s) called, which returns %s",
        //                        nf(value), nf(start1), nf(stop1),
        //                        nf(start2), nf(stop2), badness);
        //        PGraphics.showWarning(msg);
        //    }
        //    return outgoing;
        //}
    }
}
