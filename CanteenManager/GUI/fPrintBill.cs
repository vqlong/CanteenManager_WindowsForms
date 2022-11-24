using Microsoft.Reporting.WinForms;
using CanteenManager.DAO;
using System.Data;

namespace CanteenManager
{
    public partial class fPrintBill : Form
    {
        public fPrintBill(int billId)
        {
            InitializeComponent();

            LoadReport(billId); 
        }

        void LoadReport(int billId)
        {
            BindingSource bindingSource = new BindingSource() { DataSource = BillDetailDAO.Instance.GetListBillDetailByBillId(billId) };
            ReportDataSource rds = new ReportDataSource("dsBillDetail", bindingSource);

            //var data = DataProvider.Instance.ExecuteQuery($"SELECT * FROM Bill WHERE Bill.ID = {billID}");
            var bill = BillDAO.Instance.GetBillById(billId);
            var data = new List<Models.Bill> { bill };
            ReportDataSource rds2 = new ReportDataSource("dsBill", data);

            rpvPrintBill.LocalReport.ReportEmbeddedResource = "CanteenManager.Report.rpPrintBill.rdlc";
            rpvPrintBill.LocalReport.DataSources.Clear();
            rpvPrintBill.LocalReport.DataSources.Add(rds);
            rpvPrintBill.LocalReport.DataSources.Add(rds2);

            rpvPrintBill.RefreshReport();

            var dateCheckOut = Convert.ToDateTime(bill.DateCheckOut);
            rpvPrintBill.LocalReport.DisplayName = $"BillID_{billId}_" + dateCheckOut.ToString("ddMMyyy_hhmmss_tt");

        }
    }
}
