using CanteenManager.DTO;

namespace CanteenManager.Interface
{
    internal interface ITableDAO
    {
        bool CombineTable(int firstTableID, int secondTableID);
        List<Table> GetListTable();
        List<Table> GetListTableUsing();
        Table GetTableByID(int tableID);
        bool InsertTable();
        bool SwapTable(int firstTableID, int secondTableID);
        bool UpdateTable(int id, string name, int usingState);
    }
}