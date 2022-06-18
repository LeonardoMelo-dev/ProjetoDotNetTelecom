using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoTelecon.Data;
using ProjetoTelecon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace ProjetoTelecon.Controllers
{
    public class ServiceController : Controller
    {
        private readonly Context _context;

        public ServiceController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Service = _context.Services
                .Where(w => w.Active == true)
                .OrderByDescending(o => o.Name)
                .ToList();

            ViewBag.Msg = TempData["msg"];

            if (ViewBag.Msg != null)
                ViewBag.MsgType = "success";

            return View();
        }
        
    }
}
