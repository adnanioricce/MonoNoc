using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Components
{
    //TODO: asks yourlself: this should be a class or a record?
    public class RepellerObject2D
    {        
        private Texture2D texture;
        public float Strength;
        public float Radius;
        public Vector2 Position;
        public RepellerObject2D()
        {
            Strength = 5f;
            Radius = 10f;
            Position = Vector2.Zero;
        }        
        public RepellerObject2D(Texture2D _texture,Vector2 position, float radius,float strength)
        {
            texture = _texture;
            Position = position;
            Radius = radius;
            Strength = strength;
        }

        public Vector2 Repel(Vector2 reppeledPosition)
        {
            var direction = Position - reppeledPosition;
            float scale = MathHelper.Clamp(direction.Length(),0f,100f);            
            direction.Normalize();
            float force = -1 * Strength / (scale * scale);
            direction *= force;
            return direction;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,Position,Color.White);
        }
    }
}
