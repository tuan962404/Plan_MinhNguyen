using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plan.Models
{
    public partial class Model_Codelist
    {
        public int No_ { get; set; }
        public string Injection_Code { get; set; }
        public Nullable<int> Machine_No_ { get; set; }
        public Nullable<int> Machine { get; set; }
        public Nullable<double> Cycle_Time { get; set; }
        public Nullable<int> Cavity { get; set; }
        public Nullable<double> Manpower { get; set; }
        public string Resin { get; set; }
        public string Resin_Code { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Another_Code { get; set; }
    }
}