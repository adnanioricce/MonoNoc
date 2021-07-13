using System.Linq;
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
            _ball.position.X = Globals.CenterScreen.X / 2;
            _ball.position.Y = Globals.ScreenSize.Height - 32f;
        }
        public void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
            if(IsNewKeyPress(currentState,lastState,Keys.Space)){
                _ball.ApplyForce(new Vector2(20,-25));
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
        private bool IsNewKeyPress(KeyboardState currentState,KeyboardState lastState,params Keys[] keys){
            return keys.Any(k => currentState.IsKeyDown(k) && lastState.IsKeyUp(k));
        }
    }
}