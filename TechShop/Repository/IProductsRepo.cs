using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal interface IProductsRepo
    {
        public Products GetProductDetails(int productId);
        public void UpdateProductInfo(int productId, decimal newPrice, string newDescription);
        public bool IsProductInStock(int productId);
    }
}
