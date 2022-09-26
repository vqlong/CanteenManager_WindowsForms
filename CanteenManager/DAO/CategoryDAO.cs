using CanteenManager.DTO;
using CanteenManager.Interface;
using System.Data;
using Unity;

namespace CanteenManager.DAO
{
    /// <summary>
    /// Chứa các thao tác xử lý với FoodCategory.
    /// </summary>
    public class CategoryDAO : ICategoryDAO
    {
        private static ICategoryDAO instance;

        public static ICategoryDAO Instance
        {
            get => instance ?? (instance = Config.Container.Resolve<ICategoryDAO>());
            private set => instance = value;
        }

        private CategoryDAO() { }

        /// <summary>
        /// Lấy tất cả dữ liệu trong bảng FoodCategory từ database để tạo các đối tượng Category và đưa vào danh sách.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetListCategory()
        {
            List<Category> listCategory = new List<Category>();

            string query = "SELECT * FROM FoodCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Category category = new Category(row);

                listCategory.Add(category);
            }

            return listCategory;
        }

        /// <summary>
        /// Trả về list các Category có trạng thái là đang phục vụ (UsingState.Serving).
        /// </summary>
        /// <returns></returns>
        public List<Category> GetListCategoryServing()
        {
            List<Category> listCategory = new List<Category>();

            string query = "SELECT * FROM FoodCategory WHERE CategoryStatus = 1";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Category category = new Category(row);

                listCategory.Add(category);
            }

            return listCategory;
        }

        /// <summary>
        /// Tạo 1 đối tượng Category dựa vào ID.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category GetCategoryByID(int categoryID)
        {
            string query = "SELECT * FROM FoodCategory WHERE ID = " + categoryID;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            Category category = new Category(data.Rows[0]);

            return category;
        }

        public bool InsertCategory(string name)
        {
            string query = $"INSERT FoodCategory(Name) VALUES(N'{name}')";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result == 1) return true;

            return false;
        }

        public bool UpdateCategory(int id, string name, int categoryStatus)
        {
            string query = $"UPDATE FoodCategory SET Name = N'{name}', CategoryStatus = {categoryStatus} WHERE ID = {id}";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result == 1) return true;

            return false;
        }
    }
}
