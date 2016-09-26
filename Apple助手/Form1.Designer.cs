namespace Apple
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.selectInFileBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.inFileTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.randContextChk = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.randCountryChk = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectInFileBtn
            // 
            this.selectInFileBtn.Location = new System.Drawing.Point(223, 19);
            this.selectInFileBtn.Name = "selectInFileBtn";
            this.selectInFileBtn.Size = new System.Drawing.Size(61, 23);
            this.selectInFileBtn.TabIndex = 0;
            this.selectInFileBtn.Text = "浏览...";
            this.selectInFileBtn.UseVisualStyleBackColor = true;
            this.selectInFileBtn.Click += new System.EventHandler(this.selectInFileBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件名:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.ForeColor = System.Drawing.Color.LimeGreen;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(512, 254);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // inFileTxt
            // 
            this.inFileTxt.Location = new System.Drawing.Point(56, 20);
            this.inFileTxt.Name = "inFileTxt";
            this.inFileTxt.Size = new System.Drawing.Size(161, 21);
            this.inFileTxt.TabIndex = 4;
            this.inFileTxt.TextChanged += new System.EventHandler(this.inFileTxt_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.startBtn);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.selectInFileBtn);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.inFileTxt);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 53);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作区";
            // 
            // randContextChk
            // 
            this.randContextChk.AutoSize = true;
            this.randContextChk.Checked = true;
            this.randContextChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randContextChk.Location = new System.Drawing.Point(145, 20);
            this.randContextChk.Name = "randContextChk";
            this.randContextChk.Size = new System.Drawing.Size(72, 16);
            this.randContextChk.TabIndex = 5;
            this.randContextChk.Text = "内容随机";
            this.randContextChk.UseVisualStyleBackColor = true;
            this.randContextChk.CheckedChanged += new System.EventHandler(this.randContextChk_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(290, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "导入文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.CausesValidation = false;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(257, 254);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Location = new System.Drawing.Point(3, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(779, 274);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作记录区";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(367, 19);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(61, 23);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "开始执行";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(578, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // randCountryChk
            // 
            this.randCountryChk.AutoSize = true;
            this.randCountryChk.BackColor = System.Drawing.SystemColors.Control;
            this.randCountryChk.Checked = true;
            this.randCountryChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randCountryChk.Location = new System.Drawing.Point(21, 20);
            this.randCountryChk.Name = "randCountryChk";
            this.randCountryChk.Size = new System.Drawing.Size(102, 16);
            this.randCountryChk.TabIndex = 7;
            this.randCountryChk.Text = "国家/地区随机";
            this.randCountryChk.UseVisualStyleBackColor = false;
            this.randCountryChk.CheckedChanged += new System.EventHandler(this.countryChk_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox3);
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.randCountryChk);
            this.groupBox3.Controls.Add(this.randContextChk);
            this.groupBox3.Location = new System.Drawing.Point(3, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(779, 51);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选项区";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Size = new System.Drawing.Size(773, 254);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(497, 39);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(664, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(61, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "浏览...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(497, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(161, 21);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(438, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "日志路径:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(716, 30);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(694, 16);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "测试";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(270, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(42, 20);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(223, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "开始行:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(329, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "结束行:";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(376, 16);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(42, 20);
            this.comboBox2.TabIndex = 10;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(450, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "重试:";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBox3.Location = new System.Drawing.Point(483, 16);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(42, 20);
            this.comboBox3.TabIndex = 10;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Apple反馈助手";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button selectInFileBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox inFileTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox randContextChk;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox randCountryChk;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label5;
    }
}

