using CanteenManager.DTO;
using CanteenManager.Interface;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Unity;

namespace CanteenManager.DAO
{
    /// <summary>
    /// Chứa các thao tác xử lý với Account.
    /// </summary>
    public class AccountDAO : IAccountDAO
    {
        private static IAccountDAO instance;

        public static IAccountDAO Instance
        {
            get => instance ?? (instance = Config.Container.Resolve<IAccountDAO>());
            private set => instance = value;
        }

        private AccountDAO() { }

        /// <summary>
        /// Trả về tài khoản dựa theo tên đăng nhập và mật khẩu nhập vào.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns>Trả về 1 tài khoản nếu đăng nhập và mật khẩu trùng khớp, ngược lại trả về null.</returns>
        public Account Login(string userName, string passWord)
        {
            ////câu lệnh luôn đúng khi nhập passWord là ' OR 1=1 --
            //string query = $"SELECT * FROM dbo.Account WHERE UserName = '{userName}' AND PassWordAcc = '{passWord}'";
            //DataTable result = DataProvider.Instance.ExecuteQuery(query);

            ////Vẫn dùng SELECT nhưng có tham số 
            //string query = $"SELECT * FROM dbo.Account WHERE UserName = @u AND PassWordAcc = @p";

            //Dùng Store Procedure với tham số
            string query = "EXEC USP_Login @UserName, @PassWord";

            //Chuyển passWord thành mảng byte
            //byte[] bytePassWord = Encoding.Unicode.GetBytes(passWord);
            //byte[] bytePassWord1 = Encoding.UTF32.GetBytes(passWord);
            //byte[] bytePassWord2 = Encoding.UTF8.GetBytes(passWord);
            byte[] bytePassWord = ASCIIEncoding.ASCII.GetBytes(passWord);

            //Tạo hash
            //byte[] md5PassWord = new MD5CryptoServiceProvider().ComputeHash(bytePassWord);
            //byte[] md5PassWord = MD5.Create().ComputeHash(bytePassWord);
            byte[] sha256PassWord = SHA256.Create().ComputeHash(bytePassWord);

            string hashStr = "";
            foreach (var item in sha256PassWord)
            {
                hashStr += Convert.ToString(item);
            }

            ////Chuyển về dạng chuỗi
            //string s = Encoding.ASCII.GetString(sha256PassWord);

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, hashStr });

            if (data.Rows.Count == 1)
            {
                return new Account(data.Rows[0]);
            }

            return null;
        }

        /// <summary>
        /// Update DisplayName và PassWord.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="displayName"></param>
        /// <param name="passWord"></param>
        /// <returns>true, nếu update thành công, ngược lại, false.</returns>
        public bool Update(string userName, string displayName = null, string passWord = null, int? accType = null)
        {
            string query = "EXEC USP_UpdateAccount @userName, @displayName, @passWord, @accType";

            if (!string.IsNullOrEmpty(passWord))
            {
                byte[] bytePassWord = ASCIIEncoding.ASCII.GetBytes(passWord);

                byte[] sha256PassWord = SHA256.Create().ComputeHash(bytePassWord);

                passWord = "";
                foreach (var item in sha256PassWord)
                {
                    passWord += Convert.ToString(item);
                }
            }

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, displayName, passWord, accType });

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

        public bool InsertAccount(string userName)
        {
            string query = $"SELECT COUNT(*) FROM Account WHERE UserName = N'{userName}'";

            int result = (int)DataProvider.Instance.ExecuteScalar(query);

            if (result > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            query = $"INSERT Account(UserName) VALUES(N'{userName}')";

            result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result == 1) return true;

            return false;
        }

        public bool DeleteAccount(string userName)
        {
            string query = $"DELETE Account WHERE UserName = N'{userName}'";

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
    }
}
