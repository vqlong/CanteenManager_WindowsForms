using CanteenManager.DTO;
using System.Data;

namespace CanteenManager.Interface
{
    public interface IBillDAO
    {
        bool CheckOut(int billID, double totalPrice, int discount = 0);
        Bill GetBillByID(int id);
        DataTable GetDataBillByDate(DateTime fromDate, DateTime toDate);
        DataTable GetDataBillByDateAndPage(DateTime fromDate, DateTime toDate, int pageNumber = 1, int pageRow = 10);
        int GetNumberBillByDate(DateTime fromDate, DateTime toDate);
        DataTable GetRevenueByMonth(DateTime fromDate, DateTime toDate);
        int GetUnCheckBillIDByTableID(int tableID);
        bool InsertBill(int tableID);
    }
}