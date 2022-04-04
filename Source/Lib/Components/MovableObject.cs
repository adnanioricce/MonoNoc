using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Components
{
    public class Movable2DObject
    {
        public Func<Vector2, Vector2> UpdatePosition;
        private Transform2D Transform;
        public Movable2DObject(Transform2D initialTransform,Func<Vector2,Vector2> updatePosition)
        {
            Transform = initialTransform;
            UpdatePosition = updatePosition;
        }
    }
}
