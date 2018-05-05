using DataRepository.DataModel;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ChargeService:BaseService
    {
        private static List<BaseDataEntity> GetPayTypeNames(string payType)
        {
            List<BaseDataEntity> baseList = new List<BaseDataEntity>();
            if (!string.IsNullOrEmpty(payType))
            {
                List<string> payTypes = payType.Split(',').ToList();
                List<BaseDataEntity> allPayType = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "P00").ToList();
                foreach (string item in payTypes)
                {
                    if (allPayType.Exists(t => t.TypeCode == item))
                    {
                        BaseDataEntity baseData = allPayType.FirstOrDefault(t => t.TypeCode == item);
                        baseList.Add(baseData);
                    }
                }
            }

            return baseList;

        }

        private static ChargingBaseInfo TranslateChargingBaseEntity(ChargingBaseEntity chargingBaseEntity)
        {
            ChargingBaseInfo chargeBaseInfo = new ChargingBaseInfo();
            if (chargingBaseEntity != null)
            {
                chargeBaseInfo.ChargeBaseID = chargingBaseEntity.ChargeBaseID;
                chargeBaseInfo.Name = chargingBaseEntity.Name;
                chargeBaseInfo.Code = chargingBaseEntity.Code;
                chargeBaseInfo.ChargeFee = chargingBaseEntity.ChargeFee;
                chargeBaseInfo.ServerFee = chargingBaseEntity.ServerFee;
                chargeBaseInfo.ParkFee = chargingBaseEntity.ParkFee;
                chargeBaseInfo.ChargeNum = chargingBaseEntity.ChargeNum;
                chargeBaseInfo.PayType = chargingBaseEntity.PayType;
                chargeBaseInfo.Address = chargingBaseEntity.Address;
                chargeBaseInfo.Coordinate = chargingBaseEntity.Coordinate;
                chargeBaseInfo.StartTime = chargingBaseEntity.StartTime;
                chargeBaseInfo.EndTime = chargingBaseEntity.EndTime;
                chargeBaseInfo.IsUse = chargingBaseEntity.IsUse;
            }

            return chargeBaseInfo;
        }

        private static ChargingBaseEntity TranslateChargingBaseInfo(ChargingBaseInfo chargeBaseInfo)
        {
            ChargingBaseEntity chargingBaseEntity = new ChargingBaseEntity();
            if (chargeBaseInfo != null)
            {
                chargingBaseEntity.ChargeBaseID = chargeBaseInfo.ChargeBaseID;
                chargingBaseEntity.Name = chargeBaseInfo.Name;
                chargingBaseEntity.Code = chargeBaseInfo.Code;
                chargingBaseEntity.ChargeFee = chargeBaseInfo.ChargeFee;
                chargingBaseEntity.ServerFee = chargeBaseInfo.ServerFee;
                chargingBaseEntity.ParkFee = chargeBaseInfo.ParkFee;
                chargingBaseEntity.ChargeNum = chargeBaseInfo.ChargeNum;
                chargingBaseEntity.PayType = chargeBaseInfo.PayType;
                chargingBaseEntity.Address = chargeBaseInfo.Address;
                chargingBaseEntity.Coordinate = chargeBaseInfo.Coordinate;
                chargingBaseEntity.StartTime = chargeBaseInfo.StartTime;
                chargingBaseEntity.EndTime = chargeBaseInfo.EndTime;
                chargingBaseEntity.IsUse = chargeBaseInfo.IsUse;

                if (!string.IsNullOrEmpty(chargingBaseEntity.PayType))
                {
                    chargingBaseEntity.PayTypeName = GetPayTypeNames(chargingBaseEntity.PayType);
                }
            }

            return chargingBaseEntity;
        }

        private static ChargingPileInfo TranslateChargingPileEntity(ChargingPileEntity chargingPileEntity)
        {
            ChargingPileInfo chargingPileInfo = new ChargingPileInfo();

            if (chargingPileEntity != null)
            {
                chargingPileInfo.ID = chargingPileEntity.ID;
                chargingPileInfo.Code = chargingPileEntity.Code;
                chargingPileInfo.Standard = chargingPileEntity.Standard;
                chargingPileInfo.SOC = chargingPileEntity.SOC;
                chargingPileInfo.Power = chargingPileEntity.Power;
                chargingPileInfo.Electric = chargingPileEntity.Electric;
                chargingPileInfo.CElectric = chargingPileEntity.CElectric;
                chargingPileInfo.Voltage = chargingPileEntity.Voltage;
                chargingPileInfo.CVoltage = chargingPileEntity.CVoltage;
                chargingPileInfo.ChargeFee = chargingPileEntity.ChargeFee;
                chargingPileInfo.ServerFee = chargingPileEntity.ServerFee;
                chargingPileInfo.ParkFee = chargingPileEntity.ParkFee;
                chargingPileInfo.PayType = chargingPileEntity.PayType;
                chargingPileInfo.Address = chargingPileEntity.Address;
                chargingPileInfo.Coordinate = chargingPileEntity.Coordinate;
                chargingPileInfo.StartTime = chargingPileEntity.StartTime;
                chargingPileInfo.EndTime = chargingPileEntity.EndTime;
                chargingPileInfo.ChargingBaseID = chargingPileEntity.ChargingBaseID;
                chargingPileInfo.IsUse = chargingPileEntity.IsUse;
            }
            return chargingPileInfo;
        }

        private static ChargingPileEntity TranslateChargingPileInfo(ChargingPileInfo chargingPileInfo)
        {
            ChargingPileEntity chargingPileEntity = new ChargingPileEntity();

            if (chargingPileInfo != null)
            {
                chargingPileEntity.ID = chargingPileInfo.ID;
                chargingPileEntity.Code = chargingPileInfo.Code;
                chargingPileEntity.Standard = chargingPileInfo.Standard;
                chargingPileEntity.SOC = chargingPileInfo.SOC;
                chargingPileEntity.Power = chargingPileInfo.Power;
                chargingPileEntity.Electric = chargingPileInfo.Electric;
                chargingPileEntity.CElectric = chargingPileInfo.CElectric;
                chargingPileEntity.Voltage = chargingPileInfo.Voltage;
                chargingPileEntity.CVoltage = chargingPileInfo.CVoltage;
                chargingPileEntity.ChargeFee = chargingPileInfo.ChargeFee;
                chargingPileEntity.ServerFee = chargingPileInfo.ServerFee;
                chargingPileEntity.ParkFee = chargingPileInfo.ParkFee;
                chargingPileEntity.PayType = chargingPileInfo.PayType;
                chargingPileEntity.Address = chargingPileInfo.Address;
                chargingPileEntity.Coordinate = chargingPileInfo.Coordinate;
                chargingPileEntity.StartTime = chargingPileInfo.StartTime;
                chargingPileEntity.EndTime = chargingPileInfo.EndTime;
                chargingPileEntity.ChargingBaseID = chargingPileInfo.ChargingBaseID;
                chargingPileEntity.IsUse = chargingPileInfo.IsUse;

                ChargingBaseEntity chargeBase = new ChargingBaseEntity();
                chargingPileEntity.ChargingBase = chargeBase;

                if (!string.IsNullOrEmpty(chargingPileEntity.PayType))
                {
                    chargingPileEntity.PayTypeName = GetPayTypeNames(chargingPileEntity.PayType);
                }



            }
            return chargingPileEntity;
        }

        public static List<ChargingPileEntity> GetALlChargingPileEntity()
        {
            List<ChargingPileEntity> all = new List<ChargingPileEntity>();
            //GroupRepository mr = new GroupRepository();
            //List<GroupInfo> miList = Cache.Get<List<GroupInfo>>("GroupALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllGroup();
            //    Cache.Add("GroupALL", miList);
            //}
            //if (!miList.IsEmpty())
            //{
            //    foreach (GroupInfo mInfo in miList)
            //    {
            //        GroupEntity GroupEntity = TranslateGroupEntity(mInfo);
            //        all.Add(GroupEntity);
            //    }
            //}

            return all;
        }

          

    }
}
