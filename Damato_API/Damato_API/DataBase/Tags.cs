using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Damato_API.DataBase
{
    public class Tag
    {
        public Tag() { DateAdded = DateTime.Now; }
        [Key]
        public int Key { get; set; }
        public string _Tag { get; set; }
        public DateTime DateAdded { get; set; }
    }
}