using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TryAspNetCoreMirgate.EfCore;
using TryAspNetCoreMirgate.Migrate;

namespace TryAspNetCoreMirgate.Controllers
{
    public class HomeController : Controller
    {
        //private static MyDbContext _lastMyDbContext;

        //private MyDbContext _context;

        //public HomeController(MyDbContext context)
        //{
        //    _context = context;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About([FromServices]MyDbContext context)
        {
            ViewData["Message"] = "Your application description page.";
            //if (_context != context)
            //    throw new InvalidOperationException("The two contexts were not the same value!");
            //if (_lastMyDbContext == context)
            //    throw new InvalidOperationException("The last contexts was the same value!");
            //_lastMyDbContext = context;

            return View(context.MyEntities.Count());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            throw new OutstandingMigrationException();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
