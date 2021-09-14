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
                var data = _context.vipUsers.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add data
                    ViewBag.FullName = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    ViewBag.Email = data.FirstOrDefault().Email;
                    ViewBag.idUser = data.FirstOrDefault().ID;
                    int idUser = ViewBag.idUser;


                    return RedirectToAction("Succes", idUser);
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
                // TODO: Add insert logic here

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

            List<PostModel> data = _context.posts.Where(p => p.UserID.Equals(userId)).ToList();

            

            for(int i = 0; i < _context.posts.Count(); i++)  ///// To delete !!!!
            {
                if (_context.posts.ElementAt(i).UserID == userId)
                { 
                    data.Add(_context.posts.ElementAt(i));
                }
            }                                               /////////

            if (data.Any())
            {
                return data.ToList();
            }
            else data.Add(new PostModel(0, "none", "none", 0));
            return data.ToList();
        }
    }
}