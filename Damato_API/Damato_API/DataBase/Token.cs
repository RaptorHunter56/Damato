using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Damato_API.DataBase
{
    public class Token
    {
        public Token() { DateAdded = DateTime.Now; DateExpiered = DateTime.Now.AddMinutes(30); }
        [Key]
        [MaxLength(10)]
        [Index(IsUnique = true)]
        public string _Token { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateExpiered { get; set; }
        
        public User User { get; set; }
    }
}