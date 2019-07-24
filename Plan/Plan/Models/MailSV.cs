using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plan.Models
{
    public partial class MailSV
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CfmPassword { get; set; }
    }
}