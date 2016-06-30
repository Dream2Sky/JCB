using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public class CarNotEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public bool IsDeleted { get; set; }

        public Guid MemberId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string Numberplate { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string ChassisNumber { get; set; }

        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNumber { get; set; }

        /// <summary>
        /// 汽车其他信息
        /// </summary>
        public string CarDetailInfo { get; set; }
    }
}
