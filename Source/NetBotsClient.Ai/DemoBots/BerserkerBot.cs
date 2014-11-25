using System.Collections.Generic;
using System.Linq;
using NetBots.Web;
using NetBotsClient.Models;

//CHANGE THIS TO PREVENT COLLISIONS IF YOU'RE SUBMITTING YOUR DLL.
namespace NetBotsClient.Ai.DemoBots
{
    public class BerserkerBot : IRobot
    {
        public IEnumerable<BotletMove> GetMoves(MoveRequest request)
        {
            var game = GameStateParser.Parse(request);

            //Berserker Bot has pretty simple logic, and is here for demonstration
            //only. We will loop through each bot you have and make it run straight
            //toward the enemy spawn.
            var myMoves = new List<BotletMove>();
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


        private Square GetNextSquareToTarget(Square targetSquare, IEnumerable<Square> possibleMoves)
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

        private BotletMove CreateMoveFromSquares(Square origin, Square destination)
        {
            var move = new BotletMove();
            move.From = origin.LineIndex;
            move.To = destination.LineIndex;
            return move;
        }
    }
}
