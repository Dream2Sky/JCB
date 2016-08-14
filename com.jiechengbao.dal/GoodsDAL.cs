using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class GoodsDAL : DataBaseDAL<Goods>, IGoodsDAL
    {
        public IEnumerable<Goods> SelectByAllNoDeletedGoods()
        {
            try
            {
                return db.Set<Goods>().Where(n => n.IsDeleted == false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Goods SelectByCode(string code)
        {
            try
            {
                return db.Set<Goods>().SingleOrDefault(n => n.Code == code);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public IEnumerable<Goods> SelectByCondition(string condition)
        {
            try
            {
                return db.Set<Goods>().Where(n => n.Name.Contains(condition) || n.Code.Contains(condition));
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Goods SelectByName(string name)
        {
            try
            {
                return db.Set<Goods>().SingleOrDefault(n => n.Name == name);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 按照创建时间倒序排序 商品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Goods> SelectGoodsOrderByCreatedTime()
        {
            int count = db.Set<Goods>().Where(n => n.IsDeleted == false).Count();
            return SelectGoodsOrderByCreatedTime(count);
        }

        public IEnumerable<Goods> SelectGoodsOrderByCreatedTime(int count)
        {
            try
            {
                return db.Set<Goods>().Where(n=>n.IsDeleted == false).OrderByDescending(n => n.CreatedTime).Take(count);
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
