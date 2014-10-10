using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBots.Bot.Interface;
using NetBotsClient.Models;

namespace NetBotsClient.Ai
{
    public class GameStateParser
    {
        public static GameBoard Parse(GameState gameState)
        {
            var game = new GameBoard
            {
                EnemyEnergy = gameState.p2.energy,
                MyEnergy = gameState.p1.energy,
                Grid = CreateGrid(gameState),
                Turn = gameState.turnsElapsed,
                TurnLimit = gameState.maxTurns
            };
            return game;
        }

        public static Grid CreateGrid(GameState request)
        {
            Grid.MapGridValue('r', SquareType.PlayerBot);
            Grid.MapGridValue('b', SquareType.EnemyBot);
            Grid.MapGridValue('*', SquareType.Energy);
            Grid.MapGridValue('x', SquareType.DeathMarker);
            Grid.MapGridValue('.', SquareType.Empty);
            var grid = new Grid(request.cols, request.rows, request);
            return grid;
        }
    }
}
