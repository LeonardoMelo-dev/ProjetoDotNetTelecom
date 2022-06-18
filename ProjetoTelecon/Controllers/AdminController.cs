using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoTelecon.Data;
using ProjetoTelecon.Models;

namespace ProjetoTelecon.Controllers
{
    public class AdminController : Controller
    {
        private readonly Context _context;

        public AdminController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        /*======================================================== USUARIOS ========================================================*/
        public ActionResult Users()
        {
            int id = 0;

            if (RouteData.Values["id"] != null)
            {
                id = Convert.ToInt32(RouteData.Values["id"]);
            }

            ViewBag.Item = new Users();

            if (id > 0)
            {
                var obj = _context.Users.Find(id);

                ViewBag.Item = obj;
            }
            else
            {
                ViewBag.List = _context.Users.ToList();
            }

            var msg = TempData["Msg"];
            var msgType = TempData["MsgType"];
            var form = TempData["Form"];

            if (msg != null)
            {
                ViewBag.Msg = msg;
                ViewBag.MsgType = msgType;
            }

            if (form != null)
            {
                ViewBag.Item = !String.IsNullOrEmpty((string)form) ? JsonConvert.DeserializeObject<Users>((string)form) : null;
            }

            TempData.Remove("Msg");
            TempData.Remove("MsgType");
            TempData.Remove("Form");

            return View();
        }


        public ActionResult UsersAdd(Users data)
        {
            try
            {
                var utils = new Utils();

                if (String.IsNullOrEmpty(data.Name) || String.IsNullOrEmpty(data.Email) || String.IsNullOrEmpty(data.Password))
                {
                    TempData["Msg"] = "Preencha os campos obrigatórios!";
                    TempData["MsgType"] = "danger";
                    TempData["Form"] = JsonConvert.SerializeObject(data);

                    return RedirectToAction("Users", "admin", new { id = data.UserId });
                }


                if (data.UserId == 0)
                {
                    data.CreationDate = DateTime.Now;

                    var files = utils.Upload(Request.Form.Files, "Users", null);

                    foreach (var f in files)
                    {
                        if (f.Value != "" && f.Key == "Image")
                        {
                            data.Image = f.Value;
                        }
                    }

                    _context.Users.Add(data);

                    TempData["Msg"] = "Registro salvo com sucesso!";
                }
                else
                {
                    var update = _context.Users.Where(w => w.UserId == data.UserId).SingleOrDefault();

                    update.Name = data.Name;
                    update.Email = data.Email;
                    update.Age = data.Age;
                    update.Password = data.Password;
                    update.CPF = data.CPF;
                    update.Balance = data.Balance;
                    update.PhoneNumber = data.PhoneNumber;
                    update.Active = data.Active;
                    update.ModificationDate = DateTime.Now;
                    update.IsAdmin = data.IsAdmin;

                    var files = utils.Upload(Request.Form.Files, "User", null);

                    if (!String.IsNullOrEmpty(HttpContext.Request.Form["RemoveImage"]))
                    {
                        utils.RemoveFiles(update.Image);
                        update.Image = null;
                    }

                    foreach (var f in files)
                    {
                        if (f.Value != "" && f.Key == "Image")
                        {
                            if (!String.IsNullOrEmpty(update.Image))
                            {
                                utils.RemoveFiles(update.Image);
                            }
                            update.Image = f.Value;
                        }
                    }

                    _context.Entry(update).CurrentValues.SetValues(update);

                    TempData["Msg"] = "Registro atualizado com sucesso!";
                }

                _context.SaveChanges();

                TempData["MsgType"] = "success";

                return RedirectToAction("Users", "admin");
            }
            catch (Exception error)
            {
                throw;
            }
        }


        public ActionResult UsersDelete()
        {
            try
            {
                var utils = new Utils();

                var id = Convert.ToInt32(RouteData.Values["id"]);

                var data = new Users();

                data = _context.Users.Where(w => w.UserId == id).SingleOrDefault();

                if (data.Image != null)
                    utils.RemoveFiles(data.Image);

                _context.Users.Remove(data);

                _context.SaveChanges();

                TempData["Msg"] = "Registro removido com sucesso!";
                TempData["MsgType"] = "success";

                return RedirectToAction("Users", "admin");
            }
            catch (Exception error)
            {
                throw;
            }
        }


        /*======================================================== SERVICES ========================================================*/
        public IActionResult Services()
        {
            int id = 0;

            if (RouteData.Values["id"] != null)
            {
                id = Convert.ToInt32(RouteData.Values["id"]);
            }

            ViewBag.Item = new Services();

            if (id > 0)
            {
                var obj = _context.Services.Find(id);

                ViewBag.Item = obj;
            }
            else
            {
                ViewBag.List = _context.Services.ToList();
            }

            var msg = TempData["Msg"];
            var msgType = TempData["MsgType"];
            var form = TempData["Form"];

            if (msg != null)
            {
                ViewBag.Msg = msg;
                ViewBag.MsgType = msgType;
            }

            if (form != null)
            {
                ViewBag.Item = !String.IsNullOrEmpty((string)form) ? JsonConvert.DeserializeObject<Services>((string)form) : null;
            }

            TempData.Remove("Msg");
            TempData.Remove("MsgType");
            TempData.Remove("Form");

            return View();
        }

        public ActionResult ServicesAdd(Services data)
        {
            try
            {
                var utils = new Utils();

                if (String.IsNullOrEmpty(data.Name) || data.Price == null)
                {
                    TempData["Msg"] = "Preencha os campos obrigatórios!";
                    TempData["MsgType"] = "danger";
                    TempData["Form"] = JsonConvert.SerializeObject(data);

                    return RedirectToAction("services", "admin", new { id = data.ServiceId });
                }


                if (data.ServiceId == 0)
                {
                    data.CreationDate = DateTime.Now;

                    var files = utils.Upload(Request.Form.Files, "Services", null);

                    foreach (var f in files)
                    {
                        if (f.Value != "" && f.Key == "Image")
                        {
                            data.Image = f.Value;
                        }
                    }

                    _context.Services.Add(data);

                    TempData["Msg"] = "Registro salvo com sucesso!";
                }
                else
                {
                    var update = _context.Services.Where(w => w.ServiceId == data.ServiceId).SingleOrDefault();

                    update.Name = data.Name;
                    update.Price = data.Price;
                    update.Franchise = data.Franchise;
                    update.ExtraFranchise = data.ExtraFranchise;
                    update.ModificationDate = DateTime.Now;
                    update.ServiceType = data.ServiceType;
                    update.Active = data.Active;

                    var files = utils.Upload(Request.Form.Files, "Service", null);

                    if (!String.IsNullOrEmpty(HttpContext.Request.Form["RemoveImage"]))
                    {
                        utils.RemoveFiles(update.Image);
                        update.Image = null;
                    }

                    foreach (var f in files)
                    {
                        if (f.Value != "" && f.Key == "Image")
                        {
                            if (!String.IsNullOrEmpty(update.Image))
                            {
                                utils.RemoveFiles(update.Image);
                            }
                            update.Image = f.Value;
                        }
                    }

                    _context.Entry(update).CurrentValues.SetValues(update);

                    TempData["Msg"] = "Registro atualizado com sucesso!";
                }

                _context.SaveChanges();

                TempData["MsgType"] = "success";

                return RedirectToAction("services", "admin");
            }
            catch (Exception error)
            {
                throw;
            }
        }


        public ActionResult ServicesDelete()
        {
            try
            {
                var utils = new Utils();

                var id = Convert.ToInt32(RouteData.Values["id"]);

                var data = new Services();

                data = _context.Services.Where(w => w.ServiceId == id).SingleOrDefault();

                if (data.Image != null)
                    utils.RemoveFiles(data.Image);

                _context.Services.Remove(data);

                _context.SaveChanges();

                TempData["Msg"] = "Registro removido com sucesso!";
                TempData["MsgType"] = "success";

                return RedirectToAction("services", "admin");
            }
            catch (Exception error)
            {
                throw;
            }
        }



        /*======================================================== PACKETS ========================================================*/
        public ActionResult Packets()
        {
            int id = 0;

            if (RouteData.Values["id"] != null)
            {
                id = Convert.ToInt32(RouteData.Values["id"]);
            }

            ViewBag.Item = new Packets();

            if (id > 0)
            {
                var obj = _context.Packets.Find(id);

                ViewBag.Item = obj;
            }
            else
            {
                ViewBag.List = _context.Packets.ToList();
            }

            var msg = TempData["Msg"];
            var msgType = TempData["MsgType"];
            var form = TempData["Form"];

            if (msg != null)
            {
                ViewBag.Msg = msg;
                ViewBag.MsgType = msgType;
            }

            if (form != null)
            {
                ViewBag.Item = !String.IsNullOrEmpty((string)form) ? JsonConvert.DeserializeObject<Packets>((string)form) : null;
            }

            TempData.Remove("Msg");
            TempData.Remove("MsgType");
            TempData.Remove("Form");

            return View();
        }

        public ActionResult PacketsAdd(Packets data)
        {
            try
            {
                var utils = new Utils();

                if (String.IsNullOrEmpty(data.Name) || data.Price == null)
                {
                    TempData["Msg"] = "Preencha os campos obrigatórios!";
                    TempData["MsgType"] = "danger";
                    TempData["Form"] = JsonConvert.SerializeObject(data);

                    return RedirectToAction("Packets", "admin", new { id = data.PacketId });
                }


                if (data.PacketId == 0)
                {
                    data.CreationDate = DateTime.Now;

                    var files = utils.Upload(Request.Form.Files, "Packets", null);

                    foreach (var f in files)
                    {
                        if (f.Value != "" && f.Key == "Image")
                        {
                            data.Image = f.Value;
                        }
                    }

                    _context.Packets.Add(data);

                    TempData["Msg"] = "Registro salvo com sucesso!";
                }
                else
                {
                    var update = _context.Packets.Where(w => w.PacketId == data.PacketId).SingleOrDefault();

                    update.Name = data.Name;
                    update.Price = data.Price;
                    update.ModificationDate = DateTime.Now;
                    update.Active = data.Active;

                    var files = utils.Upload(Request.Form.Files, "Packet", null);

                    if (!String.IsNullOrEmpty(HttpContext.Request.Form["RemoveImage"]))
                    {
                        utils.RemoveFiles(update.Image);
                        update.Image = null;
                    }

                    foreach (var f in files)
                    {
                        if (f.Value != "" && f.Key == "Image")
                        {
                            if (!String.IsNullOrEmpty(update.Image))
                            {
                                utils.RemoveFiles(update.Image);
                            }
                            update.Image = f.Value;
                        }
                    }

                    _context.Entry(update).CurrentValues.SetValues(update);

                    TempData["Msg"] = "Registro atualizado com sucesso!";
                }

                _context.SaveChanges();

                TempData["MsgType"] = "success";

                return RedirectToAction("packets", "admin");
            }
            catch (Exception error)
            {
                throw;
            }
        }


        public ActionResult PacketsDelete()
        {
            try
            {
                var utils = new Utils();

                var id = Convert.ToInt32(RouteData.Values["id"]);

                var data = new Packets();

                data = _context.Packets.Where(w => w.PacketId == id).SingleOrDefault();

                if (data.Image != null)
                    utils.RemoveFiles(data.Image);

                _context.Packets.Remove(data);

                _context.SaveChanges();

                TempData["Msg"] = "Registro removido com sucesso!";
                TempData["MsgType"] = "success";

                return RedirectToAction("packets", "admin");
            }
            catch (Exception error)
            {
                throw;
            }
        }
    }
}
