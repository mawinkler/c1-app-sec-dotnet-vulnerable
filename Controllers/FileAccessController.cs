using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class FileAccessController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var filename = HttpContext.Request.Query["filename"].ToString();
            if (filename.Length > 0)
            {
                ViewBag.FileContent = System.IO.File.ReadAllText(filename);
            }

            return View();
        }
    }
}
