using System.Collections.Generic;
using NetBots.Web;

namespace NetBotsClient.Ai
{
    public interface IRobot
    {
        IEnumerable<BotletMove> GetMoves(MoveRequest request);
    }
}
