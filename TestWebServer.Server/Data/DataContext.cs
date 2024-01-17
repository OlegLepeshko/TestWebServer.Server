using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TestWebServer.Shared;

namespace TestWebServer.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<SubElement> SubElements { get; set; }  
    }
}
