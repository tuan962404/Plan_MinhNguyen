using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plan.Models
{
    public partial class Model_Stock
    {
        [Required(ErrorMessage = "Code không hợp lệ")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Số lượng không hợp lệ")]
        public Nullable<int> Stock { get; set; }
        public int STT { get; set; }
    }
}