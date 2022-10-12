using CanteenManager.DTO;
using CanteenManager.Interface;
using System.Data;

namespace CanteenManager.DAO
{
    public class SQLiteBillDAO : IBillDAO
    {
        private SQLiteBillDAO() { }

        /// <summary>
        /// Lấy số lượng hoá đơn dựa theo ngày truyền vào.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public int GetNumberBillByDate(DateTime fromDate, DateTime toDate)
        {
            string query = @$"SELECT COUNT(*) 
                                FROM Bill 
                               WHERE strftime('%s', DateCheckIn) >= strftime('%s', '{fromDate.ToString("o")}') 
                                 AND strftime('%s', DateCheckOut) <= strftime('%s', '{toDate.ToString("o")}') 
                                 AND BillStatus = 1;";

            var result = DataProvider.Instance.ExecuteScalar(query);

            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Lấy danh sách các hoá đơn dựa theo ngày và số trang truyền vào.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageNumber">Page muốn lấy về.</param>
        /// <param name="pageRow">Số dòng 1 page.</param>
        /// <returns></returns>
        public DataTable GetDataBillByDateAndPage(DateTime fromDate, DateTime toDate, int pageNumber = 1, int pageRow = 10)
        {
            string query = @$"WITH [temp] 
                              AS 
                              (
                              SELECT [Bill].[ID],
                                     [Bill].[DateCheckIn] AS [Ngày phát sinh],
                                     [Bill].[DateCheckOut] AS [Ngày thanh toán],
                                     [TableFood].[Name] AS [Tên bàn],
                                     [Bill].[Discount] AS [Giảm giá (%)],
                                     [Bill].[TotalPrice] AS [Tiền thanh toán (Vnđ)]
                              FROM   [Bill],
                                     [TableFood]
                              WHERE  strftime('%s', [DateCheckIn]) >= strftime('%s', '{fromDate.ToString("o")}') 
                              AND    strftime('%s', [DateCheckOut]) <= strftime('%s', '{toDate.ToString("o")}') 
                              AND    [BillStatus] = 1 
                              AND    [TableFood].[ID] = [Bill].[TableID]
                              )
                              SELECT * FROM (SELECT * FROM [temp] LIMIT {pageRow} * {pageNumber})
                              EXCEPT
                              SELECT * FROM (SELECT * FROM [temp] LIMIT {pageRow} * {pageNumber - 1});";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy danh sách các hoá đơn dựa theo ngày truyền vào.
        /// </summary>
        /// <param name="fromDate">Từ ngày này.</param>
        /// <param name="toDate">Tới ngày này.</param>
        /// <returns>Bảng danh sách các hoá đơn.
        /// <br>Chú ý: Bảng này đã được thay đổi, không phải là bảng Bill nguyên bản.</br>
        /// </returns>
        public DataTable GetDataBillByDate(DateTime fromDate, DateTime toDate)
        {
            string query = @$"SELECT Bill.ID, 
                                     Bill.DateCheckIn AS [Ngày phát sinh], 
                                     Bill.DateCheckOut AS [Ngày thanh toán], 
                                     TableFood.Name AS [Tên bàn], 
                                     Bill.Discount AS [Giảm giá (%)], 
                                     Bill.TotalPrice AS [Tiền thanh toán (Vnđ)]    
                              FROM   Bill, TableFood 
                              WHERE  strftime('%s', DateCheckIn) >= strftime('%s', '{fromDate.ToString("o")}') 
                              AND    strftime('%s', DateCheckOut) <= strftime('%s', '{toDate.ToString("o")}') 
                              AND    BillStatus = 1 
                              AND    TableFood.ID = Bill.TableID";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy ID của Bill chưa thanh toán dựa vào ID của bàn mà nó phát sinh.
        /// <br>Thành công: Trả về ID của Bill. </br>
        /// <br>Thất bại: Trả về -1. </br>
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public int GetUnCheckBillIDByTableID(int tableID)
        {
            string query = "SELECT * FROM Bill WHERE Bill.BillStatus = 0 AND Bill.TableID = " + tableID;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }

            return -1;
        }

        /// <summary>
        /// Trả về 1 đối lượng Bill dựa vào ID nó.
        /// <br>Thành công: Trả 1 Bill. </br>
        /// <br>Thất bại: Trả về null. </br>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bill GetBillByID(int id)
        {
            string query = "SELECT * FROM Bill WHERE Bill.ID = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill;
            }

            return null;
        }

        /// <summary>
        /// Thêm 1 Bill mới vào bàn dựa theo ID của bàn.
        /// <br>Bill mới thêm sẽ bao gồm:</br>
        /// <br>--  ID: tự động thêm do ràng buộc IDENTITY</br>
        /// <br>--	DateCheckIn: luôn là ngày hôm nay</br>
        /// <br>--	DateCheckOut: luôn là NULL do hoá đơn mới tạo, chưa thanh toán</br>
        /// <br>--	TableID: ID của bàn phát sinh hoá đơn</br>
        /// <br>--	BillStatus: luôn là 0 - chưa thanh toán</br>
        /// </summary>
        /// <param name="tableID"></param>
        public bool InsertBill(int tableID)
        {
            string query = $"INSERT INTO Bill(TableID) VALUES({tableID});";

            var result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result == 1) return true;

            return false;

        }

        /// <summary>
        /// Thanh toán Bill dựa vào ID của nó.
        /// </summary>
        /// <param name="id"></param>
        public bool CheckOut(int billID, double totalPrice, int discount = 0)
        {
            string query = @$"UPDATE Bill 
                                 SET BillStatus = 1, DateCheckOut = strftime('%Y-%m-%d %H:%M:%f', 'now', 'localtime'), Discount = {discount}, TotalPrice = {totalPrice}  
                               WHERE ID = {billID};";

            var result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result == 1) return true;

            return false;

        }

        /// <summary>
        /// Lấy tổng doanh thu (đã tính giảm giá) của từng tháng dựa theo ngày truyền vào.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetRevenueByMonth(DateTime fromDate, DateTime toDate)
        {
            
            string query = @$"WITH temp AS
	                             (
		                    SELECT *, strftime('%m-%Y', DateCheckOut) AS [Month]
		                      FROM Bill 
		                     WHERE BillStatus = 1 
                               AND strftime('%s', [DateCheckIn]) >= strftime('%s', '{fromDate.ToString("o")}') 
                               AND strftime('%s', [DateCheckOut]) <= strftime('%s', '{toDate.ToString("o")}')	
	                             )
	                        SELECT temp.Month, SUM(temp.TotalPrice) AS [Revenue] 
                              FROM temp 
                          GROUP BY temp.Month;";

            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
