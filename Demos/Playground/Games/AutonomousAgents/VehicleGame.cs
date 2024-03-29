﻿using Lib;
using Lib.Components;
using Lib.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VectorsMath;

namespace Playground.Games.AutonomousAgents
{
    
    public class VehicleGame : ICustomGame
    {
        readonly SpriteBatch _sb;
        readonly ContentManager _content;
        VehicleAgent _vehicle = VehicleAgent.Create(Globals.CenterScreen);
        Texture2D _circle;
        Texture2D _vehicleTex;
        Vector2 _mousePosition;
        
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
            _vehicle = VehicleAgent.Seek(_vehicle,_mousePosition);
            //Update            
            var transform = _vehicle.Transform;
            transform = transform.Accelerate();
            transform = transform.Move();
            transform.Acceleration *= 0f;
            _vehicle.Transform = transform;
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
