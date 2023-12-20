using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Model
{
    public class OrderDetails
    {
        private int orderDetailID;
        private Orders order;
        private Products product;
        private int quantity;
        private decimal discount;

        public OrderDetails() { }
        public OrderDetails(int orderDetailID, Orders order, Products product, int quantity, decimal discount)
        {
            this.orderDetailID = orderDetailID;
            this.order = order;
            this.product = product;
            this.quantity = quantity;
            this.discount = discount;   
        }
        public int OrderDetailID
        {
            get { return orderDetailID; }
            set { orderDetailID = value; }
        }

        public Orders Order
        {
            get { return order; }
            set { order = value; }
        }

        public Products Product
        {
            get { return product; }
            set { product = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Quantity must be a positive integer.");
                }
                quantity = value; }
        }
        public decimal Discount
        {
            get { return discount; }    
            set { discount = value; }   
        }

    }

}
