using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class StockInItemModel
    {
        public long StockInItemId { get; set; }
        public int ItemId { get; set; }
        public System.DateTime DateIn { get; set; }
        public int StockInLocation { get; set; }
        public Nullable<int> VendorId { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}