using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Charge
{
    public class ChargeRepository : DataAccessBase
    {
        /// <summary>
        /// 获取所有充电桩信息
        ///  1、 不传 返回所有2、 充电桩ID CPid  3、 供应点信息ID ChargeBaseID
        /// </summary>
        /// <param name="CPid">充电桩ID</param>
        /// <param name="ChargeBaseID">供应点信息ID</param>
        /// <returns></returns>
        public static List<ChargingPileInfo> GetChargingPileInfo(string CPid, string ChargeBaseID)
        {
            List<ChargingPileInfo> result = new List<ChargingPileInfo>();
            string sqltext = ChargeStatement.GetAllChargingPile;
            sqltext += " where 1=1";
            if (!string.IsNullOrEmpty(CPid))
            {
                sqltext += " and ID='" + CPid + "'";
            }
            if (!string.IsNullOrEmpty(ChargeBaseID))
            {
                sqltext += " and ChargingBaseID='" + ChargeBaseID + "'";
            }
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqltext, "Text"));
            result = command.ExecuteEntityList<ChargingPileInfo>();
            return result;
        }


        /// <summary>
        /// 获取所有供电站信息
        ///  1、 不传 返回所有2、 城市ID CPid  3、 供应点信息ID ChargeBaseID
        /// </summary>
        /// <param name="CPid">充电桩ID</param>
        /// <param name="ChargeBaseID">供应点信息ID</param>
        /// <returns></returns>
        public static List<ChargingBaseInfo> GetChargingBaseInfo(string CityID, string ChargeBaseID)
        {
            List<ChargingBaseInfo> result = new List<ChargingBaseInfo>();
            string sqltext = ChargeStatement.GetAllChargingBase;
            sqltext += " where 1=1";
            if (!string.IsNullOrEmpty(CityID))
            {
                sqltext += " and CityID='" + CityID + "'";
            }
            if (!string.IsNullOrEmpty(ChargeBaseID))
            {
                sqltext += " and ChargeBaseID='" + ChargeBaseID + "'";
            }
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqltext, "Text"));
            result = command.ExecuteEntityList<ChargingBaseInfo>();
            return result;
        }
    }
}
