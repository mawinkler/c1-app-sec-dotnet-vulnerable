using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class RedirectController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var location = HttpContext.Request.Query["destination_url"].ToString();
            if (location.Length > 0)
            {
                return Redirect(location);
            }

            return View();
        }
    }
}
