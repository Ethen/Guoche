using Common;
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


        private static CustomerExtendEntity TranslateCustomerExtendEntity(CustomerExtendInfo info)
        {
            CustomerExtendEntity entity = new CustomerExtendEntity();

            if (info != null)
            {
                entity.ID = info.ID;
                entity.CustomerID = info.CustomerID;
                entity.CustomerCode = info.CustomerCode;
                entity.AttachmentIDs = info.AttachmentIDs;
                entity.CustomerName = info.CustomerName;
                entity.Mobile = info.Mobile;
                entity.Email = info.Email;
                entity.CardType = info.CardType;
                entity.CardNo = info.CardNo;
                entity.Channel = info.Channel;
                entity.RegisterTime = info.RegisterTime;
                entity.Status = info.Status;
                entity.AuditTime = info.AuditTime;
                entity.Auditor = info.Auditor;
                entity.ModifyDate = info.ModifyDate;
                entity.Operator = info.Operator;
            }

            return entity;
        }

        private static CustomerExtendInfo TranslateCustomerExtendEntity(CustomerExtendEntity entity)
        {
            CustomerExtendInfo info = new CustomerExtendInfo();

            if (entity != null)
            {
                info.ID = entity.ID;
                info.CustomerID = entity.CustomerID;
                info.CustomerCode = entity.CustomerCode;
                info.AttachmentIDs = entity.AttachmentIDs;
                info.CustomerName = entity.CustomerName;
                info.Mobile = entity.Mobile;
                info.Email = entity.Email;
                info.CardType = entity.CardType;
                info.CardNo = entity.CardNo;
                info.Channel = entity.Channel;
                info.RegisterTime = entity.RegisterTime;
                info.Status = entity.Status;
                info.AuditTime = entity.AuditTime;
                info.Auditor = entity.Auditor;
                info.ModifyDate = entity.ModifyDate;
                info.Operator = entity.Operator;
            }

            return info;
        }


        public static CustomerExtendEntity GetCustomerExtendInfoByID(long id)
        {

            CustomerRepository mr = new CustomerRepository();
            CustomerExtendInfo info = mr.GetCustomerExtendByID(id);
            CustomerExtendEntity entity = TranslateCustomerExtendEntity(info);

            return entity;
        }

        public static List<CustomerExtendEntity> GetCustomerList(string name, string code, int status, PagerInfo pager)
        {
            List<CustomerExtendEntity> all = new List<CustomerExtendEntity>();
            CustomerRepository mr = new CustomerRepository();
            List<CustomerExtendInfo> miList = mr.GetCustomerExtend(name, code, status, pager);
            foreach (CustomerExtendInfo mInfo in miList)
            {
                CustomerExtendEntity customerExtendEntity = TranslateCustomerExtendEntity(mInfo);
                all.Add(customerExtendEntity);
            }
            return all;
        }


        public static int GetCustomerExtendCount(string name, string code, int status)
        {
            return new CustomerRepository().GetCustomerExtendCount(name, code, status);
        }


        public static List<CustomerEntity> GetCustomerList()
        {
            List<CustomerEntity> all = new List<CustomerEntity>();
            CustomerRepository mr = new CustomerRepository();
            List<CustomerInfo> miList = mr.GetCustomerAll();
            foreach (CustomerInfo mInfo in miList)
            {
                CustomerEntity customerEntity = new CustomerEntity();
                if (mInfo != null)
                {
                    customerEntity.Channel = mInfo.Channel;
                    customerEntity.CustomerCode = mInfo.CustomerCode;
                    customerEntity.CustomerID = mInfo.CustomerID;
                    customerEntity.CustomerName = mInfo.CustomerName;
                    customerEntity.Mobile = mInfo.Mobile;
                    customerEntity.Name = mInfo.Name;
                }
                all.Add(customerEntity);
            }
            return all;
        }

    }
}
