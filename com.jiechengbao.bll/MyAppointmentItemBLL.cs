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
    public class MyAppointmentItemBLL:IMyAppointmentItemBLL
    {
        private IMyAppointmentItemDAL _myAppointmentItemDAL;
        public MyAppointmentItemBLL(IMyAppointmentItemDAL myAppointmentItemDAL)
        {
            _myAppointmentItemDAL = myAppointmentItemDAL;
        }

        public IEnumerable<MyAppointmentItem> GetByMyAppointmentId(Guid myappointmentId)
        {
            return _myAppointmentItemDAL.SelectByMyAppointmentId(myappointmentId);
        }
    }
}
