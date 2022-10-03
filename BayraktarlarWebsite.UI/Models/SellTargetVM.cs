using BayraktarlarWebsite.Entities.Dtos;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class SellTargetVM
    {
        public SellListDto  SellList { get; set; }
        public HedefListDto HedefList { get; set; }
        public int SumTotalSell { get; set; }
        public TargetListDto  TargetList { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedMonth { get; set; }
        public IList<UsernameAndIdVM> UserNameAndId { get; set; }
        
    }
}
