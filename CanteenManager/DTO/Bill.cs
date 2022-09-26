using System.Data;

namespace CanteenManager.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime dateCheckIn, DateTime dateCheckOut, int tableID, int status, int discount = 0, double totalPrice = 0)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.TableID = tableID;
            this.Status = status;
            this.Discount = discount;
            this.TotalPrice = totalPrice;
        }

        public Bill(DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.DateCheckIn = Convert.ToDateTime(row["DateCheckIn"]);
            //Dưới database, mặc định khi chạy proc thêm mới 1 Bill, DateCheckOut sẽ được gán là NULL => cần kiểm tra giá trị này khi xử lý trên C#
            this.DateCheckOut = row["DateCheckOut"] == DBNull.Value ? null : Convert.ToDateTime(row["DateCheckOut"]);
            this.TableID = (int)row["TableID"];
            this.Status = (int)row["BillStatus"];
            this.Discount = (int)row["Discount"];
            this.TotalPrice = (double)row["TotalPrice"];
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
