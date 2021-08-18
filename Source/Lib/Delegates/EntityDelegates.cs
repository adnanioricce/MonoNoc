using Microsoft.Xna.Framework;
namespace Lib.Delegates
{
    public delegate void Update<T>(T obj,GameTime gameTime);
    public delegate float UpdateByVelocity(float value,float velocity);
    public delegate Vector2 UpdateByVelocity2(Vector2 value,Vector2 velocity);
    
    public delegate float UpdateVelocity(float velocity,float acceleration);
    public delegate Vector2 UpdateVelocity2(Vector2 velocity,Vector2 acceleration);
}
