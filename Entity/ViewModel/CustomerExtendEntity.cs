using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class CustomerExtendEntity
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string AttachmentIDs { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 1--身份证 2--驾驶证
        /// </summary>
        public int CardType { get; set; }
        public string CardNo { get; set; }
        public int Channel { get; set; }
        public DateTime RegisterTime { get; set; }
        public int Status { get; set; }
        public DateTime AuditTime { get; set; }
        public long Auditor { get; set; }
        public string ModifyDate { get; set; }
        public long Operator { get; set; }
    }
}
