using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NetBots.Bot.Interface;
using NetBotsClient.Models;

namespace NetBotsClient.Ai
{
    public class Brain : INetBot
    {
        public string Name { get { return "Berserker Bot!"; }}

        public IBotLetMoveCollection GetMoves(GameState gameState)
        {
            var game = GameStateParser.Parse(gameState);

            //Berserker Bot has pretty simple logic, and is here for demonstration
            //only. We will loop through each bot you have and make it run straight
            //toward the enemy spawn.
            BotletMoveCollection myMoves = new BotletMoveCollection();
            foreach (var bot in game.Grid.Where(x => x == SquareType.PlayerBot))
            {
                var validMoveLocations = bot.GetAdjacentSquares();
                var enemySpawn = game.Grid.EnemySpawn;
                var squareToMoveTo = GetNextSquareToTarget(enemySpawn, validMoveLocations);
                var botletMove = CreateMoveFromSquares(bot, squareToMoveTo);
                myMoves.Add(botletMove);
            }
            return myMoves;
        }

        public Square GetNextSquareToTarget(Square targetSquare, IEnumerable<Square> possibleMoves)
        {
            int shortestDistance = int.MaxValue;
            Square squareToMoveTo = null;
            foreach (var square in possibleMoves)
            {
                var distance = square.DistanceFrom(targetSquare);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    squareToMoveTo = square;
                }
            }
            return squareToMoveTo;
        }

        public BotletMove CreateMoveFromSquares(Square origin, Square destination)
        {
            var move = new BotletMove();
            move.from = origin.LineIndex;
            move.to = destination.LineIndex;
            return move;
        }
    }
}
