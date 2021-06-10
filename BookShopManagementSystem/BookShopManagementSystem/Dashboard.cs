//deshBoard Form
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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        //Connection SQL Server(DataBase)
        //Change it according to your PC path to database (given in project file)
        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalvi\Desktop\BookShopManagementSystem\BookShopManagementSystem\DataBase\BookShopDB.mdf;Integrated Security=True;Connect Timeout=30");

        //open books Section on click
        private void books_Click(object sender, EventArgs e)
        {
            Books books = new Books();
            books.Show();
            this.Hide();
        }
        //open User Section on click
        private void user_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
            this.Hide();

        }


        //logout on click
        private void logout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            //get  books stock from database and dispaly
            Connection.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter("select sum(BookQuantity) from BookTable",Connection);
            DataTable dataTable1 = new DataTable();
            dataAdapter1.Fill(dataTable1);
            booksStock.Text = dataTable1.Rows[0][0].ToString();

            //get  total users from database and display
            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("select Count(*) from UserTable", Connection);
            DataTable dataTable2 = new DataTable();
            dataAdapter2.Fill(dataTable2);
            TotalUsers.Text = dataTable2.Rows[0][0].ToString();

            //get  total amount from database and display
            SqlDataAdapter dataAdapter3 = new SqlDataAdapter("select sum(Amount) from BillTable", Connection);
            DataTable dataTable3 = new DataTable();
            dataAdapter3.Fill(dataTable3);
            TotalAmount.Text = dataTable3.Rows[0][0].ToString();
            Connection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //close application
        private void label1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
