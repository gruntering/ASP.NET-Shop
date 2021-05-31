using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.Tables
{
    //Class for operating with the Basket Case 
    public class ShoppingBasket
    {
        public IEnumerable<BasketProcedure> Procedures { get { return procudereCollection; } }
        private List<BasketProcedure> procudereCollection = new List<BasketProcedure>();
        public class BasketProcedure
        {
            public Material Material { get; set; }
            public int Amount { get; set; }
        }
        // Adding Item To Basket Case 
        public void AddGood(Material material, int amount)
        {
            BasketProcedure procedure = procudereCollection
                .Where(m => m.Material.MaterialID == material.MaterialID)
                .FirstOrDefault();

            if (procedure == null)
            {
                procudereCollection.Add(new BasketProcedure { Material = material, Amount = amount });
            }
            else
            {
                procedure.Amount += amount;
            }

        }
        // Deleting Item From Basket Case
        public void DeleteMaterial(Material material)
        {
            procudereCollection.RemoveAll(m => m.Material.MaterialID == material.MaterialID);
        }
        // Counts Total Price 
        public decimal CountTotalCost()
        {
            return procudereCollection.Sum(c => c.Material.Cost * c.Amount);
        }
        // Delete All Items in the Basket Case 
        public void Erase()
        {
            procudereCollection.Clear();
        }
     
       
    }
 
}
