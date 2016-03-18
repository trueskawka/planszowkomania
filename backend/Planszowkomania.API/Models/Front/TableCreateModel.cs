using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API.Models.Front
{
    public class TableCreateModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public string LocalizationName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public GameDifficulty Difficulty { get; set; }
        [Required]
        public AggresionLevel AggresionLevel { get; set; }
        [Required]
        public int UsersRequired { get; set; }
        [Required]
        public int GameId { get; set; }

        public Table GenerateTable(string userId)
        {
            var context = new AppDbContext();
            var user = context.Users.Find(userId);
            var table = context.Tables.Create();

            table.Description = Description;
            table.EventDate = EventDate;
            table.LocalizationName = LocalizationName;
            table.City = City;
            table.Difficulty = Difficulty;
            table.AggresionLevel = AggresionLevel;
            table.UsersRequired = UsersRequired;
            table.Game = context.Games.Find(GameId);
            table.Owner = user;
            table.Participations.Add(new Participation
            {
                Participant = user,
                Table = table,
                Status = AcceptanceStatus.Accepted
            });

            context.Tables.Add(table);

            context.SaveChanges();

            return table;
        }
    }
}