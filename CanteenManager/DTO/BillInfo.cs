using System.Data;

namespace CanteenManager.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int billID, int foodID, int foodCount)
        {
            this.ID = id;
            this.BillID = billID;
            this.FoodID = foodID;
            this.FoodCount = foodCount;
        }

        public BillInfo(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.BillID = (int)row["BillID"];
            this.FoodID = (int)row["FoodID"];
            this.FoodCount = (int)row["FoodCount"];
        }

        private int iD;
        private int billID;
        private int foodID;
        private int foodCount;

        public int ID { get => iD; set => iD = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int FoodCount { get => foodCount; set => foodCount = value; }
    }
}
