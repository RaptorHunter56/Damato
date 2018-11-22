using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damato_API.DataBase
{
    class File
    {
        public File() { DateAdded = DateTime.Now; }

        [Key]
        [Column("Key")]
        public int ID { get; set; }
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Path { get; set; }
        [NotMapped]
        public string[] PathParts { get { return Path.Split('\\'); } }
        public DateTime DateAdded { get; set; }
    }
}
