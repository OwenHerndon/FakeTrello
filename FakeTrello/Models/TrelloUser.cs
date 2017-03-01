using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//entity is our object relational mapper (ORM)

namespace FakeTrello.Models
{
    public class TrelloUser
    {
        [Key]
        public int TrelloUserId { get; set; }  //primary key

        [MinLength(10)]
        [MaxLength(60)]
        public string Email { get; set; }
 
        [MaxLength(60)]
        public string Username { get; set; }

        [MaxLength(60)]
        public string FullName { get; set; }

        public ApplicationUser BaseUser { get; set; } //1 to 1 relationship

        public List<Board> Boards { get; set; } //1 to many (boards) relationship


    }
}