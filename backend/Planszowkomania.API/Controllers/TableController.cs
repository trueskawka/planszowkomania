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
        private readonly AppDbContext _context;
        public TableController()
        {
            _tableService = new TableService();
            _context = new AppDbContext();
        }

        [HttpGet]
        public IHttpActionResult Details(int id)
        {
            var table = _context.Tables.Find(id);
            if (table == null)
            {
                return BadRequest("Table doesn't exist");
            }
            return Ok(new TableResult(table));
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var tables = _context.Tables
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

            var tables = _context.Tables
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

            return Ok(new
            {
                Id = table.Id
            });
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

            var table = _context.Tables
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

            return Ok(new
            {
                Message = "Successfully joined table"
            });
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Accept(TableAcceptUser tableAcceptUser)
        {
            if (!ModelState.IsValid || tableAcceptUser == null)
            {
                return BadRequest();
            }

            try
            {
                _tableService.HandleUser(User.Identity.GetUserId(), tableAcceptUser.UserId, tableAcceptUser.TableId, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Reject(TableAcceptUser tableAcceptUser)
        {
            if (!ModelState.IsValid || tableAcceptUser == null)
            {
                return BadRequest();
            }

            try
            {
                _tableService.HandleUser(User.Identity.GetUserId(), tableAcceptUser.UserId, tableAcceptUser.TableId, false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Leave(int id)
        {
            try
            {
                _tableService.Leave(User.Identity.GetUserId(), id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
