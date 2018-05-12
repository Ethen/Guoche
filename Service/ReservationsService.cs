using DataRepository.DataAccess.Reservations;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReservationsService
    {
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
