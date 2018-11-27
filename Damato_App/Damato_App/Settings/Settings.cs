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
        public ApplicationSettings()
        {
            DownLoadedSettings = new DownLoadedSettings();
        }
        public LoginSettings LoginSettings { get; set; }
        public SearchSettings SearchSettings { get; set; }
        public DownLoadedSettings DownLoadedSettings { get; set; }
    }

    public class SearchSettings
    {
        public SearchSettings() { ReturnAmount = 10; }
        public int ReturnAmount { get; set; }
    }

    public class DownLoadedSettings
    {
        public DownLoadedSettings() { DownLoaded = new List<string>(); DownLoadFileLocation = ""; }
        public List<string> DownLoaded { get; set; }
        public string DownLoadFileLocation { get; set; }
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
