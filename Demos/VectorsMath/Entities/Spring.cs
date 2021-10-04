using System;
using Lib;
using Lib.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Playground.Entities
{
    public record Spring
    {        
        public Spring(Texture2D texture,Vector2 anchor,float restLength)
        {
            _texture = texture;
            Anchor = anchor;            
            RestLength = restLength;            
        }
        public Vector2 Anchor;        
        public float RestLength;
        public float K = 0.1f;        
        readonly Texture2D _texture;
        public virtual void Connect(Mover mover)
        {
            var moverPos = mover.transform.Position;
            var force = - moverPos - Anchor;
            // var currentLength = Vector2.DistanceSquared(BallMover.position,Anchor);
            var currentLength = MathF.Sqrt(force.Length());
            float x = RestLength - currentLength;
            force.Normalize();
            force *= -1 * K * x;
            mover.ApplyForce(force);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,Anchor,Color.Black);
        }
    }
}