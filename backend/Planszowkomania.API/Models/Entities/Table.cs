using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Planszowkomania.API.Models.Entities
{
    public enum GameDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    public enum AggresionLevel
    {
        Low,
        Medium,
        High
    }

    public class Table
    {
        public Table()
        {
            Participations = new List<Participation>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string LocalizationName { get; set; }
        public string City { get; set; }
        public GameDifficulty Difficulty { get; set; }
        public AggresionLevel AggresionLevel { get; set; }
        public int UsersRequired { get; set; }

        public ICollection<Participation> Participations { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }

    }
}