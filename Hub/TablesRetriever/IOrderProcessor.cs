using Hub.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.TablesRetriever
{
    // Interface for Proccessing the Order 
   public interface IOrderProcessor
    {
        void ProcessOrder(ShoppingBasket basket, OrderDetails orderDetails);
    }
}
