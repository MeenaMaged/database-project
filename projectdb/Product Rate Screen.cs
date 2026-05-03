using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace projectdb
{
    public partial class Product_Rate_Screen : Form
    {
        private int _currentUserId;
        private int _currentProductId;

        public Product_Rate_Screen(int userId, int productId)
        {
            InitializeComponent();
            _currentUserId = userId;
            _currentProductId = productId;
        }

        private void Product_Rate_Screen_Load(object sender, EventArgs e)
        {
       
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 5;

            
            if (lblReviewPreview != null)
            {
                lblReviewPreview.Text = "";
            }
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (lblReviewPreview != null)
            {
               
                lblReviewPreview.Text = textBox1.Text;
            }
        }

        private void submitReview_Click(object sender, EventArgs e)
        {
            int ratingValue = (int)numericUpDown1.Value;
            string comment = textBox1.Text;

            DatabaseService db = new DatabaseService();
            bool isSuccess = false;

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO Rate (UserId, ProductId, Rating, Review) 
                                 VALUES (@uID, @pID, @val, @comment)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uID", _currentUserId);
                        cmd.Parameters.AddWithValue("@pID", _currentProductId);
                        cmd.Parameters.AddWithValue("@val", ratingValue);
                        cmd.Parameters.AddWithValue("@comment", string.IsNullOrEmpty(comment) ? (object)DBNull.Value : comment);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        isSuccess = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }

            if (isSuccess)
            {
                MessageBox.Show("Thank you! Your rating has been submitted.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again.");
            }
        }
    }
}
