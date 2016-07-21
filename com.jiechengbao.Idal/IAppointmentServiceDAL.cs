using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IAppointmentServiceDAL:IDataBaseDAL<AppointmentService>
    {
        bool IsExistByName(string name);
        AppointmentService SelectByCode(string code);
        IEnumerable<AppointmentService> SelectAllButNotDeleted();
    }
}
