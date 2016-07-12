using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IExchangeServiceDAL:IDataBaseDAL<ExchangeService>
    {
        ExchangeService selectByCode(string code);
        IEnumerable<ExchangeService> SelectByAnyCondition(string condition);
        IEnumerable<ExchangeService> SelectByName(string name);
    }
}
