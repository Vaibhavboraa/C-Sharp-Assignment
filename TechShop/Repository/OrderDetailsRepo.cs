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
    internal class OrderDetailsRepo
    {
        public string connectionString;
        SqlCommand cmd = null;
        public OrderDetailsRepo()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public decimal CalculateSubtotal(int orderDetailId)
        {
            decimal subtotal = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT Quantity, Products.Price FROM OrderDetails INNER JOIN Product ON OrderDetails.ProductID = Products.ProductID WHERE OrderDetailID = @OrderDetailId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@OrderDetailId", orderDetailId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int quantity = (int)reader["Quantity"];
                    decimal productPrice = (decimal)reader["Price"];

                   
                    subtotal = quantity * productPrice;
                }
            }

            return subtotal;
        }

        public void GetOrderDetailInfo(int orderDetailId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@OrderDetailId", orderDetailId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Order Detail Information for Order Detail ID {orderDetailId}:\n");
                    Console.WriteLine($"Order ID: {reader["OrderID"]}");
                    Console.WriteLine($"Product ID: {reader["ProductID"]}");
                    Console.WriteLine($"Quantity: {reader["Quantity"]}");
                   
                }
                else
                {
                    Console.WriteLine($"Order Detail ID {orderDetailId} not found.");
                }
            }
        }

        public void UpdateQuantity(int orderDetailId, int newQuantity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE OrderDetails SET Quantity = @NewQuantity WHERE OrderDetailID = @OrderDetailId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);
                cmd.Parameters.AddWithValue("@OrderDetailId", orderDetailId);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Quantity updated successfully for Order Detail ID {orderDetailId}.");
                }
                else
                {
                    Console.WriteLine($"Failed to update quantity for Order Detail ID {orderDetailId}.");
                }
            }
        }

        public void AddDiscount(int orderDetailId, decimal discountAmount)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE OrderDetails SET Discount = @DiscountAmount WHERE OrderDetailID = @OrderDetailId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                cmd.Parameters.AddWithValue("@OrderDetailId", orderDetailId);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Discount applied successfully for Order Detail ID {orderDetailId}.");
                }
                else
                {
                    Console.WriteLine($"Failed to apply discount for Order Detail ID {orderDetailId}.");
                }
            }
        }

    }
}
