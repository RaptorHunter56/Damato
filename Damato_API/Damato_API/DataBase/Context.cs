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
    }
}
