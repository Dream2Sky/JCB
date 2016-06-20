using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface ICategoryBLL
    {
        IEnumerable<Category> GetAllCategory();
        bool Add(Category category);
        bool IsExist(string categoryName);
        Category GetCategoryByCategoryNo(string categoryNo);
        bool Delete(Category category);
        bool Save(Category category);
    }
}
