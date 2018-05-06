using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System.Data;
namespace DataRepository.DataAccess.Customer
{
    public class CustomerRepository : DataAccessBase
    {
        /// <summary>
        ///  登录验证
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CustomerInfo Login(string telephone, string password)
        {
            CustomerInfo result = new CustomerInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.LoginSql, "Text"));
            command.AddInputParameter("@Mobile", DbType.String, telephone);
            command.AddInputParameter("@password", DbType.String, password);
            result = command.ExecuteEntity<CustomerInfo>();
            return result;
        }

        /// <summary>
        ///  验证手机号是否已经注册
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public CustomerInfo GetCustomerByTelephone(string telephone)
        {
            CustomerInfo result = new CustomerInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.GetCustomerByTelephoneSql, "Text"));
            command.AddInputParameter("@Mobile", DbType.String, telephone);
            result = command.ExecuteEntity<CustomerInfo>();
            return result;
        }


        /// <summary>
        ///  修改密码
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        /// <param name="vcode"></param>
        /// <returns></returns>

        public int UpdatePassword(string telephone, string password, string vcode)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.UpdatePassword, "Text"));
            command.AddInputParameter("@CustomerCode", DbType.String, vcode);
            command.AddInputParameter("@Password", DbType.String, password);
            command.AddInputParameter("@Mobile", DbType.Int32, telephone);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 注册用户信息
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        /// <param name="vcode"></param>
        /// <returns></returns>
        public int Register(string telephone, string password, string vcode)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.RegisterSql, "Text"));
            command.AddInputParameter("@CustomerName", DbType.String, "");
            command.AddInputParameter("@CustomerCode", DbType.String, vcode);
            command.AddInputParameter("@Password", DbType.String, password);
            command.AddInputParameter("@Channel", DbType.Int32, 1);
            command.AddInputParameter("@Name", DbType.String, "");
            command.AddInputParameter("@Mobile", DbType.String, telephone);
            command.AddInputParameter("@CreateDate", DbType.DateTime, DateTime.Now);
            command.AddInputParameter("@LastLoginDate", DbType.DateTime, DateTime.Now);
            return command.ExecuteNonQuery();
        }
    }
}
