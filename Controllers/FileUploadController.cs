using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace app.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            string savePath = "wwwroot/uploads/" + file.FileName;
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            ViewBag.UploadMessage = "Successfully uploaded: " + file.FileName;

            return View("Index");
        }
    }
}
