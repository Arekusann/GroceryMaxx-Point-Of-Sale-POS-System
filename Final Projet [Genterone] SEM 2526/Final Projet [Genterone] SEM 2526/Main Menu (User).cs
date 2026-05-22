using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Final_Projet__Genterone__SEM_2526
{
    public partial class Main_Menu__User : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rosal\OneDrive\Documents\GroceryMaxxDB.accdb;";
        private string currentEmployeeName = "";
        private string currentEmployeeID = "";

        // Transaction variables
        private decimal runningTotal = 0m;
        private List<CartItem> cart = new List<CartItem>();
        private string currentAmountInput = "";
        private bool isEnteringCash = false;
        private decimal cashAmountReceived = 0m; // Store cash amount entered

        public Main_Menu__User(string employeeName, string employeeID)
        {
            InitializeComponent();
            currentEmployeeName = employeeName;
            currentEmployeeID = employeeID;

            // textBox1 displays employee info
            textBox1.Text = $"Employee: {employeeName} (ID: {employeeID})";
            textBox1.ReadOnly = true;

            // textBox2 displays running total
            textBox2.Text = "0.00";
            textBox2.ReadOnly = true;
            textBox2.TextAlign = HorizontalAlignment.Right;
            textBox2.BackColor = System.Drawing.Color.LightYellow;
        }

        private void Main_Menu__User_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ProductID, ProductName, Category, Quantity, Price FROM Products ORDER BY ProductName";

                    using (OleDbDataAdapter da = new OleDbDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Count > 0)
                        {
                            dataGridView1.Columns["ProductID"].HeaderText = "ID";
                            dataGridView1.Columns["ProductName"].HeaderText = "Product";
                            dataGridView1.Columns["Category"].HeaderText = "Category";
                            dataGridView1.Columns["Quantity"].HeaderText = "Stock";
                            dataGridView1.Columns["Price"].HeaderText = "Price";

                            dataGridView1.Columns["Price"].DefaultCellStyle.Format = "C2";
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddToCart(string productID, string productName, decimal price)
        {
            foreach (var item in cart)
            {
                if (item.ProductID == productID)
                {
                    item.Quantity++;
                    UpdateTotalDisplay();
                    return;
                }
            }

            cart.Add(new CartItem
            {
                ProductID = productID,
                ProductName = productName,
                Price = price,
                Quantity = 1
            });

            UpdateTotalDisplay();
        }

        private void UpdateTotalDisplay()
        {
            runningTotal = 0;
            foreach (var item in cart)
            {
                runningTotal += item.Total;
            }
            textBox2.Text = runningTotal.ToString("F2");
            textBox2.BackColor = System.Drawing.Color.LightYellow;
            textBox2.ForeColor = System.Drawing.Color.Black;
            isEnteringCash = false;
        }

        // Number pad buttons
        private void button1_Click(object sender, EventArgs e) { AppendCashAmount("1"); }
        private void button2_Click(object sender, EventArgs e) { AppendCashAmount("2"); }
        private void button3_Click(object sender, EventArgs e) { AppendCashAmount("3"); }
        private void button4_Click(object sender, EventArgs e) { AppendCashAmount("4"); }
        private void button5_Click(object sender, EventArgs e) { AppendCashAmount("5"); }
        private void button6_Click(object sender, EventArgs e) { AppendCashAmount("6"); }
        private void button7_Click(object sender, EventArgs e) { AppendCashAmount("7"); }
        private void button8_Click(object sender, EventArgs e) { AppendCashAmount("8"); }
        private void button9_Click(object sender, EventArgs e) { AppendCashAmount("9"); }
        private void button10_Click(object sender, EventArgs e) { AppendCashAmount("0"); }
        private void button17_Click(object sender, EventArgs e) { AppendCashAmount("."); }
        private void button18_Click(object sender, EventArgs e) { AppendCashAmount("00"); }

        private void AppendCashAmount(string value)
        {
            isEnteringCash = true;

            if (currentAmountInput == "0" || string.IsNullOrEmpty(currentAmountInput))
                currentAmountInput = value;
            else
                currentAmountInput += value;

            textBox2.Text = currentAmountInput;
            textBox2.BackColor = System.Drawing.Color.LightGreen;
            textBox2.ForeColor = System.Drawing.Color.Blue;
        }

        // Clear button 
        private void button12_Click(object sender, EventArgs e)
        {
            if (isEnteringCash)
            {
                currentAmountInput = "";
                textBox2.Text = runningTotal.ToString("F2");
                textBox2.BackColor = System.Drawing.Color.LightYellow;
                textBox2.ForeColor = System.Drawing.Color.Black;
                isEnteringCash = false;
            }
            else if (cart.Count > 0)
            {
                DialogResult result = MessageBox.Show("Clear entire cart?", "Clear Cart",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    cart.Clear();
                    runningTotal = 0;
                    textBox2.Text = "0.00";
                    currentAmountInput = "";
                    isEnteringCash = false;
                    cashAmountReceived = 0;
                }
            }
        }

        // Cash payment button 
        private void button15_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Cart is empty! Please add items first.", "Empty Cart",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Use the cash amount entered from textBox2 after button19
            decimal amountReceived = cashAmountReceived;

            if (amountReceived <= 0)
            {
                MessageBox.Show("Please enter cash amount using number pad and press Sale button first!", "No Amount Entered",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (amountReceived < runningTotal)
            {
                MessageBox.Show($"Insufficient payment!\n\nTotal: ₱{runningTotal:F2}\nReceived: ₱{amountReceived:F2}\nShort: ₱{runningTotal - amountReceived:F2}",
                    "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal changeDue = amountReceived - runningTotal;

            DialogResult result = MessageBox.Show(
                $"Total: ₱{runningTotal:F2}\n" +
                $"Cash Received: ₱{amountReceived:F2}\n" +
                $"Change Due: ₱{changeDue:F2}\n\n" +
                $"Confirm CASH payment?",
                "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CompleteTransaction("CASH", amountReceived, changeDue);
            }
        }

        // Card payment button 
        private void button16_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Cart is empty! Please add items first.", "Empty Cart",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Total: ₱{runningTotal:F2}\n\n" +
                $"Confirm CARD payment?",
                "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CompleteTransaction("CARD", runningTotal, 0);
            }
        }

        private void CompleteTransaction(string paymentMethod, decimal amountReceived, decimal changeDue)
        {
            try
            {
                string transactionID = $"TRX-{DateTime.Now:yyyyMMddHHmmss}";

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    foreach (var item in cart)
                    {
                        
                        string query = @"INSERT INTO Sales (TransactionID, ProductID, ProductName, 
                               Quantity, UnitPrice, TotalPrice, EmployeeID, EmployeeName, 
                               PaymentMethod, TransactionDue, Status) 
                               VALUES (@tid, @pid, @pname, @qty, @price, @total, @eid, 
                               @ename, @pmethod, @date, @status)";

                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.Add("@tid", OleDbType.VarChar).Value = transactionID;
                            cmd.Parameters.Add("@pid", OleDbType.VarChar).Value = item.ProductID;
                            cmd.Parameters.Add("@pname", OleDbType.VarChar).Value = item.ProductName;
                            cmd.Parameters.Add("@qty", OleDbType.Integer).Value = item.Quantity;
                            cmd.Parameters.Add("@price", OleDbType.Currency).Value = item.Price;
                            cmd.Parameters.Add("@total", OleDbType.Currency).Value = item.Total;
                            cmd.Parameters.Add("@eid", OleDbType.VarChar).Value = currentEmployeeID;
                            cmd.Parameters.Add("@ename", OleDbType.VarChar).Value = currentEmployeeName;
                            cmd.Parameters.Add("@pmethod", OleDbType.VarChar).Value = paymentMethod;
                            cmd.Parameters.Add("@date", OleDbType.Date).Value = DateTime.Now;
                            cmd.Parameters.Add("@status", OleDbType.VarChar).Value = "Completed";

                            cmd.ExecuteNonQuery();
                        }

                        string updateStock = "UPDATE Products SET Quantity = Quantity - @qty WHERE ProductID = @pid";
                        using (OleDbCommand cmd = new OleDbCommand(updateStock, conn))
                        {
                            cmd.Parameters.AddWithValue("@qty", item.Quantity);
                            cmd.Parameters.AddWithValue("@pid", item.ProductID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                string receipt = GenerateReceipt(transactionID, paymentMethod, amountReceived, changeDue);
                MessageBox.Show(receipt, "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetTransaction();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateReceipt(string transactionID, string paymentMethod, decimal amountReceived, decimal changeDue)
        {
            string receipt = "";
            receipt += "==============================\n";
            receipt += "      GROCERYMAXX STORE       \n";
            receipt += "==============================\n";
            receipt += $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
            receipt += $"Cashier: {currentEmployeeName}\n";
            receipt += $"Transaction: {transactionID}\n";
            receipt += "------------------------------\n";
            receipt += "ITEMS:\n";

            foreach (var item in cart)
            {
                receipt += $"{item.ProductName,-20} x{item.Quantity} = ₱{item.Total:F2}\n";
            }

            receipt += "------------------------------\n";
            receipt += $"{"TOTAL:",-20} ₱{runningTotal:F2}\n";
            receipt += $"{"PAYMENT:",-20} {paymentMethod}\n";
            receipt += $"{"AMOUNT PAID:",-20} ₱{amountReceived:F2}\n";
            receipt += $"{"CHANGE:",-20} ₱{changeDue:F2}\n";
            receipt += "==============================\n";
            receipt += "    THANK YOU FOR SHOPPING!   \n";
            receipt += "==============================\n";

            return receipt;
        }

        private void ResetTransaction()
        {
            cart.Clear();
            runningTotal = 0;
            textBox2.Text = "0.00";
            textBox2.BackColor = System.Drawing.Color.LightYellow;
            textBox2.ForeColor = System.Drawing.Color.Black;
            currentAmountInput = "";
            isEnteringCash = false;
            cashAmountReceived = 0;
            LoadProducts();
        }

        // DataGridView cell click - Add product to cart
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string productID = row.Cells["ProductID"].Value.ToString();
                string productName = row.Cells["ProductName"].Value.ToString();
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                int stock = Convert.ToInt32(row.Cells["Quantity"].Value);

                if (stock <= 0)
                {
                    MessageBox.Show($"{productName} is out of stock!", "Out of Stock",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AddToCart(productID, productName, price);
            }
        }

        // Cancel Sale button 
        private void button13_Click(object sender, EventArgs e)
        {
            if (cart.Count > 0)
            {
                DialogResult result = MessageBox.Show("Cancel current sale?", "Cancel Sale",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    ResetTransaction();
                }
            }
        }


        // Sale button 
        private void button19_Click(object sender, EventArgs e)
        {
            if (cart.Count > 0)
            {
                // Store the cash amount from textBox2
                if (!string.IsNullOrEmpty(currentAmountInput) && currentAmountInput != "0")
                {
                    if (decimal.TryParse(currentAmountInput, out decimal amount))
                    {
                        cashAmountReceived = amount;
                    }
                    else
                    {
                        cashAmountReceived = runningTotal;
                    }
                }
                else
                {
                    cashAmountReceived = runningTotal;
                }

                MessageBox.Show($"Current Total: ₱{runningTotal:F2}\n\n" +
                               $"Cash Amount Entered: ₱{cashAmountReceived:F2}\n\n" +
                               $"Click Cash to complete payment or Card for card payment.",
                    "Sale Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cart is empty. Click products to add to cart.",
                    "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Log Out button 
        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?",
                "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }

        // Existing event handlers
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // Cart Item Class
        public class CartItem
        {
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Total { get { return Price * Quantity; } }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}