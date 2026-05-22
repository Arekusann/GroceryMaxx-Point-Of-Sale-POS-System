using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Final_Projet__Genterone__SEM_2526
{
    public partial class Pricing_Management : Form
    {
        // Database connection string 
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rosal\OneDrive\Documents\GroceryMaxxDB.accdb;";

        public Pricing_Management()
        {
            InitializeComponent();
            
            this.Activated += Pricing_Management_Activated;

            // Setup DataGridView for editing
            SetupDataGridViewForEditing();
        }

        private void SetupDataGridViewForEditing()
        {
            // Allow editing only the Price column
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            // Handle cell edit event
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
        }

        private void Pricing_Management_Load(object sender, EventArgs e)
        {
            LoadAllProducts();
        }

        // Refresh data when form gets focus/tab is selected
        private void Pricing_Management_Activated(object sender, EventArgs e)
        {
            LoadAllProducts();  // Refresh data every time this form becomes active
        }

        private void LoadAllProducts()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    // Include all columns including Price
                    string query = "SELECT ProductID, ProductName, Category, Quantity, Price FROM Products ORDER BY ProductID";

                    using (OleDbDataAdapter da = new OleDbDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Count > 0)
                        {
                            // Format headers
                            dataGridView1.Columns["ProductID"].HeaderText = "Product ID";
                            dataGridView1.Columns["ProductName"].HeaderText = "Product Name";
                            dataGridView1.Columns["Category"].HeaderText = "Category";
                            dataGridView1.Columns["Quantity"].HeaderText = "Quantity";
                            dataGridView1.Columns["Price"].HeaderText = "Price (₱)";

                           
                            dataGridView1.Columns["ProductID"].ReadOnly = true;
                            dataGridView1.Columns["ProductName"].ReadOnly = true;
                            dataGridView1.Columns["Category"].ReadOnly = true;
                            dataGridView1.Columns["Quantity"].ReadOnly = true;
                            

                            
                            dataGridView1.Columns["Price"].DefaultCellStyle.Format = "C2";
                            dataGridView1.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                            // Auto-size columns
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Save price changes when cell is edited in DataGridView
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Check if the edited column is the Price column
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Price")
                {
                    try
                    {
                        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                        string productId = row.Cells["ProductID"].Value?.ToString();

                        if (string.IsNullOrEmpty(productId))
                            return;

                        decimal priceValue = 0;
                        object priceObj = row.Cells["Price"].Value;

                        if (priceObj != null && priceObj != DBNull.Value)
                        {
                            decimal.TryParse(priceObj.ToString(), out priceValue);
                        }

                        // Update the database
                        using (OleDbConnection conn = new OleDbConnection(connectionString))
                        {
                            conn.Open();
                            string query = "UPDATE Products SET Price = @price WHERE ProductID = @id";

                            using (OleDbCommand cmd = new OleDbCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@price", priceValue);
                                cmd.Parameters.AddWithValue("@id", productId);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Success - show temporary status
                                    Console.WriteLine($"Price updated for {productId} to {priceValue:C}");
                                }
                            }
                        }

                        // Refresh the textboxes if they correspond to the edited row
                        if (textBox1.Text == productId)
                        {
                            textBox2.Text = priceValue.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving price: {ex.Message}", "Update Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoadAllProducts(); // Reload to revert changes
                    }
                }
            }
        }

        // ADD PRICE Button 
        private void button1_Click(object sender, EventArgs e)
        {
            string productId = textBox1.Text.Trim();   // Product ID
            string priceText = textBox2.Text.Trim();   // Price

            // Validate inputs
            if (string.IsNullOrEmpty(productId))
            {
                MessageBox.Show("Please enter Product ID!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("Please enter a Price!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            decimal priceValue;
            if (!decimal.TryParse(priceText, out priceValue))
            {
                MessageBox.Show("Please enter a valid Price (numbers only)!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Clear();
                textBox2.Focus();
                return;
            }

            // Check if product exists
            if (!ProductExists(productId))
            {
                MessageBox.Show($"Product ID '{productId}' not found!\nPlease add the product in Inventory Management first.",
                    "Product Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
                return;
            }

            // Add/Update the price
            AddOrUpdatePrice(productId, priceValue);
        }

        private bool ProductExists(string productId)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Products WHERE ProductID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productId);
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

        private void AddOrUpdatePrice(string productId, decimal price)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Products SET Price = @price WHERE ProductID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@id", productId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Price for Product ID '{productId}' set to ₱{price:F2} successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();

                // Refresh the DataGridView to show updated price
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting price: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // REMOVE PRICE Button
        private void button2_Click(object sender, EventArgs e)
        {
            string productId = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(productId))
            {
                MessageBox.Show("Please enter Product ID to remove price!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }

            if (!ProductExists(productId))
            {
                MessageBox.Show($"Product ID '{productId}' not found!", "Product Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Clear();
                textBox4.Focus();
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to remove the price for Product ID '{productId}'?\nThis will set it to empty.",
                "Confirm Remove Price", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                RemovePrice(productId);
            }
        }

        private void RemovePrice(string productId)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    // Set Price to NULL (empty)
                    string query = "UPDATE Products SET Price = NULL WHERE ProductID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Price for Product ID '{productId}' removed successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear remove field
                textBox4.Clear();
                textBox4.Focus();

                // Refresh the DataGridView
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing price: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Click DataGridView row to load Product ID into textBox1 and Price into textBox2
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["ProductID"].Value?.ToString() ?? "";

                // Display price in textBox2 if it exists
                if (row.Cells["Price"].Value != null && row.Cells["Price"].Value != DBNull.Value)
                {
                    textBox2.Text = row.Cells["Price"].Value.ToString();
                }
                else
                {
                    textBox2.Text = "";
                }
            }
        }

        // RETURN TO MAIN MENU Button (button3)
        private void button3_Click(object sender, EventArgs e)
        {
            Main_Menu__Admin_ adminMenu = new Main_Menu__Admin_();
            adminMenu.Show();
            this.Close();
        }

        // Existing empty event handlers
        private void label5_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}