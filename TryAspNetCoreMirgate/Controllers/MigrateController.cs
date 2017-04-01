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
            var errorMessage = _options.MigrateDatabase(MySeedDatabase);
            return View((object)errorMessage);
        }

        private string MySeedDatabase(MyDbContext context)
        {
            context.MyEntities.Add(new MyEntity());
            return null;
        }
    }
}