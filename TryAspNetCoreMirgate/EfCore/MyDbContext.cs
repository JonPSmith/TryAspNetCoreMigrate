using Microsoft.EntityFrameworkCore;

namespace TryAspNetCoreMirgate.EfCore
{
    public class MyDbContext : DbContext
    {
        public DbSet<MyEntity> MyEntities { get; set; }

        public MyDbContext(
            DbContextOptions<MyDbContext> options, bool ignoreMigations = false)
            : base(options)
        {
            this.ThrowExceptionIfPendingMigrations(ignoreMigations);
        }     
    }
}