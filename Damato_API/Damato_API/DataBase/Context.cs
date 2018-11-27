using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damato_API.DataBase
{
    
    public class DAMContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public static string Password()
        {
            try
            {
                using (StreamReader r = new StreamReader(@"D:\home\site\wwwroot\bin\DBSettings.json"))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<string>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                }
            }
            catch(Exception e)
            {
                return null;
            } 
        }
        public DAMContext()
        {
            this.Database.Connection.ConnectionString = $"Data Source=damatoapidbserver.database.windows.net,1433;Initial Catalog=DamatoAPI_db;Persist Security Info=False;User ID=Damato;Password={Password()};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        }
    }
}
