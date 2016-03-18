using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API.Models.Results
{
    public class TableResult
    {
        public string Description { get { return _table.Description; } }
        public DateTime EventDate { get { return _table.EventDate; } }
        public string LocalizationName { get { return _table.LocalizationName; } }
        public string City { get { return _table.City; } }
        public GameDifficulty Difficulty { get { return _table.Difficulty; } }
        public AggresionLevel AggresionLevel { get { return _table.AggresionLevel; } }
        public int UsersRequired { get { return _table.UsersRequired; } }
        public GameResult GameId { get { return new GameResult(_table.Game); } }
        public UserDetails Owner { get { return new UserDetails(_table.Owner); } }
        public List<ParticipantDetails> Participants { get { return _table.Participations.Select(p => new ParticipantDetails(p)).ToList(); } } 


        private readonly Table _table;

        public TableResult(Table table)
        {
            _table = table;
        }
    }
}