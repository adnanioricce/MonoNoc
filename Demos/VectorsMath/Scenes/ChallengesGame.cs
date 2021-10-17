using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;
using Playground.Games.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorsMath;

namespace Playground.Scenes
{
    public class ChallengesGame : Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;        
        readonly List<ICustomGame> Games = new List<ICustomGame>();
        GameSwitcher _gameSwitcher;
        Desktop _desktop;
        public ChallengesGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;       
        }
        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferHeight = Globals.ScreenSize.Height;
            _graphics.PreferredBackBufferWidth = Globals.ScreenSize.Width;
            _graphics.ApplyChanges();
            Games.Add(new FractalTreesGame(_spriteBatch,GraphicsDevice));
            Games.ForEach(game => game.LoadContent());
            _gameSwitcher = new GameSwitcher(Games.LastOrDefault());            
            MyraEnvironment.Game = this;
            _desktop = UIHelper.BuildMenu(Games,_gameSwitcher);            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);                        
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();            
            if(_gameSwitcher is null){
                return;
            }                        
            _gameSwitcher.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);            
            if(_gameSwitcher != null){
                _gameSwitcher.Draw(gameTime);   
            }
            _desktop.Render();
            base.Draw(gameTime);
        }        
    }
}
