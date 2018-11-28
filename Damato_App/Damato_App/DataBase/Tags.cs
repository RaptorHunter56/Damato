using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damato_App.DataBase
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
