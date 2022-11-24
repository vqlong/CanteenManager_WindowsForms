using Interfaces;
using Models;
using System.Data;

namespace SQLiteDataAccess
{
    public class FoodDAO : IFoodDAO
    {
        private FoodDAO() { }

        /// <summary>
        /// Lấy danh sách các Food dựa theo 1 CategoryID.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Food> GetListFoodByCategoryId(int categoryID)
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
            string query = $"INSERT INTO Food(Name, CategoryID, Price) VALUES( @name , @categoryID , @price ;";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {name, categoryID, price});
            
            if (result == 1) return true;

            return false;
        }

        public bool UpdateFood(int id, string name, int categoryID, double price, int foodStatus)
        {
            string query = @$"UPDATE [Food]
                                 SET [Name] = @name ,
                                     [CategoryID] = @categoryID ,
                                     [Price] = @price ,
                                     [FoodStatus] = @foodStatus 
                               WHERE [Food].[ID] = @id ;";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, categoryID, price, foodStatus, id });
            
            if (result == 1) return true;

            return false;
        }

        /// <summary>
        /// Tìm Food.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="option">True để bỏ qua phân biệt Unicode, ngược lại, False.</param>
        /// <returns></returns>
        public List<Food> SearchFood(string input, bool option = true)
        {
            List<Food> listFood = GetListFood();

            //Tìm kiếm không phân biệt unicode
            if (option)
            {
                listFood.RemoveAll(food => ConvertToUnsigned(food.Name.ToLower()).Contains(ConvertToUnsigned(input.ToLower().Trim())) == false);

                return listFood;

            }

            //Phân biệt unicode
            listFood.RemoveAll(food => food.Name.ToLower().Contains(input.ToLower().Trim()) == false);

            return listFood;

        }

        /// <summary>
        /// Lấy tổng doanh thu (chưa tính giảm giá) của từng món ăn dựa theo ngày truyền vào.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public object GetRevenueByFoodAndDate(DateTime fromDate, DateTime toDate)
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
        /// <param name="input"></param>
        /// <returns></returns>
        public string ConvertToUnsigned(string input)
        {
            string   signed = "ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ";
            string unsigned = "aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYY";

            for (int i = 0; i < input.Length; i++)
            {
                if (signed.Contains(input[i]))
                    input = input.Replace(input[i], unsigned[signed.IndexOf(input[i])]);
            }

            return input;
        }
    }
}
