using CanteenManager.DTO;
using System.Data;

namespace CanteenManager.Interface
{
    public interface IFoodDAO
    {
        List<Food> GetListFood();
        List<Food> GetListFoodByCategoryID(int categoryID);
        DataTable GetRevenueByFoodAndDate(DateTime fromDate, DateTime toDate);
        bool InsertFood(string name, int categoryID, double price);
        List<Food> SearchFood(string foodName, bool option);
        bool UpdateFood(int id, string name, int categoryID, double price, int foodStatus);
    }
}