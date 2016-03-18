using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planszowkomania.API.Models.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}