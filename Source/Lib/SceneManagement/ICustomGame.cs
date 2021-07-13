using Microsoft.Xna.Framework;

namespace Lib
{
    public interface ICustomGame
    {
        void Initialize();
        void LoadContent();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);

    }
}