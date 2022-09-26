using CanteenManager.DTO;

namespace CanteenManager.Interface
{
    public interface ICategoryDAO
    {
        Category GetCategoryByID(int categoryID);
        List<Category> GetListCategory();
        List<Category> GetListCategoryServing();
        bool InsertCategory(string name);
        bool UpdateCategory(int id, string name, int categoryStatus);
    }
}