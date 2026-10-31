using Interfaces;
using Models;
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
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Food> GetListFoodByCategoryId(int categoryId)
        {
            List<Food> listFood = new List<Food>();

            string query = "select * from Food where FoodStatus = 1 and CategoryId = " + categoryId;

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
        /// <returns></returns>
        public List<Food> GetListFood()
        {
            List<Food> listFood = new List<Food>();

            string query = "SELECT * FROM Food ORDER BY CategoryId ASC";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);

                listFood.Add(food);
            }

            return listFood;
        }

        public bool InsertFood(string name, int categoryId, double price)
        {
            string query = "EXEC USP_InsertFood @name, @categoryId, @price";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, categoryId, price });

            if (result == 1) return true;

            return false;
        }

        public bool UpdateFood(int id, string name, int categoryId, double price, int foodStatus)
        {
            string query = "EXEC USP_UpdateFood @id, @name, @categoryId, @price, @foodStatus";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id, name, categoryId, price, foodStatus });

            if (result == 1) return true;

            return false;
        }

        /// <summary>
        /// Tìm Food.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="option">True để bỏ qua phân biệt Unicode, ngược lại, False.</param>
        /// <returns></returns>
        public List<Food> SearchFood(string input, bool option)
        {           
            string query = $"EXEC USP_SearchFood @foodName";

            if (option) query = $"EXEC USP_SearchFood @foodName , 1";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { input });

            List<Food> listFood = new List<Food>(data.Rows.Count);

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
        public object GetRevenueByFoodAndDate(DateTime fromDate, DateTime toDate)
        {
            string query = "EXEC USP_GetRevenueByFoodAndDate @fromDate, @toDate";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        }
    }
}
