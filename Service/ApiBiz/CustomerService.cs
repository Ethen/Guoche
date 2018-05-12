using DataRepository.DataAccess.Customer;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApiBiz
{
    public class CustomerService : BaseService
    {
        
        /// <summary>
        ///  验证登录功能
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static CustomerEntity Login(string telephone, string password)
        {
            CustomerEntity entity = null;
            CustomerRepository CR = new CustomerRepository();
            CustomerInfo info= CR.Login(telephone, password);
            if (info != null)
            {
                entity = new CustomerEntity();
                entity.Channel = info.Channel;
                entity.CustomerCode = info.CustomerCode;
                entity.CustomerID = info.CustomerID;
                entity.CustomerName = info.CustomerName;
                entity.Mobile = info.Mobile;
                entity.Name = info.Name;
            }
            return entity;
        }

        /// <summary>
        ///  验证手机号是否已经使用
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public static CustomerEntity GetCustomerByTelephone(string telephone)
        {
            CustomerEntity entity = null;
            CustomerRepository CR = new CustomerRepository();
            CustomerInfo info = CR.GetCustomerByTelephone(telephone);
            if (info != null)
            {
                entity = new CustomerEntity();
                entity.Channel = info.Channel;
                entity.CustomerCode = info.CustomerCode;
                entity.CustomerID = info.CustomerID;
                entity.CustomerName = info.CustomerName;
                entity.Mobile = info.Mobile;
                entity.Name = info.Name;
            }
            return entity;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        /// <param name="vcode"></param>
        /// <returns></returns>
        public static CustomerEntity Register(string telephone, string password, string vcode)
        {
            CustomerEntity entity = null;
            CustomerRepository CR = new CustomerRepository();
            if (CR.Register(telephone, password, vcode) > 0)
            {
                CustomerInfo info = CR.GetCustomerByTelephone(telephone);
                if (info != null)
                {
                    entity = new CustomerEntity();
                    entity.Channel = info.Channel;
                    entity.CustomerCode = info.CustomerCode;
                    entity.CustomerID = info.CustomerID;
                    entity.CustomerName = info.CustomerName;
                    entity.Mobile = info.Mobile;
                    entity.Name = info.Name;
                }
            }
            return entity;
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        /// <param name="vcode"></param>
        /// <returns></returns>
        public static CustomerEntity UpdatePassword(string telephone, string password, string vcode)
        {
            CustomerEntity entity = null;
            CustomerRepository CR = new CustomerRepository();
            if (CR.UpdatePassword(telephone, password, vcode) > 0)
            {
                CustomerInfo info = CR.GetCustomerByTelephone(telephone);
                if (info != null)
                {
                    entity = new CustomerEntity();
                    entity.Channel = info.Channel;
                    entity.CustomerCode = info.CustomerCode;
                    entity.CustomerID = info.CustomerID;
                    entity.CustomerName = info.CustomerName;
                    entity.Mobile = info.Mobile;
                    entity.Name = info.Name;
                }
            }
            return entity;
        }


        public static CustomerEntity GetCustomerByID(long cid)
        {
            CustomerEntity entity = null;
            CustomerRepository CR = new CustomerRepository();
            CustomerInfo info = CR.GetCustomerByID(cid);
            if (info != null)
            {
                entity = new CustomerEntity();
                entity.Channel = info.Channel;
                entity.CustomerCode = info.CustomerCode;
                entity.CustomerID = info.CustomerID;
                entity.CustomerName = info.CustomerName;
                entity.Mobile = info.Mobile;
                entity.Name = info.Name;
            }
            return entity;
        }
    }
}
