using CanteenManager.DTO;
using CanteenManager.Interface;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace CanteenManager.DAO
{
    public class SQLiteAccountDAO : IAccountDAO
    {
        //private static readonly SQLiteAccountDAO instance = new SQLiteAccountDAO();

        //public static SQLiteAccountDAO Instance => instance;

        private SQLiteAccountDAO() { }

        public bool DeleteAccount(string userName)
        {
            string query = $"DELETE FROM Account WHERE UserName = '{userName}'";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result == 1) return true;

            return false;
        }

        public List<Account> GetListAccount()
        {
            string query = "SELECT * FROM Account";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<Account> listAccount = new List<Account>();

            foreach (DataRow row in data.Rows)
            {
                Account account = new Account(row);

                listAccount.Add(account);
            }

            return listAccount;
        }

        public bool InsertAccount(string userName)
        {
            string query = $"SELECT COUNT(*) FROM Account WHERE UserName = '{userName}'";

            var result = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query));

            if (result > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            query = $"INSERT INTO Account(UserName) VALUES('{userName}')";

            result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result == 1) return true;

            return false;
        }

        public Account Login(string userName, string passWord)
        {
            string query = "SELECT UserName, DisplayName, AccType FROM Account WHERE UserName = @UserName AND PassWord = @PassWord";
           
            byte[] bytePassWord = ASCIIEncoding.ASCII.GetBytes(passWord);

            byte[] sha256PassWord = SHA256.Create().ComputeHash(bytePassWord);

            string hashStr = "";
            foreach (var item in sha256PassWord)
            {
                hashStr += Convert.ToString(item);
            }

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, hashStr });

            if (data.Rows.Count == 1)
            {
                return new Account(data.Rows[0]);
            }

            return null;
        }

        public bool Update(string userName, string displayName = null, string passWord = null, int? accType = null)
        {
            string query = "";

            if (displayName != null) 
                query += @$"UPDATE Account SET DisplayName = '{displayName}' WHERE UserName = '{userName}' AND '{displayName}' IS NOT NULL AND '{displayName}' != '';";
            
            if (!string.IsNullOrEmpty(passWord))
            {
                byte[] bytePassWord = ASCIIEncoding.ASCII.GetBytes(passWord);

                byte[] sha256PassWord = SHA256.Create().ComputeHash(bytePassWord);

                passWord = "";
                foreach (var item in sha256PassWord)
                {
                    passWord += Convert.ToString(item);
                }

                query += @$"UPDATE Account SET PassWord = '{passWord}' WHERE UserName = '{userName}' AND '{passWord}' IS NOT NULL AND '{passWord}' != '';";
            }

            if (accType != null)
                query += $@"UPDATE Account SET AccType = {accType} WHERE UserName = '{userName}' AND {accType} IS NOT NULL";
           

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            //result = 0: không update được cái nào
            //result = 1: chỉ update được DisplayName hoặc PassWord hoặc AccType
            //result = 2: update được 2 cái
            //result = 3: update được cả DisplayName và PassWord và AccType
            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
