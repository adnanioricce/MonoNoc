using System.Linq;
using Lib;
using Lib.Components;
using Lib.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VectorsMath
{
    public class CannonGame : ICustomGame
    {
        readonly SpriteBatch _spriteBatch;
        readonly ContentManager _content;
        Mover _ball;
        KeyboardState lastState;
        InputHelper inputHelper = new InputHelper();
        public CannonGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
        }
        public void Initialize()
        {

        }
        public void LoadContent()
        {
            _ball = new Mover(_content.Load<Texture2D>("ball"),Globals.CenterScreen);
            var transform = _ball.transform;
            transform.Position.X = Globals.CenterScreen.X / 2;
            transform.Position.Y = Globals.ScreenSize.Height - 32f;
        }
        public void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
            if(inputHelper.IsNewKeyPress(currentState,lastState,Keys.Space)){
                _ball.transform.ApplyForce(() => new Vector2(20,-25));
            }
            lastState = currentState;
            _ball.ApplyGravity();
            _ball.Update();
            _ball.Edges(Globals.ScreenSizeToVector);
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _ball.Draw(_spriteBatch);
            _spriteBatch.End();
        }        
    }
}