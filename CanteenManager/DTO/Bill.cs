using System.Data;

namespace CanteenManager.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime dateCheckIn, DateTime dateCheckOut, int tableID, int status, int discount = 0, double totalPrice = 0)
        {
            ID = id;
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            TableID = tableID;
            Status = status;
            Discount = discount;
            TotalPrice = totalPrice;
        }

        public Bill(DataRow row)
        {
            ID = Convert.ToInt32(row["ID"]);
            DateCheckIn = Convert.ToDateTime(row["DateCheckIn"]);
            //Dưới database, mặc định khi chạy proc thêm mới 1 Bill, DateCheckOut sẽ được gán là NULL => cần kiểm tra giá trị này khi xử lý trên C#
            DateCheckOut = row["DateCheckOut"] == DBNull.Value ? null : Convert.ToDateTime(row["DateCheckOut"]);
            TableID = (int)row["TableID"];
            Status = (int)row["BillStatus"];
            Discount = (int)row["Discount"];
            TotalPrice = (double)row["TotalPrice"];
        }


        private int iD;
        private DateTime dateCheckIn;
        private DateTime? dateCheckOut;
        private int tableID;
        private int status;
        private int discount;
        private double totalPrice;

        public int ID { get => iD; set => iD = value; }
        public DateTime DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int TableID { get => tableID; set => tableID = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
