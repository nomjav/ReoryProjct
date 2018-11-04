using Chakwal.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChkProject.Models
{
    public class HomeModel
    {
        public int totalSales { get; set; }
        public decimal? monthlyIncome { get; set; }
        public decimal? totalIncome { get; set; }
        public List<ItemBuyModel> Materials { get; set; }
        public List<string> itemNames { get; set; }
        public List<decimal> itemUsed { get; set; }
    }



}