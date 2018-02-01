using Microsoft.AspNetCore.Mvc;

namespace ModernNotes.Controllers
{
    /// <summary>Home page controller</summary>
    public class HomeController : Controller{ 

        /// <summary>Index page</summary>
        public IActionResult Index(){
            ViewData["Title"] = "Modern Notes";
            return View();
        }
    }
}