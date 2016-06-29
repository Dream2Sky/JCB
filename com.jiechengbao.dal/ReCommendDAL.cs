using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class ReCommendDAL : DataBaseDAL<ReCommend>, IReCommendDAL
    {
        public ReCommend SelectByGoodsId(Guid goodsId)
        {
            try
            {
               return db.Set<ReCommend>().SingleOrDefault(n => n.GoodsId == goodsId);
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
