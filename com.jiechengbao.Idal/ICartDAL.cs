using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface ICartDAL:IDataBaseDAL<Cart>
    {
        bool IsInCart(Guid memberId, Guid goodsId);

        Cart SelectByMemberIdAndGoodsId(Guid memberId, Guid goodsId);
        bool IsAnythingsInCart(Guid memberId);
        IEnumerable<Cart> SelectByMemberId(Guid memberId);
    }
}
