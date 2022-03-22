using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Dynamics.Joints;
using Genbox.VelcroPhysics.Factories;
using Genbox.VelcroPhysics.Utilities;
using Lib;
using Lib.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using VectorsMath;

namespace Playground.Games.Physics
{
    public class JointGame : ICustomGame
    {
        private readonly ContentManager _content;
        private readonly SpriteBatch _sb;
        
        private readonly Vector2[] _points = new Vector2[10];
        private readonly Body[] _bodies = new Body[10];        
        private readonly World _world = new World(new Vector2(0, 19.82f));
        private Texture2D _sprite;
        private Texture2D _rectangleTex;        
        public JointGame(ContentManager content, SpriteBatch spriteBatch)
        {
            _content = content;
            _sb = spriteBatch;
        }
        void CreateJoint(World world,Body[] bodies)
        {
            for (int i = 1; i < bodies.Length - 1; ++i)
            {
                
                var softDistance = new DistanceJoint(bodies[i - 1], _bodies[i], Vector2.Zero, Vector2.Zero);
                JointHelper.LinearStiffness(5f, 0.3f, softDistance.BodyA, softDistance.BodyB, out var stiffness, out var damping);
                softDistance.Damping = damping;
                softDistance.Stiffness = stiffness;
                world.AddJoint(softDistance);
            }            
        }
        public void Initialize()
        {
            // Velcro Physics expects objects to be scaled to MKS (meters, kilos, seconds)
            // 1 meters equals 64 pixels here
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64f);                 
            var step = 5;
            for (int i = 0; i < 10; ++i){
                _bodies[i] = BodyFactory.CreateCircle(_world, ConvertUnits.ToSimUnits(96 / 2f), 1f, ConvertUnits.ToSimUnits(new Vector2(((i + 1) * 15) * step,0f)), BodyType.Dynamic);
                // Give it some bounce and friction
                _bodies[i].Restitution = 0.3f;
                _bodies[i].Friction = 0.5f;
                _bodies[i].Mass = i == 0 || i == 9 ? 10f : _bodies[i].Mass;
                _bodies[i].BodyType = i == 0 || i == 9 ? BodyType.Static : BodyType.Dynamic;
            }
            CreateJoint(_world, _bodies);
        }

        public void LoadContent()
        {
            _sprite = _content.Load<Texture2D>("ball");
            _rectangleTex = _content.Load<Texture2D>("GroundSprite");            
        }

        public void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);
        }
        public void Draw(GameTime gameTime)
        {
            
            _sb.Begin();
            Vector2 spriteOrigin = ConvertUnits.ToSimUnits(new Vector2(_sprite.Width / 2f, _sprite.Height / 2f));
            for (int i = 0, j = 0; i < _points.Length;i++)
            {                                
                _sb.Draw(_sprite, ConvertUnits.ToDisplayUnits(_bodies[i].Position), Color.White);
                if ((j + 1) == _points.Length)
                    continue;
                
                _sb.DrawLine(ConvertUnits.ToDisplayUnits(_bodies[j].Position + spriteOrigin), ConvertUnits.ToDisplayUnits(_bodies[j + 1].Position + spriteOrigin), Color.Black,1,5);
                j++;                
            }            
            _sb.End();
        }
    }
}
