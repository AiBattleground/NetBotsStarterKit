using System;
using System.Collections.Generic;
using System.Linq;
using NetBots.WebModels;

namespace NetBotsClient.Models
{
    public class Square
    {
        public int LineIndex { get { return _lineIndex; } }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public SquareType SquareType { get; set; }

        private readonly int _x;
        private readonly int _y;
        private readonly int _lineIndex;

        public Grid Grid {get { return _grid; }}
        private readonly Grid _grid;




        public Square(Grid grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;
            _lineIndex = (y * grid.Width) + x;
        }

        public Square(int x, int y)
        {
            _x = x;
            _y = y;
            _lineIndex = 0;
        }

        public Square ClosestWhere(Func<Square, bool> selectionFunc)
        {
            return _grid.Where(selectionFunc).OrderBy(DistanceFrom).FirstOrDefault();
        }

        public Square ClosestOfType(SquareType squareType)
        {
            return _grid.Where(x => x == squareType).OrderBy(DistanceFrom).FirstOrDefault();
        }

        public int DistanceToClosest(SquareType squareType)
        {
            var closest = ClosestWhere(x => x == squareType);
            return DistanceFrom(closest);
        }

        public int DistanceToClosest(Func<Square, bool> selectionFunc)
        {
            var closest = ClosestWhere(selectionFunc);
            return DistanceFrom(closest);
        }

        public bool IsAdjacentTo(Square otherSquare)
        {
            int xDiff = Math.Abs(otherSquare.X - this.X);
            int yDiff = Math.Abs(otherSquare.Y - this.Y);
            if (xDiff == 1 && yDiff == 0)
                return true;
            if (xDiff == 0 && yDiff == 1)
                return true;
            return false;
        }

        //public bool IsAdjacentTo(Square otherSquare)
        //{
        //    int xDiff = Math.Abs(otherSquare.X - this.X);
        //    int yDiff = Math.Abs(otherSquare.Y - this.Y);
        //    return xDiff == 1 && yDiff < 2 || yDiff == 1 && xDiff < 2;
        //}

        public IEnumerable<Square> GetAdjacentSquares()
        {
            var adjacentSquares = _grid.Where(x => x.IsAdjacentTo(this));
            return adjacentSquares;
        }

        public int DistanceFrom(Square otherSquare)
        {
            if (otherSquare == this)
                return 0;
            return (Math.Abs(this._x - otherSquare._x) + Math.Abs(this._y - otherSquare._y));
        }

        public override string ToString()
        {
            var squareText = SquareType == SquareType.Empty ? "" : SquareType.ToString();
            return String.Format("{0}: {1},{2}", squareText, X, Y);
        }

        public static bool operator ==(Square square, SquareType squareType)
        {
            if (square == null) { throw new ArgumentNullException("square", "Square cannot be null!"); }
            return square.SquareType == squareType;
        }

        public static bool operator !=(Square square, SquareType squareType)
        {
            return !(square == squareType);
        }

        protected bool Equals(Square other)
        {
            return _x == other._x && _y == other._y && _lineIndex == other._lineIndex && Equals(_grid, other._grid) && SquareType == other.SquareType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Square)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _x;
                hashCode = (hashCode * 397) ^ _y;
                hashCode = (hashCode * 397) ^ _lineIndex;
                hashCode = (hashCode * 397) ^ (_grid != null ? _grid.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)SquareType;
                return hashCode;
            }
        }

        public BotletMove GetMoveTo(Square targetSquare)
        {
            if (IsAdjacentTo(targetSquare))
            {
                return new BotletMove(this.LineIndex, targetSquare.LineIndex);
            }
            else
            {
                var distanceToTarget = DistanceFrom(targetSquare);
                var adjacentSquares = GetAdjacentSquares();
                var closerSquare = adjacentSquares.FirstOrDefault(x => DistanceFrom(x) < distanceToTarget);
                if (closerSquare != null)
                {
                    return new BotletMove(this.LineIndex, closerSquare.LineIndex);
                }
            }
            return new BotletMove(this.LineIndex, this.LineIndex);
        }

        public BotletMove GetMoveToClosest(SquareType squareType)
        {
            var closest = ClosestOfType(squareType);
            var order = GetMoveTo(closest);
            return order;
        }
    }


    public static class SquareEnumerable
    {
        public static IEnumerable<Square> OfType(this IEnumerable<Square> squares, SquareType squareType)
        {
            return squares.Where(x => x == squareType);
        }
    }


    public enum SquareType
    {
        Empty,
        Energy,
        PlayerBot,
        EnemyBot,
    }
}
