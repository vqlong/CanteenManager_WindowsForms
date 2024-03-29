﻿using Interfaces;
using Models;
using System.Data;
using Unity;

namespace CanteenManager.DAO
{
    /// <summary>
    /// Chứa các thao tác xử lý với TableFood. 
    /// </summary>
    internal class TableDAO : ITableDAO
    {
        private static ITableDAO instance;

        public static ITableDAO Instance
        {
            get => instance ?? (instance = Config.Container.Resolve<ITableDAO>());
            private set => instance = value;
        }

        private TableDAO() { }

        /// <summary>
        /// Lấy những bàn có thể được sử dụng từ database để tạo các đối tượng Table và đưa vào danh sách. 
        /// </summary>
        /// <returns></returns>
        public List<Table> GetListTableUsing()
        {
            string query = "USP_GetListTableUsing";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<Table> listTable = new List<Table>();

            foreach (DataRow row in data.Rows)
            {
                Table table = new Table(row);

                listTable.Add(table);
            }

            return listTable;
        }

        /// <summary>
        /// Lấy tất cả các bàn hiện có trong database.
        /// </summary>
        /// <returns></returns>
        public List<Table> GetListTable()
        {
            string query = "USP_GetListTable";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<Table> listTable = new List<Table>();

            foreach (DataRow row in data.Rows)
            {
                Table table = new Table(row);

                listTable.Add(table);
            }

            return listTable;
        }

        /// <summary>
        /// Tạo 1 đối tượng Table dựa vào ID.
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Table GetTableById(int tableId)
        {
            string query = "SELECT * FROM TableFood WHERE ID = " + tableId;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            Table table = new Table(data.Rows[0]);

            return table;
        }

        /// <summary>
        /// Chuyển bàn, bằng cách tráo đổi TableID của 2 Bill trên mỗi bàn.
        /// </summary>
        /// <param name="firstTableId"></param>
        /// <param name="secondTableId"></param>
        public bool SwapTable(int firstTableId, int secondTableId)
        {
            string query = "EXEC USP_SwapTable @firstTableId, @secondTableId";

            var result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { firstTableId, secondTableId });

            if (result > 0) return true;

            return false;
        }

        /// <summary>
        /// Gộp bàn, bằng cách chuyển các món ăn trên bàn này vào bàn kia.
        /// </summary>
        /// <param name="firstTableId"></param>
        /// <param name="secondTableId"></param>
        public bool CombineTable(int firstTableId, int secondTableId)
        {
            string query = "EXEC USP_CombineTable @firstTableId, @secondTableId";

            var result = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { firstTableId, secondTableId });

            if (result == 1) return true;

            return false;
        }

        public bool InsertTable()
        {
            string query = $"EXEC USP_InsertTable";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result == 1) return true;

            return false;
        }

        public bool UpdateTable(int id, string name, int usingState)
        {
            string query = $"UPDATE TableFood SET Name = @name , UsingState = @usingState WHERE Id = @id";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, usingState, id });

            if (result == 1) return true;

            return false;
        }
    }
}
