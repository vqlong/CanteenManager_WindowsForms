using CanteenManager.Interface;
using System.Data;
using System.Data.SqlClient;
using Unity;

namespace CanteenManager.DAO
{
    /// <summary>
    /// Chứa các phương thức làm việc với database của SQL Server.
    /// </summary>
    public class DataProvider : IDataProvider
    {
        //Áp dụng singleton cho class DataProvider
        private static IDataProvider instance;

        public static IDataProvider Instance
        {
            get => instance ?? (instance = Config.Container.Resolve<IDataProvider>());
            private set => instance = value;
        }

        private DataProvider() { }

        private string connectionStr = @"Data Source=.\sqlexpress;Initial Catalog=QuanLyCanteen;Integrated Security=True";

        /// <summary>
        /// Kiểm tra kết nối tới Server.
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="timeout"></param>
        /// <returns>
        /// true, nếu thành công, đồng thời gán chuỗi kết nối này cho biến connectionStr của DataProvider.
        /// <br>nếu thất bại, thông báo lỗi và trả về false.</br>
        /// </returns>
        public bool TestConnection(string connectionStr)
        {          

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT 1", connection);

                    if ((int)command.ExecuteScalar() == 1)
                    {
                        this.connectionStr = connectionStr;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Kết nối thất bại!\n" + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Trả về 1 bảng kết quả khi chạy câu truy vấn.
        /// </summary>
        /// <param name="query">Câu truy vấn.</param>
        /// <param name="parameter">Mảng các giá trị truyền cho các parameter @xyz... </param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    //cắt nhỏ câu truy vấn để lấy ra phần parameter dạng @abcxyz
                    string[] paraList = query.Split(' ');
                    int i = 0;
                    foreach (string item in paraList)
                    {
                        //nếu chứa '@' => nó là parameter
                        if (item.Contains('@'))
                        {
                            //xoá dấu "," sau đít tham số
                            string para = item.Replace(",", "");
                            //thêm vào danh sách tên parameter và giá trị tương ứng được truyền cho nó, nếu nó null thì truyền vào DBNull.Value
                            command.Parameters.AddWithValue(para, parameter[i] ?? DBNull.Value);
                            //chỉ tăng i mỗi khi tìm được tên 1 parameter
                            i++;
                        }
                    }

                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);

            }

            return data;
        }

        /// <summary>
        /// Trả về số số dòng áp dụng thành công khi chạy câu truy vấn.
        /// </summary>
        /// <param name="query">Câu truy vấn.</param>
        /// <param name="parameter">Mảng các giá trị truyền cho các parameter của store procedure
        /// (nếu câu truy vấn là thực thi 1 store procedure).</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            //tạo biến chứa số dòng áp dụng thành công khi chạy câu truy vấn
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    //cắt nhỏ câu truy vấn để lấy ra phần parameter dạng @abcxyz
                    string[] paraList = query.Split(' ');
                    int i = 0;
                    foreach (string item in paraList)
                    {
                        //nếu chứa '@' => nó là parameter
                        if (item.Contains('@'))
                        {
                            //Xoá dấu "," sau đít tham số
                            string para = item.Replace(",", "");
                            //thêm vào danh sách tên parameter và giá trị tương ứng được truyền cho nó, nếu nó null thì truyền vào DBNull.Value
                            command.Parameters.AddWithValue(para, parameter[i] ?? DBNull.Value);
                            //chỉ tăng i mỗi khi tìm được tên 1 parameter
                            i++;
                        }
                    }

                }

                data = command.ExecuteNonQuery();

            }

            return data;
        }

        /// <summary>
        /// Trả về cột đầu tiên của dòng đầu tiên trong bảng kết quả.
        /// </summary>
        /// <param name="query">Câu truy vấn.</param>
        /// <param name="parameter">Mảng các giá trị truyền cho các parameter của store procedure
        /// (nếu câu truy vấn là thực thi 1 store procedure).</param>
        /// <returns></returns>
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            //tạo biến chứa kết quả trả về
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    //cắt nhỏ câu truy vấn để lấy ra phần parameter dạng @abcxyz
                    string[] paraList = query.Split(' ');
                    int i = 0;
                    foreach (string item in paraList)
                    {
                        //nếu chứa '@' => nó là parameter
                        if (item.Contains('@'))
                        {
                            //Xoá dấu "," sau đít tham số
                            string para = item.Replace(",", "");
                            //thêm vào danh sách tên parameter và giá trị tương ứng được truyền cho nó, nếu nó null thì truyền vào DBNull.Value
                            command.Parameters.AddWithValue(para, parameter[i] ?? DBNull.Value);
                            //chỉ tăng i mỗi khi tìm được tên 1 parameter
                            i++;
                        }
                    }

                }

                data = command.ExecuteScalar();

            }

            return data;
        }

    }
}
