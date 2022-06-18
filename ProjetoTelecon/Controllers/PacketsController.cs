using Microsoft.AspNetCore.Mvc;
using ProjetoTelecon.Data;

namespace ProjetoTelecon.Controllers
{
    public class PacketsController : Controller
    {
        private readonly Context _context;
        public PacketsController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Service = _context.Packets
                .Where(w => w.Active == true)
                .OrderBy(o => o.Name)
                .ToList();

            ViewBag.Msg = TempData["msg"];

            if (ViewBag.Msg != null)
                ViewBag.MsgType = "success";

            return View();
        }

        
    }
}
