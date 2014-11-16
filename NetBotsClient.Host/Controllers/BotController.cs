using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Ajax.Utilities;
using NetBots.Web;
using NetBotsClient.Ai;

namespace NetBotsClient.Host.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BotController : ApiController
    {
        [HttpPost]
        [Route("{botName}")]
        public IHttpActionResult GetMove(string botName, MoveRequest request)
        {
            var botKey = request.State.GameId + request.Player;
            IRobot robot = BotRegistry.GetBot(botName, botKey);
            if (robot != null)
            {
                try
                {
                    var moves = robot.GetMoves(request);
                    return Ok(moves);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Could not find bot named " + botName);
        }
    }
}
