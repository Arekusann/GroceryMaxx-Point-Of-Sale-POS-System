using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Final_Projet__Genterone__SEM_2526
{
    public partial class Main_Menu__Admin_ : Form
    {
        public Main_Menu__Admin_()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Main_Menu__Admin__Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee_Management dps = new Employee_Management();
            dps.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inventory_Management dps = new Inventory_Management();
            dps.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pricing_Management dps = new Pricing_Management();
            dps.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Log Out Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Open login form
                Form1 loginForm = new Form1();
                loginForm.Show();

                // Close current form
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales_Reports salesReports = new Sales_Reports();
            salesReports.Show();
            this.Hide(); // Hides the current form
        }
    }
}
