using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBots.Web;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    public class Sentry : Soldier
    {
        public Sentry(Square square, Grid grid) : base(square, grid)
        {
        }

        public static List<Square> GetSentryPoint(Grid grid)
        {
            var sentryPoints = grid.Where(x => x.IsAdjacentTo(grid.MySpawn) && (x.X == 0 || x.Y == 0)).ToList();
            return sentryPoints;
        }

        public override BotletMove GetMove()
        {
            var sentryPoints = GetSentryPoint(Grid);
            if (sentryPoints.Contains(Square))
            {
                return new BotletMove(Square.LineIndex, Square.LineIndex);
            }
            else
            {
                var sentryPoint = sentryPoints.FirstOrDefault(x => !Army.Any(y => y.Square == x));
                if (sentryPoint != null)
                {
                    return GetMoveToTarget(sentryPoint);
                }
                else return GetMoveToTarget(Grid.EnemySpawn); //if there are already two at the sentry location, move away.
            }
        }
    }
}
