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


            var game = new Game
            {
                Name = "Catan",
                Image = "http://image.ceneo.pl/data/products/1719251/f-osadnicy-z-catanu.jpg?=69344"
            };

            context.Games.AddOrUpdate(g => g.Name, game);

            context.SaveChanges();
        }
    }
}
