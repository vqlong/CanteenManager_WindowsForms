using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDataAccess
{
    public class CategoryDAO : Interfaces.ICategoryDAO
    {
        private CategoryDAO() { }

        public Category GetCategoryById(int categoryId)
        {
            using var context = new CanteenContext();

            return context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public List<Category> GetListCategory()
        {
            using var context = new CanteenContext();

            return context.Categories.ToList();
        }

        public List<Category> GetListCategoryServing()
        {
            using var context = new CanteenContext();

            return context.Categories.Where(c => c.CategoryStatus == UsingState.Serving).ToList();
        }

        public bool InsertCategory(string name)
        {
            using var context = new CanteenContext();
            context.Categories.Add(new Category { Name = name });
            if (context.SaveChanges() == 1) return true;
            return false;
        }

        public bool UpdateCategory(int id, string name, int categoryStatus)
        {
            using var context = new CanteenContext();
            context.Categories.Update(new Category { Id = id, Name = name, CategoryStatus = (UsingState)categoryStatus });
            if (context.SaveChanges() == 1) return true;
            return false;
        }
    }
}
