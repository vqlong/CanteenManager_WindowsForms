using Microsoft.Reporting.WinForms;
using CanteenManager.DAO;
using System.Data;

namespace CanteenManager
{
    public partial class fPrintBill : Form
    {
        public fPrintBill(int billID)
        {
            InitializeComponent();

            LoadReport(billID); 
        }

        void LoadReport(int billID)
        {
            BindingSource bindingSource = new BindingSource() { DataSource = BillDetailDAO.Instance.GetListBillDetailByBillID(billID) };
            ReportDataSource rds = new ReportDataSource("dsBillDetail", bindingSource);

            DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT * FROM Bill WHERE Bill.ID = {billID}");
            ReportDataSource rds2 = new ReportDataSource("dsBill", data);

            rpvPrintBill.LocalReport.ReportEmbeddedResource = "CanteenManager.Report.rpPrintBill.rdlc";
            rpvPrintBill.LocalReport.DataSources.Clear();
            rpvPrintBill.LocalReport.DataSources.Add(rds);
            rpvPrintBill.LocalReport.DataSources.Add(rds2);

            rpvPrintBill.RefreshReport();

            var dateCheckOut = Convert.ToDateTime(data.Rows[0][2]);
            rpvPrintBill.LocalReport.DisplayName = $"BillID_{billID}_" + dateCheckOut.ToString("ddMMyyy_hhmmss_tt");

        }
    }
}
