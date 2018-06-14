using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.Data.Entity;
using Ecommerce.ViewModel;
using System.Net;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private Shopping_PortalEntities db = new Shopping_PortalEntities();

        public ActionResult HomePage()
        {

            if (Session["Username"] != null)
            {
                ViewBag.UserName = Session["Username"].ToString();
                ViewBag.UserId = Session["UserId"].ToString();
            }
            var cat = db.Categories.Include(x => x.Products).ToList();
            return View(cat);
        }

        public ActionResult Search(string SearchString)
        {
            if (String.IsNullOrEmpty(SearchString))
            {
                ViewBag.ErrorMessage = "Please insert the item !!!!";
                return View("Searcheditem", null);
            }
            // item = ViewBag.CurrentFilter;
            var itemsearched = (db.Products.Where(x => x.Product_name.Contains(SearchString)).ToList());
            if (itemsearched.Count > 0)
            {
                return View("Searcheditem", itemsearched);
            }
            ViewBag.ErrorMessage = "Item not found !!!!";
            return View("Searcheditem", null);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var products = db.ProductMaps.Include(p => p.Product).Where(x => x.Product.Category_id == id.Value).Include(p => p.Variant).OrderBy(i => i.Product_id).ToList();

                if (products == null)
                {
                    return HttpNotFound();
                }

                return View("CategoryDetail", products);
            }

        }

        public ActionResult ShowProduct(int? productId)
        {

            return RedirectToAction("Details", "ProductMaps", new { id = productId.Value });


        }

        public ActionResult Index()
        {
            return View();
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