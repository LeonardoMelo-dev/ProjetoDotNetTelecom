using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ProjetoTelecon.Data;
using ProjetoTelecon.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ProjetoTelecon.Services_;

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
        [Route("login/post")]
        public async Task<ActionResult<dynamic>> AutenticateAsync(Users u)
        {

            var user = _context.Users.Where(w => w.Email == u.Email && w.Password == u.Password).SingleOrDefault();

            if (user != null)
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

                var token = TokenService.GenerateToken(user);

                //Oculta a senha
                user.Password = "";

                TempData["Msg"] = "Bem vindo! Você acaba de entrar na plataforma";
                TempData["MsgType"] = "success";
                return RedirectToAction("Index", "Home", new {token = token});
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
