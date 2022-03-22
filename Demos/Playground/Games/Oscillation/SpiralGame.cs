using System;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VectorsMath.Games.Oscillation
{
    public class SpiralGame : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        private readonly GraphicsDevice _graphicsDevice;
        private Mover _ball;
        float radius = 0f;
        float radiusVelocity = 0.1f;
        float angle = 0f;
        Texture2D _pointTexture;
        RenderTarget2D _trailTarget;
        private Vector2 _center = Globals.ScreenSizeToVector / 2f;
        private Random rng = new Random();
        public SpiralGame(GraphicsDevice graphicsDevice,SpriteBatch spriteBatch,ContentManager content)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;
            _content = content;
        }

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            var ballTexture = _content.Load<Texture2D>("ball");
            _ball = new Mover(ballTexture,Globals.ScreenSizeToVector / 2f);
            _ball.angularAcceleration = 1f;            
            _ball.topSpeed = new Vector2(5f,5f);
            _pointTexture = new Texture2D(_graphicsDevice,1,1);
            _pointTexture.SetData(new [] {Color.White});
            _trailTarget = new RenderTarget2D(_graphicsDevice,
                Globals.ScreenSize.Width,Globals.ScreenSize.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                0,
                RenderTargetUsage.PreserveContents);
        }

        public void Update(GameTime gameTime)
        {
            if(angle > 360){
                angle = 0f;
            }            
            angle += 1f;
            if(radius > 460f){
                radiusVelocity *= -1;
            }
            if(radius < 0f){
                radiusVelocity *= -1;
            }
            radius += radiusVelocity;
            var transform = _ball.transform;
            transform.Position.X = Globals.CenterScreen.X + MathF.Cos(MathHelper.ToRadians(angle)) * radius;
            transform.Position.Y = Globals.CenterScreen.Y + MathF.Sin(MathHelper.ToRadians(angle)) * radius;            
        }
        public void Draw(GameTime gameTime)
        {            
            var time = (float)(gameTime.ElapsedGameTime.TotalSeconds * rng.NextDouble()) / 10f;
            var colorVec = new Color(new Vector3((float)rng.NextDouble(),rng.Next(1,255) * time,1 * (float)rng.NextDouble()));
            Console.WriteLine(colorVec);            
            _graphicsDevice.SetRenderTarget(_trailTarget);
            _spriteBatch.Begin();
            var transform = _ball.transform;
            _spriteBatch.Draw(_pointTexture,transform.Position + (_ball.rectangle.Size.ToVector2() / 2f),null,colorVec,1f,Vector2.Zero,4f,SpriteEffects.None,1f);
            _spriteBatch.End();
            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.Clear(Color.CornflowerBlue);            
            _spriteBatch.Begin();
            _ball.Draw(_spriteBatch);            
            _spriteBatch.Draw(_trailTarget,Vector2.Zero,Color.White);
            _spriteBatch.End();
            
        }
        private Vector2 RotateAboutOrigin(Vector2 point,Vector2 origin,float rotation)
        {
            return Vector2.Transform(point - origin,Matrix.CreateRotationZ(rotation)) + origin;
        }
    }
}