using Lib.Components;
using Microsoft.Xna.Framework;
using System;


namespace Lib.Helpers
{
   public static class ForceHelper
    {
        public static Vector2 Gravity(float gravity,float mass) 
            => Vector2.Divide(new Vector2(0f,gravity),mass);
        public static Vector2 FNewtonLaw(Vector2 force,float mass) 
            => Vector2.Divide(force,mass);
        public static Transform2D ApplyForce(Transform2D transform,Vector2 force) 
            => ApplyForce(transform,() => force);
        public static Transform2D ApplyForce(Transform2D transform,Func<Vector2> getForce){
            transform.Acceleration += getForce();
            return transform;
        }
        public static Transform2D ApplyGravity(Transform2D transform,float gravity,float mass) 
            => ApplyForce(transform,Gravity(gravity,mass));
    }
}
