using CanteenManager.DTO;
using CanteenManager.Interface;
using System.Data;
using Unity;

namespace CanteenManager.DAO
{
    /// <summary>
    /// Chứa các thao tác xử lý với Food.
    /// </summary>
    public class FoodDAO : IFoodDAO
    {
        private static IFoodDAO instance;

        public static IFoodDAO Instance
        {
            get => instance ?? (instance = Config.Container.Resolve<IFoodDAO>());
            private set => instance = value;
        }

        private FoodDAO() { }

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
            string query = "EXEC USP_InsertFood @name, @categoryID, @price";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, categoryID, price });

            if (result == 1) return true;

            return false;
        }

        public bool UpdateFood(int id, string name, int categoryID, double price, int foodStatus)
        {
            string query = "EXEC USP_UpdateFood @id, @name, @categoryID, @price, @foodStatus";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id, name, categoryID, price, foodStatus });

            if (result == 1) return true;

            return false;
        }

        /// <summary>
        /// Tìm Food.
        /// </summary>
        /// <param name="foodName"></param>
        /// <param name="option">True để bỏ qua phân biệt Unicode, ngược lại, False.</param>
        /// <returns></returns>
        public List<Food> SearchFood(string foodName, bool option)
        {
            List<Food> listFood = new List<Food>();

            string query = $"SELECT * FROM Food WHERE Name LIKE N'%{foodName}%' ORDER BY CategoryID ASC";

            if (option) query = $"SELECT * FROM Food WHERE dbo.UF_ConvertToUnsigned(Name) LIKE dbo.UF_ConvertToUnsigned(N'%{foodName}%') ORDER BY CategoryID ASC";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);

                listFood.Add(food);
            }

            return listFood;
        }

        /// <summary>
        /// Lấy tổng doanh thu (chưa tính giảm giá) của từng món ăn dựa theo ngày truyền vào.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetRevenueByFoodAndDate(DateTime fromDate, DateTime toDate)
        {
            string query = "EXEC USP_GetRevenueByFoodAndDate @fromDate, @toDate";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        }
    }
}
