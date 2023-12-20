using PayXpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal class InventoryRepo : IInventoryRepo
    {

        public string connectionString;
        SqlCommand cmd = null;
        public InventoryRepo()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public Products GetProduct(int inventoryId)
        {
            Products product = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ProductID FROM Inventory WHERE InventoryID = @InventoryId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                sqlConnection.Open();
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    int productId = Convert.ToInt32(result);


                    ProductsRepo productsRepo = new ProductsRepo();
                    product = productsRepo.GetProductDetails(productId);
                }
            }

            return product;
        }

        public int GetQuantityInStock(int inventoryId)
        {
            int quantityInStock = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT QuantityInStock FROM Inventory WHERE InventoryID = @InventoryId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                sqlConnection.Open();
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    quantityInStock = Convert.ToInt32(result);
                }
            }

            return quantityInStock;
        }


        public void AddToInventory(int inventoryId, int quantity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE Inventory SET QuantityInStock = QuantityInStock + @Quantity WHERE InventoryID = @InventoryId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Added {quantity} units to Inventory ID {inventoryId} successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to add quantity to Inventory ID {inventoryId}.");
                }
            }
        }

        public void RemoveFromInventory(int inventoryId, int quantity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE Inventory SET QuantityInStock = QuantityInStock - @Quantity WHERE InventoryID = @InventoryId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Removed {quantity} units from Inventory ID {inventoryId} successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to remove quantity from Inventory ID {inventoryId}. Insufficient stock.");
                }
            }


        }

        public void UpdateStockQuantity(int inventoryId, int newQuantity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE Inventory SET QuantityInStock = @NewQuantity WHERE InventoryID = @InventoryId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Stock quantity for Inventory ID {inventoryId} updated to {newQuantity} successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to update stock quantity for Inventory ID {inventoryId}.");
                }
            }
        }

        public bool IsProductAvailable(int inventoryId, int quantityToCheck)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT QuantityInStock FROM Inventory WHERE InventoryID = @InventoryId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                sqlConnection.Open();
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    int currentQuantity = Convert.ToInt32(result);
                    return currentQuantity >= quantityToCheck;
                }

                return false;
            }
        }


        public decimal GetInventoryValue()
        {
            decimal totalValue = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ProductID, QuantityInStock FROM Inventory", sqlConnection))
            {
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int productId = (int)reader["ProductID"];
                    int quantityInStock = (int)reader["QuantityInStock"];

                    // Fetch product details to get the price
                    ProductsRepo productsRepo = new ProductsRepo();
                    Products product = productsRepo.GetProductDetails(productId);

                    if (product != null)
                    {
                        decimal productValue = product.Price * quantityInStock;
                        totalValue += productValue;
                    }
                }
            }

            return totalValue;
        }


        public void ListLowStockProducts(int threshold)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ProductID, QuantityInStock FROM Inventory WHERE QuantityInStock < @Threshold", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Threshold", threshold);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"Products with quantities below {threshold} (Low Stock):\n");

                while (reader.Read())
                {
                    int productId = (int)reader["ProductID"];
                    int quantityInStock = (int)reader["QuantityInStock"];

                    // Fetch product details
                    ProductsRepo productsRepo = new ProductsRepo();
                    Products product = productsRepo.GetProductDetails(productId);

                    if (product != null)
                    {
                        Console.WriteLine($"Product ID: {productId}");
                        Console.WriteLine($"Product Name: {product.ProductName}");
                        Console.WriteLine($"Quantity in Stock: {quantityInStock}\n");
                    }
                }
            }



        }

        public void ListOutOfStockProducts()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ProductID FROM Inventory WHERE QuantityInStock <= 0", sqlConnection))
            {
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Products Out of Stock:\n");

                while (reader.Read())
                {
                    int productId = (int)reader["ProductID"];

                    // Fetch product details
                    ProductsRepo productsRepo = new ProductsRepo();
                    Products product = productsRepo.GetProductDetails(productId);

                    if (product != null)
                    {
                        Console.WriteLine($"Product ID: {productId}");
                        Console.WriteLine($"Product Name: {product.ProductName}\n");
                    }
                }
            }
        }

        public void ListAllProducts()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ProductID, QuantityInStock FROM Inventory", sqlConnection))
            {
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("All Products in Inventory:\n");

                while (reader.Read())
                {
                    int productId = (int)reader["ProductID"];
                    int quantityInStock = (int)reader["QuantityInStock"];

                    // Fetch product details
                    ProductsRepo productsRepo = new ProductsRepo();
                    Products product = productsRepo.GetProductDetails(productId);

                    if (product != null)
                    {
                        Console.WriteLine($"Product ID: {productId}");
                        Console.WriteLine($"Product Name: {product.ProductName}");
                        Console.WriteLine($"Quantity in Stock: {quantityInStock}\n");
                    }
                }
            }
        }

        //collection
        public SortedList<int, Inventory> GetInventory()
        {
            SortedList<int, Inventory> inventory = new SortedList<int, Inventory>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT InventoryID, ProductID, QuantityInStock, LastStockUpdate FROM Inventory";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int inventoryId = reader.GetInt32(reader.GetOrdinal("InventoryID"));
                        int productId = reader.GetInt32(reader.GetOrdinal("ProductID"));
                        int quantityInStock = reader.GetInt32(reader.GetOrdinal("QuantityInStock"));
                        DateTime lastStockUpdate = reader.GetDateTime(reader.GetOrdinal("LastStockUpdate"));

                       
                        Products product = GetProductById(productId);

                        
                        Inventory item = new Inventory
                        {
                            InventoryID = inventoryId,
                            Product = product,
                            QuantityInStock = quantityInStock,
                            LastStockUpdate = lastStockUpdate
                        };

                        // Add the item to the SortedList
                        inventory.Add(productId, item);
                    }
                }
            }

            return inventory;
        }

        public Products GetProductById(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT ProductID, ProductName, Description, Price FROM Products WHERE ProductID = @ProductId";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve product details from the database
                            string productName = reader.GetString(reader.GetOrdinal("ProductName"));
                            string description = reader.GetString(reader.GetOrdinal("Description"));
                            decimal price = reader.GetDecimal(reader.GetOrdinal("Price"));

                            // Create and return a Products object
                            return new Products
                            {
                                ProductID = productId,
                                ProductName = productName,
                                Description = description,
                                Price = price
                            };
                        }
                    }
                }
            }

            // Return null if the product is not found
            return null;
        }



        //public void UpdateInventory(int productId, int quantityChange)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Assuming there's a Products table with a QuantityInStock column
        //        string updateQuery = "UPDATE Products SET QuantityInStock = QuantityInStock + @QuantityChange WHERE ProductID = @ProductID";

        //        using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@QuantityChange", quantityChange);
        //            cmd.Parameters.AddWithValue("@ProductID", productId);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}


        public void UpdateInventoryOnOrderPlacement(int productId, int quantityOrdered)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Begin a transaction to ensure consistency
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Check current quantity in stock
                        int currentQuantityInStock = GetCurrentQuantityInStock(connection, productId, transaction);

                        // Check if there is enough stock to fulfill the order
                        if (currentQuantityInStock < quantityOrdered)
                        {
                            // If insufficient stock, roll back the transaction and throw an exception
                            transaction.Rollback();
                            throw new InvalidOperationException("Insufficient stock to fulfill the order.");
                        }

                        // Update quantity in stock
                        int newQuantityInStock = currentQuantityInStock - quantityOrdered;
                        UpdateQuantityInStock(connection, productId, newQuantityInStock, transaction);

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Handle specific exception type
                        Console.WriteLine($"Error updating inventory: {ex.Message}");
                        transaction.Rollback();
                        throw;
                    }
                    
                }
            }
        }


        private int GetCurrentQuantityInStock(SqlConnection connection, int productId, SqlTransaction transaction)
        {
            string selectQuery = "SELECT QuantityInStock FROM Inventory WHERE ProductID = @ProductId";

            using (SqlCommand cmd = new SqlCommand(selectQuery, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);
                return (int)cmd.ExecuteScalar();
            }
        }

        private void UpdateQuantityInStock(SqlConnection connection, int productId, int newQuantityInStock, SqlTransaction transaction)
        {
            string updateQuery = "UPDATE Inventory SET QuantityInStock = @NewQuantityInStock WHERE ProductID = @ProductId";

            using (SqlCommand cmd = new SqlCommand(updateQuery, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@NewQuantityInStock", newQuantityInStock);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.ExecuteNonQuery();
            }
        }




    }
}
