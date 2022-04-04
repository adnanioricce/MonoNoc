using Lib;
using Lib.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorsMath;

namespace Playground.Games.AutonomousAgents
{
    public struct Vehicle
    {
        public Transform2D Transform;
        public float Radius;
        public float MaxSpeed;
        public float MaxForce;
        public static Vehicle Create(Vector2 initialPosition){
            var vehicle = new Vehicle();
            var transform = new Transform2D();
            transform.Position = initialPosition;
            transform.Velocity = new Vector2(0, 1f);
            transform.Acceleration = new Vector2(0,0);
            vehicle.Transform = transform;
            vehicle.Radius = 6f;
            vehicle.MaxSpeed = 4f;
            vehicle.MaxForce = 0.1f;
            return vehicle;
        }                    
        
        // A method that calculates a steering force towards a target
        // STEER = DESIRED MINUS VELOCITY
        public static Vehicle Seek(Vehicle vehicle,Vector2 target)
        {
            var desired = vehicle.Transform.Position.GetDirection(target, vehicle.MaxSpeed);
            // Steering = Desired minus velocity
            var steer = desired.Seek(vehicle.Transform.Velocity);
            vehicle.Transform.Acceleration += steer;
            return vehicle;
        }
    }
    public class VehicleGame : ICustomGame
    {
        readonly SpriteBatch _sb;
        readonly ContentManager _content;
        Vehicle _vehicle = Vehicle.Create(Globals.CenterScreen);
        Texture2D _circle;
        Texture2D _vehicleTex;
        Vector2 _mousePosition = new Vector2();
        
        public VehicleGame(SpriteBatch sb,ContentManager content)
        {
            _sb = sb;
            _content = content;
        }      
        public void LoadContent()
        {
            _circle = _content.Load<Texture2D>("ball");
            _vehicleTex = _content.Load<Texture2D>("triangle");
        }
        public void Initialize()
        {

        }
        public void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();
            _mousePosition.X = state.X;
            _mousePosition.Y = state.Y;
            //seek
            _vehicle = Vehicle.Seek(_vehicle,_mousePosition);
            //Update            
            _vehicle.Transform.Accelerate();
            _vehicle.Transform.Move();
            _vehicle.Transform.Acceleration *= 0f;
        }       
        public void Draw(GameTime gameTime)
        {
            var angle = _vehicle.Transform.Velocity.GetHeadingDirection();
            var center = new Vector2(_vehicleTex.Width / 2,_vehicleTex.Height / 2);
            _sb.Begin();
            _sb.Draw(_circle,_mousePosition,Color.Black);
            _sb.Draw(_vehicleTex,_vehicle.Transform.Position,null,Color.Black,angle,center,0.5f,SpriteEffects.None,1f);
            _sb.End();
        }                        
    }
}
