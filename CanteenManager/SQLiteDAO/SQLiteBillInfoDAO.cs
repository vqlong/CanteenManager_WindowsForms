using CanteenManager.Interface;

namespace CanteenManager.DAO
{
    public class SQLiteBillInfoDAO : IBillInfoDAO
    {
        //private static readonly SQLiteBillInfoDAO instance = new SQLiteBillInfoDAO();

        //public static SQLiteBillInfoDAO Instance => instance;

        private SQLiteBillInfoDAO() { }

        public void InsertBillInfo(int billID, int foodID, int foodCount)
        {
            //Kiểm tra xem cái Bill này đã thanh toán chưa, chưa thanh toán mới được thêm BillInfo vào
            var billStatus = Convert.ToInt32(DataProvider.Instance.ExecuteScalar($"SELECT BillStatus FROM Bill WHERE ID = {billID};"));
            if (billStatus == 1) return;

            //Kiểm tra xem cái BillInfo này đã có chưa
            var countBillInfo = Convert.ToInt32(DataProvider.Instance.ExecuteScalar($"SELECT COUNT(*) FROM BillInfo WHERE BillInfo.BillID = {billID} AND BillInfo.FoodID = {foodID};"));          
            
            //Nếu chưa có thì thêm mới
            if (countBillInfo == 0 && foodCount > 0)
            {
                DataProvider.Instance.ExecuteNonQuery($"INSERT INTO BillInfo(BillID, FoodID, FoodCount) VALUES({billID}, {foodID}, {foodCount});");
                return;
            }

            //Nếu có rồi thì update số lượng món đã gọi
            var currentFoodCount = Convert.ToInt32(DataProvider.Instance.ExecuteScalar($"SELECT FoodCount FROM BillInfo WHERE BillInfo.BillID = {billID} AND BillInfo.FoodID = {foodID};"));
            var newFoodCount = currentFoodCount + foodCount;
            
            //Theo thiết kế @foodCount truyền vào có thể âm, nếu @newFoodCount <= 0 thì xoá món đó khỏi hoá đơn
            if (newFoodCount <= 0)
            {
                DataProvider.Instance.ExecuteNonQuery($"DELETE FROM BillInfo WHERE BillInfo.BillID = {billID} AND BillInfo.FoodID = {foodID};");

                //Sau mỗi lần xoá BillInfo, đếm xem cái Bill này còn BillInfo nào không, nếu không còn cái nào thì xoá luôn Bill
                var count = Convert.ToInt32(DataProvider.Instance.ExecuteScalar($"SELECT COUNT(*) FROM BillInfo WHERE BillInfo.BillID = {billID};"));
                
                if (count == 0) DataProvider.Instance.ExecuteNonQuery($"DELETE FROM Bill WHERE ID = {billID};");

                return;
                
            }

            DataProvider.Instance.ExecuteNonQuery($"UPDATE BillInfo SET FoodCount = {newFoodCount} WHERE BillInfo.BillID = {billID} AND BillInfo.FoodID = {foodID};");
                
            
        }
    }
}
