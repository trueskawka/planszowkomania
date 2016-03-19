using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Planszowkomania.API.Models.Entities;
using Planszowkomania.API.Models.Front;
using Planszowkomania.API.Models.Results;
using Planszowkomania.API.Services;

namespace Planszowkomania.API.Controllers
{
    public class TableController : ControllerBase
    {
        private readonly TableService _tableService;
        public TableController()
        {
            _tableService = new TableService();
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var context = new AppDbContext();
            var tables = context.Tables
                .Include(g => g.Game)
                .Include(g => g.Owner)
                .Include(g => g.Participations)
                .ToList()
                .Select(t => new TableResult(t)).ToList();
            return Ok(tables);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult UserTables()
        {
            var user = GetUser();
            var context = new AppDbContext();
            
            var tables = context.Tables
                .Include(g => g.Game)
                .Include(g => g.Owner)
                .Include(g => g.Participations)
                .ToList();

            var tableResults = (from t in tables from p in t.Participations where p.ParticipantId == user.Id select new TableResult(t)).ToList();

            return Ok(tableResults);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(TableCreateModel tableCreateModel)
        {
            if (!ModelState.IsValid || tableCreateModel == null)
            {
                return BadRequest();
            }
            var table = tableCreateModel.GenerateTable(User.Identity.GetUserId());

            return Ok(table.Id);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Join(TableJoinModel tableJoinModel)
        {
            if (!ModelState.IsValid || tableJoinModel == null)
            {
                return BadRequest();
            }

            var user = GetUser();
            var context = new AppDbContext();

            //var table = context.Tables.Find(tableJoinModel.TableId);
            var table = context.Tables
                .Include(t => t.Game)
                .Include(t => t.Owner)
                .Include(t => t.Participations)
                .FirstOrDefault(t => t.Id == tableJoinModel.TableId);

            try
            {
                _tableService.Join(user, table);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

    }
}
