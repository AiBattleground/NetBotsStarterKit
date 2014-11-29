using System.Collections.Generic;
using NetBots.WebModels;

namespace NetBotsClient.Ai
{
    public interface IRobot
    {
        IEnumerable<BotletMove> GetMoves(MoveRequest request);
    }
}
