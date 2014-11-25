using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBots.Web;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    class Assault : Soldier
    {
        public Assault(Square square, Grid grid) : base(square, grid)
        {
        }

        public override BotletMove GetMove()
        {
            return GetMoveToTarget(Grid.EnemySpawn);
            //var meetupPoints = GetMeetupPoints(Grid);
            //var occupiedSquares = Army.Select(x => x.Square);
            //var unOccupiedMeetupPonts = meetupPoints.Where(x => !occupiedSquares.Contains(x));
            //var meetupsToMoveTo = unOccupiedMeetupPonts.Where(TargetsTaken.Contains).ToList();
            //if (meetupsToMoveTo.Any())
            //{
            //    return GetMoveToTarget(meetupsToMoveTo.First());
            //}
            //else
            //{
            //    if (Grid.EnemySpawn.X == 1)
            //    {
            //        var nextSquare = Grid[Square.X, Square.Y - 1];
            //        return new BotletMove(Square.LineIndex, nextSquare.LineIndex);
            //    }
            //    else
            //    {
            //        var nextSquare = Grid[Square.X, Square.Y + 1];
            //        return new BotletMove(Square.LineIndex, nextSquare.LineIndex);
            //    }
            //}
        }


        public static IEnumerable<Square> GetMeetupPoints(Grid grid)
        {
            if (grid.EnemySpawn.X == 1)
            {
                return new List<Square>()
                {
                    grid[0, 10],
                    grid[1, 11]
                };
            }
            else
            {
                return new List<Square>()
                {
                    grid[19, 10],
                    grid[18, 9]
                };
            }
        } 
    }
}
