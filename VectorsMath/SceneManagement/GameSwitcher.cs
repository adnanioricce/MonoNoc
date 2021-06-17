using System;
using Microsoft.Xna.Framework;

namespace VectorsMath
{
    public class GameSwitcher
    {
        private ICustomGame _currentGame;        
        public GameSwitcher(ICustomGame initialGame)
        {
            _currentGame = initialGame;
        }
        public void Initialize()
        {
            _currentGame.Initialize();
        }
        public void LoadContent()
        {
            _currentGame.LoadContent();
        }
        public void Update(GameTime gameTime)
        {
            _currentGame.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            _currentGame.Draw(gameTime);
        }
        public void SetCurrentGame(ICustomGame game)
        {
            if(game is null)
                throw new ArgumentNullException("a null reference was passed instead of a ICustomGame reference");
            _currentGame = game;
        }
    }
}