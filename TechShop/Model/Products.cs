namespace TechShop.Model
{
    public class Products
    {

        private int productID;
        private string productName;
        private string description;
        private decimal price;
        public Products()
        {
        }

        public Products(int productID, string productName, string description, decimal price)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.Description = description;
            this.Price = price;
        }


        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public decimal Price
        {
            get { return price; }
            set {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Price cannot be negative.");
                }
                price = value; }

        }
    }
     
}
