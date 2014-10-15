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
        public static GameBoard Parse(MoveRequest request)
        {
            var gameState = request.State;
            var game = new GameBoard
            {
                MyEnergy = request.Player == "P1" ? gameState.P1.energy : gameState.P2.energy,
                EnemyEnergy = request.Player == "P2" ? gameState.P1.energy : gameState.P2.energy,
                Grid = CreateGrid(request),
                Turn = gameState.TurnsElapsed,
                TurnLimit = gameState.MaxTurns
            };
            return game;
        }

        public static Grid CreateGrid(MoveRequest request)
        {
            char myChar = request.Player == "P1" ? '1' : '2';
            char enemyChar = request.Player == "P2" ? '1' : '2';
     
            Grid.MapGridValue(myChar, SquareType.PlayerBot);
            Grid.MapGridValue(enemyChar, SquareType.EnemyBot);
            Grid.MapGridValue('*', SquareType.Energy);
            Grid.MapGridValue('x', SquareType.DeathMarker);
            Grid.MapGridValue('X', SquareType.DeathMarker);
            Grid.MapGridValue('.', SquareType.Empty);
            var grid = new Grid(request.State.Cols, request.State.Rows, request.State);
            return grid;
        }
    }
}
