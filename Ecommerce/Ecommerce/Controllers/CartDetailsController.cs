using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Ecommerce.Controllers
{
    public class CartDetailsController : Controller
    {
        private Shopping_PortalEntities db = new Shopping_PortalEntities();


        public ActionResult AddToCart(int? ProductMapId, int? ProductQuantity)
        {
            ProductQuantity = 1;
            if (ProductMapId == null || ProductQuantity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productDetail = db.ProductMaps.Where(mapId => mapId.Map_id == ProductMapId).Single();

            var stockCount = productDetail.Stock;

            CartDetail Cart = new CartDetail();
            Cart.Cart_id = Convert.ToInt32(Session["UserId"]);
            Cart.Map_id = ProductMapId.Value;
            Cart.Quantity = ProductQuantity.Value;
            db.CartDetails.Add(Cart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ViewCart()
        {

            if (Session["UserId"] == null || Session["Username"] == null)
            {

                return View("Unauthorized");
            }
            var customerId = int.Parse(Session["UserId"].ToString());
            var cartId = db.Carts.Where(asd => asd.Customer_Id == customerId).Single();
            int cart_id = cartId.Cart_Id;
            var myItems = db.CartDetails.Include(asd => asd.Cart).Where(asd => asd.Cart_id == cart_id).Include(asd => asd.ProductMap).ToList();

            return View("MyCart", myItems);

        }

        public ActionResult Index()
        {

            return View("ItemAdded");
        }


    }
}