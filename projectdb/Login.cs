using Project;

namespace projectdb
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            AuthenticationController authController = new AuthenticationController();
            var userInfo = authController.Login(email, password);

            if (userInfo.HasValue)
            {
              
                Session.UserId = userInfo.Value.UserId;
                Session.Role = userInfo.Value.Role;

               
                if (userInfo.Value.Role == "Admin")
                {
                    AdminDashboard adminDashboard = new AdminDashboard();
                    adminDashboard.Show();
                    this.Hide();
                }
                else if (userInfo.Value.Role == "User" || userInfo.Value.Role == "Customer")
                {
                    UserHomeForm home = new UserHomeForm();
                    home.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            SignUp signUpForm = new SignUp();
            signUpForm.Show();
            this.Hide();
        }
    }
}
