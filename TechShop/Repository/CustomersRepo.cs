


using PayXpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechShop.Model;

namespace TechShop.Repository
{
    internal class CustomersRepo : ICustomerRepo
    {
        public string connectionString;
        SqlCommand cmd = null;
        public CustomersRepo()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public int CalculateTotalOrders(int customerId)
        {
            int totalOrders = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS TotalOrders FROM Orders WHERE CustomerID = @CustomerId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    totalOrders = (int)reader["TotalOrders"];
                }
            }

            return totalOrders;
        }

        public Customers GetCustomerDetails(int customerId)
        {
            Customers customer = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE CustomerID = @CustomerId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Create a Customers object with the retrieved data
                    customer = new Customers
                    {
                        CustomerID = (int)reader["CustomerID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    };
                }
            }

            return customer;
        }

        public void UpdateCustomerInfo(int customerId, string newEmail, string newPhone)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE Customers SET Email = @NewEmail, Phone = @NewPhone WHERE CustomerID = @CustomerId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@NewEmail", newEmail);
                cmd.Parameters.AddWithValue("@NewPhone", newPhone);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }




        }




        public bool ValidateUserByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Customers WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }





        public bool RegisterUser(string firstName, string lastName, string email, string phone, string address)
        {
            // Validate the email address
            if (!IsValidEmail(email))
            {
                throw new InvalidDataException("Invalid email address format");
            }

            // Check if the user with the given email already exists
            if (UserExists(email))
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            // Perform user registration
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) VALUES (@FirstName, @LastName, @Email, @Phone, @Address)";

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                    insertCommand.Parameters.AddWithValue("@LastName", lastName);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Phone", phone);
                    insertCommand.Parameters.AddWithValue("@Address", address);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        // Validate email address format
        private bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }

        // Check if the user with the given email already exists
        private bool UserExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Customers WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }





    }
}
