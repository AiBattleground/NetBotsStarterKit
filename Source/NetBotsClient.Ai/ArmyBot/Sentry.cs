using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBots.WebModels;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    public class Sentry : Soldier
    {
        public Sentry(Square square) : base(square)
        {
        }

        public static List<Square> GetSentryPoint(Grid grid)
        {
            var sentryPoints = grid.Where(x => x.IsAdjacentTo(grid.MySpawn) && (x, grid)).ToList();
            return sentryPoints;
        }

        public override BotletMove GetMove()
        {
            var sentryPoints = GetSentryPoint(Grid);
            if (sentryPoints.Contains(Square))
            {
                return GetMoveToTarget(Square);
            }
            else
            {
                var armyOccupied = Army.Select(x => x.Square);
                var unoccupied = sentryPoints.FirstOrDefault(x => !armyOccupied.Contains(x));
                if (unoccupied != null)
                {
                    return GetMoveToTarget(unoccupied);
                }
                else return GetMoveToTarget(Grid.EnemySpawn); //if there are already two at the sentry location, move away.
            }
        }

        public static bool IsOnEdge(Square square, Grid grid)
        {

            if ((square.X == 0 || square.X == grid.Width - 1) ||
                square.Y == 0 || square.Y == grid.Height - 1)
            {
                return true;
            }
            return false;
        }
    }
}
