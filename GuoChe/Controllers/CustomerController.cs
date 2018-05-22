using Common;
using DataRepository.DataAccess.Order;
using Entity.ViewModel;
using Infrastructure.Cache;
using Service;
using Service.ApiBiz;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GuoChe.Controllers
{
    public class CustomerController:BaseController
    {
        public int PAGESIZE = 20;

        public ActionResult Index(string name, string code, int status=-1, int p = 1)
        {

            int count = CustomerService.GetCustomerExtendCount(name, code, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Customer";

            List<StoreEntity> stores = StoreService.GetStoreAll().Where(t => t.Status == 1).ToList();
            List<CustomerExtendEntity> customers = CustomerService.GetCustomerList(name, code, status, pager);

            ViewBag.Customers = customers;
            ViewBag.Name = name;
            ViewBag.Code = code;
            ViewBag.Status = status;
            ViewBag.Pager = pager;

            return View();
        }


        public ActionResult Edit(long cid=-1)
        {

            List<BaseDataEntity> cardTypes = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "CD00").ToList();

            CustomerExtendEntity entity = new CustomerExtendEntity();
            if (cid > 0)
            {
                entity = CustomerService.GetCustomerExtendInfoByID(cid);
                if (entity == null || entity.ID < 1)
                {
                    CustomerEntity ce = CustomerService.GetCustomerByID(cid);
                    if (ce != null && ce.CustomerID > 0)
                    {
                        entity.CustomerID = ce.CustomerID;
                        entity.CustomerName = ce.CustomerName;
                        entity.CustomerCode = ce.CustomerCode;
                        entity.Channel = ce.Channel;
                        entity.Mobile = ce.Mobile;
                    }
                }
            }
            else
            {
                entity.CustomerCode = DateTime.Now.ToString("yyyyMMddHHmmss");
                entity.RegisterTime = DateTime.Now;
                entity.AuditTime = DateTime.Parse("1753-01-01");
                entity.Channel = 3;
            }

            ViewBag.Extend = entity;
            ViewBag.CardTypes = cardTypes;

            return View();
        }

        [HttpPost]
        public void Modify(CustomerExtendEntity extendEntity)
        {

            if (extendEntity != null)
            {
                CustomerEntity customer = new CustomerEntity();
                customer.CustomerID = extendEntity.CustomerID;
                customer.CustomerName = extendEntity.CustomerName;
                customer.CustomerCode = extendEntity.CustomerCode;
                customer.Channel = extendEntity.Channel;
                customer.Mobile = extendEntity.Mobile;
                customer.Name = "";
                if (customer.CustomerID < 1)
                {
                    customer.Password = EncryptHelper.MD5Encrypt("123456");
                }
                CustomerService.ModifyCustomer(customer, extendEntity);
            }

            Response.Redirect("/Customer/");
        } 







    }
}
