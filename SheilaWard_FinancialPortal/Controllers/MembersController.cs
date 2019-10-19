using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SheilaWard_FinancialPortal.ViewModels;
using System.IO;
using SheilaWard_FinancialPortal.Models;
using SheilaWard_FinancialPortal.Helpers;


namespace SheilaWard_FinancialPortal.Controllers
{
    [RequireHttps]
    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Members
        [Authorize]
        public ActionResult EditProfile(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = User.Identity.GetUserId();
            }

            var member = db.Users.Select(u => new UserProfileViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).FirstOrDefault(u => u.Id == userId);

            return View(member);
        }

        // POST: Members
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditProfile(UserProfileViewModel member)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(member.Id);
                user.FirstName = member.FirstName;
                user.LastName = member.LastName;
                user.Email = member.Email;
                user.UserName = member.Email;
                user.PhoneNumber = member.PhoneNumber;

                if (ImageHelpers.IsWebFriendlyImage(member.Avatar))
                {
                    var fileName = Path.GetFileName(member.Avatar.FileName);
                    member.Avatar.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), fileName));
                    user.AvatarUrl = "/Avatars/" + fileName;
                }

                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return View(member);
            }
        }
    }
}