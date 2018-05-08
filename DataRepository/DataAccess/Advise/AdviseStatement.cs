using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Advise
{
    public class AdviseStatement
    {
        public static string GetAllAdviseInfo = @"SELECT * FROM [AdviseInfo](NOLOCK)";

        public static string GetAdviseInfoByID = @"SELECT * FROM [AdviseInfo](NOLOCK) WHERE [AdviseID]=@AdviseID";

        public static string GetAdviseInfoByCustomerID = @"SELECT * FROM [AdviseInfo](NOLOCK) WHERE CustomerID=@CustomerID";

        public static string GetAdviseInfoByUserID = @"SELECT * FROM [AdviseInfo](NOLOCK) WHERE Operator=@Operator";

        public static string CreateNewtAdviseInfo = @"INSERT INTO ([AdviseInfo] [AdviseTitle],[Summary],[CustomerID],[CustomerName],[CustomerMobile],[DealStatus],[DealResult],[DealSummary],[Operator],[CreateDate],[ModifyDate])
                                                    VALUES (@AdviseTitle,@Summary,@CustomerID,@CustomerName,@CustomerMobile,@DealStatus,@DealResult,@DealSummary,@Operator,@CreateDate,@ModifyDate))";

        public static string ModifytAdviseInfo = @"UPDATE [AdviseInfo] SET AdviseTitle=@AdviseTitle,Summary=@Summary,CustomerID=@CustomerID,CustomerName=@CustomerName,CustomerMobile=@CustomerMobile,DealStatus=@DealStatus,DealResult=@DealResult,DealSummary=@DealSummary,Operator=@Operator,ModifyDate=@ModifyDate
                                                   WHERE AdviseID=@AdviseID ";

        public static string DealAdviseInfo = @"UPDATE [AdviseInfo] SET DealStatus=@DealStatus,DealResult=@DealResult,DealSummary=@DealSummary,Operator=@Operator,ModifyDate=@ModifyDate WHERE AdviseID=@AdviseID";

        public static string GetAllAdviseInfoByRule = @"SELECT * FROM [AdviseInfo](NOLOCK) WHERE 1=1 ";
    }
}
