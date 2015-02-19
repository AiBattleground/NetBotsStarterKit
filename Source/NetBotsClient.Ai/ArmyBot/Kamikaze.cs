using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBots.WebModels;
using NetBotsClient.Models;

namespace NetBotsClient.Ai.ArmyBot
{
    public class Kamikaze : Soldier
    {
        public Kamikaze(Square square) : base(square)
        {
        }

        public override BotletMove GetMove()
        {
            var target = Square.ClosestWhere(x => x == SquareType.EnemyBot);
            return GetMoveToTarget(target);
        }
    }
}
