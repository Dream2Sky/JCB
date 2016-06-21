using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;
using com.jiechengbao.Idal;

namespace com.jiechengbao.bll
{
    public class GoodsImagesBLL : IGoodsImagesBLL
    {
        private IGoodsImagesDAL _goodsImagesDAL;
        public GoodsImagesBLL(IGoodsImagesDAL goodsImagesDAL)
        {
            _goodsImagesDAL = goodsImagesDAL;
        }
        public bool Add(GoodsImage gi)
        {
            return _goodsImagesDAL.Insert(gi);
        }

        /// <summary>
        /// 根据 商品id 获取对应的 GoodsImage对象
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public GoodsImage GetPictureByGoodsId(Guid goodsId)
        {
            return _goodsImagesDAL.SelectByGoodsId(goodsId);
        }

        /// <summary>
        /// 更新GoodsImages
        /// </summary>
        /// <param name="gi"></param>
        /// <returns></returns>
        public bool Update(GoodsImage gi)
        {
            return _goodsImagesDAL.Update(gi);
        }
    }
}
