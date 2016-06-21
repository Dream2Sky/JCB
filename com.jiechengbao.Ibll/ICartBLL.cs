using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface ICartBLL
    {
        bool IsInCart(Guid memberId, Guid goodsId);
        bool Add(Cart cart);
        Cart GetCartByMemberIdAndGoodsId(Guid memberId, Guid goodsId);
        bool Update(Cart cart);
        bool IsAnythingsInCart(Guid memberId);
    }
}
