using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Planszowkomania.API.Models.Front
{
    public class TableAcceptUser
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int TableId { get; set; }
    }
}