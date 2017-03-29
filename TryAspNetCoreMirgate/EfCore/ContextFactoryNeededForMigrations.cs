// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TryAspNetCoreMirgate.EfCore
{

    /// <summary>
    /// This class is needed to allow Add-Migrations command to be run. 
    /// It is not a good implmentation as it has to have a constant connection sting in it
    /// but it is Ok on a local machine, which is where you want to run the command
    /// see https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#using-idbcontextfactorytcontext
    /// </summary>
    public class ContextFactoryNeededForMigrations : IDbContextFactory<MyDbContext>
    {
        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=TryAspNetCoreMigrate;Trusted_Connection=True;MultipleActiveResultSets=true";

        public MyDbContext Create(DbContextFactoryOptions options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            return new MyDbContext(optionsBuilder.Options);
        }
    }
}