using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using NetBots.WebModels;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    class Demolitions : Soldier
    {
        public Demolitions(Square square) : base(square)
        {
        }

        public override BotletMove GetMove()
        {
            if (Square.DistanceFrom(Grid.EnemySpawn) < 2)
            {
                return GetMoveToTarget(Grid.EnemySpawn);
            }
            else if (Square.DistanceFrom(Grid.EnemySpawn) < 10 && SquareNextToEdge())
            {
                var edgeSquare = Square.GetAdjacentSquares().FirstOrDefault(x => SquareOnEdge(x) && !TargetsTaken.Contains(x));
                if (edgeSquare != null)
                {
                    return GetMoveToTarget(edgeSquare);
                }
            }
            else if (SquareOnEdge(Square))
            {
                var currentDistance = Square.DistanceFrom(Grid.EnemySpawn);
                var nextSquareOnEdge = Square.GetAdjacentSquares().FirstOrDefault(x => SquareOnEdge(x) && x.DistanceFrom(Grid.EnemySpawn) < currentDistance);
                if (nextSquareOnEdge != null)
                {
                    return GetMoveToTarget(nextSquareOnEdge);
                }
            }
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

        private bool SquareNextToEdge()
        {
            return (Square.X == 1 || Square.Y == 1 || Square.X == Grid.Width - 2 || Square.Y == Grid.Height - 2);
        }

        private bool SquareOnEdge(Square square)
        {
            return (square.X == 0 || square.Y == 0 || square.X == Grid.Width - 1 || square.Y == Grid.Height - 1);
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
