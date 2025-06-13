using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class PurchaseOrder
    {
        public string domainCode { get; set; }
        public string orderType { get; set; }
        public string purchaseOrderNumber { get; set; }
        public DateTime orderDate { get; set; }
        public string supplierCode { get; set; }
        public string siteCode { get; set; }
        public string buyerCode { get; set; }
        public string currencyCode { get; set; }
        public string orderStatus { get; set; }
        public decimal orderTotal { get; set; }
        public decimal totalAmount { get; set; }
        public string remarks { get; set; }
        public string daybookSetCode { get; set; }
        public List<PurchaseOrderLine> purchaseOrderLines { get; set; }
    }
    public class PurchaseOrderLine
    {
        public int purchaseOrderLine { get; set; }
        public string description { get; set; }
        public string purchaseOrderType { get; set; }
        public string itemCode { get; set; }
        public string purchaseAcct { get; set; }
        public string purchaseCC { get; set; }
        public string purchaseSubAcct { get; set; }
        public decimal quantityOrdered { get; set; }
        public string unitOfMeasure { get; set; }
        public decimal purchaseCost { get; set; }
        public decimal totalAmount { get; set; }
        public string siteCode { get; set; }
        public string purchaseSiteCode { get; set; }
        public string taxEnvironment { get; set; }
        public string receiptType { get; set; }
    }

    
}