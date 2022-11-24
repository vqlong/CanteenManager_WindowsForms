using Interfaces;
using Models;
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
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Trả về 1 tài khoản nếu đăng nhập và mật khẩu trùng khớp, ngược lại trả về null.</returns>
        public Account Login(string username, string password)
        {
            string query = "EXEC USP_Login @Username, @Password";

            //Chuyển passWord thành mảng byte
            byte[] bytePassword = Encoding.ASCII.GetBytes(password);

            //Tạo hash
            byte[] sha256Password = SHA256.Create().ComputeHash(bytePassword);

            string hashString = "";
            foreach (var item in sha256Password)
            {
                hashString += Convert.ToString(item);
            }

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username, hashString });

            if (data.Rows.Count == 1)
            {
                return new Account(data.Rows[0]);
            }

            return null;
        }

        /// <summary>
        /// Update Account.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="displayname"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Item1 - Kết quả cập nhật DisplayName.
        /// <br>Item2 - Kết quả cập nhật Password.</br>
        /// <br>Item3 - Kết quả cập nhật Type.</br>
        /// </returns>
        public (bool, bool, bool) Update(string username, string displayname = null, string password = null, int? type = null)
        {
            string query = "EXEC USP_UpdateAccount @username, @displayname, @password, @type";

            if (!string.IsNullOrEmpty(password))
            {
                byte[] bytePassword = Encoding.ASCII.GetBytes(password);

                byte[] sha256Password = SHA256.Create().ComputeHash(bytePassword);

                password = "";
                foreach (var item in sha256Password)
                {
                    password += Convert.ToString(item);
                }
            }

            var result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, displayname, password, type });


            bool item1 = false, item2 = false, item3 = false;
            if ((int)result.Rows[0]["ResultDisplayName"] == 1) item1 = true;
            if ((int)result.Rows[0]["ResultPassword"] == 1) item2 = true;
            if ((int)result.Rows[0]["ResultType"] == 1) item3 = true;

            return (item1, item2, item3);
        }

        public bool InsertAccount(string username)
        {
            string query = $"SELECT COUNT(*) FROM Account WHERE Username = @username";

            int result = (int)DataProvider.Instance.ExecuteScalar(query, new object[] {username});

            if (result > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            query = $"INSERT Account(Username) VALUES( @username )";

            result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { username });

            if (result == 1) return true;

            return false;
        }

        public bool DeleteAccount(string username)
        {
            string query = $"DELETE Account WHERE Username = @username";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { username });

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
