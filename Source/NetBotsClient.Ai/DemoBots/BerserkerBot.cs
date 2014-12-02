using System.Collections.Generic;
using System.Linq;
using NetBots.WebModels;
using NetBotsClient.Models;


//Berserker Bot has pretty simple logic, and is here for demonstration
//only. We will loop through each bot you have and make it run straight
//toward the enemy spawn.

//Some things in here are written the "long way" to show you a few simple things.

namespace NetBotsClient.Ai.DemoBots
{
    public class BerserkerBot : IRobot
    {
        public IEnumerable<BotletMove> GetMoves(MoveRequest request)
        {
            GameBoard game = GameStateParser.Parse(request);
            List<BotletMove> myMoves = new List<BotletMove>();
            List<Square> myBots = GetMyBots(game.Grid);
            foreach (Square bot in myBots)
            {
                var myMove = bot.GetMoveTo(game.Grid.EnemySpawn);
                myMoves.Add(myMove);
            }
            return myMoves;
        }

        private List<Square> GetMyBots(Grid grid)
        {
            //For demonstration, we will show you three different way to get all the squares
            //in a grid with your bots. 

            //You can access any square through it's X and Y coordinates. 
            return GetMyBotsByForLoop(grid);

            //Or since the grid is Enumerable, you can di it with a foreach loop
            //return GetMyBotsByForeach(grid);

            //Or just get them directly as a property on the Grid.
            //return GetMyBotsDirectly(grid);
        }

        
        private List<Square> GetMyBotsByForLoop(Grid grid)
        {
            List<Square> myBots = new List<Square>();
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var mySquare = grid[x, y];
                    if (mySquare == SquareType.PlayerBot)
                    {
                        myBots.Add(mySquare);
                    }
                }
            }
            return myBots;
        }

        
        private List<Square> GetMyBotsByForeach(Grid grid)
        {
            List<Square> myBots = new List<Square>();
            foreach (var mySquare in grid)
            {
                if (mySquare == SquareType.PlayerBot)
                {
                    myBots.Add(mySquare);
                }
                
            }
            return myBots;
        }

        private List<Square> GetMyBotsDirectly(Grid grid)
        {
            return grid.MyBots;
        }
    }
}
