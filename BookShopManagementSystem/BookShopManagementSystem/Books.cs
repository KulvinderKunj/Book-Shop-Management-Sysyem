//books Form 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookShopManagementSystem
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            showBooks();//show book on load
        }
        //Connection SQL Server(DataBase)
        //Change it according to your PC path to database (given in project file)
        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalvi\Desktop\BookShopManagementSystem\BookShopManagementSystem\DataBase\BookShopDB.mdf;Integrated Security=True;Connect Timeout=30");

        //show book from database
        private void showBooks()
        {
            Connection.Open();
            string query = "select * from BookTable";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            BooksListgrid.DataSource = dataSet.Tables[0];
            Connection.Close();
        }

        //filter Books by Category (Show specific category books from database)

        private void FilterCategary()
                 {
                     Connection.Open();
                     string query1 = " select * from BookTable where BookCategory='" + BookCategorySearch.SelectedItem.ToString() + "'";
                     SqlDataAdapter dataAdapter = new SqlDataAdapter(query1, Connection);
                     SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
                     var dataSet1 = new DataSet();
                     dataAdapter.Fill(dataSet1);
                     BooksListgrid.DataSource = dataSet1.Tables[0];
                     Connection.Close();
                 }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        //save book in database

        private void BookSave_Click_1(object sender, EventArgs e)
        {
          if (BookTitle.Text == "" || BookAuthar.Text == "" || BookQuantity.Text == "" || BookCategory.SelectedIndex == -1 || BookPrice.Text == "")
            {
                MessageBox.Show("Incomplete Information..!");
            }
            else
            {
                try
                {
                    Connection.Open();
                    String query = "insert into BookTable values('" + BookTitle.Text + " ',' " + BookAuthar.Text + "','" + BookCategory.SelectedItem.ToString() + "','" + BookQuantity.Text + "','" + BookPrice.Text + "')";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Book Saved");
                    Connection.Close();
                    showBooks();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void BookCategorySearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterCategary();//call book's filter method 
        }
        //show all books
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            showBooks();
            BookCategorySearch.SelectedIndex = -1;
        }
        //clear Fields 
        private void Reset()
   {
       BookTitle.Text = "";
       BookAuthar.Text = "";
       BookQuantity.Text = "";
       BookPrice.Text = "";
       BookCategory.Text = "";
   }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }
  
        private void Books_Load(object sender, EventArgs e)
        {
         
        }
        //Update Already Saved Books information
        private void button4_Click(object sender, EventArgs e)
        {
            if (BookTitle.Text == "" || BookAuthar.Text == "" || BookQuantity.Text == "" || BookCategory.SelectedIndex == -1 || BookPrice.Text == "")
            {
                MessageBox.Show("Incomplete Information..!");
            }
            else
            {
                try
                {
                    Connection.Open();
                    String query = "update BookTable set BookTitle ='" +BookTitle.Text+"', BookAuthar='"+ BookAuthar.Text+"',BookCategory='"+BookCategory.SelectedItem.ToString()+"', BookQuantity='"+BookQuantity.Text+"',BookPrice='"+BookPrice.Text+"' where BookId='"+key+"'";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Book Updated");
                    Connection.Close();
                    showBooks();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void BooksListgrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {}

        int key = 0;//Primary key (book Id)
        private void BooksListgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //filling fields by clicking book row
            BookTitle.Text = BooksListgrid.SelectedRows[0].Cells[1].Value.ToString();
             BookAuthar.Text = BooksListgrid.SelectedRows[0].Cells[2].Value.ToString();
            BookCategory.Text = BooksListgrid.SelectedRows[0].Cells[3].Value.ToString();
             BookQuantity.Text = BooksListgrid.SelectedRows[0].Cells[4].Value.ToString();
             BookPrice.Text = BooksListgrid.SelectedRows[0].Cells[5].Value.ToString();
            if (BookTitle.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BooksListgrid.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        //delete book from database 
        private void BookDelete_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Incomplete Information..!");
            }
            else
            {
                try
                {
                    Connection.Open();
                    String query = "delete from bookTable where BookId ="+key+";";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted");
                    Connection.Close();
                    showBooks();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        //open User Section on click
        private void userbtn_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
            this.Hide();
        }
        //open deshboard Section on click
        private void deshBoardBtn_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }
        //logout on click
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
        //close Program
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}