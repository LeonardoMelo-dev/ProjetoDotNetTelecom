using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
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
            var urlRequest = Request.QueryString.ToString();

            if(urlRequest != null)
            {
                var urlSplit = urlRequest.Split('=');

                if (urlSplit.Count() > 1)
                {
                    var token = urlSplit[1];

                    var handler = new JwtSecurityTokenHandler();

                    var jsonToken = handler.ReadToken(token);

                    var user = jsonToken as JwtSecurityToken;

                    var claims = user.Claims.ToList();

                    var level = claims[3].Value == "True" ? "admin" : "comum";

                    var logedUser = new Users()
                    {
                        UserId = Convert.ToInt32(claims[0].Value),
                        Name = claims[1].Value,
                        Email = claims[2].Value,
                        Image = claims[4].Value
                    };

                    ViewBag.LogedUser = logedUser;
                    ViewBag.LevelUser = level;
                    ViewBag.Token = token;
                }
            }


            ViewBag.Packets = _context.Packets.Where(w => w.Active == true).OrderBy(o => o.CreationDate).ToList();
            



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}