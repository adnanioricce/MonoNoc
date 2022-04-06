using Lib;
using Lib.Components;
using Lib.Entities;
using Lib.Math;
using Lib.Math.Noises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using VectorsMath;

namespace Playground.Games.AutonomousAgents
{
    
    public class FlowFieldGame : ICustomGame
    {
        readonly SpriteBatch _sb;
        readonly ContentManager _content;
        readonly GraphicsDevice _gd;
        readonly VehicleAgent[] _vehicles = new VehicleAgent[120];
        Vector2 center = Globals.CenterScreen;
        FlowField _flowField;
        Texture2D _triangleTex;
        Rectangle screenTarget;
        bool isDebug = true;
        //TODO: draw flowfield on rendertarget one time, and reuse it
        RenderTarget2D _renderTarget;
        public FlowFieldGame(
            GraphicsDevice gd,
            ContentManager content,
            SpriteBatch sb
            )
        {
            _gd = gd;
            _sb = sb;
            _content = content;
        }
        Vector2 Borders(Vector2 v,float r)
        {
            if (v.X < -r)
            {
                v.X = screenTarget.Width + r;
            }
            if (v.Y < -r)
            {
                v.Y = screenTarget.Height + r;
            }
            if (v.X > screenTarget.Width + r)
            {
                v.X = -r;
            }
            if (v.Y > screenTarget.Height+ r)
            {
                v.Y = -r;
            }
            return v;
        }
        public void LoadContent()
        {
            for (int i = 0; i < _vehicles.Length; i++)
            {
                _vehicles[i] = new VehicleAgent
                {
                    Transform = new Transform2D
                    {
                        Position = new Vector2(RNG.GetRandomFloat(0f, (Globals.ScreenSize.Width)), RNG.GetRandomFloat(0f, (Globals.ScreenSize.Height))),
                    },
                    MaxSpeed = RNG.GetRandomFloat(2f, 5f),
                    MaxForce = RNG.GetRandomFloat(0.1f, 0.5f)
                };
            }
            _triangleTex = _content.Load<Texture2D>("triangle");
            _flowField = new FlowField(Globals.ScreenSizeToVector, 20);
            
        }
        public void Initialize()
        {
            var center = Globals.CenterScreen;
            screenTarget = new Rectangle((int)center.X, (int)center.Y,Globals.ScreenSize.Width,Globals.ScreenSize.Height);            
        }
        public void Update(GameTime gameTime)
        {
            for(int i = 0;i < _vehicles.Length; ++i)
            {
                var vehicle = _vehicles[i];
                vehicle = vehicle.Follow(_flowField);
                Transform2D transform = vehicle.Transform;
                transform = transform.Accelerate();
                transform.Velocity = transform.Velocity.Limit(vehicle.MaxSpeed);
                transform = transform.Move();
                transform.Acceleration *= 0f;
                //transform = transform.Edges(Globals.ScreenSize.Width, Globals.ScreenSize.Height, screenTarget);
                transform.Position = Borders(transform.Position, vehicle.Radius);
                vehicle.Transform = transform;
                _vehicles[i] = vehicle;
            }

        }
        public void Draw(GameTime gameTime)
        {
            _sb.Begin();
            if (isDebug) {
                _flowField.Draw(_sb, _triangleTex, Globals.CenterScreen);
            }
            for (int i = 0; i < _vehicles.Length; i++)
            {
                var vehicle = _vehicles[i];
                var angle = vehicle.Transform.Position.GetHeadingDirection();
                _sb.Draw(_triangleTex, vehicle.Transform.Position, null, Color.Black, angle, center, 0.5f, SpriteEffects.None, 1f);
            }
            _sb.End();
        }
    }
}
