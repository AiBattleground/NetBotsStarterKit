using System;
using System.Collections.Generic;
using System.Linq;
using NetBots.Web;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.DemoBots
{
    public class RandomBot : IRobot
    {
        public IEnumerable<BotletMove> GetMoves(MoveRequest request)
        {
            var game = GameStateParser.Parse(request);

            Random myRandom = new Random();

            var myMoves = new List<BotletMove>();
            foreach (var bot in game.Grid.Where(x => x == SquareType.PlayerBot))
            {
                var validMoveLocations = bot.GetAdjacentSquares().ToList();
                var randomNumber = myRandom.Next(0, validMoveLocations.Count);
                var squareToMoveTo = validMoveLocations[randomNumber];
                var botletMove = CreateMoveFromSquares(bot, squareToMoveTo);
                myMoves.Add(botletMove);
            }
            return myMoves;
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
