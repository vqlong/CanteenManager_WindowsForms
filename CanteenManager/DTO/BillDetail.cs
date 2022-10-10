using System.Data;

namespace CanteenManager.DTO
{
    /// <summary>
    /// Lớp trung gian, dùng để hiển thị thông tin của Bill lên ListView.
    /// <br>Mỗi đối tượng BillDetail là 1 hàng lấy từ bảng kết hợp của Bill, BillInfo, Food, FoodCategory.</br>
    /// <br>1 Bill có thể gồm nhiều BillInfo => Cần nhiều BillDetail để hiển thị đầy đủ 1 Bill.</br>
    /// </summary>
    public class BillDetail
    {
        public BillDetail(string foodName, string categoryName, int foodCount, float price, float totalPrice = 0)
        {
            FoodName = foodName;
            CategoryName = categoryName;
            FoodCount = foodCount;
            Price = price;
            TotalPrice = totalPrice;
        }

        /// <summary>
        /// Khởi tạo đối tượng từ 1 hàng của các bảng Bill, BillInfo, Food, FoodCategory.
        /// </summary>
        /// <param name="row"></param>
        public BillDetail(DataRow row)
        {
            FoodName = row["FoodName"].ToString();
            CategoryName = row["CategoryName"].ToString();
            FoodCount = (int)row["FoodCount"];
            //Kiểu float trong C# phải có hậu tố "f" mà trong SQL thì không => ép về double luôn cho đỡ lằng nhằng
            Price = (double)row["Price"];
            TotalPrice = (double)row["TotalPrice"];
        }

        private string foodName;
        private string categoryName;
        private int foodCount;
        private double price;
        private double totalPrice;

        public string FoodName { get => foodName; set => foodName = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public int FoodCount { get => foodCount; set => foodCount = value; }
        public double Price { get => price; set => price = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
