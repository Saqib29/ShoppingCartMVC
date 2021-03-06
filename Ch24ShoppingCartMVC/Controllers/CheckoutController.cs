using Ch24ShoppingCartMVC.Models;
using Ch24ShoppingCartMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ch24ShoppingCartMVC.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            CartViewModel model = /*(CartViewModel)*/Session["cart"] as CartViewModel;
            
            if(model == null)
            {
                CartModel cart = new CartModel();
                model = cart.GetCart();
                Session["cart"] = model;
            }
            List<CheckoutViewModel> checkouts = new List<CheckoutViewModel>();

            foreach(var item in model.Cart)
            {
                CheckoutViewModel checout = new CheckoutViewModel();
                checout.ProductID = item.ProductID;
                checout.Name = item.Name;
                checout.Quantity = item.Quantity;
                checout.EachPrice = item.UnitPrice;
                checout.TotalEachPrice = item.UnitPrice * item.Quantity;

                checkouts.Add(checout);
            }
            Session["orders"] = checkouts;
            return View(checkouts);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            ViewBag.address = form["address"];
            ViewBag.payment = form["payment"];
            ViewBag.totalPrice = form["totalPrice"];
            ViewBag.totalPriceWithTax = form["totalPriceWithTax"];
            return View("Confirmation");
        }

    }
}
