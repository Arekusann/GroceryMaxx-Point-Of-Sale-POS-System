namespace Final_Projet__Genterone__SEM_2526
{
    partial class Main_Menu__User
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            label2 = new Label();
            panel3 = new Panel();
            button18 = new Button();
            button17 = new Button();
            label1 = new Label();
            button16 = new Button();
            button15 = new Button();
            textBox2 = new TextBox();
            button12 = new Button();
            button10 = new Button();
            button7 = new Button();
            button9 = new Button();
            button8 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            panel2 = new Panel();
            button19 = new Button();
            button13 = new Button();
            button11 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 51);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(627, 385);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.Green;
            panel1.Controls.Add(label2);
            panel1.Font = new Font("MS Reference Sans Serif", 13.8F);
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(937, 45);
            panel1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(148, 31);
            label2.TabIndex = 9;
            label2.Text = "GroceryMaxx";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel3.BackColor = SystemColors.Control;
            panel3.Controls.Add(button18);
            panel3.Controls.Add(button17);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(button16);
            panel3.Controls.Add(button15);
            panel3.Controls.Add(textBox2);
            panel3.Controls.Add(button12);
            panel3.Controls.Add(button10);
            panel3.Controls.Add(button7);
            panel3.Controls.Add(button9);
            panel3.Controls.Add(button8);
            panel3.Controls.Add(button6);
            panel3.Controls.Add(button5);
            panel3.Controls.Add(button4);
            panel3.Controls.Add(button3);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(textBox1);
            panel3.Location = new Point(645, 51);
            panel3.Name = "panel3";
            panel3.Size = new Size(280, 385);
            panel3.TabIndex = 3;
            // 
            // button18
            // 
            button18.Location = new Point(187, 280);
            button18.Name = "button18";
            button18.Size = new Size(74, 47);
            button18.TabIndex = 16;
            button18.Text = ",";
            button18.UseVisualStyleBackColor = true;
            button18.Click += button18_Click;
            // 
            // button17
            // 
            button17.Location = new Point(101, 280);
            button17.Name = "button17";
            button17.Size = new Size(74, 47);
            button17.TabIndex = 15;
            button17.Text = ".";
            button17.UseVisualStyleBackColor = true;
            button17.Click += button17_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 14;
            label1.Text = "Employee:";
            // 
            // button16
            // 
            button16.Location = new Point(101, 331);
            button16.Name = "button16";
            button16.Size = new Size(74, 47);
            button16.TabIndex = 13;
            button16.Text = "Card";
            button16.UseVisualStyleBackColor = true;
            button16.Click += button16_Click;
            // 
            // button15
            // 
            button15.Location = new Point(190, 331);
            button15.Name = "button15";
            button15.Size = new Size(74, 47);
            button15.TabIndex = 12;
            button15.Text = "Cash";
            button15.UseVisualStyleBackColor = true;
            button15.Click += button15_Click;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            textBox2.Location = new Point(15, 67);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(249, 48);
            textBox2.TabIndex = 11;
            textBox2.Text = "0";
            textBox2.TextAlign = HorizontalAlignment.Right;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button12
            // 
            button12.ForeColor = Color.Black;
            button12.Location = new Point(15, 333);
            button12.Name = "button12";
            button12.Size = new Size(75, 45);
            button12.TabIndex = 3;
            button12.Text = "Clear";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // button10
            // 
            button10.Location = new Point(15, 280);
            button10.Name = "button10";
            button10.Size = new Size(74, 47);
            button10.TabIndex = 10;
            button10.Text = "0";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // button7
            // 
            button7.Location = new Point(15, 227);
            button7.Name = "button7";
            button7.Size = new Size(74, 47);
            button7.TabIndex = 7;
            button7.Text = "7";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button9
            // 
            button9.Location = new Point(187, 227);
            button9.Name = "button9";
            button9.Size = new Size(74, 47);
            button9.TabIndex = 9;
            button9.Text = "9";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.Location = new Point(101, 227);
            button8.Name = "button8";
            button8.Size = new Size(74, 47);
            button8.TabIndex = 8;
            button8.Text = "8";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button6
            // 
            button6.Location = new Point(187, 174);
            button6.Name = "button6";
            button6.Size = new Size(74, 47);
            button6.TabIndex = 6;
            button6.Text = "6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new Point(101, 174);
            button5.Name = "button5";
            button5.Size = new Size(74, 47);
            button5.TabIndex = 5;
            button5.Text = "5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(15, 174);
            button4.Name = "button4";
            button4.Size = new Size(74, 47);
            button4.TabIndex = 4;
            button4.Text = "4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(187, 121);
            button3.Name = "button3";
            button3.Size = new Size(74, 47);
            button3.TabIndex = 3;
            button3.Text = "3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(101, 121);
            button2.Name = "button2";
            button2.Size = new Size(74, 47);
            button2.TabIndex = 2;
            button2.Text = "2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(15, 121);
            button1.Name = "button1";
            button1.Size = new Size(74, 47);
            button1.TabIndex = 1;
            button1.Text = "1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ControlLight;
            textBox1.Font = new Font("Segoe UI", 10F);
            textBox1.Location = new Point(15, 32);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(249, 30);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.DarkGray;
            panel2.Controls.Add(button19);
            panel2.Controls.Add(button13);
            panel2.Controls.Add(button11);
            panel2.ForeColor = Color.Orange;
            panel2.Location = new Point(0, 440);
            panel2.Name = "panel2";
            panel2.Size = new Size(937, 55);
            panel2.TabIndex = 6;
            panel2.Paint += panel2_Paint;
            // 
            // button19
            // 
            button19.ForeColor = Color.Black;
            button19.Location = new Point(850, 5);
            button19.Name = "button19";
            button19.Size = new Size(75, 45);
            button19.TabIndex = 6;
            button19.Text = "Sale";
            button19.UseVisualStyleBackColor = true;
            button19.Click += button19_Click;
            // 
            // button13
            // 
            button13.ForeColor = Color.Black;
            button13.Location = new Point(724, 7);
            button13.Name = "button13";
            button13.Size = new Size(120, 45);
            button13.TabIndex = 4;
            button13.Text = "Cancel Sale";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            // 
            // button11
            // 
            button11.ForeColor = Color.Black;
            button11.Location = new Point(12, 6);
            button11.Name = "button11";
            button11.Size = new Size(90, 45);
            button11.TabIndex = 2;
            button11.Text = "Log Out";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // Main_Menu__User
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(937, 495);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            MinimumSize = new Size(800, 500);
            Name = "Main_Menu__User";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GroceryMaxx POS - Employee Dashboard";
            Load += Main_Menu__User_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }


        private DataGridView dataGridView1;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Button button1;
        private TextBox textBox1;
        private Button button10;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Label label1;
        private Button button16;
        private Button button15;
        private TextBox textBox2;
        private Button button13;
        private Button button12;
        private Button button11;
        private Label label2;
        private Button button18;
        private Button button17;
        private Button button19;
    }
}