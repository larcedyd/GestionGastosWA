using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class CentroCostos
    {
        public Dscc dscc { get; set; }
    }
    public class TcCc
    {
        public string tc_cc { get; set; }
        public string tc_domain { get; set; }
        public string tc_description { get; set; }
        public string tc_parent_code { get; set; }
        public string tc_level_ind { get; set; }
        public bool tc_active { get; set; }
        public DateTime tc_create_date { get; set; }
        public DateTime tc_update_date { get; set; }
        public string tc_environment { get; set; }
    }

    public class Dscc
    {
        public List<TcCc> ttcc { get; set; }
    }

     
}