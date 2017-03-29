using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using TryAspNetCoreMirgate.EfCore;

[assembly: InternalsVisibleTo("Test")]

namespace TryAspNetCoreMirgate.Migrate
{
    public class MyDbContextHandleMigrations : MyDbContext
    {
        private static bool _cachedOutstandingMigrations = true;

        public MyDbContextHandleMigrations(DbContextOptions<MyDbContext> options) 
            : base(options)
        {
            if (HasOutstandingMigrations(options))
                throw new OutstandingMigrationException();
        }

        public static void MigrateDatabase(DbContextOptions<MyDbContext> options, Action<MyDbContext> seedDatabase = null)
        { 
            using (var context = new MyDbContext(options))
            {
                if (!context.Database.GetPendingMigrations().Any())
                    throw new InvalidOperationException("There were no migrations to apply.");

                context.Database.Migrate();
                seedDatabase?.Invoke(context);
            }
            _cachedOutstandingMigrations = false;
        }

        /// <summary>
        /// This is a safe method that Unit Test can call to clear the cache state
        /// </summary>
        internal static void UnitTestClearCache()
        {
            _cachedOutstandingMigrations = true;
        }

        private static bool HasOutstandingMigrations(DbContextOptions<MyDbContext> options)
        {
            if (_cachedOutstandingMigrations)
            {
                //We either don't know, or there are migrations, so we have to check every time
                using (var context = new MyDbContext(options))
                {
                    _cachedOutstandingMigrations = context.Database.GetPendingMigrations().Any();
                }
            }
            return _cachedOutstandingMigrations;
        }
    }
}