//User Login form
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        //Connection SQL Server(DataBase)
        //Change it according to your PC path to database (given in project file)
        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalvi\Desktop\BookShopManagementSystem\BookShopManagementSystem\DataBase\BookShopDB.mdf;Integrated Security=True;Connect Timeout=30");
        public static string UName="";
        private void button1_Click(object sender, EventArgs e)
        {
            //check userNam and password and if match than login 
            Connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select count(*) from UserTable where UserName='" + UserName.Text + "' and UserPassword='" + UserPassword.Text + "'",Connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            if (dataTable.Rows[0][0].ToString() == "1")
            {
                UName = UserName.Text;
                Bill bill = new Bill();
                bill.Show();
                this.Hide();
                Connection.Close();
            }
            else
            {
                MessageBox.Show("Wrong UserName or Password");
            }
            Connection.Close();

        }
        //show admin login form on click
        private void adminLoginForm_Click(object sender, EventArgs e)
        {
            AdminLoginForm adminLoginForm = new AdminLoginForm();
            adminLoginForm.Show();
            this.Hide();
        }
        //close program on click
        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
