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
    public class MyAppointmentBLL:IMyAppointmentBLL
    {
        private IMyAppointmentDAL _myAppointmentDAL;
        public MyAppointmentBLL(IMyAppointmentDAL myAppointmentDAL)
        {
            _myAppointmentDAL = myAppointmentDAL;
        }

        public IEnumerable<MyAppointment> GetAllHasPayAppointment()
        {
            return _myAppointmentDAL.SelectByPay(true);
        }

        public IEnumerable<MyAppointment> GetAllNoPayAppointment()
        {
            return _myAppointmentDAL.SelectByPay(false);
        }

        public IEnumerable<MyAppointment> GetHasPayAppointmentByMemberId(Guid memberId)
        {
            return _myAppointmentDAL.SelectByMemberIdAndPay(memberId,true);
        }

        public MyAppointment GetById(Guid Id)
        {
            return _myAppointmentDAL.SelectById(Id);
        }

        public bool Update(MyAppointment myAppointment)
        {
            return _myAppointmentDAL.Update(myAppointment);
        }

        public IEnumerable<MyAppointment> GetNoPayAppointmentByMemberId(Guid memberId)
        {
             return _myAppointmentDAL.SelectByMemberIdAndPay(memberId, false);
        }

        public bool Add(MyAppointment myappointment)
        {
            return _myAppointmentDAL.Insert(myappointment);
        }
    }
}
