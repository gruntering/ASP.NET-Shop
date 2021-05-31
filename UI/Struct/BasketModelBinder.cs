using Hub.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Struct
{
    public class BasketModelBinder : IModelBinder
    {
        private const string sessionKey = "ShoppingBasket";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ShoppingBasket basket = null;
            if (controllerContext.HttpContext.Session != null)
            {
                basket = (ShoppingBasket)controllerContext.HttpContext.Session[sessionKey];

            }
            if(basket == null)
            {
                basket = new ShoppingBasket();
                if(controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = basket;
                }
            }
            return basket;
        }
    }

}