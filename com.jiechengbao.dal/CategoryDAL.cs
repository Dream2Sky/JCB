using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class CategoryDAL : DataBaseDAL<Category>, ICategoryDAL
    {
        public IEnumerable<Category> SelectAllNotDeletedCategories()
        {
            try
            {
                return db.Set<Category>().Where(n => n.IsDeleted == false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Category SelectByCategoryName(string name)
        {
            try
            {
                return db.Set<Category>().SingleOrDefault(n => n.Name == name);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Category SelectByCategoryNo(string categoryNo)
        {
            try
            {
                return db.Set<Category>().SingleOrDefault(n => n.CategoryNO == categoryNo);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}
