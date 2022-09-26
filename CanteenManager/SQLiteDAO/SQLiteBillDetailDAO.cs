using CanteenManager.DTO;
using CanteenManager.Interface;
using System.Data;

namespace CanteenManager.DAO
{
    public class SQLiteBillDetailDAO : IBillDetailDAO
    {
        //private static readonly SQLiteBillDetailDAO instance = new SQLiteBillDetailDAO();

        //public static SQLiteBillDetailDAO Instance => instance;

        private SQLiteBillDetailDAO() { }

        /// <summary>
        /// Lấy danh sách BillDetail dựa vào TableID của Bill.
        /// <br>Bill này phải là Bill chưa thanh toán (BillStatus = 0).</br>
        /// </summary>
        /// <param name="tableID">TableID của Bill.</param>
        /// <returns></returns>
        public List<BillDetail> GetListBillDetailByTableID(int tableID)
        {
            List<BillDetail> listBillDetail = new List<BillDetail>();

            string query = @"SELECT [Food].[Name] AS [FoodName],
                                    [FoodCategory].[Name] AS [CategoryName],
                                    [BillInfo].[FoodCount] AS [FoodCount],
                                    [Food].[Price] AS [Price],
                                    [FoodCount] * [Price] AS [TotalPrice]
                               FROM [Bill],
                                    [BillInfo],
                                    [Food],
                                    [FoodCategory]
                              WHERE [BillInfo].[BillID] = [Bill].[ID] AND 
                                    [BillInfo].[FoodID] = [Food].[ID] AND 
                                    [Food].[CategoryID] = [FoodCategory].[ID] AND 
                                    [Bill].[BillStatus] = 0 AND 
                                    [Bill].[TableID] = " + tableID;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                BillDetail billDetail = new BillDetail(row);

                listBillDetail.Add(billDetail);
            }

            return listBillDetail;
        }

        /// <summary>
        /// Lấy danh sách BillDetail dựa vào ID của Bill.
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<BillDetail> GetListBillDetailByBillID(int billID)
        {
            List<BillDetail> listBillDetail = new List<BillDetail>();

            string query = @"SELECT [Food].[Name] AS [FoodName],
                                    [FoodCategory].[Name] AS [CategoryName],
                                    [BillInfo].[FoodCount] AS [FoodCount],
                                    [Food].[Price] AS [Price],
                                    [FoodCount] * [Price] AS [TotalPrice]
                               FROM [BillInfo],
                                    [Food],
                                    [FoodCategory]
                              WHERE [BillInfo].[FoodID] = [Food].[ID] AND 
                                    [Food].[CategoryID] = [FoodCategory].[ID] AND 
                                    [BillInfo].[BillID] = " + billID;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                BillDetail billDetail = new BillDetail(row);

                listBillDetail.Add(billDetail);
            }

            return listBillDetail;
        }
    }
}
