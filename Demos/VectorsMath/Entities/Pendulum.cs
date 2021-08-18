using Lib.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Playground.Entities
{
    public class Pendulum
    {
        public Vector2 Origin;
        public Vector2 Location;
        public float Angle;
        public float AngularVelocity;
        public float AngularAcceleration;
        public float Radius;
        public const float gravity = 1f;
        readonly Texture2D _texture;
        readonly Vector2 textureOffset;
        public Pendulum(Texture2D texture,Vector2 origin, Vector2 location)
        {
            _texture = texture;
            Origin = origin;
            Location = location;
            textureOffset = (_texture.Bounds.Size.ToVector2() / 2);
        }
        public void Update(GameTime gameTime)
        {
            AngularAcceleration = (-1 * gravity * MathF.Sin(Angle)) / Radius;
            AngularVelocity += AngularAcceleration;
            Angle += AngularVelocity;
            Location.X = Radius * MathF.Sin(Angle);
            Location.Y = Radius * MathF.Cos(Angle);
            
            Location += Origin;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(Origin + textureOffset,Location + textureOffset,Color.Black);
            spriteBatch.Draw(_texture,Origin,Color.White);                        
            spriteBatch.Draw(_texture,Location,Color.White);
        }
    }
}
