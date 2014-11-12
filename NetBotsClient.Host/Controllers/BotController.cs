using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using NetBots.Bot.Interface;
using NetBots.Web;
using NetBotsClient.Ai;

namespace NetBotsClient.Host.Controllers
{
    [EnableCors("*", "*", "*")]
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
