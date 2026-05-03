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

namespace Project
{
    public partial class UsersForm : Form
    {
        private projectdb.DatabaseService _dbService = new projectdb.DatabaseService();

        public UsersForm()
        {
            InitializeComponent();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {

            // Fetching all data from the Users table
            string query = "SELECT UserId, Email, Role, FirstName, LastName FROM Users";

            using (SqlConnection con = _dbService.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Users: " + ex.Message);
                }
            }
        }
    }
}
