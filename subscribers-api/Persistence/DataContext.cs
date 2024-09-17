using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Subscriber> Subscribers { get; set; }
    }
}
