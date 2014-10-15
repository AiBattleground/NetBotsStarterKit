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
            gs.Cols = 20;
            gs.Rows = 20;
            gs.MaxTurns = 10;
            gs.TurnsElapsed = 1;
            gs.Grid = "...................r" +
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
            gs.P1 = GetSamplePlaner(0, false);
            gs.P2 = GetSamplePlaner(gs.Grid.Length - 1, false);
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
            var moveRequest = new MoveRequest()
            {
                Player = "P1",
                State = gameState
            };
            var game = GameStateParser.Parse(moveRequest);
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void SquaresAreAdjacent()
        {
            var gameState = CreateSampleGameState();
            var moveRequest = new MoveRequest()
            {
                Player = "P1",
                State = gameState
            };
            var game = GameStateParser.Parse(moveRequest);
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
                var moveRequest = new MoveRequest()
                {
                    Player = "P1",
                    State = gameState
                };
                var moves = brain.GetMoves(moveRequest);
                var move = moves.First();
                Assert.IsTrue(move.From == 19 && move.To == 39);
            }
            
        }
    }
}
