using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public DAMContext()
        {
            this.Database.Connection.ConnectionString = $"Data Source=damatoapidbserver.database.windows.net,1433;Initial Catalog=DamatoAPI_db;Persist Security Info=False;User ID=Damato;Password={ps};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        }
    }
}
