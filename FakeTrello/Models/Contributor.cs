using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTrello.Models
{
    public class Contributor
    {

        public List<TrelloUser> TrelloUser { get; set; }

        public List<Card> Cards { get; set; }

    }
}