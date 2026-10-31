using Interfaces;
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

        public void InsertBillInfo(int billId, int foodId, int foodCount)
        {
            string query = "EXEC USP_InsertBillInfo @billId, @foodId, @foodCount";

            DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId, foodId, foodCount });
        }
    }
}
