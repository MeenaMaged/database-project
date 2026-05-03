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
    public partial class Cart : Form
    {
        private readonly Cartcontroller _controller = new Cartcontroller();
        private int _userid;
        public Cart()
        {
            InitializeComponent();
        }
        private void CartForm_Load(object sender, EventArgs e)
        {
            if (!Session.UserId.HasValue)
            {
                MessageBox.Show("No logged-in user found. Please login again.");
                Close();
                return;
            }

            _userid = Session.UserId.Value;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            LoadCart();
        }

        private void LoadCart()
        {
            try
            {
                // Refresh Grid
                dataGridView1.DataSource = _controller.GetCartByUserId(_userid);
                if (dataGridView1.Columns.Contains("ProductId"))
                {
                    dataGridView1.Columns["ProductId"].Visible = false;
                }

                // Refresh Total Label
                decimal total = _controller.GetCartTotal(_userid);
                lblTotal.Text = $"Total: {total:C2}"; // :C2 formats it as currency (e.g., $10.00)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnconfirmorder_Click(object sender, EventArgs e)
        {
            if (_controller.ConfirmOrderByUserId(_userid))
            {
                MessageBox.Show("Order confirmed.");
                LoadCart(); // Refresh the grid to see changes if necessary
            }
            else
            {
                MessageBox.Show("No pending cart items to confirm.");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (TryGetSelectedProductId(out int productId))
            {
                if (_controller.RemoveFromCart(_userid, productId))
                {
                    MessageBox.Show("Item removed.");
                    LoadCart(); // Refresh everything
                }
                else
                {
                    MessageBox.Show("Could not remove item from pending cart.");
                }
            }
            else
            {
                MessageBox.Show("Please select a full row to remove.");
            }
        }

        private bool TryGetSelectedProductId(out int productId)
        {
            productId = 0;
            if (!dataGridView1.Columns.Contains("ProductId"))
            {
                return false;
            }

            DataGridViewRow? row = null;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                row = dataGridView1.SelectedRows[0];
            }
            else if (dataGridView1.CurrentRow != null)
            {
                row = dataGridView1.CurrentRow;
            }

            if (row == null)
            {
                return false;
            }

            object? value = row.Cells["ProductId"].Value;
            return value != null && value != DBNull.Value && int.TryParse(value.ToString(), out productId);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
