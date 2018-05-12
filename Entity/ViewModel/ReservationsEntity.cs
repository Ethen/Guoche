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

        public string PayType { get; set; }

        public int CarID { get; set; }

        public int LeaseTime { get; set; }

        public Decimal Price { get; set; }

        public String Remark { get; set; }

        public DateTime RDate { get; set; }

        public int Status { get; set; }


        public CustomerEntity Customer { get; set; }


        public CarEntity Car { get; set; }

        public ChargingPileEntity ChargingPile { get; set; }


        public BaseDataEntity PayTypeInfo { get; set; }

    }
}
