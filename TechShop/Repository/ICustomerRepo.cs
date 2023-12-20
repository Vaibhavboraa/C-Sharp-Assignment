using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal interface ICustomerRepo
    {
        public int CalculateTotalOrders(int customerId);
        public Customers GetCustomerDetails(int customerId);
        public void UpdateCustomerInfo(int customerId, string newEmail, string newPhone);
    }
}
