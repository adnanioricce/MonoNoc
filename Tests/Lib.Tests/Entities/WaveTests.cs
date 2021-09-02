using Xunit;
using System;
using Lib.Physics;
using Microsoft.Xna.Framework;
using Lib.Delegates;

namespace Lib.Entities.Tests
{
    public class WaveTests
    {
        [Fact(DisplayName = "Update only in a space of x times specified in MillisecondsPerFrame")]
        public void UpdateTest()
        {
            // Arrange
            int waveWidth = 360;
            int amplitude = 10;
            Vector2 step = Vector2.One;
            bool onUpdatePointCalled = false;
            var testTime = new GameTime();
            float updateOscillatorPosition(Vector2 angle, GameTime time) => amplitude * MathF.Sin(angle.Y);
            void onUpdatePoint(Oscillator oscillator, GameTime time)
            {
                oscillator.position.Y = updateOscillatorPosition(oscillator.angle, testTime);
                onUpdatePointCalled = true;
            }
            var wave = new Wave(waveWidth, amplitude, step)
            {
                OnOscillate = onUpdatePoint
            };
            var oscillator = new Oscillator(null,new Vector2(0.00f,0.05f),Vector2.Zero,Vector2.Zero,Vector2.Zero,Vector2.Zero);
            wave.Oscillators.Add(oscillator);
            var gameTime = new GameTime();
            // Act            
            wave.Update(gameTime);
            // Assert                  
            Assert.All(wave.Oscillators,(oscillator) =>
            {                
                Assert.Equal(updateOscillatorPosition(oscillator.angle,testTime),oscillator.position.Y,4);
            });
            Assert.True(onUpdatePointCalled);
        }
        
        [Fact(DisplayName = "Angle should be updated based on a given delegate")]
        public void Update_angle_based_on_delegate()
        {
            // Arrange
            int waveWidth = 360;
            int amplitude = 100;
            Vector2 step = Vector2.One;
            bool onUpdateAngleCalled = false;
            var wave = new Wave(waveWidth, amplitude, step)
            {
                OnUpdateAngle = (angle, angularVelocity) =>
                {
                    onUpdateAngleCalled = true;
                    var displacement = angle + angularVelocity;
                    return displacement;
                }
            };
            var oscillator = new Oscillator(null,Vector2.Zero,new Vector2(0.05f,0.0f),Vector2.Zero,Vector2.Zero,Vector2.Zero);
            wave.Oscillators.Add(oscillator);
            var gameTime = new GameTime();
            // Act            
            for(int i = 0;i < 10;i++){
                wave.UpdateAngle(gameTime);
            }
            // Assert
            Assert.All(wave.Oscillators,(oscillator) => {                
                Assert.Equal(0.5f,oscillator.angle.X,2);
            });
            Assert.True(onUpdateAngleCalled);
        }        
    }
}