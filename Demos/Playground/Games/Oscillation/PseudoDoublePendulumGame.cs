using Lib;
using Lib.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Playground.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using VectorsMath;

namespace Playground.Games.Oscillation
{
    public class PseudoDoublePendulumGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;
        readonly GraphicsDevice _graphicsDevice;
        readonly List<Pendulum> pendulums = new List<Pendulum>();
        Texture2D _pixel;
        RenderTarget2D _renderTarget;
        Queue<Vector2> FirstPendumlumTrail = new Queue<Vector2>();
        public PseudoDoublePendulumGame(SpriteBatch spriteBatch,ContentManager content,GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _graphicsDevice = graphicsDevice;
        }
        public void LoadContent()
        {
            var screenSize = Globals.ScreenSizeToVector;
            var firstOrigin = new Vector2(screenSize.X / 2,0f);
            var firstLocation = screenSize / 2f;            
            var firstPendulum = new Pendulum(_content.Load<Texture2D>("ball"),firstOrigin,firstLocation);
            firstPendulum.Angle = MathF.PI / 4f;
            firstPendulum.Radius = 250;
            var secondLocation = new Vector2(firstLocation.X * 2f,firstLocation.Y + (firstLocation.Y * 0.5f));
            var secondPendulum = new Pendulum(_content.Load<Texture2D>("ball"),firstLocation,secondLocation);
            secondPendulum.Angle = MathF.PI / 2f;
            secondPendulum.Radius = 250;
            pendulums.Add(firstPendulum);
            pendulums.Add(secondPendulum);
            _renderTarget = new RenderTarget2D(_graphicsDevice, 
                width: (int)screenSize.X, 
                height: (int)screenSize.Y, 
                mipMap: false,
                preferredFormat: SurfaceFormat.Color,
                preferredDepthFormat: DepthFormat.Depth24,
                preferredMultiSampleCount: 0,
                usage: RenderTargetUsage.PreserveContents);
            _pixel = new Texture2D(_graphicsDevice, 1, 1);
            _pixel.SetData(new Color[] { Color.White });
        }
        public void Initialize()
        {
            
        }        

        public void Update(GameTime gameTime)
        {            
            for(int i = 1;i < pendulums.Count;++i) {
                
                var first = pendulums[i - 1];
                first.Update(gameTime);                
                var second = pendulums[i]; 
                second.Origin = first.Location;
                FirstPendumlumTrail.Enqueue(second.Location);
                second.Update(gameTime);
                FirstPendumlumTrail.Enqueue(second.Location);
            }
        }

        public void Draw(GameTime gameTime)
        {
            _graphicsDevice.SetRenderTarget(_renderTarget);
            //_graphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);                            
            var previousPos = FirstPendumlumTrail.Dequeue();            
            while (FirstPendumlumTrail.Any())
            {
                var pos = FirstPendumlumTrail.Dequeue();
                var x = MathHelper.Clamp((previousPos.X + pos.X) * 0.001f,0f,1f);
                var y = MathHelper.Clamp((previousPos.Y + pos.Y) * 0.001f, 0f, 1f);
                _spriteBatch.DrawLine(previousPos, pos, new Color(new Vector3(x,y,MathF.Sin(previousPos.Y + previousPos.X))));
                previousPos = pos;
            }                        
            _spriteBatch.End();
            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, Vector2.Zero, _renderTarget.Bounds, Color.White);
            pendulums.ForEach(pendulum => pendulum.Draw(_spriteBatch));            
            _spriteBatch.End();
            
        }
    }
}
