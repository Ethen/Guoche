using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    [Serializable]
    public class OrderInfo
    {
        [DataMapping("OrderInnerID", DbType.Int64)]
        public long OrderInnerID { get; set; }

        [DataMapping("OrderID", DbType.String)]
        public string OrderID { get; set; }

        [DataMapping("PreID", DbType.Int64)]
        public long PreID { get; set; }

        [DataMapping("CustomerID", DbType.Int64)]
        public long CustomerID { get; set; }

        [DataMapping("CustomerName", DbType.String)]
        public string CustomerName { get; set; }

        [DataMapping("Mobile", DbType.String)]
        public string Mobile { get; set; }

        [DataMapping("EID", DbType.Int64)]
        public long EID { get; set; }

        [DataMapping("UseType", DbType.String)]
        public string UseType { get; set; }

        [DataMapping("PayType", DbType.String)]
        public string PayType { get; set; }

        [DataMapping("SupplierID", DbType.Int32)]
        public int SupplierID { get; set; }

        [DataMapping("SupplierCode", DbType.Int32)]
        public string SupplierCode { get; set; }

        [DataMapping("LeaseTime", DbType.String)]
        public int LeaseTime { get; set; }

        [DataMapping("Price", DbType.Decimal)]
        public decimal Price { get; set; }

        [DataMapping("Amount", DbType.String)]
        public decimal Amount { get; set; }

        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        [DataMapping("AttachmentIDs", DbType.String)]
        public string AttachmentIDs { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("ModifyDate", DbType.DateTime)]
        public DateTime ModifyDate { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataMapping("MenuID", DbType.String)]
        public string Operator { get; set; }
    }
}
