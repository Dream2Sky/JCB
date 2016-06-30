using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface ICarDAL:IDataBaseDAL<Car>
    {
        IEnumerable<Car> SelectByMemberId(Guid memberId);
        bool IsExist(string numberplate);
        Car SelectByNumberPlate(string numberplate);
    }
}
