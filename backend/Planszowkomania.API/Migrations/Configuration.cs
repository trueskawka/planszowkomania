using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Planszowkomania.API.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Planszowkomania.API.AppDbContext context)
        {
            var manager = new UserManager<User>(new UserStore<User>(new AppDbContext()));

            var user = new User()
            {
                UserName = "test",
                Email = "test@test.com",
                EmailConfirmed = true,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "123qwe");
        }
    }
}
