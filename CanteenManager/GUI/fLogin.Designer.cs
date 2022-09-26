namespace CanteenManager
{
    partial class fLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAdvanceOption = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ckbShowPassWord = new System.Windows.Forms.CheckBox();
            this.txbPassWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTestConnection = new System.Windows.Forms.Label();
            this.plAdvanceOption = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtgvShowServer = new System.Windows.Forms.DataGridView();
            this.btnSearchServerName = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txbDatabase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txbServerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.plAdvanceOption.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvShowServer)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAdvanceOption);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 170);
            this.panel1.TabIndex = 0;
            // 
            // lblAdvanceOption
            // 
            this.lblAdvanceOption.AutoSize = true;
            this.lblAdvanceOption.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.lblAdvanceOption.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblAdvanceOption.Location = new System.Drawing.Point(8, 145);
            this.lblAdvanceOption.Name = "lblAdvanceOption";
            this.lblAdvanceOption.Size = new System.Drawing.Size(54, 15);
            this.lblAdvanceOption.TabIndex = 4;
            this.lblAdvanceOption.Text = "Tuỳ chọn";
            this.lblAdvanceOption.Click += new System.EventHandler(this.lblAdvanceOption_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(240, 137);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(142, 137);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ckbShowPassWord);
            this.panel3.Controls.Add(this.txbPassWord);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(5, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(313, 59);
            this.panel3.TabIndex = 1;
            // 
            // ckbShowPassWord
            // 
            this.ckbShowPassWord.AutoSize = true;
            this.ckbShowPassWord.Location = new System.Drawing.Point(139, 37);
            this.ckbShowPassWord.Name = "ckbShowPassWord";
            this.ckbShowPassWord.Size = new System.Drawing.Size(121, 19);
            this.ckbShowPassWord.TabIndex = 2;
            this.ckbShowPassWord.TabStop = false;
            this.ckbShowPassWord.Text = "Hiển thị mật khẩu";
            this.ckbShowPassWord.UseVisualStyleBackColor = true;
            this.ckbShowPassWord.CheckedChanged += new System.EventHandler(this.ckbShowPassWord_CheckedChanged);
            // 
            // txbPassWord
            // 
            this.txbPassWord.Location = new System.Drawing.Point(139, 8);
            this.txbPassWord.Name = "txbPassWord";
            this.txbPassWord.Size = new System.Drawing.Size(171, 23);
            this.txbPassWord.TabIndex = 1;
            this.txbPassWord.Text = "123";
            this.txbPassWord.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mật khẩu:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txbUserName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(5, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(313, 59);
            this.panel2.TabIndex = 0;
            // 
            // txbUserName
            // 
            this.txbUserName.Location = new System.Drawing.Point(139, 8);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(171, 23);
            this.txbUserName.TabIndex = 1;
            this.txbUserName.Text = "admin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // lblTestConnection
            // 
            this.lblTestConnection.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTestConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTestConnection.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lblTestConnection.Location = new System.Drawing.Point(0, 188);
            this.lblTestConnection.Name = "lblTestConnection";
            this.lblTestConnection.Size = new System.Drawing.Size(348, 37);
            this.lblTestConnection.TabIndex = 1;
            this.lblTestConnection.Text = "Click để kiểm tra kết nối...";
            this.lblTestConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTestConnection.Click += new System.EventHandler(this.lblTestConnection_Click);
            this.lblTestConnection.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTestConnection_MouseDown);
            // 
            // plAdvanceOption
            // 
            this.plAdvanceOption.Controls.Add(this.panel4);
            this.plAdvanceOption.Controls.Add(this.panel5);
            this.plAdvanceOption.Controls.Add(this.panel6);
            this.plAdvanceOption.Location = new System.Drawing.Point(12, 188);
            this.plAdvanceOption.Name = "plAdvanceOption";
            this.plAdvanceOption.Size = new System.Drawing.Size(324, 381);
            this.plAdvanceOption.TabIndex = 2;
            this.plAdvanceOption.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dtgvShowServer);
            this.panel4.Controls.Add(this.btnSearchServerName);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(6, 139);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(312, 239);
            this.panel4.TabIndex = 2;
            // 
            // dtgvShowServer
            // 
            this.dtgvShowServer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvShowServer.Location = new System.Drawing.Point(2, 36);
            this.dtgvShowServer.Name = "dtgvShowServer";
            this.dtgvShowServer.RowTemplate.Height = 25;
            this.dtgvShowServer.Size = new System.Drawing.Size(307, 200);
            this.dtgvShowServer.TabIndex = 0;
            this.dtgvShowServer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvShowServer_CellClick);
            // 
            // btnSearchServerName
            // 
            this.btnSearchServerName.Location = new System.Drawing.Point(138, 7);
            this.btnSearchServerName.Name = "btnSearchServerName";
            this.btnSearchServerName.Size = new System.Drawing.Size(75, 23);
            this.btnSearchServerName.TabIndex = 3;
            this.btnSearchServerName.Text = "Bắt đầu";
            this.btnSearchServerName.UseVisualStyleBackColor = true;
            this.btnSearchServerName.Click += new System.EventHandler(this.btnSearchServerName_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(2, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tìm trên máy:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txbDatabase);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(5, 72);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(313, 61);
            this.panel5.TabIndex = 1;
            // 
            // txbDatabase
            // 
            this.txbDatabase.Location = new System.Drawing.Point(139, 8);
            this.txbDatabase.Name = "txbDatabase";
            this.txbDatabase.Size = new System.Drawing.Size(171, 23);
            this.txbDatabase.TabIndex = 1;
            this.txbDatabase.Text = "CanteenManager";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(3, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Database:";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txbServerName);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Location = new System.Drawing.Point(5, 7);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(313, 59);
            this.panel6.TabIndex = 0;
            // 
            // txbServerName
            // 
            this.txbServerName.Location = new System.Drawing.Point(139, 8);
            this.txbServerName.Name = "txbServerName";
            this.txbServerName.Size = new System.Drawing.Size(171, 23);
            this.txbServerName.TabIndex = 1;
            this.txbServerName.Text = "SQLEXPRESS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(3, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Tên Server:";
            // 
            // fLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(348, 576);
            this.Controls.Add(this.plAdvanceOption);
            this.Controls.Add(this.lblTestConnection);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(364, 1000);
            this.Name = "fLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fLogin_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.plAdvanceOption.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvShowServer)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TextBox txbUserName;
        private Label label1;
        private Panel panel3;
        private TextBox txbPassWord;
        private Label label2;
        private Button btnExit;
        private Button btnLogin;
        private CheckBox ckbShowPassWord;
        private Label lblTestConnection;
        private Panel plAdvanceOption;
        private Panel panel5;
        private TextBox txbDatabase;
        private Label label4;
        private Panel panel6;
        private TextBox txbServerName;
        private Label label5;
        private Label lblAdvanceOption;
        private Label label3;
        private Button btnSearchServerName;
        private DataGridView dtgvShowServer;
        private Panel panel4;
    }
}