using PayXpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal class ProductsRepo : IProductsRepo
    {

        public string connectionString;
        SqlCommand cmd = null;
        public ProductsRepo()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
            //for collection
            productList = new List<Products>();
        }
        public List<Products> productList;

        public Products GetProductDetails(int productId)
        {
            Products product = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE ProductID = @ProductId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    product = new Products
                    {
                        ProductID = (int)reader["ProductID"],
                        ProductName = reader["ProductName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"]

                    };
                }
            }

            return product;
        }
        public void UpdateProductInfo(int productId, decimal newPrice, string newDescription)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE Products SET Price = @NewPrice, Description = @NewDescription WHERE ProductID = @ProductId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@NewPrice", newPrice);
                cmd.Parameters.AddWithValue("@NewDescription", newDescription);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool IsProductInStock(int productId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT QuantityInStock FROM Inventory WHERE ProductID = @ProductId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);

                sqlConnection.Open();
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    int quantityInStock = Convert.ToInt32(result);
                    return quantityInStock > 0;
                }

                return false;
            }

        }


        ////collections


        public void AddProduct(Products product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the product with the same ID already exists
                if (!IsProductExists( product.ProductID))
                {
                    string insertQuery = "INSERT INTO Products (ProductName, Description, Price) " +
                      "VALUES (@ProductName, @Description, @Price)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                        cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@Price", product.Price);

                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine($"Product added successfully: {product.ProductName}");
                }
                else
                {
                    Console.WriteLine($"Product with ID {product.ProductID} already exists. Cannot add duplicate products.");
                }
            }
        }


        public bool IsProductExists(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); 

                string selectQuery = "SELECT COUNT(*) FROM Products WHERE ProductID = @ProductID";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    int productCount = (int)cmd.ExecuteScalar();

                    return productCount > 0;
                }
            }
        }




        //public bool IsProductExists( int productId)
        //{
        //    string selectQuery = "SELECT COUNT(*) FROM Products WHERE ProductID = @ProductID";

        //    using (SqlCommand cmd = new SqlCommand(selectQuery))
        //    {
        //        cmd.Parameters.AddWithValue("@ProductID", productId);

        //        int productCount = (int)cmd.ExecuteScalar();

        //        return productCount > 0;
        //    }
        //}

        public void UpdateProduct(Products updatedProduct)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the product with the specified ID exists
                if (IsProductExists (updatedProduct.ProductID))
                {
                    string updateQuery = "UPDATE Products " +
                                         "SET ProductName = @ProductName, Description = @Description, Price = @Price " +
                                         "WHERE ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", updatedProduct.ProductName);
                        cmd.Parameters.AddWithValue("@Description", updatedProduct.Description);
                        cmd.Parameters.AddWithValue("@Price", updatedProduct.Price);
                        cmd.Parameters.AddWithValue("@ProductID", updatedProduct.ProductID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Product with ID {updatedProduct.ProductID} updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update product with ID {updatedProduct.ProductID}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Product with ID {updatedProduct.ProductID} does not exist. Cannot update.");
                }
            }
        }

        public void RemoveProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the product with the specified ID exists
                if (IsProductExists(productId))
                {
                    string deleteQuery = "DELETE FROM Products WHERE ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Product with ID {productId} removed successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to remove product with ID {productId}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Product with ID {productId} does not exist. Cannot remove.");
                }
            }
        }


        public List<Products> SearchProductsByName(string searchName)
        {
            List<Products> result = new List<Products>();

            
            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Products WHERE ProductName LIKE @SearchName";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@SearchName", "%" + searchName + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                Products product = new Products
                                {
                                    ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                    ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                   
                                };

                                result.Add(product);
                            }
                        }
                    }
                }
            
           

            return result;
        }


        public void DuplicateAddProduct(Products product)
        {
            // Check for duplicate product by name
            if (IsDuplicateProduct(product))
            {
               
                throw new InvalidOperationException("Product with the same name already exists.");
               
            }

            // If not a duplicate, proceed to add the product
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertProductQuery = "INSERT INTO Products (ProductName, Description, Price) " +
                                            "VALUES (@ProductName, @Description, @Price); " +
                                            "SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(insertProductQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    int productId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Update the product object with the generated ProductID
                    product.ProductID = productId;

                    // Add the product to the list
                    productList.Add(product);

                    Console.WriteLine($"Product added successfully. ProductID: {productId}");
                }
            }
        }

        public bool IsDuplicateProduct(Products newProduct)
        {
            // Check for duplicates by name
            return productList.Any(p => p.ProductName.Equals(newProduct.ProductName, StringComparison.OrdinalIgnoreCase));
        }


    }
}
