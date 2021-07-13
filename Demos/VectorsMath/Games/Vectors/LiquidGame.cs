using System.Linq;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VectorsMath
{
    public class LiquidGame : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        private readonly GraphicsDevice _graphicsDevice;
        private Mover _mover;        
        private RenderTarget2D _whiteHalf;        
        private RenderTarget2D _blackHalf;
        private Texture2D _pixel;        
        private readonly (int Width,int Height) _screenSize = (0,0);
        private Vector2 _screenCenter;
        private Vector2 _gravity = new Vector2(0.0f,0.1f);
        public LiquidGame(SpriteBatch spriteBatch,
        ContentManager content,
        GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _graphicsDevice = graphicsDevice;
            _screenSize = (graphicsDevice.PresentationParameters.BackBufferWidth,graphicsDevice.PresentationParameters.BackBufferHeight);
        }        

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            _screenCenter = new Vector2(0,_screenSize.Height / 2);
            _mover = new Mover(_content.Load<Texture2D>("ball"),new Vector2(_screenSize.Width / 2,0),0.5f);            
            _whiteHalf = new RenderTarget2D(_graphicsDevice,
            _screenSize.Width,
            _screenSize.Height / 2);            
            _blackHalf = new RenderTarget2D(_graphicsDevice,
            _screenSize.Width,
            _screenSize.Height / 2,
            false,
            SurfaceFormat.Color,
            DepthFormat.Depth24,
            0,
            RenderTargetUsage.DiscardContents);
            _pixel = new Texture2D(_graphicsDevice,1,1);
            _pixel.SetData(new Color[]{Color.White});
        }        
        public void Update(GameTime gameTime)
        {
            // var ballRec = new Rectangle(_mover.position.X,_mover.position.Y,_mover.)
            _mover.ApplyForce(_gravity);
            if(_mover.position.Y >= _screenCenter.Y){
                var dragForce = new Vector2(_mover.velocity.X,_mover.velocity.Y);
                dragForce.Normalize();
                var coefficient = -0.03f;
                var speed = _mover.velocity.LengthSquared();
                dragForce *= speed * coefficient;
                _mover.ApplyForce(dragForce);
            }
            _mover.Update();
            _mover.Edges(_screenSize.Width,_screenSize.Height);
        }
        public void Draw(GameTime gameTime)
        {
            _graphicsDevice.SetRenderTarget(_whiteHalf);
            _graphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            _spriteBatch.Draw(_pixel,_whiteHalf.Bounds,Color.White);            
            _spriteBatch.End();
            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.SetRenderTarget(_blackHalf);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            _spriteBatch.Draw(_pixel,_blackHalf.Bounds,Color.Black);
            _spriteBatch.End();
            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.Clear(Color.Blue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_whiteHalf,Vector2.Zero, Color.White);
            _spriteBatch.Draw(_blackHalf,_screenCenter, Color.Black);
            _mover.Draw(_spriteBatch);
            
            _spriteBatch.End();
        }
    }
}