using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TryAspNetCoreMirgate.EfCore;

namespace TryAspNetCoreMirgate.Migrate
{
    public static class StartupDbContext
    {
        /// <summary>
        /// This will register the MyDbContext to go through the MyDbContextHandleMigrations context
        /// If you want to change the applicaton's DbContext then you need to:
        /// 1. Replace the type 'MyDbContext' in this code with your application's DbContext class type
        /// 2. Edit the class MyDbContextHandleMigrations to inherit from application's DbContext class
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="optionsAction"></param>
        /// <param name="contextLifetime"></param>
        public static void RegisterDbContextHandleMigrations(this IServiceCollection serviceCollection,
            Action<DbContextOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped) 
        {
            if (optionsAction == null)
                throw new ArgumentException(nameof(optionsAction));

            var builder = new DbContextOptionsBuilder<MyDbContext>(
                new DbContextOptions<MyDbContext>(new Dictionary<Type, IDbContextOptionsExtension>()));
            optionsAction.Invoke(builder);
            serviceCollection.AddSingleton(builder.Options);
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(MyDbContext), typeof(MyDbContextHandleMigrations), contextLifetime));
        }

    }
}