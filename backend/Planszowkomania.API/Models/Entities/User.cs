using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Planszowkomania.API.Models.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
        }

        [Required]
        public DateTime JoinDate { get; set; }
        [Required]
        public string Image { get; set; }
    }
}