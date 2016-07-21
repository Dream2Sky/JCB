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
    public class AppointmentServiceBLL:IAppointmentServiceBLL
    {
        private IAppointmentServiceDAL _appointmentServiceDAL;
        public AppointmentServiceBLL(IAppointmentServiceDAL appointmentServiceDAL)
        {
            _appointmentServiceDAL = appointmentServiceDAL;
        }

        public bool Add(AppointmentService appointmentService)
        {
            return _appointmentServiceDAL.Insert(appointmentService);
        }

        public IEnumerable<AppointmentService> GetAllAppointmentService()
        {
            return _appointmentServiceDAL.SelectAll();
        }

        public IEnumerable<AppointmentService> GetAllAppointmentServiceButNotDeleted()
        {
            return _appointmentServiceDAL.SelectAllButNotDeleted();
        }

        public AppointmentService GetByCode(string code)
        {
            return _appointmentServiceDAL.SelectByCode(code);
        }

        public AppointmentService GetById(Guid id)
        {
            return _appointmentServiceDAL.SelectById(id);
        }

        public bool IsExistByName(string name)
        {
            return _appointmentServiceDAL.IsExistByName(name);
        }

        public bool Update(AppointmentService appointmentService)
        {
            return _appointmentServiceDAL.Update(appointmentService);
        }
    }
}
