using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace co.jiechengbao.entity
{
    public class Category:DataEntity
    {
        [Required]
        [MaxLength(20)]
        public string CategoryNO { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
