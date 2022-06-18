using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ProjetoTelecon.Data;
using ProjetoTelecon.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoTelecon.Controllers
{
    public class Login : Controller
    {
        private readonly Context _context;
        public Login(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Post(Users u)
        {

            var user = _context.Users.Where(w => w.Email == u.Email).SingleOrDefault();

            if (user != null && user.Password == u.Password)
            {
                string userType;

                if (user.IsAdmin == true)
                {
                    userType = "Admin";
                }
                else
                {
                    userType = "Comum";
                }
                
                var claims = new List<Claim>
                {
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email),
                    new Claim("UserType", userType),
                    new Claim("UserId", user.UserId.ToString(), ClaimValueTypes.Integer)
                };

                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(userIdentity);

                var authenticationProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(8),
                    IsPersistent = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authenticationProperties);
                

                TempData["Msg"] = "Bem vindo! Você acaba de entrar na plataforma";
                TempData["MsgType"] = "success";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Msg"] = "E-mail ou senha incorretos!";
                TempData["MsgType"] = "danger";

                return RedirectToAction("Index", "Login");
            }

            
        }





    }
}
