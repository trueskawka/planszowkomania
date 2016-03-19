using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API.Services
{
    public class TableService
    {
        private readonly AppDbContext _context;

        public TableService()
        {
            _context = new AppDbContext();
        }

        public void Join(User user, Table table)
        {
            if (table == null)
            {
                throw new Exception("Wrong tableId");
            }

            if (table.Participations.Any(p => p.Participant.Id == user.Id))
            {
                throw new Exception("Already joined");
            }

            var participation = new Participation
            {
                Participant = user,
                Status = AcceptanceStatus.Pending,
                Table = table
            };
            _context.Participations.Add(participation);
            _context.SaveChanges();
        }
    }
}