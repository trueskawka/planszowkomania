using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API.Models.Results
{
    public class UserDetails
    {
        public string Id { get { return _user.Id; } }
        public string UserName { get { return _user.UserName; }}
        public DateTime JoinDate { get { return _user.JoinDate; } }

        private readonly User _user;

        public UserDetails(User user)
        {
            _user = user;
        }
    }
}