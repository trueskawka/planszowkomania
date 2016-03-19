using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API.Models.Results
{
    public class TableResult
    {
        public int Id { get { return _table.Id; } }
        public string Description { get { return _table.Description; } }
        public DateTime EventDate { get { return _table.EventDate; } }
        public string LocalizationName { get { return _table.LocalizationName; } }
        public string City { get { return _table.City; } }
        public GameDifficulty Difficulty { get { return _table.Difficulty; } }
        public AggresionLevel AggresionLevel { get { return _table.AggresionLevel; } }
        public int UsersRequired { get { return _table.UsersRequired; } }

        public GameResult Game
        {
            get
            {
                if (_table.Game == null)
                {
                    return new GameResult(_context.Games.Find(_table.GameId));
                }
                return new GameResult(_table.Game);
            }
        }

        public UserDetails Owner
        {
            get
            {
                if (_table.Owner == null)
                {
                    return new UserDetails(_context.Users.Find(_table.OwnerId));
                }
                return new UserDetails(_table.Owner);
            }
        }

        public List<ParticipantDetails> Participants
        {
            get
            {
                var participations = _context.Participations
                    .Include(p => p.Participant)
                    .Where(p => p.TableId == _table.Id)
                    .ToList();
                return participations.Select(p => new ParticipantDetails(p)).ToList();
            }
        }

        private readonly Table _table;
        private readonly AppDbContext _context;

        public TableResult(Table table)
        {
            _table = table;
            _context = AppDbContext.Create();
        }
    }
}