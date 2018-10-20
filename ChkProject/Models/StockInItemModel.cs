using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace ChkProject.Models
{
    public class StockInItemModel
    {
        public StockInItemModel()
        {
             StockInItemList = new List<StockInItemModel>();
        }
        public List<StockInItemModel> StockInItemList { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int ProductId { get; set; }
        public bool ReportGenerated { get; set; }
        public long StockInItemId { get; set; }
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public decimal? TotalPrice { get; set; }
        public int ItemId { get; set; }
        public DateTime DateIn { get; set; }
        public int StockInLocation { get; set; }
        public Nullable<int> VendorId { get; set; }
        public decimal? Quantity { get; set; }
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