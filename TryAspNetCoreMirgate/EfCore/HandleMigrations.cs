using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TryAspNetCoreMirgate.Migrate;

namespace TryAspNetCoreMirgate.EfCore
{
    public static class HandleMigrations
    {
        private static bool _cachedOutstandingMigrations = true;

        public static void ThrowExceptionIfPendingMigrations(this DbContext context, bool adminAccess)
        {
            if (!adminAccess && HasOutstandingMigrations(context))
                throw new OutstandingMigrationException();
        }

        public static string MigrateDatabase(this DbContextOptions<MyDbContext> options, Func<MyDbContext, string> seedDatabase = null)
        {
            string errorMessage = null;
            using (var context = new MyDbContext(options, true))
            {
                if (!context.Database.GetPendingMigrations().Any())
                    throw new InvalidOperationException("There were no migrations to apply.");

                context.Database.Migrate();
                if (seedDatabase != null)
                {
                    errorMessage = seedDatabase.Invoke(context);
                    if (errorMessage == null)
                        context.SaveChanges();
                }
            }
            _cachedOutstandingMigrations = false;

            return errorMessage;
        }

        /// <summary>
        /// This is a safe method that Unit Test can call to clear the cache state
        /// </summary>
        internal static void UnitTestClearCache()
        {
            _cachedOutstandingMigrations = true;
        }

        private static bool HasOutstandingMigrations(DbContext context)
        {
            if (_cachedOutstandingMigrations)
            {
                //We either don't know, or there are migrations, so we have to check every time
                _cachedOutstandingMigrations = context.Database.GetPendingMigrations().Any();
            }
            return _cachedOutstandingMigrations;
        }
    }
}