namespace CanteenManager
{
    partial class fShowBill
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
            this.dtgvShowBill = new System.Windows.Forms.DataGridView();
            this.btnPrintBill = new System.Windows.Forms.Button();
            this.lbBillID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvShowBill)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvShowBill
            // 
            this.dtgvShowBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvShowBill.Location = new System.Drawing.Point(12, 33);
            this.dtgvShowBill.Name = "dtgvShowBill";
            this.dtgvShowBill.RowTemplate.Height = 25;
            this.dtgvShowBill.Size = new System.Drawing.Size(776, 376);
            this.dtgvShowBill.TabIndex = 0;
            // 
            // btnPrintBill
            // 
            this.btnPrintBill.Location = new System.Drawing.Point(713, 415);
            this.btnPrintBill.Name = "btnPrintBill";
            this.btnPrintBill.Size = new System.Drawing.Size(75, 23);
            this.btnPrintBill.TabIndex = 1;
            this.btnPrintBill.Text = "In";
            this.btnPrintBill.UseVisualStyleBackColor = true;
            // 
            // lbBillID
            // 
            this.lbBillID.AutoSize = true;
            this.lbBillID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbBillID.Location = new System.Drawing.Point(12, 9);
            this.lbBillID.Name = "lbBillID";
            this.lbBillID.Size = new System.Drawing.Size(98, 21);
            this.lbBillID.TabIndex = 2;
            this.lbBillID.Text = "ID hoá đơn:";
            // 
            // fShowBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbBillID);
            this.Controls.Add(this.btnPrintBill);
            this.Controls.Add(this.dtgvShowBill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "fShowBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoá đơn";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvShowBill)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dtgvShowBill;
        private Button btnPrintBill;
        private Label lbBillID;
    }
}