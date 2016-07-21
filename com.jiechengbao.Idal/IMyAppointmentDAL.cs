using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IMyAppointmentDAL:IDataBaseDAL<MyAppointment>
    {
        IEnumerable<MyAppointment> SelectByPay(bool isPay);
        IEnumerable<MyAppointment> SelectByMemberIdAndPay(Guid memberId,bool isPay);
    }
}
