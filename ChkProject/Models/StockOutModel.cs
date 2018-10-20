using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class StockOutModel
    {

        public StockOutModel()
        {
            StockOutModelList = new List<StockOutModel>();
            DDLProduct = new List<DDLProducts>();
            DDLCompanyLocation = new List<DDLCompanyLocation>();
        }
        public List<StockOutModel> StockOutModelList { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool ReportGenerated { get; set; }
        public List<StockOutModel> StockOutitemList { get; set; }
        public long StockOutId { get; set; }
        public int ProductId { get; set; }
        public System.DateTime DateOut { get; set; }
        public int StockOutLocation { get; set; }
        public Nullable<int> LocationTo { get; set; }
        public decimal? Quantity { get; set; }
        public string CompanyName { get; set; }
        public decimal? Price { get; set; }
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
        public List<StockOutModel> StockOutList { get; set; }

        public string ProductName { get; set; }
        public string LocationName { get; set; }
    }


}