using Lib.Components;
using Lib.Extensions;
using Lib.Math.Noises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lib.Entities
{
    public class FlowField
    {
        // A flow field is a two dimensional array of PVectors
        readonly Vector2[,] _field;
        Vector2 _size; // Columns and Rows
        int _resolution; // How large is each "cell" of the flow field

        public FlowField(Vector2 screenSize, int r)
        {
            var width = screenSize.X;
            var height = screenSize.Y;
            _resolution = r;
            // Determine the number of columns and rows based on sketch's width and height
            _size.Y = width / _resolution;
            _size.X = height / _resolution;
            _field = new Vector2[(int)_size.X, (int)_size.Y];
            Init();
        }

        void Init()
        {
            // Reseed noise so we get a new flow field every time
            //noiseSeed((int)random(10000));
            float xoff = 0;
            for (int i = 0; i < _size.Y; i++)
            {
                float yoff = 0;
                for (int j = 0; j < _size.X; j++)
                {
                    //float theta = map(noise(xoff, yoff), 0, 1, 0, MathF.PI * 2);
                    float theta = MathHelper.Clamp(Perlin.Noise(xoff, yoff), 0, MathF.PI * 2);
                    // Polar to cartesian coordinate transformation to get x and y components of the vector
                    _field[i, j].X = MathF.Cos(theta);
                    _field[i, j].Y = MathF.Sin(theta);
                    yoff += 0.1f;
                }
                xoff += 0.1f;
            }
        }        

        // Renders a vector object 'v' as an arrow and a position 'x,y'
        void DrawVector(SpriteBatch sb,Texture2D triangleTex,Vector2 v, Vector2 center,float scayl)
        {
            float arrowsize = 4;
            var angle = v.GetHeadingDirection();
            float len = v.Length() * scayl;
            //sb.Draw(triangleTex, v, null, Color.Black, angle, center, 0.5f, SpriteEffects.None, 1f);
            sb.DrawLine(v, arrowsize, angle, Color.Black);
        }

        // Draw every vector
        public void Draw(SpriteBatch sb,Texture2D triangleTex,Vector2 center)
        {
            for (int i = 0; i < _size.Y; i++)
            {
                for (int j = 0; j < _size.X; j++)
                {
                    //i* _resolution, j *_resolution
                    DrawVector(sb, triangleTex, _field[i, j],center, _resolution - 2);
                }
            }

        }
        public Vector2 Lookup(Vector2 lookup)
        {
            int column = (int)(MathHelper.Clamp(lookup.X / _resolution, 0, _size.Y - 1));
            int row = (int)(MathHelper.Clamp(lookup.Y / _resolution, 0, _size.X - 1));
            return _field[column, row];
        }


    }
}
