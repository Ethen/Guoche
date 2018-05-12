using DataRepository.DataAccess.Reservations;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.ApiBiz;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReservationsService
    {

        private static ReservationsInfo TranslateReservationsInfo(ReservationsEntity reservationsEntity)
        {
            ReservationsInfo info = new ReservationsInfo();

            if (reservationsEntity != null)
            {
                info.ID = reservationsEntity.ID;
                info.CustomerID = reservationsEntity.CustomerID;
                info.CustomerName = reservationsEntity.CustomerName;
                info.RType = reservationsEntity.RType;
                info.PayType = reservationsEntity.PayType;
                info.CarID = reservationsEntity.CarID;
                info.LeaseTime = reservationsEntity.LeaseTime;
                info.Price = reservationsEntity.Price;
                info.Remark = reservationsEntity.Remark;
                info.RDate = reservationsEntity.RDate;
                info.Status = reservationsEntity.Status;


            }

            return info;
        }

        private static ReservationsEntity TranslateReservationsEntity(ReservationsInfo info)
        {
            ReservationsEntity reservationsEntity = new ReservationsEntity();

            if (info != null)
            {
                reservationsEntity.ID = info.ID;
                reservationsEntity.CustomerID = info.CustomerID;
                reservationsEntity.CustomerName = info.CustomerName;
                reservationsEntity.RType = info.RType;
                reservationsEntity.PayType = info.PayType;
                reservationsEntity.CarID = info.CarID;
                reservationsEntity.LeaseTime = info.LeaseTime;
                reservationsEntity.Price = info.Price;
                reservationsEntity.Remark = info.Remark;
                reservationsEntity.RDate = info.RDate;
                reservationsEntity.Status = info.Status;

                CustomerEntity customer = CustomerService.GetCustomerByID(info.CustomerID);
                reservationsEntity.Customer = customer;

                if (info.RType == 1)//汽车预约
                {
                    CarEntity car = CarService.GetCarEntityById(info.CarID);
                    reservationsEntity.Car = car;
                }
                else if(info.RType==2)//充电桩预约
                {
                    ChargingPileEntity chargingPile = ChargeService.GetChargingPileEntityById(info.CarID);
                    reservationsEntity.ChargingPile = chargingPile;
                }

                BaseDataEntity pay = BaseDataService.GetBaseDataByPCode("P00").FirstOrDefault(t => t.TypeCode == info.PayType) ?? new BaseDataEntity();
                reservationsEntity.PayTypeInfo = pay;//支付信息


            }

            return reservationsEntity;
        }


        public ReservationsEntity GetReservationsByID(long rid)
        {
            ReservationsRepository rr = new ReservationsRepository();
            ReservationsInfo info=rr.GetReservationsInfoByID(rid);
            return TranslateReservationsEntity(info);
        }

        public static bool AddReservations(ReservationsEntity entity)
        {
            int result = 0;
            ReservationsRepository rR = new ReservationsRepository(); 
            if (entity != null)
            {
                ReservationsInfo info = new ReservationsInfo();
                info.CarID = entity.CarID;
                info.CreateDate = DateTime.Now;
                info.CustomerID = entity.CustomerID;
                info.CustomerName = entity.CustomerName;
                info.LeaseTime = entity.LeaseTime;
                info.PayType = entity.PayType;
                info.Price = entity.Price;
                info.RDate = DateTime.Now;
                info.Remark = entity.Remark;
                info.RType = entity.RType;
                info.Status = entity.Status;
               result = rR.CreateNew(info);
            }
            return result > 0;
        }
    }
}
