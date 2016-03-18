using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Participation> Participations { get; set; }
        public virtual DbSet<Game> Games { get; set; }

    }
}