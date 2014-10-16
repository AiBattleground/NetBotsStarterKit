using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NetBots.Bot.Interface;
using NetBotsClient.Ai;

namespace NetBotsClient.Host.Controllers
{
    public class BotController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetMove(MoveRequest request)
        {
            INetBot robot = new RandomBot();
            var moves = robot.GetMoves(request);
            return Ok(moves);
        }
    }
}
