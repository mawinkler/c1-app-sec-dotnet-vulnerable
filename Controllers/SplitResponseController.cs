using System;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class SplitResponseController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Perform()
        {
            var value = HttpContext.Request.Form["custom_header"];

            HttpContext.Response.Headers.Add("custom_header", value);

            return Json(new { result = "Ok" });
        }
    }
}
