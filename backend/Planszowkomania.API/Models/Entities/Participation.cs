using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planszowkomania.API.Models.Entities
{
    public enum AcceptanceStatus
    {
        Accepted,
        Rejected,
        Pending
    }

    public class Participation
    {
        public int Id { get; set; }
        public AcceptanceStatus Status { get; set; }

        public User Participant { get; set; }
        public Table Table { get; set; }
    }
}