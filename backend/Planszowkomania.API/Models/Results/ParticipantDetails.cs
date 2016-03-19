using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planszowkomania.API.Models.Entities;

namespace Planszowkomania.API.Models.Results
{
    public class ParticipantDetails
    {
        public string Id { get { return _participation.ParticipantId; } }
        public string UserName { get { return _participation.Participant.UserName; } }
        public string Status { get { return _participation.Status.ToString(); } }

        private readonly Participation _participation;

        public ParticipantDetails(Participation participation)
        {
            _participation = participation;
        }
    }
}