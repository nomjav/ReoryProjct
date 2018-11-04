using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class BillModel
    {
        public long BillId { get; set; }
        public string BuyerName { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public string Billnumber { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public StockOutModel StockoutDetail { get; set; }
        public string BillType { get; set; }


    }
}