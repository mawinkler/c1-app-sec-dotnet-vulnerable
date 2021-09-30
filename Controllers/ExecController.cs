using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class ExecController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var filter = HttpContext.Request.Query["filter"].ToString();
            if (filter.Length > 0)
            {
                try
                {
                    var proc = new Process 
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "/bin/sh",
                            Arguments = "-c \"ls -l | grep '" + filter + "';\"",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        }
                    };
                    proc.Start();
                    proc.WaitForExit();
                    ViewBag.ExecOutput = proc.StandardError.ReadToEnd() +
                        "\n" + proc.StandardOutput.ReadToEnd();
                }
                catch (InvalidOperationException e)
                {
                    ViewBag.ExecOutput = "Error: " + e.ToString();
                }
            }

            return View();
        }
    }
}
