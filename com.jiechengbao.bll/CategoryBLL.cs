using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;

namespace com.jiechengbao.bll
{
    public class CategoryBLL:ICategoryBLL
    {
        private ICategoryDAL _categoryDAL;
        public CategoryBLL(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        /// <summary>
        /// 添加一个商品分类
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool Add(Category category)
        {
            return _categoryDAL.Insert(category);
        }

        /// <summary>
        /// 删除指定分类对象
        /// 未涉及到 GoodsCategory的部分逻辑尚未实现
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool Delete(Category category)
        {
            // category 的删除还涉及到 GoodsCategory的删除

            // 稍后再写这部分的逻辑

            return _categoryDAL.Delete(category);
        }

        /// <summary>
        /// 获取所有的分类
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetAllCategory()
        {
            return _categoryDAL.SelectAll();
        }

        /// <summary>
        /// 根据分类编号获得分类
        /// </summary>
        /// <param name="categoryNo"></param>
        /// <returns></returns>
        public Category GetCategoryByCategoryNo(string categoryNo)
        {
            return _categoryDAL.SelectByCategoryNo(categoryNo);
        }

        /// <summary>
        /// 根据categoryId 获取Category对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Category GetCategoryById(Guid Id)
        {
            return _categoryDAL.SelectById(Id);
        }

        public bool IsExist(string categoryName)
        {
            Category cate = _categoryDAL.SelectByCategoryName(categoryName);
            if (cate == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool Save(Category category)
        {
            return _categoryDAL.Update(category);
        }
    }
}
