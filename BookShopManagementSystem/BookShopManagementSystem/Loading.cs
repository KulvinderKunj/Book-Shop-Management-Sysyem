//loading Form
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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }
        //prograssbar value
        int startProgress = 0;

        //timer to increase progress bar value
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            startProgress++;
            ProgressBar.Value = startProgress;
            percentage.Text = startProgress + "%";
            if (ProgressBar.Value == 100)
            {
                ProgressBar.Value = 0;
                timer1.Stop();
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }
        //start progressbar on form load
        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
