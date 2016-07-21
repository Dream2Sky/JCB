using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;

namespace com.jiechengbao.bll
{
    public class AppointmentTimeBLL:IAppointmentTimeBLL
    {
        private IAppointmentTimeDAL _appointmentTimeDAL;
        public AppointmentTimeBLL(IAppointmentTimeDAL appointmentTimeDAL)
        {
            _appointmentTimeDAL = appointmentTimeDAL;
        }

        public bool Add(AppointmentTime at)
        {
            return _appointmentTimeDAL.Insert(at);
        }

        public AppointmentTime GetById(Guid Id)
        {
            return _appointmentTimeDAL.SelectById(Id);
        }

        public IEnumerable<AppointmentTime> GetLastAppointmentTimeList()
        {
            return _appointmentTimeDAL.SelectAppointmentTimeLastDay();
        }

        public bool Update(AppointmentTime at)
        {
            return _appointmentTimeDAL.Update(at);
        }
    }
}
