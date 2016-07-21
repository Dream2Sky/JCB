using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IAppointmentTimeBLL
    {
        /// <summary>
        /// 获取最新的时间段列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<AppointmentTime> GetLastAppointmentTimeList();

        bool Add(AppointmentTime at);

        AppointmentTime GetById(Guid Id);
        bool Update(AppointmentTime at);
    }
}
