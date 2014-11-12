using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using NetBots.Web;
using NetBotsClient.Ai;
using NetBotsClient.Host.Models;

namespace NetBotsClient.Host.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BotController : ApiController
    {
        [HttpPost]
        [Route("{botName}")]
        public IHttpActionResult GetMoveBerserker(string botName, MoveRequest request)
        {
            IRobot robot = BotRepository.Instance.GetBot(botName);
            var moves = robot.GetMoves(request);
            return Ok(moves);
        }
    }
}
