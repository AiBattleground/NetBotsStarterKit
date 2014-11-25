using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBots.Web;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    class Trooper : Soldier
    {
        public Trooper(Square square, Grid grid) : base(square, grid)
        {
        }

        public override BotletMove GetMove()
        {
            var targetSquare = GetEnergySqaSquareImClosestTo();
            if (targetSquare == null)
            {
                targetSquare = Square.ClosestWhere(x => x == SquareType.Energy && x.DistanceFrom(Square) < 20);
            }
            if (targetSquare == null)
            {
                targetSquare = Grid.EnemySpawn;
            }
            return GetMoveToTarget(targetSquare);
        }


        private Square GetEnergySqaSquareImClosestTo()
        {
            foreach (var energySquare in Grid.Where(x => x == SquareType.Energy))
            {
                var closestGoodGuySquare = energySquare.ClosestWhere(x => x == SquareType.PlayerBot);
                var closestGgDistance = closestGoodGuySquare.DistanceFrom(energySquare);
                var myDistance = Square.DistanceFrom(energySquare);
                if (myDistance == closestGgDistance)
                {
                    return energySquare;
                }
            }
            return null;
        }
    }
}
