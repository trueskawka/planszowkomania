using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Planszowkomania.API.Models.Entities;
using Planszowkomania.API.Services;

namespace Planszowkomania.API.Controllers
{
    public class ControllerBase : ApiController
    {
        private readonly AppUserManager _appUserManager = null;

        protected AppUserManager AppUserManager
        {
            get
            {
                return _appUserManager ?? Request.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        protected User GetUser()
        {
            var dbContext = new AppDbContext();
            return dbContext.Users.Find(User.Identity.GetUserId());
        }
        
        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}