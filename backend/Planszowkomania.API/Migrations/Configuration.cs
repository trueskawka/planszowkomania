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

            AddGame(context, "Carcassonne", "carcassonne.jpg");
            AddGame(context, "Twilight Imperium", "twilight.jpg");
            AddGame(context, "Gra o tron", "thrones.jpg");
            AddGame(context, "Tzlokin", "tzolkin.jpg");
            AddGame(context, "�wiat dysku", "discworld.jpg");
            AddGame(context, "Osadnicy z Catanu", "catan.jpg");

            context.SaveChanges();
        }

        private void AddGame(AppDbContext context, string name, string image)
        {
            var game = new Game
            {
                Name = name,
                Image = image
            };
            context.Games.AddOrUpdate(g => g.Name, game);
        }
    }
}
