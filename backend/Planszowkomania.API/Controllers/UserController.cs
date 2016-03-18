﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Planszowkomania.API.Models.Front;
using Planszowkomania.API.Models.Results;

namespace Planszowkomania.API.Controllers
{
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IHttpActionResult Details()
        {
            var user = GetUser();
            return Ok(new UserDetails(user));
        }
    }
}
