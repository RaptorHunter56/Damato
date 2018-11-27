using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damato_API.DataBase
{
    public class DamatoDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<File> Files { get; set; }

        public DamatoDBContext() : base("name=damatoapidbserver")
        {
        }
    }
}
