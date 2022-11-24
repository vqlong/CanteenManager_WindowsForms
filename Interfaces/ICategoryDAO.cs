using Models;

namespace Interfaces
{
    public interface ICategoryDAO
    {
        Category GetCategoryById(int categoryId);
        List<Category> GetListCategory();
        List<Category> GetListCategoryServing();
        bool InsertCategory(string name);
        bool UpdateCategory(int id, string name, int categoryStatus);
    }
}