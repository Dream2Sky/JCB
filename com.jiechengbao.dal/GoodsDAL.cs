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
    }
}
