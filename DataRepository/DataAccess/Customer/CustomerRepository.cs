using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System.Data;
using Common;
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
            command.AddInputParameter("@Mobile", DbType.String, telephone);
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

        public CustomerInfo GetCustomerByID(long cid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.GetCustomerByID, "Text"));
            command.AddInputParameter("@CustomerID", DbType.Int64, cid);
            return command.ExecuteEntity<CustomerInfo>();
        }


        public List<CustomerInfo> GetCustomerAll()
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.GetCutomerAll, "Text"));
            return command.ExecuteEntityList<CustomerInfo>();
        }

        public List<CustomerExtendInfo> GetCustomerExtend(string name, string code,int status,PagerInfo pager)
        {
            List<CustomerExtendInfo> result = new List<CustomerExtendInfo>();


            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND CustomerName LIKE '%'+@CustomerName+'%' ");
            }
            if (!string.IsNullOrEmpty(code))
            {
                builder.Append(" AND CustomerCode LIKE '%'+@CustomerCode+'%' ");
            }
            if (status > -1)
            {
                builder.Append(" AND Status = @Status ");
            }

            string sql = CustomerStatement.GetCustomerAllPagerHeader + builder.ToString() + CustomerStatement.GetCustomerAllPagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sql, "Text"));

            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@CustomerName", DbType.String, name);
            }
            if (!string.IsNullOrEmpty(code))
            {
                command.AddInputParameter("@AdviseTitle", DbType.String, code);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }

            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<CustomerExtendInfo>();
            return result;
        }

        public int GetCustomerExtendCount(string name, string code, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(CustomerStatement.GetCustomerAllCount);
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND CustomerName LIKE '%'+@CustomerName+'%' ");
            }
            if (!string.IsNullOrEmpty(code))
            {
                builder.Append(" AND CustomerCode LIKE '%'+@CustomerCode+'%' ");
            }
            if (status > -1)
            {
                builder.Append(" AND Status = @Status ");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (!string.IsNullOrEmpty(name))
            {
                command.AddInputParameter("@CustomerName", DbType.String, name);
            }
            if (!string.IsNullOrEmpty(code))
            {
                command.AddInputParameter("@AdviseTitle", DbType.String, code);
            }
            if (status > -1)
            {
                command.AddInputParameter("@Status", DbType.Int32, status);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        public CustomerExtendInfo GetCustomerExtendByID(long cid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(CustomerStatement.GetCustomerByID, "Text"));
            command.AddInputParameter("@ID", DbType.Int64, cid);
            return command.ExecuteEntity<CustomerExtendInfo>();
        }




    }
}
