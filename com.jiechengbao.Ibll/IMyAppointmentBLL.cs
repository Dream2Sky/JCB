using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IMyAppointmentBLL
    {
        IEnumerable<MyAppointment> GetAllHasPayAppointment();
        IEnumerable<MyAppointment> GetAllNoPayAppointment();
        MyAppointment GetById(Guid Id);
        bool Update(MyAppointment myAppointment);
        IEnumerable<MyAppointment> GetHasPayAppointmentByMemberId(Guid memberId);
        IEnumerable<MyAppointment> GetNoPayAppointmentByMemberId(Guid memberId);
    }
}
