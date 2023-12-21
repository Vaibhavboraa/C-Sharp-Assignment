using TechShop.Model;
using TechShop.Repository;





while (true)
{
    Console.WriteLine("Select an option:");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Register");

    int mainChoice = GetOption(2);

    switch (mainChoice)
    {
        case 1:
            Console.WriteLine("1. Customers");
            Console.WriteLine("2. Products");
            Console.WriteLine("3. Orders");
            Console.WriteLine("4. Order Details");
            Console.WriteLine("5. Inventory");

            int loginChoice = GetOption(5);

            switch (loginChoice)
            {
                case 1:
                    Console.WriteLine("1. CalculateTotalOrders");
                    Console.WriteLine("2. GetCustomerDetails");
                    Console.WriteLine("3. UpdateCustomerInfo");

                    int customerChoice = GetOption(3);

                    switch (customerChoice)
                    {
                        case 1:
                            CalculateTotalOrders();
                            break;
                        case 2:
                            GetCustomerDetails();
                            break;
                        case 3:
                            UpdateCustomerInfo();
                            break;
                        default:
                            Console.WriteLine("Invalid option for Customer.");
                            break;
                    }
                    break;

                case 2:
                    Console.WriteLine("1. GetProductDetails");
                    Console.WriteLine("2. UpdateProductInfo");
                    Console.WriteLine("3. IsProductInStock");
                    Console.WriteLine("4. AddProduct");
                    Console.WriteLine("5. RemoveProduct");
                    Console.WriteLine("6. SearchProductByName");

                    int productChoice = GetOption(6);

                    switch (productChoice)
                    {
                        case 1:
                            GetProductDetails();
                            break;
                        case 2:
                            UpdateProductInfo();
                            break;
                        case 3:
                            IsProductInStock();
                            break;
                        case 4:
                            AddProduct();
                            break;
                        case 5:
                            RemoveProduct();
                            break;
                        case 6:
                            SearchByName();
                            break;
                        default:
                            Console.WriteLine("Invalid option for Product.");
                            break;
                    }
                    break;

                case 3:
                    Console.WriteLine("1. CalculateTotalAmount");
                    Console.WriteLine("2. GetOrderDetails");
                    Console.WriteLine("3. UpdateOrderStatus");
                    Console.WriteLine("4. AddOrder");

                    int orderChoice = GetOption(4);

                    switch (orderChoice)
                    {
                        case 1:
                            CalculateTotalAmount();
                            break;
                        case 2:
                            GetOrderDetails();
                            break;
                        case 3:
                            UpdateOrderStatus();
                            break;
                        case 4:
                            AddOrder();
                            break;
                        default:
                            Console.WriteLine("Invalid option for Order.");
                            break;
                    }
                    break;

                case 4:
                    Console.WriteLine("1. CalculateSubtotal");
                    Console.WriteLine("2. GetOrderDetailInfo");
                    Console.WriteLine("3. UpdateQuantity");
                    Console.WriteLine("4. AddDiscount");

                    int orderDetailsChoice = GetOption(4);

                    switch (orderDetailsChoice)
                    {
                        case 1:
                            CalculateSubtotal();
                            break;
                        case 2:
                            GetOrderDetailInfo();
                            break;
                        case 3:
                            updateQuantity();
                            break;
                        case 4:
                            adddiscount();
                            break;
                        default:
                            Console.WriteLine("Invalid option for Order Details.");
                            break;
                    }
                    break;

                case 5:
                    Console.WriteLine("1. GetProduct");
                    Console.WriteLine("2. GetQuantityInStock");
                    Console.WriteLine("3. AddToInventory");
                    Console.WriteLine("4. RemoveFromInventory");
                    Console.WriteLine("5. UpdateStockQuantity");
                    Console.WriteLine("6. IsProductAvailable");
                    Console.WriteLine("7. GetInventoryValue");
                    Console.WriteLine("8. ListLowStockProducts");
                    Console.WriteLine("9. ListOutOfStockProducts");
                    Console.WriteLine("10. ListAllProducts");

                    int inventoryChoice = GetOption(10);

                    switch (inventoryChoice)
                    {
                        case 1:
                            GetProduct();
                            break;
                        case 2:
                            GetQuantityInStock();
                            break;
                        case 3:
                            AddToInventory();
                            break;
                        case 4:
                            RemoveFromInventory();
                            break;
                        case 5:
                            UpdateStockQuantity();
                            break;
                        case 6:
                            IsProductAvailable();
                            break;
                        case 7:
                            GetInventoryValue();
                            break;
                        case 8:
                            ListLowStockProducts();
                            break;
                        case 9:
                            ListOutOfStockProducts();
                            break;
                        case 10:
                            ListAllProducts();
                            break;
                        default:
                            Console.WriteLine("Invalid option for Inventory.");
                            break;
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            break;

        case 2:
            RegisterUser();
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}
    

    static int GetOption(int maxOption)
{
    int option;

    do
    {
        Console.Write("Enter option: ");
    } while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > maxOption);

    return option;
}





/////////customers

/////////1.



void CalculateTotalOrders()
{
    CustomersRepo customersRepo = new CustomersRepo();

    Console.WriteLine("Enter customer ID to calculate total orders:");
    if (int.TryParse(Console.ReadLine(), out int customerId))
    {
        int totalOrders = customersRepo.CalculateTotalOrders(customerId);
        Console.WriteLine($"Total orders for customer {customerId}: {totalOrders}");
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid customer ID.");
    }
}


/////2.
void GetCustomerDetails()
{
    CustomersRepo customersRepo = new CustomersRepo();

    Console.WriteLine("Enter customer ID to get details:");
    if (int.TryParse(Console.ReadLine(), out int customerId))
    {
        Customers customer = customersRepo.GetCustomerDetails(customerId);

        if (customer != null)
        {
            Console.WriteLine($"Customer Details for ID {customer.CustomerID}:\n");
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone: {customer.Phone}");
            Console.WriteLine($"Address: {customer.Address}");
        }
        else
        {
            Console.WriteLine($"Customer with ID {customerId} not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid customer ID.");
    }
}

//3.
void UpdateCustomerInfo()
{
    CustomersRepo customersRepo = new CustomersRepo();

    Console.WriteLine("Enter customer ID to update:");
    if (int.TryParse(Console.ReadLine(), out int customerId))
    {

        Customers existingCustomer = customersRepo.GetCustomerDetails(customerId);

        if (existingCustomer != null)
        {
            Console.WriteLine($"Existing Customer Details for ID {customerId}:\n");
            Console.WriteLine($"Name: {existingCustomer.FirstName} {existingCustomer.LastName}");
            Console.WriteLine($"Email: {existingCustomer.Email}");
            Console.WriteLine($"Phone: {existingCustomer.Phone}");
            Console.WriteLine($"Address: {existingCustomer.Address}\n");

            Console.Write("Enter new email: ");
            string newEmail = Console.ReadLine();

            Console.Write("Enter new phone: ");
            string newPhone = Console.ReadLine();


            customersRepo.UpdateCustomerInfo(customerId, newEmail, newPhone);

            Console.WriteLine($"Customer information updated for ID {customerId}.\n");


            Customers updatedCustomer = customersRepo.GetCustomerDetails(customerId);
            Console.WriteLine($"Updated Customer Details for ID {customerId}:\n");
            Console.WriteLine($"Name: {updatedCustomer.FirstName} {updatedCustomer.LastName}");
            Console.WriteLine($"Email: {updatedCustomer.Email}");
            Console.WriteLine($"Phone: {updatedCustomer.Phone}");
            Console.WriteLine($"Address: {updatedCustomer.Address}");
        }
        else
        {
            Console.WriteLine($"Customer with ID {customerId} not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid customer ID.");
    }
}
















// PRODUCT
//1.
void GetProductDetails()
{
    ProductsRepo productsRepo = new ProductsRepo();

    Console.WriteLine("Enter product ID to get details:");
    if (int.TryParse(Console.ReadLine(), out int productId))
    {
        Products product = productsRepo.GetProductDetails(productId);

        if (product != null)
        {
            Console.WriteLine($"Product Details for ID {product.ProductID}:\n");
            Console.WriteLine($"Product Name: {product.ProductName}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Price: {product.Price}");

        }
        else
        {
            Console.WriteLine($"Product with ID {productId} not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid product ID.");
    }
}


//2.
void UpdateProductInfo()
{
    ProductsRepo productsRepo = new ProductsRepo();

    Console.WriteLine("Enter product ID to update details:");
    if (int.TryParse(Console.ReadLine(), out int productId))
    {
        Console.Write("Enter new price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
        {
            Console.Write("Enter new description: ");
            string newDescription = Console.ReadLine();

            productsRepo.UpdateProductInfo(productId, newPrice, newDescription);

            Console.WriteLine($"Product information updated for ID {productId}.");


            Products updatedProduct = productsRepo.GetProductDetails(productId);

            if (updatedProduct != null)
            {
                Console.WriteLine($"Updated Product Details for ID {updatedProduct.ProductID}:\n");
                Console.WriteLine($"Product Name: {updatedProduct.ProductName}");
                Console.WriteLine($"Description: {updatedProduct.Description}");
                Console.WriteLine($"Price: {updatedProduct.Price:C}");

            }
            else
            {
                Console.WriteLine($"Product with ID {productId} not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input for price. Please enter a valid decimal value.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid product ID.");
    }
}

//3.
void IsProductInStock()
{
    ProductsRepo productsRepo = new ProductsRepo();

    Console.WriteLine("Enter product ID to check stock:");
    if (int.TryParse(Console.ReadLine(), out int productId))
    {
        bool isProductInStock = productsRepo.IsProductInStock(productId);

        if (isProductInStock)
        {
            Console.WriteLine($"Product with ID {productId} is in stock.");
        }
        else
        {
            Console.WriteLine($"Product with ID {productId} is out of stock.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid product ID.");
    }
}


















// ORDERS

//1.
void CalculateTotalAmount()
{
    OrdersRepo ordersRepo = new OrdersRepo();

    Console.WriteLine("Enter order ID to calculate total amount:");
    if (int.TryParse(Console.ReadLine(), out int orderId))
    {
        decimal totalAmount = ordersRepo.CalculateTotalAmount(orderId);

        Console.WriteLine($"Total Amount for Order ID {orderId}: {totalAmount}");
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid order ID.");
    }
}

//2.
void GetOrderDetails()
{
    OrdersRepo ordersRepo = new OrdersRepo();

    Console.WriteLine("Enter order ID to get details:");
    if (int.TryParse(Console.ReadLine(), out int orderId))
    {
        // Call the GetOrderDetails method
        ordersRepo.GetOrderDetails(orderId);
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid order ID.");
    }
}

//3.
void UpdateOrderStatus()
{
    OrdersRepo ordersRepo = new OrdersRepo();

    Console.WriteLine("Enter order ID to update status:");
    if (int.TryParse(Console.ReadLine(), out int orderId))
    {
        Console.WriteLine("Enter new status (e.g., processing, shipped):");
        string newStatus = Console.ReadLine();

        ordersRepo.UpdateOrderStatus(orderId, newStatus);
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid order ID.");
    }
}

//4.
















///////    ORDER DETAIlS

//1.
void CalculateSubtotal()
{
    OrderDetailsRepo ordersRepo = new OrderDetailsRepo();

    Console.WriteLine("Enter OrderDetail ID to calculate subtotal:");
    if (int.TryParse(Console.ReadLine(), out int orderDetailId))
    {
        decimal subtotal = ordersRepo.CalculateSubtotal(orderDetailId);

        if (subtotal > 0)
        {
            Console.WriteLine($"Subtotal for OrderDetail ID {orderDetailId}: {subtotal}");
        }
        else
        {
            Console.WriteLine($"Failed to calculate subtotal for OrderDetail ID {orderDetailId}.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid OrderDetail ID.");
    }
}

////2.
void GetOrderDetailInfo()
{
    OrderDetailsRepo orderDetailsRepo = new OrderDetailsRepo();

    Console.WriteLine("Enter OrderDetail ID to get information:");
    if (int.TryParse(Console.ReadLine(), out int orderDetailId))
    {
        orderDetailsRepo.GetOrderDetailInfo(orderDetailId);
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid OrderDetail ID.");
    }
}


//3. 
void updateQuantity()
{
    OrderDetailsRepo orderDetailsRepo = new OrderDetailsRepo();

    Console.WriteLine("Enter OrderDetail ID to update quantity:");
    if (int.TryParse(Console.ReadLine(), out int orderDetailId))
    {
        Console.WriteLine("Enter new quantity:");
        if (int.TryParse(Console.ReadLine(), out int newQuantity))
        {
            orderDetailsRepo.UpdateQuantity(orderDetailId, newQuantity);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid quantity.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid OrderDetail ID.");
    }
}


//4.
void adddiscount()
{
    OrderDetailsRepo orderDetailsRepo = new OrderDetailsRepo();

    Console.WriteLine("Enter OrderDetail ID to apply discount:");
    if (int.TryParse(Console.ReadLine(), out int orderDetailId))
    {
        Console.WriteLine("Enter discount amount:");
        if (decimal.TryParse(Console.ReadLine(), out decimal discountAmount))
        {
            orderDetailsRepo.AddDiscount(orderDetailId, discountAmount);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid discount amount.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid OrderDetail ID.");
    }
}


#region Inventory

////////////Inventory


//1.

void GetProduct()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    Console.WriteLine("Enter Inventory ID to get associated product:");
    if (int.TryParse(Console.ReadLine(), out int inventoryId))
    {
        Products product = inventoryRepo.GetProduct(inventoryId);

        if (product != null)
        {
            Console.WriteLine($"Product ID: {product.ProductID}");
            Console.WriteLine($"Product Name: {product.ProductName}");
            // Add other product details as needed
        }
        else
        {
            Console.WriteLine($"Product not found for Inventory ID {inventoryId}.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid Inventory ID.");
    }
}


//2.

void GetQuantityInStock()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    Console.WriteLine("Enter Inventory ID to get quantity in stock:");
    if (int.TryParse(Console.ReadLine(), out int inventoryId))
    {
        int quantityInStock = inventoryRepo.GetQuantityInStock(inventoryId);

        Console.WriteLine($"Quantity in Stock for Inventory ID {inventoryId}: {quantityInStock}");
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid Inventory ID.");
    }
}


//3.


void AddToInventory()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    Console.WriteLine("Enter Inventory ID to add quantity:");
    if (int.TryParse(Console.ReadLine(), out int inventoryId))
    {
        Console.WriteLine("Enter quantity to add:");
        if (int.TryParse(Console.ReadLine(), out int quantity))
        {
            inventoryRepo.AddToInventory(inventoryId, quantity);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid quantity.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid Inventory ID.");
    }
}




//4.



void RemoveFromInventory()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    Console.WriteLine("Enter Inventory ID to remove quantity:");
    if (int.TryParse(Console.ReadLine(), out int inventoryId))
    {
        Console.WriteLine("Enter quantity to remove:");
        if (int.TryParse(Console.ReadLine(), out int quantity))
        {
            inventoryRepo.RemoveFromInventory(inventoryId, quantity);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid quantity.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid Inventory ID.");
    }
}



//5.



void UpdateStockQuantity()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    Console.WriteLine("Enter Inventory ID to update stock quantity:");
    if (int.TryParse(Console.ReadLine(), out int inventoryId))
    {
        Console.WriteLine("Enter new quantity:");
        if (int.TryParse(Console.ReadLine(), out int newQuantity))
        {
            inventoryRepo.UpdateStockQuantity(inventoryId, newQuantity);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid new quantity.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid Inventory ID.");
    }
}



//6.




void IsProductAvailable()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    Console.WriteLine("Enter Inventory ID to check availability:");
    if (int.TryParse(Console.ReadLine(), out int inventoryId))
    {
        Console.WriteLine("Enter quantity to check:");
        if (int.TryParse(Console.ReadLine(), out int quantityToCheck))
        {
            bool isAvailable = inventoryRepo.IsProductAvailable(inventoryId, quantityToCheck);

            if (isAvailable)
            {
                Console.WriteLine($"The specified quantity is available in Inventory ID {inventoryId}.");
            }
            else
            {
                Console.WriteLine($"Insufficient stock in Inventory ID {inventoryId}.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid quantity to check.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid Inventory ID.");
    }
}




//7.


void GetInventoryValue()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    decimal totalInventoryValue = inventoryRepo.GetInventoryValue();

    Console.WriteLine($"Total value of products in the inventory: {totalInventoryValue}");
}



//8.


void ListLowStockProducts()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    Console.WriteLine("Enter threshold quantity to list low stock products:");
    if (int.TryParse(Console.ReadLine(), out int threshold))
    {
        inventoryRepo.ListLowStockProducts(threshold);
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid threshold quantity.");
    }
}



//9.



void ListOutOfStockProducts()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    inventoryRepo.ListOutOfStockProducts();
}




//10.

void ListAllProducts()
{
    InventoryRepo inventoryRepo = new InventoryRepo();

    inventoryRepo.ListAllProducts();
}

#endregion

void AddProduct()
{
    Products newProduct = new Products();



    Console.WriteLine("Enter the product name:");
    newProduct.ProductName = Console.ReadLine();

    Console.WriteLine("Enter the product description:");
    newProduct.Description = Console.ReadLine();

    Console.WriteLine("Enter the product price:");
    newProduct.Price = Convert.ToDecimal(Console.ReadLine());

    ProductsRepo productsRepo = new ProductsRepo();
    productsRepo.AddProduct(newProduct);

    // Example usage of IsProductExists
    Console.WriteLine("Enter the product ID to check existence:");
    int productIdToCheck = Convert.ToInt32(Console.ReadLine());

    bool productExists = productsRepo.IsProductExists(productIdToCheck);

    if (productExists)
    {
        Console.WriteLine($"Product with ID {productIdToCheck} exists.");
    }
    else
    {
        Console.WriteLine($"Product with ID {productIdToCheck} does not exist.");
    }

}


void UpdateProduct()
{

    ProductsRepo productRepo = new ProductsRepo();


    Console.WriteLine("Enter the ID of the product you want to update:");
    int productIdToUpdate = Convert.ToInt32(Console.ReadLine());


    Products updatedProduct = new Products
    {
        ProductID = productIdToUpdate
    };

    Console.WriteLine("Enter the updated product name:");
    updatedProduct.ProductName = Console.ReadLine();

    Console.WriteLine("Enter the updated product description:");
    updatedProduct.Description = Console.ReadLine();

    Console.WriteLine("Enter the updated product price:");
    updatedProduct.Price = Convert.ToDecimal(Console.ReadLine());


    productRepo.UpdateProduct(updatedProduct);


}


void RemoveProduct()
{
    ProductsRepo productRepo = new ProductsRepo();


    Console.WriteLine("Enter the ID of the product you want to remove:");
    int productIdToRemove = Convert.ToInt32(Console.ReadLine());


    productRepo.RemoveProduct(productIdToRemove);

}





void AddOrder()
{
    OrdersRepo ordersRepo = new OrdersRepo();


    Console.WriteLine("Enter customer ID:");
    int customerId = int.Parse(Console.ReadLine());


    Customers customer = new Customers
    {
        CustomerID = customerId,

    };


    Console.WriteLine("Enter total amount:");
    decimal totalAmount = decimal.Parse(Console.ReadLine());


    Orders order = new Orders
    {
        Customer = customer,
        OrderDate = DateTime.Now,
        TotalAmount = totalAmount,
        Status = "Pending"
    };


    ordersRepo.AddOrder(order);


    Console.WriteLine($"Order ID after insertion: {order.OrderID}");
}




void RemoveCanceledOders()
{
    OrdersRepo ordersRepo = new OrdersRepo();


    Console.WriteLine("Enter the Order ID to remove:");
    int orderIdToRemove = Convert.ToInt32(Console.ReadLine());


    ordersRepo.RemoveOrder(orderIdToRemove);
}




void SortByDate()
{
    OrdersRepo ordersRepo = new OrdersRepo();


    List<Orders> sortedOrders = ordersRepo.GetOrdersSortedByDateAscending();


    foreach (var order in sortedOrders)
    {
        Console.WriteLine($"OrderID: {order.OrderID}, OrderDate: {order.OrderDate}, TotalAmount: {order.TotalAmount}, Status: {order.Status}");

        Console.WriteLine($"CustomerID: {order.Customer.CustomerID}, FirstName: {order.Customer.FirstName}, LastName: {order.Customer.LastName}");
    }

}



void InventoryManangementWithSortedList()
{

    InventoryRepo inventoryRepo = new InventoryRepo();

    // Get the inventory
    SortedList<int, Inventory> inventory = inventoryRepo.GetInventory();

    // Display the inventory
    Console.WriteLine("Inventory Details:");
    foreach (var kvp in inventory)
    {
        Console.WriteLine($"Product ID: {kvp.Key}");
        DisplayProductDetails(kvp.Value.Product);
        Console.WriteLine($"Quantity In Stock: {kvp.Value.QuantityInStock}");
        Console.WriteLine($"Last Stock Update: {kvp.Value.LastStockUpdate}");
        Console.WriteLine("--------------");
    }


    static void DisplayProductDetails(Products product)
    {

        if (product != null)
        {
            Console.WriteLine($"Product Name: {product.ProductName}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Price: {product.Price}");
        }
        else
        {
            Console.WriteLine("Product details not found.");
        }

    }
}









void UpdateInventoryOnOrderPlacement()
{
    try
    {
        // Accept user input for product ID
        Console.Write("Enter Product ID: ");
        int productId = int.Parse(Console.ReadLine());

        // Accept user input for quantity ordered
        Console.Write("Enter Quantity Ordered: ");
        int quantityOrdered = int.Parse(Console.ReadLine());

        // Create an instance of your InventoryRepo class
        InventoryRepo inventoryRepo = new InventoryRepo();

        // Call the method to update inventory on order placement
        inventoryRepo.UpdateInventoryOnOrderPlacement(productId, quantityOrdered);

        // Continue with the rest of your order processing logic here...

        Console.WriteLine("Order processed successfully.");
    }
    catch (Exception ex)
    {
        // Handle or log the exception as needed
        Console.WriteLine($"Error processing order: {ex.Message}");
    }
}





void SearchByName()
{

    ProductsRepo productsRepo = new ProductsRepo();


    Console.Write("Enter product name to search: ");
    string searchName = Console.ReadLine();


    List<Products> searchResults = productsRepo.SearchProductsByName(searchName);

    Console.WriteLine("Search Results:");
    foreach (var product in searchResults)
    {
        Console.WriteLine($"ProductID: {product.ProductID}, ProductName: {product.ProductName}, Description: {product.Description}");
    }
}



void DuplicateProductHandling()
{
    ProductsRepo productsRepo = new ProductsRepo();


    Console.Write("Enter product name: ");
    string productName = Console.ReadLine();

    Console.Write("Enter product description: ");
    string description = Console.ReadLine();

    Console.Write("Enter product price: ");
    if (double.TryParse(Console.ReadLine(), out double price))
    {

        Products newProduct = new Products
        {
            ProductName = productName,
            Description = description,
            Price = (decimal)price

        };

        try
        {

            productsRepo.DuplicateAddProduct(newProduct);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Invalid price format. Please enter a valid number.");
    }
}






void CustomerLogin()
{

    CustomersRepo repo = new CustomersRepo();

    Console.WriteLine("Enter the email to validate:");
    string userEmail = Console.ReadLine();

    bool isValidUser = repo.ValidateUserByEmail(userEmail);

    if (isValidUser)
    {
        Console.WriteLine("Welcome To Tech!");
    }
    else
    {
        Console.WriteLine("Not A Registered User. Please Register.");
    }
}








void RegisterUser()
{


    CustomersRepo repo = new CustomersRepo();

    try
    {
        Console.WriteLine("Enter First Name:");
        string firstName = Console.ReadLine();

        Console.WriteLine("Enter Last Name:");
        string lastName = Console.ReadLine();

        Console.WriteLine("Enter Email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter Phone:");
        string phone = Console.ReadLine();

        Console.WriteLine("Enter Address:");
        string address = Console.ReadLine();

        bool isRegistered = repo.RegisterUser(firstName, lastName, email, phone, address);

        if (isRegistered)
        {
            Console.WriteLine("User registered successfully!");
        }
        else
        {
            Console.WriteLine("Failed to register the user.");
        }
    }
    catch (InvalidDataException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}