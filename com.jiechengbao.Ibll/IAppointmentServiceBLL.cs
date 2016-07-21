using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IAppointmentServiceBLL
    {
        IEnumerable<AppointmentService> GetAllAppointmentService();
        bool IsExistByName(string name);

        bool Add(AppointmentService appointmentService);

        AppointmentService GetByCode(string code);

        bool Update(AppointmentService appointmentService);
        IEnumerable<AppointmentService> GetAllAppointmentServiceButNotDeleted();
    }
}
