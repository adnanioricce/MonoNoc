using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VectorsMath.Games.Oscillation
{
    public class SpaceshipGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;
        Mover spaceship;
        public SpaceshipGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }        

        public void Initialize()
        {            
        }

        public void LoadContent()
        {            
            spaceship = new Mover(_content.Load<Texture2D>("ball"),Globals.CenterScreen);
        }

        public void Update(GameTime gameTime)
        {            
            spaceship.Update();
            spaceship.Edges(Globals.ScreenSizeToVector);
        }
        public void Draw(GameTime gameTime)
        {            
            _spriteBatch.Begin();
            spaceship.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}