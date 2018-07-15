using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class SalerEntity
    {
        public long SID { get; set; }
        public String SCode { get; set; }
        public String Name { get; set; }
        public int Sex { get; set; }
        public DateTime Birthday { get; set; }
        public String CertificateType { get; set; }
        public String CertificateNo { get; set; }
        public String WXCode { get; set; }
        public String Mobile { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }

        public string StatusDesc
        {
            get
            {
                string result = string.Empty;
                switch (Status)
                {
                    case 0: result = "不可用"; break;
                    case 1: result = "可用"; break;
                }

                return result;
            }
        }

        public List<CustomerExtendEntity> Customers { get; set; }
    }
}
