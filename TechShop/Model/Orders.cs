using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Model
{
    public class Orders
    {
        private int orderId;
        private Customers customer;
        private DateTime orderDate;
        private decimal totalAmount;
        private string status;

        public Orders() { } 

        public Orders(int orderId, Customers customer, DateTime orderDate, decimal totalAmount, string status)
        {
            this.orderId = orderId;
            this.customer = customer;
            this.orderDate = orderDate;
            this.totalAmount = totalAmount;
            this.status = status;   
        }
        public int OrderID
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }
        public Customers Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        public string Status
        { 
         get { return status; } 
         set { status = value; } 
        }


    }
}


