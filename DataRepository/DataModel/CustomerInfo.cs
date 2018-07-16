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
    public class CustomerInfo
    {
        [DataMapping("CustomerID", DbType.Int64)]
        public long CustomerID { get; set; }

        [DataMapping("CustomerName", DbType.String)]
        public string CustomerName { get; set; }

        [DataMapping("CustomerCode", DbType.String)]
        public string CustomerCode { get; set; }

        [DataMapping("Password", DbType.String)]
        public string Password { get; set; }

        [DataMapping("Channel", DbType.Int32)]
        public int Channel { get; set; }

        [DataMapping("Name", DbType.String)]
        public string Name { get; set; }

        [DataMapping("Mobile", DbType.String)]
        public string Mobile { get; set; }

        [DataMapping("WXCode", DbType.String)]
        public string WXCode { get; set; }


    }
}
