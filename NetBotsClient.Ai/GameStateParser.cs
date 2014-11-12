using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetBotsClient.Models;
using NetBots.Web;

namespace NetBotsClient.Ai
{
    public class GameStateParser
    {
        public static GameBoard Parse(MoveRequest request)
        {
            var gameState = request.State;
            Grid myGrid = GetGrid(request);
            var game = new GameBoard
            {
                MyEnergy = request.Player.ToLower() == "p1" ? gameState.P1.Energy : gameState.P2.Energy,
                EnemyEnergy = request.Player.ToLower() == "p2" ? gameState.P1.Energy : gameState.P2.Energy,
                Turn = gameState.TurnsElapsed,
                TurnLimit = gameState.MaxTurns,
                Grid = myGrid
            };
            return game;
        }

        public static Grid GetGrid(MoveRequest request)
        {
            var me = request.Player.ToLower() == "p1" ? request.State.P1 : request.State.P2;
            var enemy = request.Player.ToLower() == "p2" ? request.State.P1 : request.State.P2;
            var myDic = GetCharDic(request);
            var myGrid = new Grid(request.State.Cols, request.State.Rows, me.Spawn, enemy.Spawn, x =>
            {
                char myChar = request.State.Grid[x.LineIndex];
                x.SquareType = myDic[myChar];
            });
            myGrid.MySpawnActive = !me.SpawnDisabled;
            myGrid.EnemySpawnActive = !enemy.SpawnDisabled;
            return myGrid;
        }


        public static Dictionary<char, SquareType> GetCharDic(MoveRequest request)
        {
            var myDic = new Dictionary<char, SquareType>();

            char myChar = request.Player.ToLower() == "p1" ? '1' : '2';
            char enemyChar = request.Player.ToLower() == "p2" ? '1' : '2';

            myDic.Add(myChar, SquareType.PlayerBot);
            myDic.Add(enemyChar, SquareType.EnemyBot);
            myDic.Add('*', SquareType.Energy);
            myDic.Add('x', SquareType.DeathMarker);
            myDic.Add('X', SquareType.DeathMarker);
            myDic.Add('.', SquareType.Empty);

            return myDic;
        } 

    }
}
