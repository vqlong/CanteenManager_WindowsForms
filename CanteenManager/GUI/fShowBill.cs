using CanteenManager.DAO;

namespace CanteenManager
{
    public partial class fShowBill : Form
    {
        public fShowBill(object value)
        {
            InitializeComponent();

            if(int.TryParse(value.ToString(), out int billID))
            {
                LoadBill(billID);

                btnPrintBill.Click += delegate { new fPrintBill(billID).ShowDialog(); };
            }

            //Tạo 1 nút bấm để thoát form
            var btnExit = new Button();
            btnExit.Click += delegate { Close(); };

            CancelButton = btnExit;
        }

        void LoadBill(int billID)
        {
            dtgvShowBill.DataSource = BillDetailDAO.Instance.GetListBillDetailByBillID(billID);
            dtgvShowBill.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvShowBill.EditMode = DataGridViewEditMode.EditProgrammatically;

            dtgvShowBill.Columns[0].HeaderText = "Tên món";
            dtgvShowBill.Columns[1].HeaderText = "Thể loại";
            dtgvShowBill.Columns[2].HeaderText = "Số lượng";
            dtgvShowBill.Columns[3].HeaderText = "Đơn giá";
            dtgvShowBill.Columns[4].HeaderText = "Tổng tiền";

            dtgvShowBill.Columns[3].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-vn");
            dtgvShowBill.Columns[3].DefaultCellStyle.Format = "c0";
            dtgvShowBill.Columns[4].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-vn");
            dtgvShowBill.Columns[4].DefaultCellStyle.Format = "c0";

            lbBillID.Text = $"ID hoá đơn: {billID}";
        }
    }
}
