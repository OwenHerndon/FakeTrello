using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.Models
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public TrelloUser Owner { get; set; }

        public List<List> Lists { get; set; } //1 to many (lists) relationship
    }
}