using Common;
using DataRepository.DataAccess.Customer;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.IO;
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
            long cid = CR.Register(telephone, password, vcode);
            if ( cid > 0)
            {
                CustomerExtendInfo extend = new CustomerExtendInfo();
                extend.CustomerID = 0;
                extend.CustomerName = "";
                extend.Mobile = telephone;
                extend.AttachmentIDs = "";
                extend.Email = "";
                extend.Channel = 1;
                extend.RegisterTime = DateTime.Now;
                extend.AuditTime = DateTime.MinValue;
                extend.Auditor = 0;
                extend.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                extend.Operator = 0;
                CR.CreateNewCustomerExtend(extend);
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


        private static CustomerExtendEntity TranslateCustomerExtendEntity(CustomerExtendInfo info, bool isAPI = false)
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
                entity.Base64 = info.Base64;
                if (!string.IsNullOrEmpty(entity.CardType))
                {
                    BaseDataEntity cardType = BaseDataService.GetBaseDataAll().First(t => t.TypeCode == entity.CardType) ?? new BaseDataEntity();
                    entity.CardTypeInfo = cardType;
                }

                List<AttachmentEntity> attachments = new List<AttachmentEntity>();
                if (!string.IsNullOrEmpty(entity.AttachmentIDs))
                {
                    attachments = BaseDataService.GetAttachmentInfoByKyes(entity.AttachmentIDs,isAPI);
                }
                entity.AttachmentInfos = attachments;
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

        private static CustomerInfo TranslateCustomerEntity(CustomerEntity entity)
        {
            CustomerInfo info = new CustomerInfo();

            if (entity != null)
            {
                info.CustomerID = entity.CustomerID;
                info.CustomerCode = entity.CustomerCode;
                info.Password = entity.Password;
                info.CustomerName = entity.CustomerName;
                info.Mobile = entity.Mobile;
                info.Name = entity.Name;
                info.Channel = entity.Channel;
            }

            return info;
        }

        private static CustomerEntity TranslateCustomerInfo(CustomerInfo info)
        {
            CustomerEntity entity = new CustomerEntity();

            if (info != null)
            {
                entity.CustomerID = info.CustomerID;
                entity.CustomerCode = info.CustomerCode;
                entity.Password = info.Password;
                entity.CustomerName = info.CustomerName;
                entity.Mobile = info.Mobile;
                entity.Name = info.Name;
                entity.Channel = info.Channel;

               CustomerExtendEntity detail= GetCustomerExtendInfoByID(entity.CustomerID);
               if (detail != null && detail.ID > 0)
               {
                   entity.Detail = detail;
               }
            }

            return entity;
        }


        public static CustomerExtendEntity GetCustomerExtendInfoByID(long id,bool isAPI=false)
        {

            CustomerRepository mr = new CustomerRepository();
            CustomerExtendInfo info = mr.GetCustomerExtendByID(id);
            CustomerExtendEntity entity = TranslateCustomerExtendEntity(info,isAPI);

            return entity;
        }


        public static CustomerExtendEntity GetCustomerExtendByCustomerID(long id, bool isAPI = false)
        {

            CustomerRepository mr = new CustomerRepository();
            CustomerExtendInfo info = mr.GetCustomerExtendByCustomerID(id);
            CustomerExtendEntity entity = TranslateCustomerExtendEntity(info, isAPI);

            return entity;
        }

        public static List<CustomerExtendEntity> GetCustomerList(string name, string code, int status, PagerInfo pager,bool isAPI=false)
        {
            List<CustomerExtendEntity> all = new List<CustomerExtendEntity>();
            CustomerRepository mr = new CustomerRepository();
            List<CustomerExtendInfo> miList = mr.GetCustomerExtend(name, code, status, pager);
            foreach (CustomerExtendInfo mInfo in miList)
            {
                CustomerExtendEntity customerExtendEntity = TranslateCustomerExtendEntity(mInfo,isAPI);
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

        public static int CreateNewCustomer(CustomerEntity customerEntity, CustomerExtendEntity extendEntity)
        {
            CustomerRepository cr = new CustomerRepository();
            int result = 0;
            if (customerEntity != null && extendEntity != null)
            {
                CustomerInfo info = TranslateCustomerEntity(customerEntity);
                CustomerExtendInfo extendInfo = TranslateCustomerExtendEntity(extendEntity);
                long customerid = cr.CreateNewCustomer(info);
                extendInfo.CustomerID = customerid;
                result = cr.CreateNewCustomerExtend(extendInfo);
            }

            return result;
        }


        public static int ModifyCustomer(CustomerEntity customerEntity, CustomerExtendEntity extendEntity)
        {


            CustomerRepository cr = new CustomerRepository();
            int result = 0;
            if (customerEntity != null && extendEntity != null)
            {
                CustomerInfo info = TranslateCustomerEntity(customerEntity);
                CustomerExtendInfo extendInfo = TranslateCustomerExtendEntity(extendEntity);

                if (customerEntity.CustomerID > 0)
                {
                    cr.ModifyCustomer(info);
                    result = cr.ModifyCustomer(extendInfo);
                }
                else
                {
                    long customerid = cr.CreateNewCustomer(info);
                    extendInfo.CustomerID = customerid;
                    result = cr.CreateNewCustomerExtend(extendInfo);
                }


            }

            return result;
        }


        public static CustomerExtendEntity AddFile(long userid, string base64, string fileType)
        {
            CustomerRepository customerRes = new CustomerRepository();
            CustomerExtendEntity cEntity = new CustomerExtendEntity();
            CustomerExtendInfo customerInfo = customerRes.GetCustomerExtendByCustomerID(userid);
            if (customerInfo != null)//已经存在
            {
                CustomerExtendInfo cInfo = new CustomerExtendInfo();                
                cInfo.CustomerID = userid;
                cInfo.CardType = fileType;
                cInfo.ID = customerInfo.ID;
                cInfo.Base64 = base64;
                customerRes.ModifyCustomer(cInfo);

            }
            else
            {
                CustomerExtendInfo cInfo = new CustomerExtendInfo();                
                cInfo.CustomerID = userid;
                cInfo.CardType = fileType;
                cInfo.Base64 = base64;
                customerRes.CreateNewCustomerExtend(cInfo);
            }
            CustomerExtendEntity detail = GetCustomerExtendByCustomerID(userid, true);
            return detail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="fileName"></param>
        /// <param name="fileType">身份证：CD01/驾驶证：CD02/用户头像:CD05</param>
        /// <returns></returns>
        public static CustomerExtendEntity AddFile(long userid, string fileName, string fileType, string FilePath, string fileExtension)
        {
            CustomerRepository customerRes = new CustomerRepository();
            AttachmentEntity attachment = new AttachmentEntity();
            attachment.FileName = fileName.Substring(0, fileName.IndexOf("."));
            attachment.FileExtendName = fileExtension;
            attachment.FilePath ="~"+ FilePath;
            attachment.FileType = fileType;
            attachment.BusinessType = fileType;
            attachment.Remark = "";
            attachment.FileSize = "";
            attachment.Channel = "Offline";
            attachment.UploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            attachment.Operator = userid;
            attachment.AttachmentID = BaseDataService.CreateAttachment(attachment);
            //用户扩展信息插入

            CustomerExtendEntity cEntity = new CustomerExtendEntity();            
            CustomerExtendInfo customerInfo= customerRes.GetCustomerExtendByID(userid);  
            if (customerInfo != null)//已经存在
            {
                CustomerExtendInfo cInfo = new CustomerExtendInfo();
                cInfo.AttachmentIDs = attachment.AttachmentID.ToString();
                cInfo.CustomerID = userid;
                cInfo.CardType = fileType;
                cInfo.ID = customerInfo.ID;                
                customerRes.ModifyCustomer(cInfo);
    
            }
            else
            {
                CustomerExtendInfo cInfo = new CustomerExtendInfo();
                cInfo.AttachmentIDs = attachment.AttachmentID.ToString();
                cInfo.CustomerID = userid;
                cInfo.CardType = fileType;
                customerRes.CreateNewCustomerExtend(cInfo);
            }
            CustomerExtendEntity detail = GetCustomerExtendInfoByID(userid,true);
            return detail;
        }

    }
}
