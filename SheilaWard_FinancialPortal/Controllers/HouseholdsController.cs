using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SheilaWard_FinancialPortal.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using SheilaWard_FinancialPortal.ViewModels;

namespace SheilaWard_FinancialPortal.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        [Authorize(Roles="Lobbyist")]
        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Greeting")] Household household)
        {
            if (ModelState.IsValid)
            {
                household.Created = DateTime.Now;
                db.Households.Add(household);
                db.SaveChanges();

                //Update my User record to include the newly created Household Id
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                user.HouseholdId = household.Id;
                db.SaveChanges();

                //Assign this person the role of HOH
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                userManager.RemoveFromRole(userId, "Lobbyist");
                userManager.AddToRole(userId, "HeadOfHousehold");

                //Need to 'Reauthorize' so role will take effect
                await user.ReauthorizeAsync();


                return RedirectToAction("Dashboard", new { id = user.HouseholdId });
            }
            return View();
        }

        [Authorize(Roles="HeadOfHousehold,Member")]
        // GET: Households/Dashboard
        public ActionResult Dashboard(int? id)
        {
            if (id == null)
            {
                var userId = User.Identity.GetUserId();
                id = db.Users.Find(userId).HouseholdId;
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }


        [Authorize(Roles="HeadOfHousehold")]
        // GET: Households/Wizard
        public ActionResult Wizard()
        {
            var houseId = db.Users.Find(User.Identity.GetUserId()).HouseholdId;
            var myViewModel = new ConfigureViewModel();
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "Id", "Type");
            //myViewModel.AccountTypeId = new SelectList(db.AccountTypes, "Id", "Type");
            myViewModel.BankAccount.HouseholdId = (int)houseId;
            myViewModel.Budget.HouseholdId = (int)houseId;

            return View(myViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Households/Wizard
        public ActionResult Wizard(ConfigureViewModel configureViewModel, int accountTypeId)
        {
            //if (ModelState.IsValid)
            //{
                configureViewModel.BankAccount.Created = DateTime.Now;
                configureViewModel.BankAccount.AccountTypeId = accountTypeId;
                configureViewModel.BankAccount.CurrentBalance = configureViewModel.BankAccount.StartingBalance;
                db.BankAccounts.Add(configureViewModel.BankAccount);

                db.Budgets.Add(configureViewModel.Budget);
                db.SaveChanges();

                configureViewModel.BudgetItem.BudgetId = configureViewModel.Budget.Id;
                db.BudgetItems.Add(configureViewModel.BudgetItem);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            //}
            //return View(configureViewModel);
        }





        // POST: Households/Wizard
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Wizard([Bind(Include = "Id,Name,Greeting,Created")] Household household)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Households.Add(household);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(household);
        //}

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Greeting,Created")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
