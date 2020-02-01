using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cykelfest_web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using cykelfest;

namespace cykelfest_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Result(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var outputFilePath = new Solver().CreateOutputFile(filePath);

            var fileContent = System.IO.File.ReadAllBytes(outputFilePath);

            return File(fileContent, "application/zip", "result.zip");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
