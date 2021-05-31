using Hub.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class BasketIndexViewModel
    {
        public ShoppingBasket Basket { get; set; }
        public string ReturnUrl { get; set; }
    }
}