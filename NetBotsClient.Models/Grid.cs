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

        //private static readonly Dictionary<char, SquareType> CharSquareAssociation = new Dictionary<char, SquareType>();

        //public Grid(MoveRequest mr)
        //{
        //    Height = mr.State.Rows;
        //    Width = mr.State.Cols;
        //    _grid = new Square[Width, Height];
        //    for (int h = 0; h < Height; h++)
        //    {
        //        for (int w = 0; w < Width; w++)
        //        {
        //            _grid[h, w] = new Square(this, h, w);
        //        }
        //    }
        //    Initialize(mr);
        //}

        //This constructor is mostly for unit testing.
        public Grid(int width, int height, int mySpawnIndex, int enemySpawnIndex, Action<Square> initAction)
        {
            Height = height;
            Width = width;
            _grid = new Square[width, height];
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    var square = new Square(this, h, w);
                    _grid[h, w] = square;
                    initAction(square);
                    if (square.LineIndex == mySpawnIndex)
                    {
                        MySpawn = square;
                    }
                    else if (square.LineIndex == enemySpawnIndex)
                    {
                        EnemySpawn = square;
                    }
                }
            }
        }


        //public static void MapGridValue(char c, SquareType st)
        //{
        //    CharSquareAssociation.Add(c, st);
        //}

        //private void Initialize(MoveRequest request)
        //{
        //    var gameState = request.State;
        //    var mySpawnInDex = request.Player == "p1" ? gameState.P1.Spawn : gameState.P2.Spawn;
        //    var hisSpawnIndex = request.Player == "P2" ? gameState.P1.Spawn : gameState.P2.Spawn;

        //    var gridString = gameState.Grid;
        //    for (int i = 0; i < gridString.Length; i++)
        //    {
        //        int col = (int) Math.Floor((double) (i)/this.Width);
        //        int row = (int) Math.Floor((double) i - (this.Width*col));
        //        char gridChar = gridString[i];
        //        var square = this[row, col];
        //        square.SquareType = CharSquareAssociation[gridChar];

        //        if (square.LineIndex == mySpawnInDex)
        //        {
        //            MySpawn = square;
        //            MySpawnActive = !gameState.P1.SpawnDisabled;
        //        }
        //        else if (square.LineIndex == hisSpawnIndex)
        //        {
        //            EnemySpawn = square;
        //            EnemySpawnActive = !gameState.P1.SpawnDisabled;
        //        }
        //    }
        //}


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
