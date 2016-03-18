using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Planszowkomania.API.Models.Front;

namespace Planszowkomania.API.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (userLoginModel.UserName == "test" && userLoginModel.Password == "123qwe")
            {
                return Ok(new
                {
                    Token = "9384jw9vpv95g983",
                    ExpiresIn = 60*60*24*30
                });
            }
            else
            {
                return Conflict();
            }
        }

        [HttpPost]
        public IHttpActionResult Register(UserRegisterModel userRegisterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
