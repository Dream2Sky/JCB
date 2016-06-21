using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IGoodsBLL
    {
        IEnumerable<Goods> GetAllNoDeteledGoods();
        bool Add(Goods goods);
    }
}
