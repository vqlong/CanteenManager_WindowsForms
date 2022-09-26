using System.Data;

namespace CanteenManager.DTO
{
    /// <summary>
    /// Tượng trưng cho bảng TableFood của database.
    /// </summary>
    public class Table
    {
        public Table() { }

        public Table(int id, string name, string status, UsingState usingState)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
            this.UsingState = usingState;
        }

        /// <summary>
        /// Khởi tạo đối tượng từ 1 hàng của bảng TableFood.
        /// </summary>
        /// <param name="row"></param>
        public Table(DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.Name = row["Name"].ToString();
            this.Status = row["TableStatus"].ToString();
            this.UsingState = (UsingState)row["UsingState"];
        }

        private int iD;
        public int ID { get => iD; set => iD = value; }

        private string name;
        public string Name { get => name; set => name = value; }

        private string status;
        public string Status { get => status; set => status = value; }
        public UsingState UsingState { get => usingState; set => usingState = value; }

        private UsingState usingState;


    }
}
