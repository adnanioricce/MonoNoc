using Microsoft.Xna.Framework;

namespace Lib.Components
{    
    public delegate Vector2 ApplyForce(Vector2 force);
    /// <summary>
    /// Contains data and behavior to perform basic physics operations on 2D objects
    /// </summary>
    public record PhysicForce2D
    {        
        public float Mass;        
        public Vector2 Force;
        public ApplyForce ApplyForceCallback;
        public PhysicForce2D() : this(Vector2.One)
        {

        }
        public PhysicForce2D(Vector2 force,float mass = 1f)
        {
            Mass = mass;
            Force = force;
            ApplyForceCallback = (force) => PhysicsForces.ApplyNewtonianForce(mass,force);
        }
        public PhysicForce2D(ApplyForce applyForce,float mass = 1f) : this(Vector2.One,mass)
        {
            ApplyForceCallback = applyForce;
            Mass = mass;
        }        
        //TODO: This kind of interface makes no sense
        public void ApplyForce(Transform2D transform) => transform.Acceleration += ApplyForceCallback(Force);
    }
    public static class PhysicsForces
    {
        /// <summary>
        /// Performs basic Newton force based on mass and force
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="force"></param>
        public static Vector2 ApplyNewtonianForce(float mass,Vector2 force)
        {
            return Vector2.Divide(force,mass);
        }
    }
}
