using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectdb
{
    public class Cartcontroller
    {

        public DataTable GetCartByUserId(int userId)
        {
            projectdb.DatabaseService dbService = new projectdb.DatabaseService();
            DataTable table = new DataTable();

            using (SqlConnection con = dbService.GetConnection())
            {
                // This query finds the OrderID first, then joins to get the items
                string query = @"
    SELECT oi.ProductId, 
           p.Name AS [Product], 
           oi.Quantity, 
           oi.UnitPrice AS [Price], 
           (oi.Quantity * oi.UnitPrice) AS [Total]
    FROM OrderItem oi
    JOIN Product p ON oi.ProductId = p.ProductId
    JOIN Orders o ON oi.OrderId = o.OrderId
            WHERE o.UserId = @UserId
              AND UPPER(LTRIM(RTRIM(o.Status))) = 'PENDING'";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching cart: " + ex.Message);
                }
            }
            return table;
        }
        public bool ConfirmOrderByUserId(int userId)
        {
            string query = @"UPDATE Orders
SET Status = 'Confirmed'
WHERE UserId = @UserId
    AND UPPER(LTRIM(RTRIM(Status))) = 'PENDING'";
            projectdb.DatabaseService dbService = new projectdb.DatabaseService();

            using (SqlConnection con = dbService.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        // 1. Calculate the Grand Total
        public decimal GetCartTotal(int userId)
        {
            string query = @"SELECT SUM(oi.Quantity * oi.UnitPrice) 
                     FROM OrderItem oi
                     JOIN Orders o ON oi.OrderId = o.OrderId
                                         WHERE o.UserId = @UserId
                                             AND UPPER(LTRIM(RTRIM(o.Status))) = 'PENDING'";

            projectdb.DatabaseService dbService = new projectdb.DatabaseService();
            using (SqlConnection con = dbService.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();

                object result = cmd.ExecuteScalar(); // ExecuteScalar is best for single values
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        // 2. Remove an Item
        public bool RemoveFromCart(int userId, int productId)
        {
            // We target the OrderItem belonging to the user's 'Pending' order
            string query = @"DELETE oi FROM OrderItem oi
                     JOIN Orders o ON oi.OrderId = o.OrderId
                     WHERE o.UserId = @UserId AND oi.ProductId = @ProductId 
                     AND UPPER(LTRIM(RTRIM(o.Status))) = 'PENDING'";

            projectdb.DatabaseService dbService = new projectdb.DatabaseService();
            using (SqlConnection con = dbService.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AddToCart(int userId, int productId, int quantity, decimal unitPrice)
        {
            projectdb.DatabaseService dbService = new projectdb.DatabaseService();
            using (SqlConnection con = dbService.GetConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    // 1. Get or Create Pending Order
                    int orderId = -1;
                    string checkOrder = @"SELECT TOP 1 OrderId
FROM Orders
WHERE UserId = @UserId
    AND UPPER(LTRIM(RTRIM(Status))) = 'PENDING'
ORDER BY OrderId DESC";
                    using (SqlCommand cmd = new SqlCommand(checkOrder, con, trans))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            orderId = Convert.ToInt32(result);
                        }
                        else
                        {
                            string createOrder = "INSERT INTO Orders (UserId, Status, CreatedAt) OUTPUT INSERTED.OrderId VALUES (@UserId, 'Pending', GETDATE())";
                            using (SqlCommand cmdInsert = new SqlCommand(createOrder, con, trans))
                            {
                                cmdInsert.Parameters.AddWithValue("@UserId", userId);
                                orderId = (int)cmdInsert.ExecuteScalar();
                            }
                        }
                    }

                    // 2. Check if product already in cart
                    string checkItem = "SELECT Quantity FROM OrderItem WHERE OrderId = @OrderId AND ProductId = @ProductId";
                    using (SqlCommand cmd = new SqlCommand(checkItem, con, trans))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Update quantity
                            string updateItem = "UPDATE OrderItem SET Quantity = Quantity + @Qty WHERE OrderId = @OrderId AND ProductId = @ProductId";
                            using (SqlCommand cmdUpd = new SqlCommand(updateItem, con, trans))
                            {
                                cmdUpd.Parameters.AddWithValue("@Qty", quantity);
                                cmdUpd.Parameters.AddWithValue("@OrderId", orderId);
                                cmdUpd.Parameters.AddWithValue("@ProductId", productId);
                                cmdUpd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Insert new item
                            string insertItem = "INSERT INTO OrderItem (OrderId, ProductId, Quantity, UnitPrice) VALUES (@OrderId, @ProductId, @Qty, @Price)";
                            using (SqlCommand cmdIns = new SqlCommand(insertItem, con, trans))
                            {
                                cmdIns.Parameters.AddWithValue("@OrderId", orderId);
                                cmdIns.Parameters.AddWithValue("@ProductId", productId);
                                cmdIns.Parameters.AddWithValue("@Qty", quantity);
                                cmdIns.Parameters.AddWithValue("@Price", unitPrice);
                                cmdIns.ExecuteNonQuery();
                            }
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Error adding to cart: " + ex.Message);
                }
            }
        }
    }
}
