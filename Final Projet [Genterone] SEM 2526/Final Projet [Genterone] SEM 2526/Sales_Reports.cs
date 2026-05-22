using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace Final_Projet__Genterone__SEM_2526
{
    public partial class Sales_Reports : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rosal\OneDrive\Documents\GroceryMaxxDB.accdb;";
        private PrintDocument printDocument = new PrintDocument();
        private string printContent = "";

        public Sales_Reports()
        {
            InitializeComponent();
        }

        private void Sales_Reports_Load(object sender, EventArgs e)
        {
            LoadRecentSales();

            // Setup print document
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
        }

        private void LoadRecentSales()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TOP 50 
                                    TransactionID, 
                                    TransactionDue AS SaleDate,
                                    EmployeeName AS Cashier,
                                    ProductName,
                                    Quantity,
                                    UnitPrice,
                                    TotalPrice,
                                    PaymentMethod
                                   FROM Sales 
                                   ORDER BY TransactionDue DESC";

                    using (OleDbDataAdapter da = new OleDbDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Count > 0)
                        {
                            dataGridView1.Columns["TransactionID"].HeaderText = "Transaction ID";
                            dataGridView1.Columns["SaleDate"].HeaderText = "Date";
                            dataGridView1.Columns["Cashier"].HeaderText = "Cashier";
                            dataGridView1.Columns["ProductName"].HeaderText = "Product";
                            dataGridView1.Columns["Quantity"].HeaderText = "Qty";
                            dataGridView1.Columns["UnitPrice"].HeaderText = "Unit Price";
                            dataGridView1.Columns["TotalPrice"].HeaderText = "Total";
                            dataGridView1.Columns["PaymentMethod"].HeaderText = "Payment";

                            dataGridView1.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
                            dataGridView1.Columns["TotalPrice"].DefaultCellStyle.Format = "C2";
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                    TransactionID, 
                                    TransactionDue AS SaleDate,
                                    EmployeeName AS Cashier,
                                    ProductName,
                                    Quantity,
                                    UnitPrice,
                                    TotalPrice,
                                    PaymentMethod
                                   FROM Sales 
                                   WHERE TransactionDue BETWEEN @startDate AND @endDate
                                   ORDER BY TransactionDue DESC";

                    using (OleDbDataAdapter da = new OleDbDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@startDate", startDate);
                        da.SelectCommand.Parameters.AddWithValue("@endDate", endDate);

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Count > 0)
                        {
                            dataGridView1.Columns["TransactionID"].HeaderText = "Transaction ID";
                            dataGridView1.Columns["SaleDate"].HeaderText = "Date";
                            dataGridView1.Columns["Cashier"].HeaderText = "Cashier";
                            dataGridView1.Columns["ProductName"].HeaderText = "Product";
                            dataGridView1.Columns["Quantity"].HeaderText = "Qty";
                            dataGridView1.Columns["UnitPrice"].HeaderText = "Unit Price";
                            dataGridView1.Columns["TotalPrice"].HeaderText = "Total";
                            dataGridView1.Columns["PaymentMethod"].HeaderText = "Payment";

                            dataGridView1.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
                            dataGridView1.Columns["TotalPrice"].DefaultCellStyle.Format = "C2";
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Print Sales Report Button
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data to print!", "Print Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GeneratePrintContent();

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void GeneratePrintContent()
        {
            printContent = "";
            printContent += "==============================\n";
            printContent += "      GROCERYMAXX STORE       \n";
            printContent += "         SALES REPORT         \n";
            printContent += "==============================\n";
            printContent += $"Date Printed: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
            printContent += $"Total Records: {dataGridView1.Rows.Count}\n";
            printContent += "------------------------------\n\n";

            // Add column headers
            printContent += "ID".PadRight(15) + "Date".PadRight(12) + "Cashier".PadRight(20) +
                           "Product".PadRight(25) + "Qty".PadRight(5) + "Price".PadRight(12) + "Total".PadRight(12) + "Payment\n";
            printContent += "".PadRight(120, '-') + "\n";

            // Add data rows
            decimal grandTotal = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string transID = row.Cells["TransactionID"].Value?.ToString() ?? "";
                    string saleDate = Convert.ToDateTime(row.Cells["SaleDate"].Value).ToString("MM/dd HH:mm");
                    string cashier = row.Cells["Cashier"].Value?.ToString() ?? "";
                    string product = row.Cells["ProductName"].Value?.ToString() ?? "";
                    string qty = row.Cells["Quantity"].Value?.ToString() ?? "";
                    string price = Convert.ToDecimal(row.Cells["UnitPrice"].Value).ToString("F2");
                    string total = Convert.ToDecimal(row.Cells["TotalPrice"].Value).ToString("F2");
                    string payment = row.Cells["PaymentMethod"].Value?.ToString() ?? "";

                    printContent += $"{transID,-15} {saleDate,-12} {cashier,-20} {product,-25} {qty,-5} ₱{price,-10} ₱{total,-10} {payment,-8}\n";

                    if (decimal.TryParse(total, out decimal totalValue))
                        grandTotal += totalValue;
                }
            }

            printContent += "\n" + "".PadRight(120, '-') + "\n";
            printContent += $"{"GRAND TOTAL:",-100} ₱{grandTotal:F2}\n";
            printContent += "==============================\n";
            printContent += "    THANK YOU FOR SHOPPING!   \n";
            printContent += "==============================";
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font printFont = new Font("Courier New", 9);
            float lineHeight = printFont.GetHeight(e.Graphics);
            float y = e.MarginBounds.Top;
            int lineCount = 0;

            string[] lines = printContent.Split('\n');

            foreach (string line in lines)
            {
                e.Graphics.DrawString(line, printFont, Brushes.Black, e.MarginBounds.Left, y);
                y += lineHeight;
                lineCount++;

                if (y > e.MarginBounds.Bottom && lineCount < lines.Length)
                {
                    e.HasMorePages = true;
                    return;
                }
            }
            e.HasMorePages = false;
        }

        // Refresh button
        private void button1_Click(object sender, EventArgs e)
        {
            LoadRecentSales();
            MessageBox.Show("Sales report refreshed!", "Refresh",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Filter by date range
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            LoadSalesByDateRange(startDate, endDate);

            label3.Text = $"Showing sales from {startDate:MM/dd/yyyy} to {endDate:MM/dd/yyyy}";
        }

        // Show all sales
        private void button3_Click(object sender, EventArgs e)
        {
            LoadRecentSales();
            label3.Text = "Showing most recent 50 sales";
        }

        // Close button
        private void button4_Click(object sender, EventArgs e)
        {
            // Open the Admin Main Menu
            Main_Menu__Admin_ adminMenu = new Main_Menu__Admin_();
            adminMenu.Show();

            
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
