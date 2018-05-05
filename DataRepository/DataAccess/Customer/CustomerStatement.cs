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
        public static string RegisterSql = "INSERT INTO Customer(CustomerName,CustomerCode,Password,Channel,Name,Mobile,CreateDate, LastLoginDate)VALUES(@CustomerName,@CustomerCode, @Password,@Channel,@Name, @Mobile, @CreateDate, @LastLoginDate)";

        public static string UpdatePassword = "Update Customer set Password=@Password,CustomerCode=@CustomerCode where Mobile=@Mobile";
    }
}
