using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Planszowkomania.API.Models.Results;

namespace Planszowkomania.API.Controllers
{
    public class GameController : ControllerBase
    {

        [HttpGet]
        public IHttpActionResult All()
        {
            var context = new AppDbContext();
            var games = context.Games.ToList().Select(g => new GameResult(g)).ToList();
            return Ok(games);
        }


    }
}
