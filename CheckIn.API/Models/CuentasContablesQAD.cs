using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class CuentasContablesQAD
    {
        public Dsgl dsgl { get; set; }
    }
    public class GlItem
    {
        public string t_acct { get; set; }
        public string t_desc { get; set; }
        public bool t_active { get; set; }
        public string t_parent_code { get; set; }
        public string t_level_ind { get; set; }
        public DateTime t_create_date { get; set; }
        public DateTime t_update_date { get; set; }
        public decimal t_balance { get; set; }
        public decimal t_balance_usd { get; set; }
        public string t_domain { get; set; }
        public string t_environment { get; set; }
    }

    public class Dsgl
    {
        public List<GlItem> ttgl { get; set; }
    }
}