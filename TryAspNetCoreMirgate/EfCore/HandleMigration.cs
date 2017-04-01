using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TryAspNetCoreMirgate.EfCore
{
    public static class HandleMigration
    {
        public static string MigrateDatabase(this DbContextOptions<MyDbContext> options, Func<MyDbContext, string> seedDatabase = null)
        {
            using (var context = new MyDbContext(options, true))
            {
                if (!context.Database.GetPendingMigrations().Any())
                    throw new InvalidOperationException("There were no migrations to apply.");

                context.Database.Migrate();
                if (seedDatabase == null) return null; //No seed to run, so 

                var errorMessage = seedDatabase.Invoke(context);
                if (errorMessage == null)
                    context.SaveChanges();

                return errorMessage;
            }
        }
    }
}