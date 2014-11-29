using System.Collections.Generic;
using System.Linq;
using NetBots.WebModels;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    public class ArmyBot : IRobot
    {
        public IEnumerable<BotletMove> GetMoves(MoveRequest request)
        {
            Soldier.Reset();
            var game = GameStateParser.Parse(request);
            var squaresToAssign = game.Grid.Where(x => x == SquareType.PlayerBot).OrderBy(x => x.DistanceFrom(game.Grid.MySpawn)).ToList();
            List<Soldier> myArmy = new List<Soldier>();
            for (int i = 0; i < squaresToAssign.Count; i++)
            {
                var square = squaresToAssign[i];
                myArmy.Add(GetAssignment(square, game.Grid, squaresToAssign.Count, i));
            }
            List<BotletMove> moves = new List<BotletMove>();
            foreach (var soldier in myArmy)
            {
                var move = soldier.GetMove();
                moves.Add(move);
            }
            return moves;
        }

        private Soldier GetAssignment(Square square, Grid grid, int armySize, int soldierNum)
        {
            if (SentriesPosted(grid) && Sentry.GetSentryPoint(grid).Contains(square))
                return new Sentry(square, grid);
            else if (!grid.EnemySpawnActive && grid.MySpawnActive && armySize > grid.Count(x => x == SquareType.EnemyBot) + 5)
                return new Assassain(square, grid);
            else if (armySize > 6 && (soldierNum == 0 /*|| soldierNum == 1*/))
                return new Sentry(square, grid);
            else if (square.DistanceFrom(grid.EnemySpawn) < 10 && grid.EnemySpawnActive)
                return new Demolitions(square, grid);
            else return new Trooper(square, grid);
        }

        private bool SentriesPosted(Grid grid)
        {
            var sentryPoints = Sentry.GetSentryPoint(grid);
            return sentryPoints.All(x => x == SquareType.PlayerBot);
        }
    }
}
