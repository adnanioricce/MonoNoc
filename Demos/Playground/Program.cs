using Microsoft.Xna.Framework;
using Playground.Games.Challenges;
using Playground.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using VectorsGame;

namespace VectorsMath
{
    public static class Program
    {
        private static void GlobalSetup()
        {
            Globals.ScreenSize = (1000, 1000);
        }
        private static Dictionary<int, Game> Options = new Dictionary<int, Game>
        {
            { 1, new VectorsGame() },
            { 2, new OscillationGame()},
            { 3, new ParticlesGame()},
            { 4, new ChallengesGame() },
            { 5, new PhysicsGame() },
            { 6, new HelloPhysicsGame() },
            { 7, new AutonomousAgentsGame() }
        };
        private static string OptionStr = string.Join(",", Options.Select(e => $"{e.Key} -> {e.Value.GetType().Name}").ToArray());
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("Options:");
            Console.WriteLine(OptionStr);            
            GlobalSetup();
            var game = Options[Options.Count];
            game.Run();
        }
    }
}
