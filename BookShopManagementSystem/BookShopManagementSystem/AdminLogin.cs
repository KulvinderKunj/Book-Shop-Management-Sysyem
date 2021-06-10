//Admin Login
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagementSystem
{
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
        }
        //Login Click Button Function
        private void AdminLogin_Click(object sender, EventArgs e)
        {
            if(adminPassword.Text == "admin")
            {
                Books books = new Books();
                books.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Password");
            }
          
        }
    }
}
