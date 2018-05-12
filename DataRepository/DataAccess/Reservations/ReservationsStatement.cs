using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Reservations
{
    public class ReservationsStatement
    {
        public static string SelectSql = @"SELECT * FROM Reservations";

        public static string InsertSql = @"INSERT INTO Reservations(CustomerID,CustomerName,RType,PayType,CarID,LeaseTime,Price,Remark,RDate,Status,CreateDate)VALUES(@CustomerID,@CustomerName,@RType,@PayType,@CarID,@LeaseTime,@Price,@Remark,@RDate,@Status,@CreateDate)";
    }
}
