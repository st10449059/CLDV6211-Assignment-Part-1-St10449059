using System.Diagnostics;
using CLDV6211_Assignment_Part_1_St10449059.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLDV6211_Assignment_Part_1_St10449059.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
