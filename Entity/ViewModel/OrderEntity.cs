using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class OrderEntity
    {
        public long OrderInnerID { get; set; }
        public string OrderID { get; set; }
        public long PreID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public long EID { get; set; }
        public string UseType { get; set; }
        public string PayType { get; set; }
        public int SupplierID { get; set; }
        public string SupplierCode { get; set; }
        public int LeaseTime { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public string AttachmentIDs { get; set; }
        public int Status { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Operator { get; set; }

        public string OrderStatus
        {
            get {
                string result = string.Empty;
                switch (Status)
                {
                    case -1: result = "已取消"; break;
                    case 0: result = "待处理"; break;
                    case 1: result = "进行中"; break;
                    case 2: result = "已付款"; break;
                    case 3: result = "已完成"; break;
                    default:result="待处理";break;
                }

                return result;
            }
        }


        public CustomerEntity Customer { get; set; }

        public StoreEntity Store { get; set; }

        public CarEntity Car { get; set; }

        public ChargingPileEntity ChargePile { get; set; }

        public BaseDataEntity PayTypeInfo { get; set; }



    }
}
