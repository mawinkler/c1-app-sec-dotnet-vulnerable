using System;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class StatusCodeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var status = HttpContext.Request.Query["status"].ToString();
            if (status.Length > 0)
            {
                HttpContext.Response.StatusCode = Convert.ToInt32(status.ToString());
            }

            return View();
        }
    }
}
