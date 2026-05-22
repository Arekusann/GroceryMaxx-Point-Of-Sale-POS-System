namespace Final_Projet__Genterone__SEM_2526
{
    partial class Employee_Management
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label7 = new Label();
            textBox3 = new TextBox();
            button2 = new Button();
            textBox4 = new TextBox();
            label6 = new Label();
            textBox2 = new TextBox();
            label8 = new Label();
            textBox5 = new TextBox();
            label9 = new Label();
            textBox6 = new TextBox();
            label10 = new Label();
            textBox7 = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Green;
            panel1.Controls.Add(label1);
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(0, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1084, 45);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F);
            label1.Location = new Point(3, 8);
            label1.Name = "label1";
            label1.Size = new Size(257, 31);
            label1.TabIndex = 7;
            label1.Text = "Employee Management";
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkGray;
            panel2.Controls.Add(button3);
            panel2.ForeColor = Color.Orange;
            panel2.Location = new Point(0, 451);
            panel2.Name = "panel2";
            panel2.Size = new Size(1084, 37);
            panel2.TabIndex = 4;
            // 
            // button3
            // 
            button3.Location = new Point(3, 0);
            button3.Name = "button3";
            button3.Size = new Size(180, 36);
            button3.TabIndex = 45;
            button3.Text = "Return to Main Menu";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(815, 354);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(900, 275);
            button1.Name = "button1";
            button1.Size = new Size(131, 53);
            button1.TabIndex = 6;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(927, 104);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(143, 27);
            textBox1.TabIndex = 8;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(829, 52);
            label2.Name = "label2";
            label2.Size = new Size(107, 20);
            label2.TabIndex = 9;
            label2.Text = "Add Employee";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(829, 340);
            label3.Name = "label3";
            label3.Size = new Size(133, 20);
            label3.TabIndex = 10;
            label3.Text = "Remove Employee";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(824, 366);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 11;
            label4.Text = "Employee ID:";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(869, 107);
            label5.Name = "label5";
            label5.Size = new Size(52, 20);
            label5.TabIndex = 12;
            label5.Text = "Name:";
            label5.Click += label5_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(829, 140);
            label7.Name = "label7";
            label7.Size = new Size(100, 20);
            label7.TabIndex = 19;
            label7.Text = "Middle Initial:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(927, 137);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(143, 27);
            textBox3.TabIndex = 18;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(900, 396);
            button2.Name = "button2";
            button2.Size = new Size(131, 53);
            button2.TabIndex = 20;
            button2.Text = "Remove";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(927, 363);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(143, 27);
            textBox4.TabIndex = 21;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(869, 173);
            label6.Name = "label6";
            label6.Size = new Size(42, 20);
            label6.TabIndex = 23;
            label6.Text = "Role:";
            label6.Click += label6_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(927, 170);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(143, 27);
            textBox2.TabIndex = 22;
            textBox2.TextChanged += textBox2_TextChanged_1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(829, 210);
            label8.Name = "label8";
            label8.Size = new Size(85, 20);
            label8.TabIndex = 25;
            label8.Text = "Date Hired:";
            label8.Click += label8_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(927, 203);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(143, 27);
            textBox5.TabIndex = 24;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(840, 74);
            label9.Name = "label9";
            label9.Size = new Size(81, 20);
            label9.TabIndex = 27;
            label9.Text = "UserName:";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(927, 71);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(143, 27);
            textBox6.TabIndex = 26;
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(824, 245);
            label10.Name = "label10";
            label10.Size = new Size(97, 20);
            label10.TabIndex = 30;
            label10.Text = "Employee ID:";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(927, 242);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(143, 27);
            textBox7.TabIndex = 31;
            textBox7.TextChanged += textBox7_TextChanged;
            textBox7.Enter += textBox7_Enter;
            textBox7.Leave += textBox7_Leave;
            // 
            // Employee_Management
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1085, 500);
            Controls.Add(label10);
            Controls.Add(textBox7);
            Controls.Add(label9);
            Controls.Add(textBox6);
            Controls.Add(label8);
            Controls.Add(textBox5);
            Controls.Add(label6);
            Controls.Add(textBox2);
            Controls.Add(textBox4);
            Controls.Add(button2);
            Controls.Add(label7);
            Controls.Add(textBox3);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Employee_Management";
            Text = "Employee Management";
            Load += Employee_Management_Load_1;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private DataGridView dataGridView1;
        private Button button1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private TextBox textBox3;
        private Button button2;
        private TextBox textBox4;
        private Button button3;
        private Label label6;
        private TextBox textBox2;
        private Label label8;
        private TextBox textBox5;
        private Label label9;
        private TextBox textBox6;
        private Label label10;      // NEW
        private TextBox textBox7;    // NEW
    }
}
