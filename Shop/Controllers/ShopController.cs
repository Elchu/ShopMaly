using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        private ShopContext db = new ShopContext();

        List<Item> _productList = new List<Item>();

        // GET: Shop
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        public ActionResult OrderNow(int? id)
        {
            //brakuje walidacji id
            if (id != null)
            {//sprawdzam czy produkt istnieje
                if (db.Product.FirstOrDefault(p => p.ProductId == id) != null)
                {//jesli produkt istnieje w bazie to mozemy go dodac
                    if (Session["cart"] == null)
                    {
                        _productList.Add(new Item(db.Product.Find(id), 1));
                        Session["cart"] = _productList;
                    }
                    else
                    {
                        _productList = (List<Item>)Session["cart"];

                        if (_productList.Any(p => p.Product.ProductId == id))
                            _productList.First(p => p.Product.ProductId == id).Quantity++;
                        else
                            _productList.Add(new Item(db.Product.Find(id), 1));

                        Session["cart"] = _productList;
                    }        
                }
            }

            return View(Session["cart"]);
        }

        public ActionResult DeleteItem(int? id)
        {
            //brakuje walidacji id
            if (id != null)
            {
            //sprawdzam czy podany zostal id produktu
                if (db.Product.FirstOrDefault(p => p.ProductId == id) != null)
                {
                //jesli produkt istnieje w bazie to mozemy go dodac
                    if (Session["cart"] != null)
                    {
                        _productList = (List<Item>) Session["cart"];

                        if (_productList.Any(p => p.Product.ProductId == id))
                            _productList.Remove(_productList.First(p => p.Product.ProductId == id));
                    }

                    Session["cart"] = _productList;

                    //return View("OrderNow", Session["cart"]);     
                }
            }
            return View("OrderNow", Session["cart"]);  
        }
    }
}