using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NetBots.Bot.Interface;

namespace NetBotsClient.Host.Controllers
{
    public class Bot : ApiController
    {
        // POST api/bot
        public IHttpActionResult Post(MoveRequest request)
        {
            INetBot robot = new Ai.Brain();
            var moves = robot.GetMoves(request);
            return Ok(moves);
        }
    }
}
