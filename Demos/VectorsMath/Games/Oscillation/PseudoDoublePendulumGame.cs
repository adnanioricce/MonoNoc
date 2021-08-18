using Lib;
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
        readonly List<Pendulum> pendulums = new List<Pendulum>();        
        public PseudoDoublePendulumGame(SpriteBatch spriteBatch,ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
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
                second.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            pendulums.ForEach(pendulum => pendulum.Draw(_spriteBatch));
            _spriteBatch.End();
        }
    }
}
