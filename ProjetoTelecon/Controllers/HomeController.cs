using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoTelecon.Data;
using ProjetoTelecon.Models;


namespace ProjetoTelecon.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            //var userId = Convert.ToInt32(TempData["UserId"]);
            var userType = TempData["UserType"];

            //ViewBag.LogedUser = _context.Users.Where(w => w.UserId == userId).SingleOrDefault();



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}