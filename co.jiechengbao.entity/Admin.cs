using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class Admin:DataEntity
    {
        [MaxLength(50)]
        [Required]
        public string Account { get; set; }

        [MaxLength(50)]
        [Required]
        public string Password { get; set; }

    }
}
