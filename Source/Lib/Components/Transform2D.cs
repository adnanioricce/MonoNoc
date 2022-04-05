using Microsoft.Xna.Framework;

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
    }    
}
