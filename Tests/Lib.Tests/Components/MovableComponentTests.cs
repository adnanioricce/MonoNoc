using Lib.Components;
using Microsoft.Xna.Framework;
using System;
using Xunit;

namespace Lib.Tests.Components
{
    public class MovableComponentTests
    {
        [Fact(DisplayName = "Move should be applied to object based on delegate")]
        public void MoveTest()
        {
            // Arrange
            UpdatePosition<Vector2> moveCallback = (pos) =>
            {
                pos.Location += pos.Velocity;
                pos.Velocity += pos.Acceleration;
                pos.Acceleration = Vector2.Multiply(pos.Acceleration,0f);
            };
            var component = new MovableObject(moveCallback);
            // Act           
        }
    }
}
