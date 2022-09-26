using CanteenManager.DTO;

namespace CanteenManager.Interface
{
    public interface IBillDetailDAO
    {
        List<BillDetail> GetListBillDetailByBillID(int billID);
        List<BillDetail> GetListBillDetailByTableID(int tableID);
    }
}