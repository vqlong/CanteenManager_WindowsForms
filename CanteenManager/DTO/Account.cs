using System.Data;

namespace CanteenManager.DTO
{
    /// <summary>
    /// Tượng trưng cho bảng Account của database.
    /// </summary>
    public class Account
    {
        public Account(string userName, string displayName, AccountType accType, string passWord = null)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.AccType = accType;
            this.PassWord = passWord;
        }

        public Account(DataRow row)
        {
            this.UserName = row["UserName"].ToString();
            this.DisplayName = row["DisplayName"].ToString();
            this.AccType = (AccountType)row["AccType"];
            //this.PassWord = row["PassWord"].ToString();
        }

        private string userName;
        private string displayName;
        private string passWord;
        private AccountType accType;

        public string UserName { get => userName; set => userName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public AccountType AccType { get => accType; set => accType = value; }
    }

    public enum AccountType
    {
        Admin = 1,
        Staff = 0
    }
}
