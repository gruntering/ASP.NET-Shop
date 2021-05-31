using Hub;
using Hub.Context;
using Hub.Tables;
using Hub.TablesRetriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class BasketController : Controller
    {
     
        private IMaterialRepository repository;
        private IOrderProcessor orderProcessor;
        public ViewResult Index (ShoppingBasket basket,string returnUrl)
        {
            return View(new BasketIndexViewModel
            {

                Basket = basket,
                ReturnUrl = returnUrl

            });
               
        }
        public BasketController(IMaterialRepository repos, IOrderProcessor process)
        {
            repository = repos;
            orderProcessor = process;
        }
        //Add Item To Bakset

        public RedirectToRouteResult AddItemToBasket(ShoppingBasket basket, int materialID, string returnUrl)
        {
            Material material = repository.Materials
                .FirstOrDefault(m => m.MaterialID == materialID);
            if (material != null)
            {
                basket.AddGood(material, 1);

            }

            return RedirectToAction("Index", new { returnUrl });

        }
        //Delete Selected Items

        public RedirectToRouteResult DeleteItemFromBasket(ShoppingBasket basket, int materialID, string returnUrl)
        {
            Material material = repository.Materials
                .FirstOrDefault(m => m.MaterialID == materialID);
            if (material != null)
            {
               basket.DeleteMaterial(material);

            }
            
            
            return RedirectToAction("Index", new { returnUrl });

        }
        //Delete All Items
        public RedirectToRouteResult EraseAllItems(ShoppingBasket basket, string returnUrl)
        {
            basket.Erase();
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Order()
        {

            
           return View(new OrderDetails());
            

        }
        [HttpPost]
        public ViewResult Order(ShoppingBasket basket, OrderDetails orderDetails)
        {
         //   string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         //@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         //@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
         //   Regex re = new Regex(strRegex);
            if (basket.Procedures.Count() == 0)
            {
                ModelState.AddModelError("", "Your Basket Case Is Empty");
            }
            
            // If Model is valid the order is processed and will transfer to Accomplished Page
            if (ModelState.IsValid)
            { 
                orderProcessor.ProcessOrder(basket, orderDetails);
                basket.Erase();
                return View("Accomplished");
            }
           // If not will return the same page to retry 

            else {
                return View(new OrderDetails());
            }


        }


        public PartialViewResult Summary(ShoppingBasket basket)
        {

            return PartialView(basket);
        }
    }
}