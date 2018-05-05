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
    public class ChargingPileInfo
    {
        [DataMapping("ID", DbType.Int64)]
        public long ID { get; set; }

        [DataMapping("Code", DbType.String)]
        public string Code { get; set; }
        [DataMapping("Standard", DbType.String)]
        public string Standard { get; set; }
        [DataMapping("SOC", DbType.String)]
        public string SOC { get; set; }
        [DataMapping("Power", DbType.String)]
        public string Power { get; set; }
        [DataMapping("Electric", DbType.String)]
        public string Electric { get; set; }
        [DataMapping("CElectric", DbType.String)]
        public string CElectric { get; set; }
        [DataMapping("Voltage", DbType.String)]
        public string Voltage { get; set; }
        [DataMapping("CVoltage", DbType.String)]
        public string CVoltage { get; set; }
        [DataMapping("ChargeFee", DbType.Decimal)]
        public decimal ChargeFee { get; set; }
        [DataMapping("ServerFee", DbType.Decimal)]
        public decimal ServerFee { get; set; }
        [DataMapping("ParkFee", DbType.Decimal)]
        public decimal ParkFee { get; set; }
        [DataMapping("PayType", DbType.String)]
        public string PayType { get; set; }
        [DataMapping("Address", DbType.String)]
        public string Address { get; set; }
        [DataMapping("Coordinate", DbType.String)]
        public string Coordinate { get; set; }
        [DataMapping("StartTime", DbType.String)]
        public string StartTime { get; set; }
        [DataMapping("EndTime", DbType.String)]
        public string EndTime { get; set; }
        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
        [DataMapping("IsUse", DbType.Int32)]
        public int IsUse { get; set; }
        [DataMapping("ChargingBaseID", DbType.Int32)]
        public int ChargingBaseID { get; set; }
    }
}
