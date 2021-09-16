using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VIP_Panel.Data;
using System.Security.Cryptography;
using VIP_Panel.Models;

namespace VIP_Panel.Controllers

{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _context;
        //HttpContext context = HttpContext.Current;

        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = password;
                    var data = _context.vipUsers.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList(); //moze zawierac wiecej niz 1 rekord gdy sa takie same rekordy w bazie (POTRZEBNA WALIDACJA EMAILI)
                if (data.Count() > 0)
                {
                    //add data
                    ViewBag.FullName = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    ViewBag.Email = data.FirstOrDefault().Email;
                    ViewBag.idUser = data.ElementAt(0).ID; 
                    int idUser = ViewBag.idUser;


                    return RedirectToAction("Succes", new { idUser = idUser }); // userId is throwing by url //need JWT tokenm or other security
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            // Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        
        public ActionResult Succes(int idUser)
        {

            return View(showPostsThatBelongsToUser(idUser));
        }




        // GET: Login/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Login/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(IFormCollection collection)
        {
            try
            {
                Console.WriteLine(_context.Database.CanConnect());
                // Remember .value!
                _context.vipUsers.Add(new VipUserModel(collection.ElementAt(0).Value.ToString(), collection.ElementAt(1).Value.ToString(), collection.ElementAt(2).Value.ToString(), collection.ElementAt(3).Value.ToString())); ;
                _context.SaveChanges(); //not async
               

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [NonAction]
        public List<PostModel> showPostsThatBelongsToUser(int userId)
        {

            List<PostModel> data = _context.posts.Where(p => p.UserID==(userId)).ToList();

            Console.WriteLine("!!!!!!!!! ==? same content!!!!!!!!!!!!");
            Console.WriteLine(data.Count());
            Console.WriteLine(userId);
            //   Console.WriteLine((_context.posts.Where(p => p.UserID.Equals(userId)).ToList()).ElementAt(0).Content.ToString());
                                                      

            if (data.Any())
            {
                return data.ToList();
            }
            else data.Add(new PostModel(0, "none", "none", 0));
            return data.ToList();
        }
    }
}