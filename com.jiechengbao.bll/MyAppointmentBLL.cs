using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.bll
{
    public class MyAppointmentBLL:IMyAppointmentBLL
    {
        private IMyAppointmentDAL _myAppointmentDAL;
        public MyAppointmentBLL(IMyAppointmentDAL myAppointmentDAL)
        {
            _myAppointmentDAL = myAppointmentDAL;
        }
    }
}
