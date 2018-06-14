using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace Ecommerce.Controllers
{
    public class OrdersController : Controller
    {
        private Shopping_PortalEntities db = new Shopping_PortalEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyOrders() {

            return View("Final");
        }

        public ActionResult Final() {

            return View();
        }
    }
}