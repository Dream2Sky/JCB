using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class GoodsImagesDAL : DataBaseDAL<GoodsImage>, IGoodsImagesDAL
    {
        public GoodsImage SelectByGoodsId(Guid goodsId)
        {
            try
            {
                return db.Set<GoodsImage>().SingleOrDefault(n => n.GoodsId == goodsId);
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
