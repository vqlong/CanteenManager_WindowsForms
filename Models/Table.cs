using System.Data;

namespace Models
{
    /// <summary>
    /// Bảng TableFood của database.
    /// </summary>
    public class Table
    {
        public Table() { }

        public Table(int id, string name, string status, UsingState usingState)
        {
            Id = id;
            Name = name;
            Status = status;
            UsingState = usingState;
        }

        /// <summary>
        /// Khởi tạo đối tượng từ 1 hàng của bảng TableFood.
        /// </summary>
        /// <param name="row"></param>
        public Table(DataRow row)
        {
            Id = Convert.ToInt32(row["Id"]);
            Name = row["Name"].ToString();
            Status = row["TableStatus"].ToString();
            UsingState = (UsingState)row["UsingState"];
        }

        private int id;
        public int Id { get => id; set => id = value; }

        private string name;
        public string Name { get => name; set => name = value; }

        private string status = "Trống";
        public string Status { get => status; set => status = value; } 
        public UsingState UsingState { get => usingState; set => usingState = value; }

        private UsingState usingState;

        public virtual ICollection<Bill> Bills { get; set; } = new HashSet<Bill>();
    }
}
