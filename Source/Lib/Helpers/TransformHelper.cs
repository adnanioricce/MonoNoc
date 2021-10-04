using Lib.Components;
using Microsoft.Xna.Framework;

namespace Lib.Helpers
{
    public static class TransformHelper
    {
        public static Transform2D Drag(this Transform2D transform,Vector2 positionToDrag)
        {
            transform.Position = positionToDrag;
            return transform;
        }
    }
}
