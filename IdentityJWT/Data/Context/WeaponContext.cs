using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WeaponAuthorization.Data
{
    public class WeaponContext : DbContext
    {
        public WeaponContext(DbContextOptions<WeaponContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Heroes");
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
        }
    }
}
