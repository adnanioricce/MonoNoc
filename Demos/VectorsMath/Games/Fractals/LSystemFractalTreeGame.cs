using Lib;
using Lib.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Games.Fractals
{
    struct AxiomRule
    {
        public string A;
        public string B;
    }
    struct NodePosition
    {
        public Vector2 Location;
        public float Angle;
    }
    public class LSystemFractalTreeGame : ICustomGame
    {        
        private AxiomRule[] Rules = new[]
        {
            new AxiomRule
            {
                A = "F",
                B = "FF+[+F-F-F]-[-F+F+F]"
            }
        };
        private string GenerateSequence(string previousSequence)
        {
            string newSequence = "";
            
            bool found = false;
            for (int i = 0; i < previousSequence.Length; ++i) {
                string current = previousSequence[i].ToString();
                foreach (var rule in Rules)
                {
                    if (current != rule.A)
                        continue;

                    found = true;
                    newSequence += rule.B;
                }
                if (!found)
                {
                    newSequence += current;
                }
            }                        
            return newSequence;
        }
        private void DrawSequence(SpriteBatch sb,string sequence,float lineLength = 25)
        {
            foreach (var ch in sequence)
            {
                switch (ch)
                {
                    case 'F':
                        sb.DrawLine(Vector2.Zero,new Vector2(0,-lineLength),Color.Black);
                        break;
                    case '+':
                        //TODO:Rotate Clockwise
                        break;
                    case '-':
                        //TODO:Rotate Counter-Clockwise
                        break;
                    case '[':
                        //TODO: Save transformations
                        break;
                    case ']':
                        //TODO: Throwback/pop transformations
                        break;
                }
            }
        }        
        private string Sequence = "";
        private readonly SpriteBatch _sb;
        public LSystemFractalTreeGame(SpriteBatch sb)
        {
            _sb = sb;
        }
        public void Initialize()
        {
            Sequence = GenerateSequence(Sequence);
            Sequence = GenerateSequence(Sequence);
        }

        public void LoadContent()
        {            
        }

        public void Update(GameTime gameTime)
        {
        }
        public void Draw(GameTime gameTime)
        {
            DrawSequence(_sb, Sequence);
        }
    }
}
