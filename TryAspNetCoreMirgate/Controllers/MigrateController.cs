using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryAspNetCoreMirgate.EfCore;
using TryAspNetCoreMirgate.Migrate;

namespace TryAspNetCoreMirgate.Controllers
{
    public class MigrateController : Controller
    {
        private readonly DbContextOptions<MyDbContext> _options;

        public MigrateController(DbContextOptions<MyDbContext> options)
        {
            _options = options;
        }

        public IActionResult Index()
        {
            MyDbContextHandleMigrations.MigrateDatabase(_options, MySeedDatabase);
            return View();
        }

        private void MySeedDatabase(MyDbContext context)
        {
            context.MyEntities.Add(new MyEntity());
            context.SaveChanges();
        }
    }
}