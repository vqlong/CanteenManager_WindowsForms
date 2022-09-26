using System.Data;

namespace CanteenManager.DTO
{
    /// <summary>
    /// Tượng trưng cho bảng FoodCategory của database.
    /// </summary>
    public class Category
    {
        public Category(int id, string name, UsingState categoryStatus)
        {
            this.ID = id;
            this.Name = name;
            this.CategoryStatus = categoryStatus;
        }

        /// <summary>
        /// Khởi tạo đối tượng từ 1 hàng của bảng FoodCategory.
        /// </summary>
        /// <param name="row"></param>
        public Category(DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.Name = row["Name"].ToString();
            this.CategoryStatus = (UsingState)row["CategoryStatus"];
        }

        private int iD;
        private string name;
        private UsingState categoryStatus;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public UsingState CategoryStatus { get => categoryStatus; set => categoryStatus = value; }
    }
}
