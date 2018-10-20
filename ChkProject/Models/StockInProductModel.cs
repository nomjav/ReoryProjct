using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class StockInProductModel
    {
        public StockInProductModel()
        {
            StockInProductList = new List<StockInProductModel>();
            DDLProduct = new List<DDLProducts>();
            DDLCompanyLocation = new List<DDLCompanyLocation>();
        }

        public long StockInId { get; set; }
        public int ProductId { get; set; }
        public System.DateTime DateIn { get; set; }
        public int StockInLocation { get; set; }
        public Nullable<int> LocationFrom { get; set; }
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
        public List<DDLProducts> DDLProduct { get; set; }
        public List<DDLCompanyLocation> DDLCompanyLocation { get; set; }
        public List<StockInProductModel> StockInProductList { get; set; }

        public string ProductName { get; set; }
        public string LocationName { get; set; }

    }


    public class DDLProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? CurrentQuantity { get; set; }
    }

    public class DDLCompanyLocation
    {
        public int CompanyLocationId { get; set; }
        public string LocationName { get; set; }
    }


}