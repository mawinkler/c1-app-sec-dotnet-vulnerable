using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    public class EFCoreController : Controller
    {
        private readonly MyContext _context;

        public EFCoreController(MyContext context)
        {
            _context = context;
            if (_context.Database.EnsureCreated())
            {
                var users = _context.Set<User>();
                users.Add(new User("Bob"));
                users.Add(new User("Greg"));
                users.Add(new User("Tony"));
                users.Add(new User("Muffin"));
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult Index([FromQuery(Name = "method")] string method)
        {
            if (method == null)
            {
                ViewBag.Method = "FromSqlRaw";
            }
            else
            {
                ViewBag.Method = method;
            }
            ViewBag.Users = new User[0];

            return View("Index");
        }

        [HttpGet]
        public IActionResult Perform(
            [FromQuery(Name = "method")] string method,
            [FromQuery(Name = "sql")] string sql)
        {
            ViewBag.Method = method;
            if (method == "FromSqlRaw")
            {
                ViewBag.Users = _context.Set<User>().FromSqlRaw(sql).ToList();
            }
            else
            {
                ViewBag.Users = _context.Set<User>().Where(user => user.Name == sql).ToList();
            }

            return View("Index");
        }
    }
}
