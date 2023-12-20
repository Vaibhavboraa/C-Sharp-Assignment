using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Model
{
    public class Inventory
    {
        private int inventoryID;
        private Products product;
        private int quantityInStock;
        private DateTime lastStockUpdate;

        public Inventory() { }  
        public Inventory(int inventoryID, Products product, int quantityInStock, DateTime lastStockUpdate)
        {
            this.inventoryID = inventoryID;
            this.product = product;
            this.quantityInStock = quantityInStock;
            this.lastStockUpdate = lastStockUpdate;
        }

        public int InventoryID
        {
            get { return inventoryID; }
            set { inventoryID = value; }
        }

        public Products Product
        {
            get { return product; }
            set { product = value; }
        }

        public int QuantityInStock
        {
            get { return quantityInStock; }
            set { quantityInStock = value; }
        }

        public DateTime LastStockUpdate
        {
            get { return lastStockUpdate; }
            set { lastStockUpdate = value; }
        }


    }
}
