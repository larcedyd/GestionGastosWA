using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class NormasRepartoQAD
    {
        public Dssub dssub { get; set; }
    }
    public class SubItem
    {
        public string ts_sub { get; set; }
        public string ts_domain { get; set; }
        public string ts_description { get; set; }
        public string ts_parent_code { get; set; }
        public string ts_level_ind { get; set; }
        public bool ts_active { get; set; }
        public DateTime ts_create_date { get; set; }
        public DateTime ts_update_date { get; set; }
    }

    public class Dssub
    {
        public List<SubItem> ttsub { get; set; }
    }
}