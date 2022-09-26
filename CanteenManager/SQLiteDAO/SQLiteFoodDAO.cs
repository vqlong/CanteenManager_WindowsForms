using CanteenManager.DTO;
using CanteenManager.Interface;
using System.Data;

namespace CanteenManager.DAO
{
    public class SQLiteFoodDAO : IFoodDAO
    {
        private SQLiteFoodDAO() { }

        //private static readonly SQLiteFoodDAO instance = new SQLiteFoodDAO();

        //public static SQLiteFoodDAO Instance => instance;

        /// <summary>
        /// Lấy danh sách các Food dựa theo 1 CategoryID.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Food> GetListFoodByCategoryID(int categoryID)
        {
            List<Food> listFood = new List<Food>();

            string query = "select * from Food where FoodStatus = 1 and CategoryID = " + categoryID;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);

                listFood.Add(food);
            }

            return listFood;
        }

        /// <summary>
        /// Lấy danh sách các Food.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Food> GetListFood()
        {
            List<Food> listFood = new List<Food>();

            string query = "SELECT * FROM Food ORDER BY CategoryID ASC";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);

                listFood.Add(food);
            }

            return listFood;
        }

        public bool InsertFood(string name, int categoryID, double price)
        {
            string query = $"INSERT INTO Food(Name, CategoryID, Price) VALUES('{name}', {categoryID}, {price});";

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            
            if (result == 1) return true;

            return false;
        }

        public bool UpdateFood(int id, string name, int categoryID, double price, int foodStatus)
        {
            string query = @$"UPDATE [Food]
                                 SET [Name] = '{name}',
                                     [CategoryID] = {categoryID},
                                     [Price] = {price},
                                     [FoodStatus] = {foodStatus}
                               WHERE [Food].[ID] = {id};";

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            
            if (result == 1) return true;

            return false;
        }

        /// <summary>
        /// Tìm Food.
        /// </summary>
        /// <param name="foodName"></param>
        /// <param name="option">True để bỏ qua phân biệt Unicode, ngược lại, False.</param>
        /// <returns></returns>
        public List<Food> SearchFood(string foodName, bool option = true)
        {
            List<Food> listFood = GetListFood();

            //Tìm kiếm không phân biệt unicode
            if (option)
            {
                listFood.RemoveAll(food => ConvertToUnsigned(food.Name.ToLower()).Contains(ConvertToUnsigned(foodName.ToLower().Trim())) == false);

                return listFood;

            }

            //Phân biệt unicode
            listFood.RemoveAll(food => food.Name.ToLower().Contains(foodName.ToLower().Trim()) == false);

            return listFood;

            ////Tìm kiếm bình thường
            //string query = $"SELECT * FROM Food WHERE tolower(Name) LIKE '%{foodName.ToLower().Trim()}%' ORDER BY CategoryID ASC";

            //DataTable data = DataProvider.Instance.ExecuteQuery(query);

            //foreach (DataRow row in data.Rows)
            //{
            //    Food food = new Food(row);

            //    listFood.Add(food);
            //}

            //return listFood;
        }

        /// <summary>
        /// Lấy tổng doanh thu (chưa tính giảm giá) của từng món ăn dựa theo ngày truyền vào.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetRevenueByFoodAndDate(DateTime fromDate, DateTime toDate)
        {
            string query = @$"WITH [temp] AS (
                            SELECT [BillInfo].[ID],
                                   [BillID],
                                   [FoodID],
                                   [Name],
                                   [FoodCount],
                                   [Price],
                                   [FoodCount] * [Price] AS [TotalPrice],
                                   [DateCheckIn],
                                   [DateCheckOut]
                              FROM [BillInfo]
                              JOIN [Food] 
                                ON [Food].[ID] = [BillInfo].[FoodID]
                              JOIN [Bill] 
                                ON [Bill].[ID] = [BillInfo].[BillID] AND 
                                   [BillStatus] = 1 AND 
                                   [DateCheckIn] >= '{fromDate.ToString("o")}' AND 
                                   [DateCheckOut] <= '{toDate.ToString("o")}'
                                    )
                            SELECT [temp].[Name],
                                   SUM([temp].[FoodCount]) AS [TotalFoodCount],
                                   SUM([temp].[TotalPrice]) AS [Revenue]
                              FROM [temp]
                          GROUP BY [temp].[Name];";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Chuyển chuỗi sang dạng không dấu.
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public string ConvertToUnsigned(string strInput)
        {
            string   signedStr = "ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ";
            string unsignedStr = "aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYY";

            for (int i = 0; i < strInput.Length; i++)
            {
                if (signedStr.Contains(strInput[i]))
                    strInput = strInput.Replace(strInput[i], unsignedStr[signedStr.IndexOf(strInput[i])]);
            }

            return strInput;
        }
    }
}
