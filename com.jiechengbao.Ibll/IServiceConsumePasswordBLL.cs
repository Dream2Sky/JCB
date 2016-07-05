using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IServiceConsumePasswordBLL
    {
        ServiceConsumePassword GetServicePassword();
        bool Update(ServiceConsumePassword scp);
    }
}
