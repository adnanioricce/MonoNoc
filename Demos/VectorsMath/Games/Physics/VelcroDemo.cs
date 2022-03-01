using Genbox.VelcroPhysics.Collision.Filtering;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Factories;
using Genbox.VelcroPhysics.Utilities;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Games.Physics
{
    public class VelcroDemo : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        private readonly Func<MouseState> _getMouseState;
        private readonly Vector2 _screenSize;
        Texture2D ballTexture;
        Texture2D rectangleTexture;
        List<Body> balls = new List<Body>();        
        World _world = new World(new Vector2(0,19.82f));        
        private Matrix _view = Matrix.Identity;
        private Body _circleBody;
        private Body _groundBody;

        private Texture2D _circleSprite;
        private Texture2D _groundSprite;        

        private Vector2 _cameraPosition;
        private Vector2 _screenCenter;
        private Vector2 _groundOrigin;
        private Vector2 _circleOrigin;
        //private Vector2 _cameraPosition = Vector2.Zero;
        public VelcroDemo(
            SpriteBatch spriteBatch, 
            ContentManager content,
            Vector2 screenSize,
            Func<MouseState> getMouseState)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _getMouseState = getMouseState;
            _screenSize = screenSize;
            _screenCenter = screenSize / 2f;
        }
        public void LoadContent()
        {
            ballTexture = _content.Load<Texture2D>("ball");
            rectangleTexture = _content.Load<Texture2D>("blue_rectangle");
        }
        public void Initialize()
        {
            // Velcro Physics expects objects to be scaled to MKS (meters, kilos, seconds)
            // 1 meters equals 64 pixels here
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64f);
            // Initialize camera controls
            _view = Matrix.Identity;
            _cameraPosition = Vector2.Zero;

            // Load sprites
            _circleSprite = _content.Load<Texture2D>("CircleSprite"); //  96px x 96px => 1.5m x 1.5m
            _groundSprite = _content.Load<Texture2D>("GroundSprite"); // 512px x 64px =>   8m x 1m

            /* We need XNA to draw the ground and circle at the center of the shapes */
            _groundOrigin = new Vector2(_groundSprite.Width / 2f, _groundSprite.Height / 2f);
            _circleOrigin = new Vector2(_circleSprite.Width / 2f, _circleSprite.Height / 2f);

                        

            /* Circle */
            // Convert screen center from pixels to meters
            Vector2 circlePosition = ConvertUnits.ToSimUnits(_screenCenter) + new Vector2(0, -1.5f);
            _circleBody = CreateCircle(circlePosition);
            balls.Add(_circleBody);
            /* Ground */
            Vector2 groundPosition = ConvertUnits.ToSimUnits(_screenCenter) + new Vector2(0, 8.8f);

            // Create the ground fixture
            _groundBody = BodyFactory.CreateRectangle(_world, ConvertUnits.ToSimUnits(1000f), ConvertUnits.ToSimUnits(64f), 1f, groundPosition);
            _groundBody.BodyType = BodyType.Static;
            _groundBody.Restitution = 0.3f;
            _groundBody.Friction = 0.5f;
        }

        private Body CreateCircle(Vector2 circlePosition)
        {

            // Create the circle fixture
            var circleBody = BodyFactory.CreateCircle(_world, ConvertUnits.ToSimUnits(96 / 2f), 1f, circlePosition, BodyType.Dynamic);

            // Give it some bounce and friction
            circleBody.Restitution = 0.3f;
            circleBody.Friction = 0.5f;
            return circleBody;
        }

        void AddBall(Func<MouseState> getMouseState)
        {
            var mouseState = getMouseState();
            var ball = CreateCircle(ConvertUnits.ToSimUnits(mouseState.Position.ToVector2()));
            balls.Add(ball);            
        }
         
        public void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed) {
                AddBall(_getMouseState);
            }
            VelcroPhysicsVersion(gameTime);
        }

        private void VelcroPhysicsVersion(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _view);
            balls.ForEach(b => _spriteBatch.Draw(_circleSprite, ConvertUnits.ToDisplayUnits(b.Position), null, Color.White, b.Rotation, _circleOrigin, 1f, SpriteEffects.None, 0f));            
            _spriteBatch.Draw(_groundSprite, ConvertUnits.ToDisplayUnits(_groundBody.Position), null, Color.White, 0f, _groundOrigin, 2f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }                        
    }
}
