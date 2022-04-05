using Lib;
using Lib.Components;
using Lib.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VectorsMath;

namespace Playground.Games.AutonomousAgents
{   
    //TODO: Refactor VehicleGame to use a single game, editing the state and logic through UI components.
    //TODO: the object movement is too robotic, it needs to be more smooth
    public class VehicleStayingWithinWallsGame : ICustomGame
    {
        readonly SpriteBatch _sb;
        readonly ContentManager _content;
        readonly GraphicsDevice _gd;
        VehicleAgent _vehicle = VehicleAgent.Create(Globals.CenterScreen);        
        Texture2D _vehicleTex;        
        float d = -25f;
        public VehicleStayingWithinWallsGame(
            GraphicsDevice gd,
            ContentManager content,
            SpriteBatch sb
            )
        {
            _gd = gd;
            _sb = sb;
            _content = content;
        }
        public void LoadContent()
        {            
            _vehicleTex = _content.Load<Texture2D>("triangle");
            //TODO:
        }
        public void Initialize()
        {
            //TODO:
        }
        VehicleAgent Boundaries(VehicleAgent vehicle)
        {
            var transform = vehicle.Transform;
            var position = transform.Position;
            var velocity = transform.Velocity;
            var maxspeed = vehicle.MaxSpeed;
            var width = Globals.ScreenSize.Width / 2f;
            var height = Globals.ScreenSize.Height / 2f;
            Vector2 desired = Vector2.Zero;

            if (position.X < d)
            {
                desired = new Vector2(maxspeed, velocity.Y);
            }
            else if (position.X > width - d)
            {
                desired = new Vector2(-maxspeed, velocity.Y);
            }

            if (position.Y < d)
            {
                desired = new Vector2(velocity.X, maxspeed);
            }
            else if (position.Y > height - d)
            {
                desired = new Vector2(velocity.X, -maxspeed);
            }

            if (desired == Vector2.Zero)
                return vehicle;
            
            desired.Normalize();
            desired *= maxspeed;
            var steer = desired - velocity;
            steer = steer.Limit(vehicle.MaxForce);
            transform = transform.ApplyForce(steer);
            vehicle.Transform = transform;
            return vehicle;
        }
        public void Update(GameTime gameTime)
        {
            _vehicle = Boundaries(_vehicle);            
            //seek            
            //Update            
            var transform = _vehicle.Transform;
            transform = transform.Accelerate();            
            transform.Velocity = transform.Velocity.Limit(_vehicle.MaxSpeed);
            transform = transform.Move();
            //transform = transform.Move();
            transform.Acceleration *= 0f;
            _vehicle.Transform = transform;
            // Apply boundary logic
        }
        public void Draw(GameTime gameTime)
        {
            //TODO: Draw the bondaries
            var angle = _vehicle.Transform.Velocity.GetHeadingDirection() + MathHelper.ToRadians(90);
            var center = new Vector2(_vehicleTex.Width / 2, _vehicleTex.Height / 2);
            _sb.Begin();            
            _sb.Draw(_vehicleTex, _vehicle.Transform.Position, null, Color.Black, angle, center, 0.5f, SpriteEffects.None, 1f);
            _sb.End();
        }
    }
}