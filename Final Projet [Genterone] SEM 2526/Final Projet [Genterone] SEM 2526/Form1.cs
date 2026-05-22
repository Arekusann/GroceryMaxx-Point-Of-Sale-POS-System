using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Final_Projet__Genterone__SEM_2526
{
    public partial class Form1 : Form
    {
        // IMPORTANT: Change this path to your MS Access database location
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rosal\OneDrive\Documents\GroceryMaxxDB.accdb;";

        public Form1()
        {
            InitializeComponent();

            // Set password character masking
            textBox2.PasswordChar = '*';

            // Clear fields when form loads
            textBox1.Clear();
            textBox2.Clear();

            // Change button text to just "Login"
            button1.Text = "Login";

            // Make form title more descriptive
            this.Text = "GroceryMaxx POS - Login";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional: Add real-time validation
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SINGLE LOGIN BUTTON - Automatically detects if Admin, Manager, or Employee
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            // Check if fields are empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username and Password!", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // First check if user is an Admin
            if (AuthenticateAdmin(username, password))
            {
                MessageBox.Show($"Welcome Admin {username}!", "Login Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Admin Main Menu
                Main_Menu__Admin_ adminMenu = new Main_Menu__Admin_();
                adminMenu.Show();
                this.Hide();
                return;
            }

            

            // If not Admin, check if user is an Employee
            EmployeeInfo employeeInfo = AuthenticateEmployee(username, password);

            if (employeeInfo != null)
            {
                MessageBox.Show($"Welcome {employeeInfo.FullName}!", "Login Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the Employee POS Dashboard with employee info
                Main_Menu__User userDashboard = new Main_Menu__User(employeeInfo.FullName, employeeInfo.EmployeeID);
                userDashboard.Show();
                this.Hide();
                return;
            }

            // If neither Admin,nor Employee found
            MessageBox.Show("Invalid Username or Password!\nPlease check your credentials and try again.",
                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            textBox2.Clear();
            textBox2.Focus();
        }

        // Method to authenticate Admin from database
        private bool AuthenticateAdmin(string username, string password)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Admin WHERE Username = @user AND Password = @pass";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Method to authenticate Manager from database
        private string AuthenticateManager(string username, string password)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT FullName FROM Manager WHERE Username = @user AND Password = @pass";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return result.ToString();
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Method to authenticate Employee and return FullName and EmployeeID
        private EmployeeInfo AuthenticateEmployee(string username, string password)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT FullName, EmployeeID FROM Employee WHERE Username = @user AND [Password] = @pass";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                EmployeeInfo info = new EmployeeInfo();
                                info.FullName = reader["FullName"].ToString();
                                info.EmployeeID = reader["EmployeeID"].ToString();
                                return info;
                            }
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Allow Enter key to trigger login
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }

    // Helper class to hold Employee information
    public class EmployeeInfo
    {
        public string FullName { get; set; }
        public string EmployeeID { get; set; }
    }
}