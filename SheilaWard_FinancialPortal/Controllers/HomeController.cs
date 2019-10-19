using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SheilaWard_FinancialPortal.Models;
using SheilaWard_FinancialPortal.ViewModels;

namespace SheilaWard_FinancialPortal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize (Roles="Lobbyist")]
        // GET: Home/Lobby
        public ActionResult Lobby()
        {
            var userId = User.Identity.GetUserId();
            var member = db.Users.Select(u => new UserProfileViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                AvatarUrl = u.AvatarUrl
            }).FirstOrDefault(u => u.Id == userId);

            return View(member);
        }

        [Authorize]
        // GET: Home/Lobby2
        public ActionResult Lobby2()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);


            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}