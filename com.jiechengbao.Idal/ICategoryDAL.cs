using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface ICategoryDAL:IDataBaseDAL<Category>
    {
        Category SelectByCategoryName(string name);
        Category SelectByCategoryNo(string categoryNo);
        IEnumerable<Category> SelectAllNotDeletedCategories();
    }
}
