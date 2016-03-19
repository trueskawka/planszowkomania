using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Planszowkomania.API.Models.Entities;
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

        [HttpPost]
        public async Task<IHttpActionResult> Register(UserRegisterModel userRegisterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await AppUserManager.CreateAsync(new User
            {
                Email = userRegisterModel.Email,
                UserName = userRegisterModel.UserName,
                JoinDate = DateTime.UtcNow
            }, userRegisterModel.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(new
            {
                Message = "User registered."
            });
        }
    }
}
