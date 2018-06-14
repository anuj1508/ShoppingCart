using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class ProductMapsController : Controller
    {
        private Shopping_PortalEntities db = new Shopping_PortalEntities();

        // GET: ProductMaps
        public ActionResult Index()
        {
            var productMaps = db.ProductMaps.Include(p => p.Product).Include(p => p.Variant);
            return View(productMaps.ToList());
        }

        // GET: ProductMaps/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] == null)
            {
                return View("Unauthorized");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMap productMap = db.ProductMaps.Find(id);
            if (productMap == null)
            {
                return HttpNotFound();
            }
            return View(productMap);
        }

        //public ActionResult Details(int? id,string message)
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        return View("Unauthorized");
        //    }
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProductMap productMap = db.ProductMaps.Find(id);
        //    if (productMap == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.countMessage = message;
        //    return View(productMap);
        //}

        public ActionResult AddToCart(int id)
        {

            return RedirectToAction("AddToCart", "CartDetails", new { ProductMapId = id });

        }

        // GET: ProductMaps/Create
        public ActionResult Create()
        {
            ViewBag.Product_id = new SelectList(db.Products, "Product_id", "Product_name");
            ViewBag.Variant_id = new SelectList(db.Variants, "Variant_id", "Variant1");
            return View();
        }

        // POST: ProductMaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Map_id,Product_id,Variant_id,Product_price")] ProductMap productMap)
        {
            if (ModelState.IsValid)
            {
                db.ProductMaps.Add(productMap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_id = new SelectList(db.Products, "Product_id", "Product_name", productMap.Product_id);
            ViewBag.Variant_id = new SelectList(db.Variants, "Variant_id", "Variant1", productMap.Variant_id);
            return View(productMap);
        }

        // GET: ProductMaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMap productMap = db.ProductMaps.Find(id);
            if (productMap == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_id = new SelectList(db.Products, "Product_id", "Product_name", productMap.Product_id);
            ViewBag.Variant_id = new SelectList(db.Variants, "Variant_id", "Variant1", productMap.Variant_id);
            return View(productMap);
        }

        // POST: ProductMaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Map_id,Product_id,Variant_id,Product_price")] ProductMap productMap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productMap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_id = new SelectList(db.Products, "Product_id", "Product_name", productMap.Product_id);
            ViewBag.Variant_id = new SelectList(db.Variants, "Variant_id", "Variant1", productMap.Variant_id);
            return View(productMap);
        }

        // GET: ProductMaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMap productMap = db.ProductMaps.Find(id);
            if (productMap == null)
            {
                return HttpNotFound();
            }
            return View(productMap);
        }

        // POST: ProductMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductMap productMap = db.ProductMaps.Find(id);
            db.ProductMaps.Remove(productMap);
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
