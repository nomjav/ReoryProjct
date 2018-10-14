using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class CompanyModel
    {
        public CompanyModel()
        {
            CompanyList = new List<CompanyModel>();
            CompanylocationList = new List<CompanyLocationModel>();
        }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyOwner { get; set; }
        public string CompanyLogoPath { get; set; }
        public string CompanyAddress { get; set; }
        public Nullable<int> LocationAllowed { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public List<CompanyLocationModel> CompanylocationList { get; set; }
        public List<CompanyModel> CompanyList { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }

    }
}