using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plan.Models
{
    public partial class MUser
    {
        public int ID { get; set; }
        public string User1 { get; set; }
        [StringLength(32, ErrorMessage = "Mật khẩu phải nhập tối thiểu {2} kí tự và tối đa {1} kí tự!", MinimumLength = 6)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string BoPhan { get; set; }
        public Nullable<int> NumberPhone { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}