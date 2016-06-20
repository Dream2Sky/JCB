using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class GoodsCategory:DataEntity
    {
        [Required]
        public Guid GoodsId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
