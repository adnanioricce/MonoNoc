using Lib;
using Lib.Components;
using Lib.Entities.Particles;
using Lib.Factories;
using Lib.Input;
using Lib.Math.Noises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using VectorsMath;

namespace Playground.Games.Particles
{
    public class SmokeParticlesGame : ICustomGame
    {
        private ParticleSystem particleSystem;
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly ContentManager _content;
        KeyboardState lastState;
        InputHelper inputHelper = new InputHelper();
        private Func<Vector2> _force = () => Vector2.Zero;   
        private RepellerObject2D _repeller;
        private Texture2D particleTexture;
        
        public SmokeParticlesGame(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            _content = content;
        }
        private void UpdateParticleCallback(Particle part,GameTime time)
        {
            var location = part.Location;
            location.Accelerate();
            location.Move();
            location.Acceleration *= 0f;
            part.Location = location;
            part.Lifespan -= 0.8f;
            var partPos = location.Position;
            part.Color = 
                new Color(
                    Perlin.Noise(partPos.X,partPos.Y) / 4,
                    Perlin.Noise(partPos.Y,partPos.X) / 255,
                    (partPos.X + partPos.Y) 
                );            
        }
        Particle ParticleFactoryWithTexture(Vector2 origin,Func<Texture2D> textureFactory)
        {
            var transform = new Transform2D();
            transform.Position = origin;
            transform.Acceleration = new Vector2(Globals.GetRandomFloat(-.05f, .05f), Globals.GetRandomFloat(-.05f, .05f));
            transform.Velocity = new Vector2(Globals.GetRandomFloat(-1.0f, 1.0f), Globals.GetRandomFloat(-2, 2));
            var physics = new PhysicForce2D();            
            physics.Force = _force();
            var particle = new Particle(textureFactory(),transform,physics,UpdateParticleCallback);
            return particle;
        }
        Particle ParticleFactory(ParticleSystem system) {
            var transform = new Transform2D();
            transform.Position = system.Origin;
            var sampleAcc = Globals.GetRandomFloat(-15, 0) / 100f;
            sampleAcc = Perlin.Noise(sampleAcc,sampleAcc);
            transform.Acceleration = new Vector2(
                sampleAcc,
                sampleAcc);
            var sampleVel = Globals.GetRandomFloat(0, 4) / 100f;
            sampleVel = Perlin.Noise(sampleVel,sampleVel);
            transform.Velocity = new Vector2(
                sampleVel,
                sampleVel);
            var physics = new PhysicForce2D();            
            physics.Force = _force();
            //var particle = new Particle(TextureFactory.CreateSolidTexture(_graphicsDevice, Color.WhiteSmoke, 16, 16), transform, physics);
            var tex = TextureFactory.CreateSolidCircleTexture(_graphicsDevice,16f,Color.CornflowerBlue);
            var particle = new Particle(tex, transform, physics,UpdateParticleCallback);
            return particle;
        }
        public void LoadContent()
        {           
            //particleTexture = T
            var radius = 12f;
            _repeller = new RepellerObject2D(
                TextureFactory.CreateSolidCircleTexture(_graphicsDevice,radius,
                new Color(Perlin.Noise(200f,400f) / 255f,Perlin.Noise(100f,50f) / 255f,Perlin.Noise(12f,24f) / 255f)),
                new Vector2(600,450),
                radius,
                50f);
            particleSystem = new ParticleSystem(1,new Vector2(600,310),this.ParticleFactory);            
        }

        public void Initialize()
        {
            
        }        
        
        public void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
            if(inputHelper.IsNewKeyPress(currentState,lastState,Keys.Space)){
                _force = () =>
                {
                    return new Vector2(Globals.GetRandomFloat(-0.0005f,0.0005f),Globals.GetRandomFloat(-0.0005f,0.0005f));
                };
            }
            particleSystem.ApplyForce();
            particleSystem.ApplyReppeler(_repeller);
            particleSystem.Update(gameTime);
            for (int i = 0; i < 5; i++)
            {
                particleSystem.AddParticle(() => { 
                    var particle = ParticleFactory(particleSystem);
                
                    return particle;
                });
            }                        
        }

        public void Draw(GameTime gameTime)
        {            
            _graphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Additive);
            _repeller.Draw(_spriteBatch);
            particleSystem.Draw(_spriteBatch,gameTime);
            _spriteBatch.End();
        }        

    }
}
