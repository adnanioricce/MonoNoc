using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetManagementBase;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;
using VectorsMath.Games.Oscillation;

namespace VectorsMath
{
    public class VectorsGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;        
        private readonly List<ICustomGame> Games = new List<ICustomGame>();
        private GameSwitcher _gameSwitcher;        
        private Desktop _desktop;
        public VectorsGame()
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
            Games.Add(new LineGame(GraphicsDevice,_spriteBatch,Globals.ScreenSize));
            Games.Add(new AccelerationGame(_spriteBatch,Content));            
            Games.Add(new FrictionGame(_spriteBatch,Content));
            Games.Add(new DragGame(_spriteBatch,Content));
            Games.Add(new LiquidGame(_spriteBatch,Content,GraphicsDevice));
            Games.Add(new GravitationGame(_spriteBatch,Content));            
            Games.ForEach(game => game.LoadContent());            
            _desktop = new Desktop();
            _gameSwitcher = new GameSwitcher(Games[0]);            
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
