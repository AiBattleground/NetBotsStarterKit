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
            foreach (var bot in game.Grid.MyBots)
            {
                var possibleMoves = bot.GetAdjacentSquares().ToList();
                var randomNumber = myRandom.Next(0, possibleMoves.Count);
                var squareToMoveTo = possibleMoves[randomNumber];
                var botletMove = bot.GetMoveTo(squareToMoveTo);
                myMoves.Add(botletMove);
            }
            return myMoves;
        }
    }
}
