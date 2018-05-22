using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Customer
{
    public class CustomerStatement
    {
        //登录验证
        public static string LoginSql = "SELECT CustomerID,CustomerName,CustomerCode,Password,Channel,Name,Mobile  FROM Customer (NOLOCK) where Mobile=@Mobile and Password=@Password";
        //验证手机号是否已经注册
        public static string GetCustomerByTelephoneSql = "SELECT CustomerID,CustomerName,CustomerCode,Password,Channel,Name,Mobile  FROM Customer (NOLOCK) where Mobile=@Mobile ";
        //注册用户
        public static string RegisterSql = "INSERT INTO Customer(CustomerName,CustomerCode,Password,Channel,Name,Mobile,CreateDate, LastLoginDate)VALUES(@CustomerName,@CustomerCode, @Password,@Channel,@Name, @Mobile, @CreateDate, @LastLoginDate)SELECT @CustomerID=@@IDENTITY ";

        public static string UpdatePassword = "Update Customer set Password=@Password,CustomerCode=@CustomerCode where Mobile=@Mobile";

        public static string GetCustomerByID = @"SELECT * FROM Customer(NOLOCK) WHERE CustomerID=@CustomerID ";

        public static string GetCustomerExtendByID = @"SELECT * FROM CustomerExtend(NOLOCK) WHERE ID=@ID ";

        public static string GetCutomerAll = @"SELECT * FROM Customer(NOLOCK) ";

        public static string GetCustomerAllCount = @"SELECT * FROM CustomerExtend(NOLOCK) WHERE 1=1 ";


        public static string CreateCustomer = @"INSERT INTO Customer(CustomerName,CustomerCode,Password,Channel,Name,Mobile,CreateDate, LastLoginDate)VALUES(@CustomerName,@CustomerCode, @Password,@Channel,@Name, @Mobile, @CreateDate, @LastLoginDate);SELECT @CustomerID=@@IDENTITY ";

        public static string CreateCustomerExtend = @"INSERT INTO [CustomerExtend] ([CustomerID],[CustomerCode],[AttachmentIDs],[CustomerName],[Mobile],[Email],[CardType],[CardNo],[Channel],[RegisterTime],[Status],[AuditTime],[Auditor],[CreateDate],[ModifyDate],[Operator])
                                                        VALUES (@CustomerID,@CustomerCode,@AttachmentIDs,@CustomerName,@Mobile,@Email,@CardType,@CardNo,@Channel,@RegisterTime,@Status,@AuditTime,@Auditor,@CreateDate,@ModifyDate,@Operator)";

        public static string ModifyCustomerExtend = @"UPDATE [CustomerExtend] SET AttachmentIDs=@AttachmentIDs,CustomerName=@CustomerName,Mobile=@Mobile,Email=@Email,CardType=@CardType,CardNo=@CardNo,Channel=@Channel,Status=@Status,ModifyDate=@ModifyDate,Operator=@Operator WHERE ID=@ID";

        public static string ModifyCustomer = @"  UPDATE [Customer] SET [CustomerName]=@CustomerName,[Name]=@Name,[Mobile]=@Mobile WHERE CustomerID=@CustomerID";

        public static string GetCustomerAllPagerHeader = @"DECLARE @UP INT
        
	                                                  ---------分页区间计算-------------最大页数
                                                      IF(@recordCount<@PageSize*(@PageIndex-1)) 
                                                      BEGIN
                                                        SET @PageIndex= @recordCount/@PageSize+1
                                                      END
                                                      --最小页数
	                                                  IF(@PageIndex<1)
	                                                     SET @PageIndex=1
                                                      --当前页起始游标值
	                                                  IF(@recordCount>@PageSize*(@PageIndex-1))
	                                                  BEGIN
		                                                  SET @UP=@PageSize*(@PageIndex-1);
		                                                  ---------分页查询-----------
		                                                  WITH ce AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY [Status]) AS RowNumber FROM (SELECT * FROM CustomerExtend WHERE 1=1 ";

        public static string GetCustomerAllPagerFooter = @")as T ) 
		                                                  SELECT *  FROM ce 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

    }
}
