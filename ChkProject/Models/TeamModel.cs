using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class TeamModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public Nullable<System.TimeSpan> ShiftStartTime { get; set; }
        public Nullable<System.TimeSpan> ShiftEndTime { get; set; }
        public Nullable<int> CompanyLocationId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}