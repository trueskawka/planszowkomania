using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API.Models.Results
{
    public class GameResult
    {
        public int Id
        {
            get { return _game.Id; }
        }
        public string Name
        {
            get { return _game.Name; }
        }

        public string Image
        {
            get { return _game.Image; }
        }

        public string Description
        {
            get { return _game.Description; }
        }

        private readonly Game _game;
        public GameResult(Game game)
        {
            _game = game;
        }
    }
}