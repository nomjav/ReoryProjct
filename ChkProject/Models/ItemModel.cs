using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class ItemModel
    {
        public ItemModel()
        {
        ItemList =new List<ItemModel>();
    }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string UnitPrice { get; set; }
        public string MeasureUnit { get; set; }
        public decimal QuantityPresent { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public List<ItemModel> ItemList { get; set; }
    }
}