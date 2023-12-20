using PayXpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal class OrdersRepo
    {

        public string connectionString;
        SqlCommand cmd = null;
        public OrdersRepo()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public decimal CalculateTotalAmount(int orderId)
        {
            decimal totalAmount = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT TotalAmount FROM Orders WHERE OrderID = @OrderId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                sqlConnection.Open();
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    totalAmount = Convert.ToDecimal(result);
                }
            }

            return totalAmount;
        }
        public void GetOrderDetails(int orderId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM OrderDetails WHERE OrderID = @OrderId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"Order Details for Order ID {orderId}:\n");

                while (reader.Read())
                {
                    int productId = (int)reader["ProductID"];
                    int quantity = (int)reader["Quantity"];

                    ProductsRepo productsRepo = new ProductsRepo();
                    Products product = productsRepo.GetProductDetails(productId);

                    if (product != null)
                    {
                        Console.WriteLine($"Product ID: {productId}");
                        Console.WriteLine($"Product Name: {product.ProductName}");
                        Console.WriteLine($"Quantity: {quantity}\n");
                    }
                }

            }
        }
        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE Orders SET Status = @NewStatus WHERE OrderID = @OrderId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Order ID {orderId} status updated to {newStatus} successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to update order status for Order ID {orderId}.");
                }
            }
        }


        public void AddOrder(Orders order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();



                string insertOrderQuery = "INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, Status) " +
                                          "VALUES (@CustomerID, @OrderDate, @TotalAmount, @Status); " +
                                          "SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(insertOrderQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", order.Customer.CustomerID);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@Status", order.Status);

                    // Retrieve the generated OrderID after insertion
                    int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Update the order object with the generated OrderID
                    order.OrderID = orderId;

                    Console.WriteLine($"Order added successfully. OrderID: {orderId}");
                }
            }

        }

        public void RemoveOrder(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // First, check if the order exists
                if (IsOrderExists(connection, orderId))
                {
                    // Delete the order
                    string deleteOrderQuery = "DELETE FROM Orders WHERE OrderID = @OrderId";

                    using (SqlCommand cmd = new SqlCommand(deleteOrderQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Order ID {orderId} removed successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to remove order with ID {orderId}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderId} does not exist. Cannot remove.");
                }
            }
        }

        private bool IsOrderExists(SqlConnection connection, int orderId)
        {
            string selectQuery = "SELECT COUNT(*) FROM Orders WHERE OrderID = @OrderId";

            using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                int orderCount = (int)cmd.ExecuteScalar();

                return orderCount > 0;
            }
        }


        public List<Orders> GetOrdersSortedByDateAscending()
        {
            List<Orders> orders = new List<Orders>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Orders ORDER BY OrderDate ASC";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int orderId = reader.GetInt32(reader.GetOrdinal("OrderID"));
                        int customerId = reader.GetInt32(reader.GetOrdinal("CustomerID"));
                        DateTime orderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"));
                        decimal totalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));
                        string status = reader.GetString(reader.GetOrdinal("Status"));

                       
                        int customerID = reader.GetInt32(reader.GetOrdinal("CustomerID"));
                        string customerFirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string customerLastName = reader.GetString(reader.GetOrdinal("LastName"));
                        string customerEmail = reader.GetString(reader.GetOrdinal("Email"));
                        string customerPhone = reader.GetString(reader.GetOrdinal("Phone"));
                        string customerAddress = reader.GetString(reader.GetOrdinal("Address"));

                        
                        Customers customer = new Customers(customerID, customerFirstName, customerLastName, customerEmail, customerPhone, customerAddress);

                      
                        Orders order = new Orders(orderId, customer, orderDate, totalAmount, status);
                        orders.Add(order);
                    }
                }

               
                orders.Sort((o1, o2) => o1.OrderDate.CompareTo(o2.OrderDate));

                return orders;
            }
        }



    }
}

