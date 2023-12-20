using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Repository
{
    internal interface IOrdersRepo
    {
        public decimal CalculateTotalAmount(int orderId);
        public void GetOrderDetails(int orderId);
        public void UpdateOrderStatus(int orderId, string newStatus);
    }
}
