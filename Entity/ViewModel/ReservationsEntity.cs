using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public  class ReservationsEntity
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }

        public string CustomerName { get; set; }

        public int RType { get; set; }

        public int PayType { get; set; }

        public int CarID { get; set; }

        public int LeaseTime { get; set; }

        public Decimal Price { get; set; }

        public String Remark { get; set; }

        public DateTime RDate { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
