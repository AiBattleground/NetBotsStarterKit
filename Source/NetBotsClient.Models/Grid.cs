using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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

        private List<Square> _myBots = null;
        public List<Square> MyBots
        {
            get
            {
                if (_myBots == null)
                {
                    _myBots = this.Where(x => x == SquareType.PlayerBot).ToList();
                }
                return _myBots.ToList();
            }
        }

        private List<Square> _enemyBots = null;
        public List<Square> EnemyBots
        {
            get
            {
                if (_enemyBots == null)
                {
                    _enemyBots = this.Where(x => x == SquareType.EnemyBot).ToList();
                }
                return _enemyBots.ToList();
            }
        }

        private List<Square> _energy = null;
        public List<Square> Energy
        {
            get
            {
                if (_energy == null)
                {
                    _energy = this.Where(x => x == SquareType.Energy).ToList();
                }
                return _energy.ToList();
            }
        }


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
