using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class ChargingPileEntity
    {

        public long ID { get; set; }
        public string Code { get; set; }
        public string Standard { get; set; }
        public string SOC { get; set; }
        public string Power { get; set; }
        public string Electric { get; set; }
        public string CElectric { get; set; }
        public string Voltage { get; set; }
        public string CVoltage { get; set; }
        public decimal ChargeFee { get; set; }
        public decimal ServerFee { get; set; }
        public decimal ParkFee { get; set; }
        public string PayType { get; set; }
        public string Address { get; set; }
        public string Coordinate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int ChargingBaseID { get; set; }

        public int IsUse { get; set; }

        public string UseStatus
        {
            get
            {
                string result = "正常使用";
                if (IsUse == 0)
                {
                    result = "停用";
                }

                return result;
            }
        }

        public ChargingBaseEntity ChargingBase { get; set; }
        public List<BaseDataEntity> PayTypeName { get; set; }

    }
}
