using System;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Lib.Extensions;
namespace VectorsMath
{
    public class PolarCoordsGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;
        Vector2 origin = Vector2.Zero;
        Mover mover;
        float r = 150f;
        float a = 0f;
        public PolarCoordsGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }
        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            mover = new Mover(_content.Load<Texture2D>("ball"),new Vector2(Globals.ScreenSize.Width / 2,Globals.ScreenSize.Height / 2),1f);
            mover.angularAcceleration = 0.005f;
        }

        public void Update(GameTime gameTime)
        {
            var x = r * MathF.Cos(a);
            var y = r * MathF.Sin(a);      
            var transform = mover.transform;
            transform.Position.X = x;
            transform.Position.Y = y;
            mover.transform = transform;
            mover.Update();            
            mover.UpdateAngularVelocity();
            mover.Edges(Globals.ScreenSizeToVector);
            a = mover.angle;
            

        }
        public void Draw(GameTime gameTime)
        {            
            Matrix transform = Matrix.CreateScale(1f) * Matrix.CreateRotationZ(a * MathHelper.Pi) * Matrix.CreateTranslation(new Vector3(Globals.ScreenSize.Width / 2,Globals.ScreenSize.Height / 2,0f));                        
            _spriteBatch.Begin(transformMatrix:transform);
            var moverCenter = mover.rectangle.Size.ToVector2() / 2f;            
            var posOffseted = mover.transform.Position + moverCenter;
            _spriteBatch.DrawLine(posOffseted,origin,Color.Black);
            mover.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}