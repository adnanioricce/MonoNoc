using System;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath
{
    public class LineGame : ICustomGame
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;
        private Texture2D _lineTex;
        private Vector2 _linePos;
        private Vector2 _mousePos;
        private readonly float _scale = 0.5f;
        private readonly int _width;
        private readonly int _height;
        public LineGame(GraphicsDevice graphicsDevice,SpriteBatch spriteBatch,(int Width,int Height) screenSize)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;
            _width = screenSize.Width;
            _height = screenSize.Height;
        }
        public void Initialize(){}
        public void LoadContent()
        {
            _lineTex = new Texture2D(_graphicsDevice,1,1);
            _lineTex.SetData(new Color[] { Color.Black });
            _linePos = new Vector2(_width / 2,_height / 2);
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            _mousePos.X = mouseState.X;
            _mousePos.Y = mouseState.Y;
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawLine(_spriteBatch,_linePos,_mousePos,Color.Black,_scale);
            _spriteBatch.End();   
        }
        protected void DrawLine(SpriteBatch spriteBatch,Vector2 pos1,Vector2 pos2,Color color,float scale = 1f,float thickness = 1f)
        {
            var distance = Vector2.Distance(pos1,pos2);
            var angle = MathF.Atan2(pos2.Y - pos1.Y,pos2.X - pos1.X);
            DrawLine(spriteBatch,pos1,distance * scale,angle,color,thickness);
        }
        protected void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(_lineTex, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
}