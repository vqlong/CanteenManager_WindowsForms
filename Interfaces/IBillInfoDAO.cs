namespace Interfaces
{
    public interface IBillInfoDAO
    {
        void InsertBillInfo(int billId, int foodId, int foodCount);
    }
}