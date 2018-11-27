using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damato_API.DataBase
{
    public class File
    {
        public File() { DateAdded = DateTime.Now; }

        [Key]
        [Column("Key")]
        public int ID { get; set; }
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Path { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string[] PathParts { get { return Path.Split('\\'); } }
        public DateTime DateAdded { get; set; }

        [NotMapped]
        [JsonIgnore]
        public int RLevel { get; set; }
        [NotMapped]
        [JsonIgnore]
        public int WLevel { get; set; }
        [NotMapped]
        [JsonIgnore]
        public int DLevel { get; set; }
        public string Level
        {
            get { return $"{RLevel.ToString()},{WLevel.ToString()},{DLevel.ToString()}"; }
            set
            {
                string[] levels = value.Split(',');
                RLevel = Int32.Parse(levels[0]);
                WLevel = Int32.Parse(levels[1]);
                DLevel = Int32.Parse(levels[2]);
            }
        }

        public User User { get; set; }
        
        public List<Tag> MainTags { get; set; }
    }

    public class TFile
    {
        public string Path { get; set; }
        public Byte[] File { get; set; }
    }
}
