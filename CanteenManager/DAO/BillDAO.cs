using CanteenManager.DTO;
using CanteenManager.Interface;
using System.Data;
using Unity;

namespace CanteenManager.DAO
{
    public class BillDAO : IBillDAO
    {
        private static IBillDAO instance;

        public static IBillDAO Instance
        {
            get => instance ?? (instance = Config.Container.Resolve<IBillDAO>());
            private set => instance = value;
        }

        private BillDAO() { }

        /// <summary>
        /// Lấy số lượng hoá đơn dựa theo ngày truyền vào.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public int GetNumberBillByDate(DateTime fromDate, DateTime toDate)
        {
            string query = "SELECT COUNT(*) FROM Bill WHERE DateCheckIn >= @fromDate AND DateCheckOut <= @toDate AND BillStatus = 1";

            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { fromDate, toDate });
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
            string query = "EXEC USP_GetListBillByDateAndPage @fromDate, @toDate, @pageNumber, @pageRow";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate, pageNumber, pageRow });
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
            string query = "EXEC USP_GetListBillByDate @fromDate, @toDate";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
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
            string query = "EXEC USP_InsertBill @tableID";

            var result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableID });         

            if (result == 1) return true;

            return false;

        }

        /// <summary>
        /// Thanh toán Bill dựa vào ID của nó.
        /// </summary>
        /// <param name="id"></param>
        public bool CheckOut(int billID, double totalPrice, int discount = 0)
        {
            string query = "EXEC USP_CheckOut @billID, @discount, @totalPrice";

            var result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billID, discount, totalPrice });

            if (result == 1) return true;

            return false;

        }

        /// <summary>
        /// Lấy tổng doanh thu (đã tính giảm giá) của từng tháng dựa theo ngày truyền vào
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetRevenueByMonth(DateTime fromDate, DateTime toDate)
        {
            string query = "EXEC USP_GetRevenueByMonth @fromDate, @toDate";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        }
    }
}
