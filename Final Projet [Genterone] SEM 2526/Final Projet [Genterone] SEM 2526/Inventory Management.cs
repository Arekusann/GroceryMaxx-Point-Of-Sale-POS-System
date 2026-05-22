using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Globalization;

namespace Final_Projet__Genterone__SEM_2526
{
    public partial class Inventory_Management : Form
    {
        // Database connection string 
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rosal\OneDrive\Documents\GroceryMaxxDB.accdb;";

        private DataTable currentDataTable; // Store the current data table for updates

        public Inventory_Management()
        {
            InitializeComponent();
           
            this.Activated += Inventory_Management_Activated;

            // Enable editing in DataGridView
            SetupDataGridViewForEditing();

        }

        private void SetupDataGridViewForEditing()
        {
            // Allow editing
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;

            // Handle cell validation events
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.UserDeletingRow += dataGridView1_UserDeletingRow;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
        }

        private void Inventory_Management_Load(object sender, EventArgs e)
        {
            LoadAllProducts();
        }

        // Refresh data when form gets focus/tab is selected
        private void Inventory_Management_Activated(object sender, EventArgs e)
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
                    
                    string query = "SELECT ProductID, ProductName, Category, Quantity, [Date Added] FROM Products ORDER BY ProductID";

                    using (OleDbDataAdapter da = new OleDbDataAdapter(query, conn))
                    {
                        currentDataTable = new DataTable();
                        da.Fill(currentDataTable);
                        dataGridView1.DataSource = currentDataTable;

                        if (dataGridView1.Columns.Count > 0)
                        {
                            dataGridView1.Columns["ProductID"].HeaderText = "Product ID";
                            dataGridView1.Columns["ProductName"].HeaderText = "Product Name";
                            dataGridView1.Columns["Category"].HeaderText = "Category";
                            dataGridView1.Columns["Quantity"].HeaderText = "Quantity";
                            dataGridView1.Columns["Date Added"].HeaderText = "Date Added";

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            // Format date column
                            dataGridView1.Columns["Date Added"].DefaultCellStyle.Format = "MMMM dd, yyyy";

                            // Make ProductID read-only (Primary Key shouldn't be edited)
                            dataGridView1.Columns["ProductID"].ReadOnly = true;
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}\n\nMake sure the database file exists at:\n{connectionString}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Save changes when a cell is edited
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    string productId = row.Cells["ProductID"].Value?.ToString();

                    if (string.IsNullOrEmpty(productId))
                        return;

                    string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

                    // If column name has a space, wrap it in brackets
                    string safeColumnName = columnName.Contains(" ") ? $"[{columnName}]" : columnName;
                    string newValue = row.Cells[e.ColumnIndex].Value?.ToString() ?? "";

                    // Update the database
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = $"UPDATE Products SET {safeColumnName} = @value WHERE ProductID = @id";

                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@value", newValue);
                            cmd.Parameters.AddWithValue("@id", productId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Refresh the textboxes if they correspond to the edited row
                    if (textBox2.Text == productId)
                    {
                        textBox1.Text = row.Cells["ProductName"].Value?.ToString() ?? "";
                        textBox3.Text = row.Cells["Category"].Value?.ToString() ?? "";
                        textBox5.Text = row.Cells["Quantity"].Value?.ToString() ?? "";
                        if (row.Cells["Date Added"].Value != null && row.Cells["Date Added"].Value != DBNull.Value)
                        {
                            DateTime dateValue = Convert.ToDateTime(row.Cells["Date Added"].Value);
                            textBox6.Text = dateValue.ToString("MMMM dd, yyyy");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving changes: {ex.Message}", "Update Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadAllProducts(); // Reload to revert changes
                }
            }
        }

        // Handle row deletion
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row != null)
            {
                string productId = e.Row.Cells["ProductID"].Value?.ToString();

                if (!string.IsNullOrEmpty(productId))
                {
                    DialogResult result = MessageBox.Show($"Are you sure you want to delete Product ID '{productId}'?\nThis action cannot be undone!",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (OleDbConnection conn = new OleDbConnection(connectionString))
                            {
                                conn.Open();
                                string query = "DELETE FROM Products WHERE ProductID = @id";
                                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", productId);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting product: {ex.Message}", "Delete Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Cancel the deletion
                        e.Cancel = true;
                    }
                }
            }
        }

        // Handle new row added
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // This event fires when a new row is added
            // The actual save will happen in CellEndEdit
        }

        // ADD PRODUCT Button
        private void button1_Click(object sender, EventArgs e)
        {
            string productId = textBox2.Text.Trim();
            string productName = textBox1.Text.Trim();
            string category = textBox3.Text.Trim();
            string quantity = textBox5.Text.Trim();
            string dateString = textBox6.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(productId))
            {
                MessageBox.Show("Please enter Product ID!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(productName))
            {
                MessageBox.Show("Please enter Product Name!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Please enter Product Category!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }

            int quantityValue = 0;
            if (!string.IsNullOrEmpty(quantity) && !int.TryParse(quantity, out quantityValue))
            {
                MessageBox.Show("Please enter a valid Quantity!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Clear();
                textBox5.Focus();
                return;
            }

            // Parse the date from textBox6
            DateTime dateAdded = DateTime.Now; // Default value
            if (!string.IsNullOrEmpty(dateString) && dateString != "May 15, 2024")
            {
                string[] formats = { "MMMM dd, yyyy", "MMM dd, yyyy", "MMMM d, yyyy", "MM/dd/yyyy", "yyyy-MM-dd" };
                if (!DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAdded))
                {
                    MessageBox.Show("Please enter a valid Date in format: May 15, 2024",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox6.Clear();
                    textBox6.Focus();
                    return;
                }
            }

            if (ProductExists(productId))
            {
                DialogResult result = MessageBox.Show($"Product ID '{productId}' already exists!\nDo you want to update the quantity and date instead?",
                    "Duplicate Product ID", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UpdateProductQuantityAndDate(productId, quantityValue, dateAdded);
                }
                return;
            }

            AddNewProduct(productId, productName, category, quantityValue, dateAdded);
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

        private void AddNewProduct(string productId, string productName, string category, int quantity, DateTime dateAdded)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    
                    string query = @"INSERT INTO Products (ProductID, ProductName, Category, Quantity, [Date Added]) 
                                   VALUES (@id, @name, @cat, @qty, @date)";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productId);
                        cmd.Parameters.AddWithValue("@name", productName);
                        cmd.Parameters.AddWithValue("@cat", category);
                        cmd.Parameters.AddWithValue("@qty", quantity);
                        cmd.Parameters.AddWithValue("@date", dateAdded);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Product '{productName}' added successfully!\nDate Added: {dateAdded:MMMM dd, yyyy}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateProductQuantityAndDate(string productId, int additionalQuantity, DateTime newDate)
        {
            try
            {
                int currentQty = GetCurrentQuantity(productId);
                int newQty = currentQty + additionalQuantity;

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    
                    string query = "UPDATE Products SET Quantity = @qty, [Date Added] = @date WHERE ProductID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@qty", newQty);
                        cmd.Parameters.AddWithValue("@date", newDate);
                        cmd.Parameters.AddWithValue("@id", productId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Product updated!\nPrevious Quantity: {currentQty} | New Quantity: {newQty}\nDate Added: {newDate:MMMM dd, yyyy}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetCurrentQuantity(string productId)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Quantity FROM Products WHERE ProductID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productId);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        // REMOVE PRODUCT Button 
        private void button2_Click(object sender, EventArgs e)
        {
            string productId = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(productId))
            {
                MessageBox.Show("Please enter Product ID to remove!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            DialogResult result = MessageBox.Show($"Are you sure you want to delete Product ID '{productId}'?\nThis action cannot be undone!",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteProduct(productId);
            }
        }

        private void DeleteProduct(string productId)
        {
            try
            {
                string productName = GetProductName(productId);
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Products WHERE ProductID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Product '{productName}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox4.Clear();
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetProductName(string productId)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ProductName FROM Products WHERE ProductID = @id";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productId);
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
            textBox2.Clear();
            textBox1.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Text = "May 15, 2024";
            textBox2.Focus();
        }

        // RETURN TO MAIN MENU Button 
        private void button3_Click(object sender, EventArgs e)
        {
            Main_Menu__Admin_ adminMenu = new Main_Menu__Admin_();
            adminMenu.Show();
            this.Close();
        }

        // Click DataGridView row to load into textboxes
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox2.Text = row.Cells["ProductID"].Value?.ToString() ?? "";
                textBox1.Text = row.Cells["ProductName"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["Category"].Value?.ToString() ?? "";
                textBox5.Text = row.Cells["Quantity"].Value?.ToString() ?? "";

                if (row.Cells["Date Added"].Value != null && row.Cells["Date Added"].Value != DBNull.Value)
                {
                    DateTime dateValue = Convert.ToDateTime(row.Cells["Date Added"].Value);
                    textBox6.Text = dateValue.ToString("MMMM dd, yyyy");
                }
                else
                {
                    textBox6.Text = "May 15, 2024";
                }
            }
        }

        // TextBox6 focus events for date entry
        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "May 15, 2024")
            {
                textBox6.Text = "";
                textBox6.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox6.Text = "May 15, 2024";
                textBox6.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // Existing empty event handlers
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }

        private void Inventory_Management_Load_1(object sender, EventArgs e)
        {

        }
    }
}