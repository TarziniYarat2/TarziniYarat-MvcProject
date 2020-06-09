using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TarziniYarat.BusinessLogic.Abstract;
using TarziniYarat.Model;
using TarziniYarat.UI.MVC.Models;

namespace TarziniYarat.UI.MVC.Controllers
{
    public class CardController : Controller
    {
        IProductService _productService;
        public CardController(IProductService productService)
        {
            _productService = productService;
        }
        public JsonResult AddToCart(int productID)
        {
            Product p = _productService.GetByID(productID);
            List<CartItem> cartProducts = null;

            if (p != null)
            {
                CartItem cartItem = new CartItem();
                cartItem.Name = p.ProductName;
                cartItem.Price = p.UnitPrice;
                cartItem.ProductID = p.ProductID;
                cartItem.Quantity = 1;

                if (Session["cart"] != null)
                {
                    cartProducts = Session["cart"] as List<CartItem>;
                    CartItem cartItem2 = cartProducts.SingleOrDefault(a => a.ProductID == cartItem.ProductID);
                    if (cartItem2 == null)
                    {
                        cartProducts.Add(cartItem);
                    }
                    else
                    {
                        cartItem2.Quantity++;
                    }
                }
                else
                {
                    cartProducts = new List<CartItem>();
                    cartProducts.Add(cartItem);
                }

                Session["cart"] = cartProducts;

                return Json(cartProducts.Count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Ürün bulunamadı", JsonRequestBehavior.AllowGet);
            }
        }

        public ViewResult List()
        {
            return View();
        }

        public ActionResult GetCart()
        {
            List<CartItem> cart = Session["cart"] == null ? new List<CartItem>() : Session["cart"] as List<CartItem>;
            return PartialView("_CartList", cart);
        }
    }
}