using System.Collections.Generic;
using System.Linq;
using NetBots.Web;
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


        //This is for demonstration. Here you can see that the grid is a strongly typed two dimensional array.
        //You can access any square through it's X and Y coordinates. The two alternate versions of GetMyBots
        //below are commented out, but do the exact same thing.
        private List<Square> GetMyBots(Grid grid)
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

        //This does the exact same thing as GetMyBots() but does it through a foreach loop.
        //This is to demonstrate that the Grid is enumerable.
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

        //This also does the exact same thing as GetMyBots(), but shows that 
        //there is a simple property on the grid that will return the bots as well.
        private List<Square> GetMyBotsDirectly(Grid grid)
        {
            return grid.MyBots;
        }
    }
}
