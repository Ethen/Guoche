using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
     [Serializable]
    public class AdviseEntity
    {
        public long AdviseID { get; set; }
        public string AdviseTitle { get; set; }
        public string Summary { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public int DealStatus { get; set; }
        public int DealResult { get; set; }
        public string DealSummary { get; set; }
        public long Operator { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
