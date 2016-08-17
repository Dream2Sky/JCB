using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.jiechengbao.entity
{
    public class Advertisement:DataEntity
    {
        [Required]
        [MaxLength(35)]
        public string AdCode { get; set; }

        [Required]
        [MaxLength(30)]
        public string AdName { get; set; }

        [Required]
        [MaxLength(255)]
        public string AdDescription { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string AdImagePath { get; set; }

    }
}
