using CanteenManager.Interface;
using Unity;

namespace CanteenManager.DAO
{
    public class BillInfoDAO : IBillInfoDAO
    {
        private static IBillInfoDAO instance;

        public static IBillInfoDAO Instance
        {
            get => instance ?? (instance = Config.Container.Resolve<IBillInfoDAO>());
            private set => instance = value;
        }

        private BillInfoDAO() { }

        public void InsertBillInfo(int billID, int foodID, int foodCount)
        {
            string query = "EXEC USP_InsertBillInfo @billID, @foodID, @foodCount";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { billID, foodID, foodCount });
        }
    }
}
