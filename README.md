# TryAspNetCoreMigrate

This is my attempt at a solution for Migration on a web site running multiple instances.

## Things to look at

The key files are all in the directory 
[../TryAspCoreMigrate/Migrate](https://github.com/JonPSmith/TryAspNetCoreMigrate/tree/master/TryAspNetCoreMirgate/Migrate)

The Startup->ConfigureServices method calls 

```c#
services.AddMvc(options => options.Filters.Add(typeof(MigrateExceptionFilter)));
services.RegisterDbContextHandleMigrations(options => options.UseSqlServer(connection));
```

... which sets everything up.

## Controllers->Actions

1. `Home->About` tries to use the application's DbContext, `MyDbContext`.
If migrations are pending then it throws an `OutstandingMigrationsException` 
which is picked up by the `MigrateExceptionFilter` and shows the view `Shared->DatabaseError`
2. In the `DatabaseError` view is a link to the action `Migrate->Index`, which 
calls `MyDbContextHandleMigrations.MigrateDatabase` that calls `context.Database.Migrate(),
with a optional seed database method

## Things that I would like to improve, but not sure how

* The `RegisterDbContextHandleMigrations` method has to be edited to put in your DbContext.
* The `MyDbContextHandleMigrations` has to be edited to inhert from your DbContext

It feels like a should use a factory pattern, but I can't see quite how to do that and make everything generic.
Any help or suggestions welcome!