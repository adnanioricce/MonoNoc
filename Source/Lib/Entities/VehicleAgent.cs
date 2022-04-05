using Lib.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Entities
{
    public struct VehicleAgent
    {
        public Transform2D Transform;
        public float Radius;
        public float MaxSpeed;
        public float MaxForce;
        public static VehicleAgent Create(Vector2 initialPosition)
        {
            var vehicle = new VehicleAgent();
            var transform = new Transform2D();
            transform.Position = initialPosition;
            transform.Velocity = new Vector2(0, 1f);
            transform.Acceleration = new Vector2(0, 0);
            vehicle.Transform = transform;
            vehicle.Radius = 6f;
            vehicle.MaxSpeed = 4f;
            vehicle.MaxForce = 0.1f;
            return vehicle;
        }

        // A method that calculates a steering force towards a target
        // STEER = DESIRED MINUS VELOCITY
        public static VehicleAgent Seek(VehicleAgent vehicle, Vector2 target)
        {
            var desired = vehicle.Transform.Position.GetDirection(target, vehicle.MaxSpeed);
            // Steering = Desired minus velocity
            var steer = desired.Seek(vehicle.Transform.Velocity);
            vehicle.Transform.Acceleration += steer;
            return vehicle;
        }

        public static VehicleAgent Arrive(VehicleAgent vehicle, Vector2 target)
        {
            var desired = target - vehicle.Transform.Position;  // A vector pointing from the position to the target
            float d = desired.Length();            
            if (d < 100)
            {                
                float m = MathHelper.Clamp(d, 0f,vehicle.MaxSpeed);
                desired = desired.ToLength(m);
            }
            else
            {
                desired = desired.ToLength(vehicle.MaxSpeed);
            }

            // Steering = Desired minus Velocity
            var steer = desired - vehicle.Transform.Velocity;
            steer = steer.Limit(vehicle.MaxForce);  // Limit to maximum steering force
            vehicle.Transform = vehicle.Transform.ApplyForce(steer);
            return vehicle;
        }
    }
}
