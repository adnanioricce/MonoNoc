using Lib.Delegates;
using Lib.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lib.Entities
{
    
    //TODO: Implement it 
    public class Wave 
    {
        public int WaveWidth;
        public float Amplitude;
        public float Angle;
        public Vector2 Step = Vector2.One;        
        public Update<Oscillator> OnOscillate = (oscillator,time) => oscillator.Update(time);
        public UpdateByVelocity2 OnUpdateAngle = (angle,velocity) => angle + velocity;
        public UpdateVelocity2 UpdateAngleVelocity = (angleVelocity,angleAcceleration) => angleVelocity + angleAcceleration;
        
        public List<Oscillator> Oscillators { get; set; } = new List<Oscillator>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waveWidth">Width of the wave</param>
        /// <param name="amplitude">amplitude of the oscillators</param>
        /// <param name="updatePoint">delegate with a function to update the position of each oscillators on Update method</param>
        public Wave(int waveWidth,float amplitude,Vector2 step)
        {
            WaveWidth = waveWidth;
            Amplitude = amplitude;            
            Step = step;            
        }
        public virtual void UpdateAngle(GameTime gameTime)
        {
            this.Oscillators.ForEach(oscillator =>
            {
                oscillator.angle = OnUpdateAngle(oscillator.angle,oscillator.velocity);
            });
        }
        public virtual void Update(GameTime gameTime)
        {
            this.Oscillators.ForEach((oscillator) => {                
                OnOscillate(oscillator,gameTime);
            });
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Oscillators.ForEach(oscillator => oscillator.Draw(spriteBatch));
        }
    }
}