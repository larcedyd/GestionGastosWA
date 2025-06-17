using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models.ModelCliente
{
    [Table("ParametrosQAD")]
    public partial class ParametrosQAD
    {
        public int id { get; set; }

        public string UrlTokenQAD { get; set; }

        public string UrlCentroCostosQAD { get; set; }

        public string UrlCuentasContablesQAD { get; set; }

        public string UrlProveedoresQAD { get; set; }

        public string UrlEnviarAsientoQAD { get; set; }
        public string urlNormasRepartoQAD { get; set; }
        public string domainCode { get; set; }
        public string siteCode { get; set; }
        public string buyerCode { get; set; }
        public string orderStatus { get; set; }
        public string daybookSetCode { get; set; }

        public string purchaseSiteCode { get; set; }
        public string taxEnvironment { get; set; }
    }
}