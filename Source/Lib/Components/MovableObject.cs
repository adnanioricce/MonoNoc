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
        public UpdatePosition<Vector2> UpdatePosition;
        private Position<Vector2> Position;
        public Movable2DObject(Position<Vector2> initialPosition,UpdatePosition<Vector2> updatePosition)
        {
            UpdatePosition = updatePosition;
        }
    }
}
