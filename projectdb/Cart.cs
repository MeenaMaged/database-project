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
        private Cartcontroller _controller = new Cartcontroller();
        private int _userid = -1;
        public Cart()
        {
            InitializeComponent();
            if (Session.UserId.HasValue)
            {
                _userid = Session.UserId.Value;
            }
        }
        private void CartForm_Load(object sender, EventArgs e)
        {
            if (_userid == -1)
            {
                MessageBox.Show("Please login to view your cart.");
                this.Close();
                return;
            }
            LoadCart();
        }

        private void LoadCart()
        {
            try
            {
                // Refresh Grid
                dataGridView1.DataSource = _controller.GetCartByUserId(_userid);

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
                MessageBox.Show("Order pending");
                LoadCart(); // Refresh the grid to see changes if necessary
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the ProductId from the hidden column or cell
                // Note: Make sure ProductId is included in your SELECT query in the controller!
                int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ProductId"].Value);

                if (_controller.RemoveFromCart(_userid, productId))
                {
                    MessageBox.Show("Item removed.");
                    LoadCart(); // Refresh everything
                }
            }
            else
            {
                MessageBox.Show("Please select a full row to remove.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
