using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VectorsMath
{
    public class AnglesGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;
        readonly (int Width,int Height) _screenSize;
        Attractor _attractor;
        Mover _mover;
        
        public AnglesGame(SpriteBatch spriteBatch,ContentManager content,(int Width,int Height) screenSize)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _screenSize = screenSize;
        }        

        public void LoadContent()
        {
            var ballTexture = _content.Load<Texture2D>("ball");
            _mover = new Mover(ballTexture,new Vector2(_screenSize.Width,_screenSize.Height),1f);
            _attractor = new Attractor(ballTexture,new Vector2(_screenSize.Width,_screenSize.Height),1f);
        }
        public void Initialize()
        {
            
        }        

        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(GameTime gameTime)
        {            
        }
    }
}