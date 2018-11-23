using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damato_App.DataBase
{
    public class User
    {
        public User() { DateAdded = DateTime.Now; }
        [Key]
        [Column("Key")]
        public int ID { get; set; }
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string PasswordDecrypted { get { return Cipher.Decrypt(Password, "Pa55w0rd"); } set { Password = Cipher.Encrypt(value, "Pa55w0rd"); } }
        [JsonIgnore]
        public string Password { get; set; }
        public int Level { get; set; }
        [JsonIgnore]
        public DateTime DateAdded { get; set; }
    }
}
