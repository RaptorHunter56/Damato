using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damato_App.Settings
{
    public class ApplicationSettings
    {
        public LoginSettings LoginSettings { get; set; }
        //public TargetServer TargetServer { get; set; }
    }

    public class LoginSettings
    {
        public string UserName { get; set; }
        [JsonIgnore]
        public string password { get { return Cipher.Decrypt(Password, "Pa55w0rd"); } set { Password = Cipher.Encrypt(value, "Pa55w0rd"); } }
        public string Password { get; set; }
        public bool KeepLogdIn { get; set; }
    }
}
