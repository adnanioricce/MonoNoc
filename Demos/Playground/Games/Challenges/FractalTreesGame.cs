using Lib;
using Lib.Extensions.Test;
using Lib.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VectorsMath;
using Lib.Math.Noises;
using System.Threading.Tasks;
using Lib.Structures;

namespace Playground.Games.Challenges
{
    public class SimpleLeaf 
    {
        public Vector2 Start;
        public Vector2 End;
        public void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.White;
            var noiseR = End.X * Perlin.Noise(End.Y / 1000f,End.X / 500f);
            var noiseG = End.Y * Perlin.Noise(Start.Y / 1000f,Start.X / 500f);
            var noiseB = ((End.X + End.Y) / 2f) * Perlin.Noise(((End.X + End.Y) / 2f) / 1000f,((Start.Y + Start.Y) / 2f) / 1000f);
            color.R *= (byte)(noiseR);            
            color.G *= (byte)(noiseG);            
            color.B *= (byte)(noiseB);
            spriteBatch.DrawLine(Start.X,Start.Y,End.X,End.Y,color);
        }
    }

    public class FractalTreesGame : ICustomGame
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphics;
        private float scale = 0.32f;
        private readonly float length = 300;        
        private readonly (int Width,int Height) Size = Globals.ScreenSize;        
        private BinaryTree<SimpleLeaf> Tree;        
        int millisecondsSinceLastUpdate = 0;
        bool grow = true;       
        public FractalTreesGame(SpriteBatch spriteBatch,GraphicsDevice graphics)
        {
            _spriteBatch = spriteBatch;         
            _graphics = graphics;
            Tree = new BinaryTree<SimpleLeaf>{
                Left = new BinaryTree<SimpleLeaf>(),
                Right = new BinaryTree<SimpleLeaf>()
            };
        }
        private SimpleLeaf CreateLeaf(Vector2 previous,float leafLength,float leafAngle)
        {            
            var pos2 = new Vector2(previous.X + leafLength * MathF.Cos(leafAngle),previous.Y + leafLength * MathF.Sin(leafAngle));
            var leafLeft = new SimpleLeaf();
            leafLeft.Start = previous;
            leafLeft.End = pos2;
            return leafLeft;
        }

        private BinaryTree<SimpleLeaf> GenerateTree()
        {
            var currentLength = length;
            var pos = new Vector2((float)Size.Width / 2,(float)Size.Height);
            var angle = 3.0f * MathHelper.Pi / 2.0f;            
            var leaf = CreateLeaf(pos,currentLength,angle);
            var tree = BinaryTree<SimpleLeaf>.CreateNode(leaf);
            CreateTree(tree.Left,pos,currentLength,angle);
            CreateTree(tree.Right,pos,currentLength,angle);
            return tree;
        }
        private void CreateTree(BinaryTree<SimpleLeaf> currentNode ,Vector2 previous,float leafLength,float leafAngle)
        {            
            if (leafLength < 1.0)
                return;
                                  
            var leaf = CreateLeaf(previous,leafLength,leafAngle);            
            currentNode.AddChild(leaf);
            CreateTree(currentNode.Left,leaf.End,leafLength * scale,leafAngle + MathHelper.Pi / 5.0f);
            CreateTree(currentNode.Right,leaf.End,leafLength * scale,leafAngle - MathHelper.Pi / 5.0f);                            
        }
        
        public void LoadContent()
        {                                    
            Tree = GenerateTree();           
        }

        public void Initialize()
        {
            
        }        
        
        public void Update(GameTime gameTime)
        {
            millisecondsSinceLastUpdate += (int)gameTime.ElapsedGameTime.TotalMilliseconds;            
            if(millisecondsSinceLastUpdate < TimeSpan.FromSeconds(1.5).TotalMilliseconds)
                return;

            scale += grow ? 0.0005f : -0.0005f;

            if(grow && scale > 0.69f)
            {
                grow = !grow;
            }
            if(!grow && scale < 0.2f)
            {
                grow = !grow;
            }
            Tree = GenerateTree();
            
        }

        public void Draw(GameTime gameTime)
        {            
            _graphics.Clear(Color.White * 0.21f);
            _spriteBatch.Begin();            
            Tree.TraverseTree((leaf) => leaf?.Draw(_spriteBatch));
            _spriteBatch.End();
        }        
    }
}
