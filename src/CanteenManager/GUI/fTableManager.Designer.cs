namespace CanteenManager
{
    partial class fTableManager
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAccountProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCombineTable = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txbTotalPrice = new System.Windows.Forms.TextBox();
            this.cbListTable = new System.Windows.Forms.ComboBox();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.nmDiscount = new System.Windows.Forms.NumericUpDown();
            this.btnSwapTable = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.nmFoodCount = new System.Windows.Forms.NumericUpDown();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.cbFood = new System.Windows.Forms.ComboBox();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.flpTable = new System.Windows.Forms.FlowLayoutPanel();
            this.gbBill = new System.Windows.Forms.GroupBox();
            this.lsvBill = new System.Windows.Forms.ListView();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDiscount)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmFoodCount)).BeginInit();
            this.gbBill.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdmin,
            this.tsmiInfo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(898, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiAdmin
            // 
            this.tsmiAdmin.Name = "tsmiAdmin";
            this.tsmiAdmin.Size = new System.Drawing.Size(55, 20);
            this.tsmiAdmin.Text = "&Admin";
            this.tsmiAdmin.Click += new System.EventHandler(this.tsmiAdmin_Click);
            // 
            // tsmiInfo
            // 
            this.tsmiInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAccountProfile,
            this.tsmiAbout,
            this.tsmiLogout});
            this.tsmiInfo.Name = "tsmiInfo";
            this.tsmiInfo.Size = new System.Drawing.Size(70, 20);
            this.tsmiInfo.Text = "Thông tin";
            // 
            // tsmiAccountProfile
            // 
            this.tsmiAccountProfile.Name = "tsmiAccountProfile";
            this.tsmiAccountProfile.Size = new System.Drawing.Size(180, 22);
            this.tsmiAccountProfile.Text = "Tài khoản";
            this.tsmiAccountProfile.Click += new System.EventHandler(this.tsmiAccountProfile_Click);
            // 
            // tsmiLogout
            // 
            this.tsmiLogout.Name = "tsmiLogout";
            this.tsmiLogout.Size = new System.Drawing.Size(180, 22);
            this.tsmiLogout.Text = "Đăng &xuất";
            this.tsmiLogout.Click += new System.EventHandler(this.tsmiLogout_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnCombineTable);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txbTotalPrice);
            this.panel2.Controls.Add(this.cbListTable);
            this.panel2.Controls.Add(this.btnCheckOut);
            this.panel2.Controls.Add(this.nmDiscount);
            this.panel2.Controls.Add(this.btnSwapTable);
            this.panel2.Location = new System.Drawing.Point(434, 473);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(452, 62);
            this.panel2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(273, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 27);
            this.label4.TabIndex = 13;
            this.label4.Text = "Thành tiền:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(183, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 27);
            this.label3.TabIndex = 12;
            this.label3.Text = "Giảm giá:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCombineTable
            // 
            this.btnCombineTable.Location = new System.Drawing.Point(4, 32);
            this.btnCombineTable.Name = "btnCombineTable";
            this.btnCombineTable.Size = new System.Drawing.Size(84, 27);
            this.btnCombineTable.TabIndex = 11;
            this.btnCombineTable.TabStop = false;
            this.btnCombineTable.Text = "Gộp vào";
            this.btnCombineTable.UseVisualStyleBackColor = true;
            this.btnCombineTable.Click += new System.EventHandler(this.btnCombineTable_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(93, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 27);
            this.label2.TabIndex = 10;
            this.label2.Text = "Chọn bàn:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbTotalPrice
            // 
            this.txbTotalPrice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txbTotalPrice.Location = new System.Drawing.Point(273, 35);
            this.txbTotalPrice.Name = "txbTotalPrice";
            this.txbTotalPrice.ReadOnly = true;
            this.txbTotalPrice.Size = new System.Drawing.Size(84, 23);
            this.txbTotalPrice.TabIndex = 8;
            this.txbTotalPrice.TabStop = false;
            this.txbTotalPrice.Text = "0";
            this.txbTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbListTable
            // 
            this.cbListTable.FormattingEnabled = true;
            this.cbListTable.Location = new System.Drawing.Point(93, 35);
            this.cbListTable.Name = "cbListTable";
            this.cbListTable.Size = new System.Drawing.Size(84, 23);
            this.cbListTable.TabIndex = 7;
            this.cbListTable.TabStop = false;
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.Location = new System.Drawing.Point(363, 3);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(84, 55);
            this.btnCheckOut.TabIndex = 3;
            this.btnCheckOut.TabStop = false;
            this.btnCheckOut.Text = "T&hanh toán";
            this.btnCheckOut.UseVisualStyleBackColor = true;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // nmDiscount
            // 
            this.nmDiscount.Location = new System.Drawing.Point(183, 35);
            this.nmDiscount.Name = "nmDiscount";
            this.nmDiscount.Size = new System.Drawing.Size(84, 23);
            this.nmDiscount.TabIndex = 5;
            this.nmDiscount.TabStop = false;
            this.nmDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSwapTable
            // 
            this.btnSwapTable.Location = new System.Drawing.Point(4, 3);
            this.btnSwapTable.Name = "btnSwapTable";
            this.btnSwapTable.Size = new System.Drawing.Size(84, 27);
            this.btnSwapTable.TabIndex = 6;
            this.btnSwapTable.TabStop = false;
            this.btnSwapTable.Text = "Chuyển tới";
            this.btnSwapTable.UseVisualStyleBackColor = true;
            this.btnSwapTable.Click += new System.EventHandler(this.btnSwapTable_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.nmFoodCount);
            this.panel3.Controls.Add(this.btnAddFood);
            this.panel3.Controls.Add(this.cbFood);
            this.panel3.Controls.Add(this.cbCategory);
            this.panel3.Location = new System.Drawing.Point(434, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(452, 62);
            this.panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(279, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Số lượng:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nmFoodCount
            // 
            this.nmFoodCount.Location = new System.Drawing.Point(279, 31);
            this.nmFoodCount.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmFoodCount.Name = "nmFoodCount";
            this.nmFoodCount.Size = new System.Drawing.Size(77, 23);
            this.nmFoodCount.TabIndex = 2;
            this.nmFoodCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmFoodCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAddFood
            // 
            this.btnAddFood.Location = new System.Drawing.Point(362, 3);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(84, 52);
            this.btnAddFood.TabIndex = 2;
            this.btnAddFood.TabStop = false;
            this.btnAddFood.Text = "Thêm &món";
            this.btnAddFood.UseVisualStyleBackColor = true;
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // cbFood
            // 
            this.cbFood.FormattingEnabled = true;
            this.cbFood.Location = new System.Drawing.Point(3, 31);
            this.cbFood.Name = "cbFood";
            this.cbFood.Size = new System.Drawing.Size(268, 23);
            this.cbFood.TabIndex = 1;
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(3, 3);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(268, 23);
            this.cbCategory.TabIndex = 0;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // flpTable
            // 
            this.flpTable.AutoScroll = true;
            this.flpTable.Location = new System.Drawing.Point(12, 27);
            this.flpTable.Name = "flpTable";
            this.flpTable.Size = new System.Drawing.Size(416, 508);
            this.flpTable.TabIndex = 0;
            // 
            // gbBill
            // 
            this.gbBill.Controls.Add(this.lsvBill);
            this.gbBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbBill.Location = new System.Drawing.Point(434, 95);
            this.gbBill.Name = "gbBill";
            this.gbBill.Size = new System.Drawing.Size(452, 375);
            this.gbBill.TabIndex = 5;
            this.gbBill.TabStop = false;
            this.gbBill.Text = "Hoá đơn chưa thanh toán";
            // 
            // lsvBill
            // 
            this.lsvBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsvBill.Location = new System.Drawing.Point(4, 18);
            this.lsvBill.Name = "lsvBill";
            this.lsvBill.Size = new System.Drawing.Size(443, 354);
            this.lsvBill.TabIndex = 0;
            this.lsvBill.TabStop = false;
            this.lsvBill.UseCompatibleStateImageBehavior = false;
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(180, 22);
            this.tsmiAbout.Text = "Về chương trình";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // fTableManager
            // 
            this.AcceptButton = this.btnAddFood;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 547);
            this.Controls.Add(this.gbBill);
            this.Controls.Add(this.flpTable);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "fTableManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Canteen";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDiscount)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmFoodCount)).EndInit();
            this.gbBill.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem tsmiAdmin;
        private ToolStripMenuItem tsmiInfo;
        private ToolStripMenuItem tsmiAccountProfile;
        private ToolStripMenuItem tsmiLogout;
        private Panel panel2;
        private ComboBox cbListTable;
        private NumericUpDown nmDiscount;
        private Button btnSwapTable;
        private Button btnCheckOut;
        private Panel panel3;
        private NumericUpDown nmFoodCount;
        private Button btnAddFood;
        private ComboBox cbFood;
        private ComboBox cbCategory;
        private FlowLayoutPanel flpTable;
        private TextBox txbTotalPrice;
        private Label label1;
        private GroupBox gbBill;
        private ListView lsvBill;
        private Label label2;
        private Label label4;
        private Label label3;
        private Button btnCombineTable;
        private ToolStripMenuItem tsmiAbout;
    }
}