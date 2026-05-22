using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Final_Projet__Genterone__SEM_2526
{
    public partial class Employee_Management : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rosal\OneDrive\Documents\GroceryMaxxDB.accdb;";

        public Employee_Management()
        {
            InitializeComponent();
            this.Activated += Employee_Management_Activated;

            textBox5.Text = "May 15, 2024";
            textBox5.ForeColor = System.Drawing.Color.Gray;

            textBox6.Text = "Enter username";
            textBox6.ForeColor = System.Drawing.Color.Gray;

            textBox7.Text = "Enter Employee ID";
            textBox7.ForeColor = System.Drawing.Color.Gray;
        }

        private void Employee_Management_Load(object sender, EventArgs e)
        {
            LoadAllEmployees();
        }

        private void Employee_Management_Activated(object sender, EventArgs e)
        {
            LoadAllEmployees();
        }

        private void LoadAllEmployees()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT EmployeeID, FullName, MiddleInitial, Username, [Role], DateHired, DateAdded FROM Employee ORDER BY EmployeeID";

                    using (OleDbDataAdapter da = new OleDbDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Count > 0)
                        {
                            dataGridView1.Columns["EmployeeID"].HeaderText = "Employee ID";
                            dataGridView1.Columns["FullName"].HeaderText = "Full Name";
                            dataGridView1.Columns["MiddleInitial"].HeaderText = "M.I.";
                            dataGridView1.Columns["Username"].HeaderText = "Username";
                            dataGridView1.Columns["Role"].HeaderText = "Role/Position";
                            dataGridView1.Columns["DateHired"].HeaderText = "Date Hired";
                            dataGridView1.Columns["DateAdded"].HeaderText = "Date Added";

                            dataGridView1.Columns["DateHired"].DefaultCellStyle.Format = "MMMM dd, yyyy";
                            dataGridView1.Columns["DateAdded"].DefaultCellStyle.Format = "MMMM dd, yyyy";
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ADD EMPLOYEE Button - Manual input for all fields
        private void button1_Click(object sender, EventArgs e)
        {
            string employeeId = textBox7.Text.Trim();      // Manual Employee ID input
            string fullName = textBox1.Text.Trim();
            string middleInitial = textBox3.Text.Trim();
            string role = textBox2.Text.Trim();
            string dateString = textBox5.Text.Trim();
            string username = textBox6.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(employeeId) || employeeId == "Enter Employee ID")
            {
                MessageBox.Show("Please enter Employee ID!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox7.Focus();
                return;
            }

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please enter Full Name!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please enter Role/Position!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(username) || username == "Enter username")
            {
                MessageBox.Show("Please enter Username!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox6.Focus();
                return;
            }

            if (string.IsNullOrEmpty(dateString) || dateString == "May 15, 2024")
            {
                MessageBox.Show("Please enter Date Hired in format: May 15, 2024", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Focus();
                return;
            }

            DateTime dateHired;
            string[] formats = { "MMMM dd, yyyy", "MMM dd, yyyy", "MMMM d, yyyy", "MM/dd/yyyy", "yyyy-MM-dd" };

            if (!DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateHired))
            {
                MessageBox.Show("Please enter a valid Date Hired in format: May 15, 2024",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Clear();
                textBox5.Focus();
                return;
            }

            // Check if Employee ID already exists
            if (EmployeeExists(employeeId))
            {
                MessageBox.Show($"Employee ID '{employeeId}' already exists! Please use a different ID.",
                    "Duplicate Employee ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox7.Clear();
                textBox7.Focus();
                return;
            }

            // Check if username already exists
            if (UsernameExists(username))
            {
                MessageBox.Show($"Username '{username}' already exists! Please choose a different username.",
                    "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox6.Clear();
                textBox6.Focus();
                return;
            }

            string password = username;

            AddNewEmployee(employeeId, fullName, middleInitial, username, password, role, dateHired);
        }

        private bool EmployeeExists(string employeeId)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Employee WHERE EmployeeID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", employeeId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private bool UsernameExists(string username)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Employee WHERE Username = @user";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private void AddNewEmployee(string employeeId, string fullName, string middleInitial,
                                   string username, string password, string role, DateTime dateHired)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO Employee (EmployeeID, FullName, MiddleInitial, 
                                   Username, [Password], [Role], DateHired, DateAdded) 
                                   VALUES (@id, @name, @mi, @user, @pass, @role, @hired, @added)";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", employeeId);
                        cmd.Parameters.AddWithValue("@name", fullName);
                        cmd.Parameters.AddWithValue("@mi", string.IsNullOrEmpty(middleInitial) ? (object)DBNull.Value : middleInitial);
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.Add("@hired", OleDbType.Date).Value = dateHired.Date;
                        cmd.Parameters.Add("@added", OleDbType.Date).Value = dateHired.Date;
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Employee '{fullName}' added successfully!\n\n" +
                               $"Employee ID: {employeeId}\n" +
                               $"Username: {username}\n" +
                               $"Password: {username}\n" +
                               $"Role: {role}\n" +
                               $"Date Hired: {dateHired:MMMM dd, yyyy}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputFields();
                LoadAllEmployees();

                textBox5.Text = "May 15, 2024";
                textBox5.ForeColor = System.Drawing.Color.Gray;
                textBox6.Text = "Enter username";
                textBox6.ForeColor = System.Drawing.Color.Gray;
                textBox7.Text = "Enter Employee ID";
                textBox7.ForeColor = System.Drawing.Color.Gray;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding employee: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // REMOVE EMPLOYEE Button
        private void button2_Click(object sender, EventArgs e)
        {
            string employeeId = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Please enter Employee ID to remove!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }

            if (!EmployeeExists(employeeId))
            {
                MessageBox.Show($"Employee ID '{employeeId}' not found!", "Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Clear();
                textBox4.Focus();
                return;
            }

            string employeeName = GetEmployeeName(employeeId);

            DialogResult result = MessageBox.Show($"Are you sure you want to delete Employee '{employeeName}' (ID: {employeeId})?\nThis action cannot be undone!",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteEmployee(employeeId);
            }
        }

        private void DeleteEmployee(string employeeId)
        {
            try
            {
                string employeeName = GetEmployeeName(employeeId);

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Employee WHERE EmployeeID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", employeeId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Employee '{employeeName}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox4.Clear();
                LoadAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting employee: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetEmployeeName(string employeeId)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT FullName FROM Employee WHERE EmployeeID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", employeeId);
                        object result = cmd.ExecuteScalar();
                        return result != null ? result.ToString() : "Unknown";
                    }
                }
            }
            catch
            {
                return "Unknown";
            }
        }

        private void ClearInputFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox4.Text = row.Cells["EmployeeID"].Value?.ToString() ?? "";
                textBox1.Text = row.Cells["FullName"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["MiddleInitial"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["Role"].Value?.ToString() ?? "";
                textBox6.Text = row.Cells["Username"].Value?.ToString() ?? "";
                textBox7.Text = row.Cells["EmployeeID"].Value?.ToString() ?? "";

                if (row.Cells["DateHired"].Value != null && row.Cells["DateHired"].Value != DBNull.Value)
                {
                    DateTime dateValue = Convert.ToDateTime(row.Cells["DateHired"].Value);
                    textBox5.Text = dateValue.ToString("MMMM dd, yyyy");
                }
                else
                {
                    textBox5.Text = "May 15, 2024";
                }
            }
        }

        // textBox5 (Date Hired) focus events
        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "May 15, 2024")
            {
                textBox5.Text = "";
                textBox5.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.Text = "May 15, 2024";
                textBox5.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // textBox6 (Username) focus events
        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Enter username")
            {
                textBox6.Text = "";
                textBox6.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox6.Text = "Enter username";
                textBox6.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // textBox7 (Employee ID) focus events
        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Enter Employee ID")
            {
                textBox7.Text = "";
                textBox7.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                textBox7.Text = "Enter Employee ID";
                textBox7.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // textBox6 TextChanged - Real-time username validation
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string username = textBox6.Text.Trim();
            if (!string.IsNullOrEmpty(username) && username != "Enter username")
            {
                if (UsernameExists(username))
                {
                    textBox6.BackColor = System.Drawing.Color.LightPink;
                }
                else
                {
                    textBox6.BackColor = System.Drawing.Color.White;
                }
            }
            else
            {
                textBox6.BackColor = System.Drawing.Color.White;
            }
        }

        // textBox7 TextChanged - Real-time Employee ID validation
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string employeeId = textBox7.Text.Trim();
            if (!string.IsNullOrEmpty(employeeId) && employeeId != "Enter Employee ID")
            {
                if (EmployeeExists(employeeId))
                {
                    textBox7.BackColor = System.Drawing.Color.LightPink;
                }
                else
                {
                    textBox7.BackColor = System.Drawing.Color.White;
                }
            }
            else
            {
                textBox7.BackColor = System.Drawing.Color.White;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main_Menu__Admin_ adminMenu = new Main_Menu__Admin_();
            adminMenu.Show();
            this.Close();
        }

        // Existing empty event handlers
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged_1(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void Employee_Management_Load_1(object sender, EventArgs e) { }
    }
}
