using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class ItemUsedModel
    {
        public ItemUsedModel()
        {
            DDLItems = new List<ItemModel>();
            ListItemUsed = new List<ItemUsedModel>();
        }
        public long ItemUsedId { get; set; }
        public Nullable<long> ProductionId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<decimal> QuantityUsed { get; set; }
        public Nullable<System.DateTime> UseDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        public List<ItemModel> DDLItems { get; set; }
        public List<ItemUsedModel> ListItemUsed { get; set; }

        public string ItemName { get; set; }
        public string MeasureUnit { get; set; } 
    }

}