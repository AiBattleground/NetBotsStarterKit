using System.Collections.Generic;
using System.Linq;
using System.Resources;
using NetBots.WebModels;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    public abstract class Soldier
    {
        public readonly Square Square;
        protected readonly Grid Grid;
        protected static List<Soldier> Army = new List<Soldier>();
        protected static List<Square> TargetsTaken = new List<Square>();

        public static void Reset()
        {
            Army = new List<Soldier>();
            TargetsTaken = new List<Square>();
        }

        protected Soldier(Square square)
        {
            Square = square;
            Grid = square.Grid;
            Army.Add(this);
        }

        public abstract BotletMove GetMove();

        protected BotletMove GetMoveToTarget(Square targetSquare)
        {
            if (targetSquare == null)
            {
                TargetsTaken.Add(Square);
                return new BotletMove(Square.LineIndex, Square.LineIndex);
            }
            if (targetSquare == Square)
            {
                TargetsTaken.Add(Square);
                return new BotletMove(Square.LineIndex, Square.LineIndex);
            }
            var adjacentSquares = Square.GetAdjacentSquares().ToList();
            adjacentSquares.Shuffle();
            int shortestDistance = int.MaxValue;
            Square squareToMoveTo = Square;
            foreach (var square in adjacentSquares.Where(x => !TargetsTaken.Contains(x)))
            {
                var distance = square.DistanceFrom(targetSquare);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    squareToMoveTo = square;
                }
            }
            TargetsTaken.Add(squareToMoveTo);
            return new BotletMove(Square.LineIndex, squareToMoveTo.LineIndex);
        }

        public int DistanceToTarget(Square target)
        {
            return target.DistanceFrom(Square);
        }
    }
}
