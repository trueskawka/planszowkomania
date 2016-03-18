using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Planszowkomania.API.Models.Front;

namespace Planszowkomania.API.Controllers
{
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IHttpActionResult Details()
        {
            return Ok(new
            {
                UserName = User.Identity.Name
            });
        }
    }
}
