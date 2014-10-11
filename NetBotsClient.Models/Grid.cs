using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NetBots.Bot.Interface;

namespace NetBotsClient.Models
{
    public class Grid : IEnumerable<Square>
    {
        public readonly int Height;
        public readonly int Width;

        public bool MySpawnActive { get; set; }
        public bool EnemySpawnActive { get; set; }

        public Square MySpawn { get; private set; }
        public Square EnemySpawn { get; private set; }

        private static readonly Dictionary<char, SquareType> CharSquareAssociation = new Dictionary<char, SquareType>();

        public Grid(int width, int height, GameState gs)
        {
            Height = height;
            Width = width;
            _grid = new Square[width, height];
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    _grid[h, w] = new Square(this, h, w);
                }
            }
            Initialize(gs);
        }

        //This constructor is mostly for unit testing.
        public Grid(int width, int height, Action<Square> initAction)
        {
            Height = height;
            Width = width;
            _grid = new Square[width, height];
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    _grid[h, w] = new Square(this, h, w);
                }
            }
            foreach (var square in this)
            {
                initAction(square);
            }
        }


        public static void MapGridValue(char c, SquareType st)
        {
            CharSquareAssociation.Add(c, st);
        }

        private void Initialize(GameState gameState)
        {
            var gridString = gameState.grid;
            for (int i = 0; i < gridString.Length; i++)
            {
                int col = (int) Math.Floor((double) (i)/this.Width);
                int row = (int) Math.Floor((double) i - (this.Width*col));
                char gridChar = gridString[i];
                var square = this[row, col];
                square.SquareType = CharSquareAssociation[gridChar];
                if (square.LineIndex == gameState.p1.spawn)
                {
                    MySpawn = square;
                    MySpawnActive = !gameState.p1.spawnDisabled;
                }
                else if (square.LineIndex == gameState.p2.spawn)
                {
                    EnemySpawn = square;
                    EnemySpawnActive = !gameState.p1.spawnDisabled;
                }
            }
        }


        private readonly Square[,] _grid;

        public Square this[int x, int y]
        {
            get { return _grid[x, y]; }
        }

        public IEnumerator<Square> GetEnumerator()
        {
            return _grid.Cast<Square>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
