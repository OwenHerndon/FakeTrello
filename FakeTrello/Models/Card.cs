using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [MaxLength(60)]
        public string Title { get; set; }

        public string Description { get; set; }

        public List ListCardBelongsTo { get; set; }
    }
}