using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Noises
{
    public class Game1 : Game
    {
        readonly Random rng = new Random();
        (int Width,int Height) Size = (0,0);
        private Texture2D texture;
        private bool hasDraw = false;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D target;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();            
            Size = (_graphics.PreferredBackBufferWidth,_graphics.PreferredBackBufferHeight);
            target = new RenderTarget2D(GraphicsDevice,Size.Width,Size.Height);
            texture = new Texture2D(GraphicsDevice,1,1);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(!hasDraw){
                CreateImage();
                hasDraw = true;
            }
            GraphicsDevice.Clear(Color.CornflowerBlue);                            
            _spriteBatch.Begin();
            _spriteBatch.Draw(target,Vector2.Zero,target.Bounds,Color.White);
            _spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void GenerateClouds(float[,] noise){
            var colors = new List<Color>(Size.Width * Size.Height);
            for(int y = 0;y < Size.Height;++y){
                for(int x = 0;x < Size.Width;++x){
                    var n = (Perlin.Turbulence(x,y,128,Size,noise)) / 255;
                    // var (r,g,b) = (255 / 255,65 / 255,255 * n);
                    var color = new Color(0.24f,1f,255 * n);                    
                    
                    colors.Add(color);
                }
            }
        }
        private void CreateNoiseImage(Color[] data)
        {                      
            //logging  
            foreach(var color in data.OrderBy(c => c.B).Take(10)){
                Console.WriteLine("color:{0}",color.ToString());
            }       

            target.SetData(data.ToArray());
            
        }
        private Color[] GenerateTurbulenceNoise(float[,] noiseMap){
            var colors = new List<Color>();
            for (int y = 0; y < Size.Height; y++){
                for (int x = 0; x < Size.Width; x++){
                    var n = Perlin.Turbulence(x,y,512,Size,noiseMap);
                    var color = new Color(n,n,n);
                    colors.Add(color);
                }
            }
            return colors.ToArray();
        }
        private void CreateImage()
        {
            var noise = GenerateNoise();            
            var colors = Perlin.SineMap(Size,noise);
            for(int i = 0;i < colors.Length;++i)
            {
                colors[i].R = (byte)255;
                colors[i].G = (byte)255;
            }
            foreach(var color in colors.OrderBy(c => c.B).Take(10)){
                Console.WriteLine("color:{0}",color.ToString());
            }
            target.SetData(colors.ToArray());
            SaveTargetToFile("result.jpg");
        }
        private void SaveTargetToFile(string fileName){
            using var file = File.Create(fileName);
            target.SaveAsJpeg(file,Size.Width,Size.Height);
        }
        private float[,] GenerateNoise()
        {
            var noises = new float[Size.Height,Size.Width];
            for(int y = 0;y < Size.Height;++y){
                for(int x = 0;x < Size.Width;++x){
                    // noises[y,x] = (float)(((rng.NextDouble() * 1000) % 32768) / 32768.0);
                    noises[y,x] = CRandom.Gen(x,y);
                }
            }
            return noises;
        }
    }
}
