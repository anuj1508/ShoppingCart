using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.Data.Entity.Validation;
using Ecommerce.ViewModel;

namespace Ecommerce.Controllers
{
    public class CustomersController : Controller
    {
        private Shopping_PortalEntities db = new Shopping_PortalEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Customer customer)
        {
            Shopping_PortalEntities db = new Shopping_PortalEntities();

            try
            {

                var checkUser = (from s in db.Customers where s.Email_id == customer.Email_id select s).FirstOrDefault();
                if (checkUser == null)
                {
                    if (ModelState.IsValid)
                    {
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        var customerId = (from s in db.Customers where s.Email_id == customer.Email_id select s.Customer_id).Single();
                        Cart cart = new Cart();
                        cart.Customer_Id = customerId;
                        db.Carts.Add(cart);
                        db.SaveChanges();

                        return RedirectToAction("Login");

                    }
                    ViewBag.ErrorMessage = "Error Please Try Again !!";
                    return View();
                }
                ViewBag.ErrorMessage = "Email Id Already Exists!!";
                return View();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                //ViewBag.ErrorMessage = "Some exception occured" + e;
                return View();

            }

        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_id,Customer_name,Customer_Address,Phone_number,Email_id,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        [HttpGet]
        public ActionResult Login()
        {

            if (Session["Username"] != null || Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Home", new { Username = Session["Username"].ToString() });
            }
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginCustomer customer)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var getUser = (from s in db.Customers where s.Email_id == customer.Email_id select s).Single();
                    if (getUser != null)
                    {
                        string password = (getUser.Password);
                        if (String.IsNullOrEmpty(password))
                        {
                            ViewBag.ErrorMessage = "Please entrer the Password!!";
                            return View();
                        }
                        var query = (from s in db.Customers where (s.Email_id == customer.Email_id) && customer.Password.Equals(password) select s).Single();
                        if (query != null)
                        {
                            Session["Username"] = getUser.Customer_name;
                            Session["UserId"] = getUser.Customer_id;
                            //return RedirectToAction("Edit", "Customer");
                            return RedirectToAction("HomePage", "Home");
                        }
                        ViewBag.ErrorMessage = "Invalid Password";
                        return View();
                    }
                    ViewBag.ErrorMessagae = "Invalid Username/EmailId or Password";
                    return View();
                }
                // ViewBag.ErrorMessage = "Invalid UserName or Password";
                return View();

            }
            catch (Exception)
            {
                //throw e;
                ViewBag.ErrorMessage = "Invalid Details!! please enter correct details";
                return View();
            }


        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["Username"] = null;
            Session.Abandon();
            return RedirectToAction("HomePage", "Home");
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
