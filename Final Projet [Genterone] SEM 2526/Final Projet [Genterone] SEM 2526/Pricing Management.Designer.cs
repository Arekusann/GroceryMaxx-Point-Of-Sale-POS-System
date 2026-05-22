namespace Final_Projet__Genterone__SEM_2526
{
    partial class Pricing_Management
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
            textBox4 = new TextBox();
            button2 = new Button();
            label6 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            button1 = new Button();
            label5 = new Label();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
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
            panel1.Location = new Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(841, 45);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F);
            label1.Location = new Point(3, 6);
            label1.Name = "label1";
            label1.Size = new Size(228, 31);
            label1.TabIndex = 8;
            label1.Text = "Pricing Management";
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkGray;
            panel2.Controls.Add(button3);
            panel2.ForeColor = Color.Orange;
            panel2.Location = new Point(1, 481);
            panel2.Name = "panel2";
            panel2.Size = new Size(841, 37);
            panel2.TabIndex = 5;
            // 
            // button3
            // 
            button3.Location = new Point(11, 3);
            button3.Name = "button3";
            button3.Size = new Size(175, 34);
            button3.TabIndex = 44;
            button3.Text = "Return to Main Menu";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(570, 319);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(202, 27);
            textBox4.TabIndex = 43;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(600, 365);
            button2.Name = "button2";
            button2.Size = new Size(145, 51);
            button2.TabIndex = 42;
            button2.Text = "Remove";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(644, 153);
            label6.Name = "label6";
            label6.Size = new Size(44, 20);
            label6.TabIndex = 41;
            label6.Text = "Price:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(570, 176);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(202, 27);
            textBox2.TabIndex = 40;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(632, 296);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 38;
            label4.Text = "Product ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(570, 265);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 37;
            label3.Text = "Remove Price";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(570, 52);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 36;
            label2.Text = "Add Price";
            // 
            // button1
            // 
            button1.Location = new Point(620, 220);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 34;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(620, 83);
            label5.Name = "label5";
            label5.Size = new Size(82, 20);
            label5.TabIndex = 39;
            label5.Text = "Product ID:";
            label5.Click += label5_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(570, 106);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(202, 27);
            textBox1.TabIndex = 35;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Location = new Point(1, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(514, 374);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Pricing_Management
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(843, 517);
            Controls.Add(textBox4);
            Controls.Add(button2);
            Controls.Add(label6);
            Controls.Add(textBox2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Name = "Pricing_Management";
            Text = "Pricing_Management";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView dataGridView1;
        private Panel panel2;
        private TextBox textBox4;
        private Button button2;
        private Label label6;
        private TextBox textBox2;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button button1;
        private Label label5;
        private TextBox textBox1;
        private Button button3;
    }
}