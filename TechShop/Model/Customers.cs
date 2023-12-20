namespace TechShop.Model
{
    public class Customers
    {


        private int customerID;
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private string address;

        public Customers()
        {
        }
        public Customers(int customerID, string firstName, string lastName, string email, string phone, string address)
        {
            this.CustomerID = customerID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }

        }
    }
}
