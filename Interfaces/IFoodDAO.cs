using Models;
using System.Data;

namespace Interfaces
{
    public interface IFoodDAO
    {
        List<Food> GetListFood();
        List<Food> GetListFoodByCategoryId(int categoryId);
        object GetRevenueByFoodAndDate(DateTime fromDate, DateTime toDate);
        bool InsertFood(string name, int categoryId, double price);
        List<Food> SearchFood(string input, bool option);
        bool UpdateFood(int id, string name, int categoryId, double price, int foodStatus);
    }
}