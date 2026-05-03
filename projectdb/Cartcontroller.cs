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
    WHERE o.UserId = @UserId AND o.Status = 'Pending'";

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
            string query = "UPDATE Orders SET Status = 'Confirmed' WHERE UserId = @UserId";
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
                     WHERE o.UserId = @UserId AND o.Status = 'Pending'";

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
                     AND o.Status = 'Pending'";

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

    }
}
