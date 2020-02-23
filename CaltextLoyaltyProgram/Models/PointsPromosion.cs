using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaltextLoyaltyProgram.Models
{
    public class PointsPromosion
    {
        public string PointsPromosionId { get; set; }
        public string PromosionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public int ForEachDollar { get; set; }
    }
}
