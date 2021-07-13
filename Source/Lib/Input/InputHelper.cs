using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Lib.Input
{
    public class InputHelper
    {
        public bool IsNewKeyPress(KeyboardState currentState,KeyboardState lastState,params Keys[] keys)
        {
            return keys.Any(k => currentState.IsKeyDown(k) && lastState.IsKeyUp(k));
        }
    }
}
