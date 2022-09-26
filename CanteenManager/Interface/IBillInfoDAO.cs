namespace CanteenManager.Interface
{
    public interface IBillInfoDAO
    {
        void InsertBillInfo(int billID, int foodID, int foodCount);
    }
}