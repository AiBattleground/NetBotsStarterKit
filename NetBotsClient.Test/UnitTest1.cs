using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetBots.Bot.Interface;
using NetBotsClient.Ai;
using NetBotsClient.Models;

namespace NetBotsClient.Test
{
    [TestClass]
    public class UnitTest1
    {
        private GameState CreateSampleGameState()
        {
            GameState gs = new GameState();
            gs.cols = 20;
            gs.rows = 20;
            gs.maxTurns = 10;
            gs.turnsElapsed = 1;
            gs.grid = "...................r" +
                      "...................." +
                      "...................." +
                      "...*................" +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      "...................." +
                      ".........b.........." +
                      ".........b..........";
            gs.p1 = GetSamplePlaner(0, false);
            gs.p2 = GetSamplePlaner(gs.grid.Length - 1, false);
            return gs;
        }

        private Player GetSamplePlaner(int spawnLoc, bool spawnDisabled)
        {
            var player = new Player();
            player.energy = 0;
            player.spawn = spawnLoc;
            player.spawnDisabled = spawnDisabled;
            return player;
        }


        [TestMethod]
        public void CanCreateGame()
        {
            var gameState = CreateSampleGameState();
            var game = GameStateParser.Parse(gameState);
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void SquaresAreAdjacent()
        {
            var gameState = CreateSampleGameState();
            var game = GameStateParser.Parse(gameState);
            Assert.IsTrue(game.Grid[0, 19].IsAdjacentTo(game.Grid[0, 18]));
        }

        //Sample bot-specific test. Should be deleted or modified
        //once you implement your own.
        [TestMethod]
        public void Berserks()
        {
            var brain = new Brain();
            if (brain.Name == "Berserker Bot!")
            {
                var gameState = CreateSampleGameState();
                var moves = brain.GetMoves(gameState);
                var move = moves.First();
                Assert.IsTrue(move.from == 19 && move.to == 39);
            }
            
        }
    }
}
