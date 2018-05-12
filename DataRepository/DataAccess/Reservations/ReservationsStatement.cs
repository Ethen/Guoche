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

        public static string GetReservationByRule = @"SELECT * FROM Reservations(NOLOCK) WHERE 1=1 ";

        public static string GetReservationByID = @"SELECT * FROM Reservations(NOLOCK) WHERE ID=@ID ";
    }
}
