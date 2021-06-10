//Bill Form (User)
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
using System.Configuration;

namespace BookShopManagementSystem
{
    public partial class Bill : Form
    {
        public Bill()
        {
            InitializeComponent();
            showBooks();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        //Connection SQL Server(DataBase)
        //Change it according to your PC path to database (given in project file)
        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalvi\Desktop\BookShopManagementSystem\BookShopManagementSystem\DataBase\BookShopDB.mdf;Integrated Security=True;Connect Timeout=30");

        //get books from database and show in grid view
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
        private void addToBill_Click(object sender, EventArgs e)
        {

        }
        int key = 0;//primary key(book id)
        int stock = 0;
        private void BooksListgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //fill fields by clicking book row
            BookTitle.Text = BooksListgrid.SelectedRows[0].Cells[1].Value.ToString();
            BookPrice.Text = BooksListgrid.SelectedRows[0].Cells[5].Value.ToString();
            if (BookTitle.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BooksListgrid.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BooksListgrid.SelectedRows[0].Cells[4].Value.ToString());
            }
        }
        //clear Fields
        private void Reset()
        {
            BookTitle.Text = "";
            BookQuantity.Text = "";
            BookPrice.Text = "";
            if (cNameFlag == 0) {
                customerName.Text = "";
            }
          
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }
        //updatae books details in database
        private void booksUpdate()
        {
            int newQuantity = stock - (Convert.ToInt32(BookQuantity.Text));
                try{
                    Connection.Open();
                    String query = "update BookTable set BookQuantity='" + newQuantity + "' where BookId='" + key + "'";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                    Connection.Close();
                    showBooks();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            
        }
        int TotalBill = 0;//total bill 
        int num = 0;
        int cNameFlag = 0;

        //Add books to list
        private void addToBill_Click_1(object sender, EventArgs e)
        {
            //Check Information  
            if (BookQuantity.Text == "" || BookTitle.Text == "" || BookPrice.Text == "" || customerName.Text=="")
            {
                MessageBox.Show("Missing Information");
            }//Check Book Stock
            else if (Convert.ToInt32(BookQuantity.Text)>stock)
            {
                MessageBox.Show("No Enough Stock");
            }
           
            else
            {
                //add books to List
                int total = Convert.ToInt32(BookQuantity.Text) * Convert.ToInt32(BookPrice.Text);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(BillListgrid);
                row.Cells[0].Value = num+1;
                row.Cells[1].Value = BookTitle.Text;
                row.Cells[2].Value = BookPrice.Text;
                row.Cells[3].Value = BookQuantity.Text;
                row.Cells[4].Value = total;
                BillListgrid.Rows.Add(row);
                //update bool
                booksUpdate();
                //Calculate Total  Bill
                TotalBill = TotalBill + total;
                TotalBillLabel.Text = TotalBill+"/-";
                num++;
                customerName.Enabled = false;
                cNameFlag = 1;
                Reset();

            }
       
          
        }

        private void BillListgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //show availabe book on load of form
        private void Bill_Load(object sender, EventArgs e)
        {
            UserName.Text = Login.UName;
        }
        int prodid, prodprice,prodquantity,prodtotal,pos=100;

        private void label14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //logout Function
        private void logout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

      

        string prodname;
        //Craete Bill Document 
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("The Book River", new Font("Reggae One", 16, FontStyle.Bold),Brushes.DeepSkyBlue,new Point(80,20));
            e.Graphics.DrawString("    Sr#   |  PRODUCT |   PRRICE   | QUANTITY |   TOTAL  ", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(10,70));
            foreach(DataGridViewRow row in BillListgrid.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodquantity = Convert.ToInt32(row.Cells["Column4"].Value);
                prodtotal = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Regular), Brushes.Black, new Point(30, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Regular), Brushes.Black, new Point(65, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Regular), Brushes.Black, new Point(135, pos));
                e.Graphics.DrawString("" + prodquantity, new Font("Century Gothic", 8, FontStyle.Regular), Brushes.Black, new Point(200, pos));
                e.Graphics.DrawString("" + prodtotal, new Font("Century Gothic", 8, FontStyle.Regular), Brushes.Black, new Point(255, pos));
                pos= pos + 25;
            }
            e.Graphics.DrawString("Total Amount : Rs ", new Font("Century Gothic", 14, FontStyle.Bold), Brushes.DeepSkyBlue, new Point(50, pos+50));
            e.Graphics.DrawString(""+TotalBill+"/-", new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Blue, new Point(215, pos + 50));
            e.Graphics.DrawString("--------Thank You For shoping Here--------", new Font("Century Gothic", 10, FontStyle.Italic), Brushes.Black, new Point(5, pos + 85));
            BillListgrid.Rows.Clear();
            BillListgrid.Refresh();
            pos = 100;
            TotalBill = 0;
        }


        private void printBill_Click(object sender, EventArgs e)
        {
            try
            {
                //Adding Bill data in DataBase
                Connection.Open();
                String query = "insert into BillTable values('" + UserName.Text + " ',' " + customerName.Text + "','" + TotalBill + "')";
                SqlCommand command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Bill Saved");
                Connection.Close();
                num = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            //show bill
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 300, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            
            TotalBillLabel.Text = "";
            customerName.Enabled = true;
            cNameFlag = 0;
            Reset();
        
            
        }
    }
}
;