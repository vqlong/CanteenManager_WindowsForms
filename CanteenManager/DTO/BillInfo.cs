using System.Data;

namespace CanteenManager.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int billID, int foodID, int foodCount)
        {
            ID = id;
            BillID = billID;
            FoodID = foodID;
            FoodCount = foodCount;
        }

        public BillInfo(DataRow row)
        {
            ID = (int)row["ID"];
            BillID = (int)row["BillID"];
            FoodID = (int)row["FoodID"];
            FoodCount = (int)row["FoodCount"];
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
