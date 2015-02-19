using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBots.WebModels;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    class Trooper : Soldier
    {
        public Trooper(Square square) : base(square)
        {
        }

        public override BotletMove GetMove()
        {
            var targetSquare = GetEnergySquareImClosestTo();
            if (targetSquare == null)
            {
                if (Grid.EnemySpawnActive)
                {
                    targetSquare = Grid.EnemySpawn;
                }
                else
                {
                    targetSquare = FindEmpySpace();
                }
                
            }
            return GetMoveToTarget(targetSquare);
        }

        private Square FindEmpySpace()
        {
            Square target = null;
            var farthest = int.MinValue;
            foreach (var square in Grid)
            {
                var distanceToBot = square.DistanceToClosest(x => x == SquareType.PlayerBot || x == SquareType.EnemyBot);
                if (distanceToBot > farthest)
                {
                    farthest = distanceToBot;
                    target = square;
                }
            }
            return target;
        }


        private Square GetEnergySquareImClosestTo()
        {
            var energySquares = Grid.Where(x => x == SquareType.Energy).OrderBy(x => x.DistanceFrom(Square));
            foreach (var energySquare in energySquares)
            {
                var closestGoodGuySquare = energySquare.ClosestWhere(x => x == SquareType.PlayerBot);
                var closestGgDistance = closestGoodGuySquare.DistanceFrom(energySquare);
                var myDistance = Square.DistanceFrom(energySquare);
                if (myDistance == 1)
                {
                    return Square;
                }
                if (myDistance <= closestGgDistance)
                {
                    return energySquare;
                }
            }
            return null;
        }
    }
}
