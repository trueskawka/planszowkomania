using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string ParticipantId { get; set; }
        [ForeignKey("ParticipantId")]
        public User Participant { get; set; }
        public int TableId { get; set; }
        [ForeignKey("TableId")]
        public Table Table { get; set; }
    }
}