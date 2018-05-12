using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Reservations
{
    public class ReservationsRepository : DataAccessBase
    {
        public int CreateNew(ReservationsInfo info)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ReservationsStatement.InsertSql, "Text"));
            command.AddInputParameter("@CustomerID", DbType.Int64, info.CustomerID);
            command.AddInputParameter("@CustomerName", DbType.String, info.CustomerName);
            command.AddInputParameter("@RType", DbType.Int32, info.RType);
            command.AddInputParameter("@PayType", DbType.Int32, info.PayType);
            command.AddInputParameter("@CarID", DbType.Int32, info.CarID);
            command.AddInputParameter("@LeaseTime", DbType.Int32, info.LeaseTime);
            command.AddInputParameter("@Price", DbType.Decimal, info.Price);
            command.AddInputParameter("@Remark", DbType.String, info.Remark);
            command.AddInputParameter("@RDate", DbType.DateTime, info.RDate);
            command.AddInputParameter("@Status", DbType.Int32, info.Status);
            command.AddInputParameter("@CreateDate", DbType.DateTime, info.CreateDate);

            return command.ExecuteNonQuery();
        }
    }
}
