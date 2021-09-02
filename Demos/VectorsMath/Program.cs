﻿using Playground.Scenes;
using System;
using VectorsGame;

namespace VectorsMath
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Globals.ScreenSize = (1600,900);
            // using var game = new VectorsGame();
            // using var game = new OscillationGame();
            using var game = new ParticlesGame();
            game.Run();
        }
    }
}
