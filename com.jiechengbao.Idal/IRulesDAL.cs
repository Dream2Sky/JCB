using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IRulesDAL:IDataBaseDAL<Rules>
    {
        Rules SelectByVIP(int VIP);
        bool Insert(List<Rules> rulesList);
    }
}
