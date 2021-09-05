using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Components
{
    /// <summary>
    /// Defines information to handle transform operations on 2D worlds
    /// </summary>
    public record Transform2D
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Transform2D() : this(Vector2.Zero,Vector2.Zero,Vector2.Zero)
        {            

        }
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
        
    }    
}
