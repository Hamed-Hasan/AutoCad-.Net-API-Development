using autoCadApiDevelopment.Models;
using Microsoft.EntityFrameworkCore;

namespace autoCadApiDevelopment.Data
{
    public class AutoCadContext : DbContext
    {
        public AutoCadContext(DbContextOptions<AutoCadContext> options)
            : base(options)
        {
        }

        public DbSet<Drawing> Drawings { get; set; }
    }
}
