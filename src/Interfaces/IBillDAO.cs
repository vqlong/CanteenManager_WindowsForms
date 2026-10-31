using Models;
using System.Data;

namespace Interfaces
{
    public interface IBillDAO
    {
        bool CheckOut(int billId, double totalPrice, int discount = 0);
        Bill GetBillById(int id);
        object GetDataBillByDate(DateTime fromDate, DateTime toDate);
        object GetDataBillByDateAndPage(DateTime fromDate, DateTime toDate, int pageNumber = 1, int pageSize = 10);
        int GetNumberBillByDate(DateTime fromDate, DateTime toDate);
        object GetRevenueByMonth(DateTime fromDate, DateTime toDate);
        int GetUnCheckBillIdByTableId(int tableId);
        bool InsertBill(int tableId);
    }
}