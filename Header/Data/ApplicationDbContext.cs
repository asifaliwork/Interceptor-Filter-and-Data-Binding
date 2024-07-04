using Header.Models;
using Microsoft.EntityFrameworkCore;

namespace Header.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base (options)
        {

       
            
        }
        public DbSet<Book> books { get; set; }

        public DbSet<Authors> authors { get; set; }





    }
}
