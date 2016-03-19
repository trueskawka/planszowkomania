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

            if (table.Participations.Any(p => p.ParticipantId == user.Id))
            {
                throw new Exception("Already joined");
            }

            var participation = new Participation
            {
                ParticipantId = user.Id,
                Status = AcceptanceStatus.Pending,
                TableId = table.Id
            };

            _context.Participations.Add(participation);
            _context.SaveChanges();
        }

        public void HandleUser(string ownerId, string userId, int tableId, bool accept)
        {
            var participation = _context.Participations.FirstOrDefault(p => p.ParticipantId == userId && p.TableId == tableId && p.Table.OwnerId == ownerId);
            if (participation == null)
            {
                throw new Exception("User didn't participate in table");
            }

            participation.Status = accept ? AcceptanceStatus.Accepted : AcceptanceStatus.Rejected;
            _context.SaveChanges();
        }

        public void Leave(string userId, int tableId)
        {
            var participation = _context.Participations.FirstOrDefault(p => p.ParticipantId == userId && p.TableId == tableId);
            if (participation == null)
            {
                throw new Exception("User didn't participate in table");
            }

            _context.Participations.Remove(participation);
            _context.SaveChanges();
        }
    }
}