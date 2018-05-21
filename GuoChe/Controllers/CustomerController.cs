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


        public ActionResult Edit(long cid)
        {

            List<BaseDataEntity> cardTypes = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "CD00").ToList();

            CustomerExtendEntity entity = new CustomerExtendEntity();
            if (cid > 0)
            {
                entity = CustomerService.GetCustomerExtendInfoByID(cid);
            }

            ViewBag.Extend = entity;
            ViewBag.CardTypes = cardTypes;

            return View();
        }

        public void Modify(CustomerExtendEntity extendEntity)
        {

            if (extendEntity != null)
            {
                CustomerEntity customer = new CustomerEntity();
                customer.CustomerID = extendEntity.CustomerID;
                customer.CustomerName = extendEntity.CustomerName;
                customer.CustomerCode = extendEntity.CustomerCode;
                customer.Channel = 3;
                customer.Mobile = extendEntity.Mobile;
                customer.Name = "";

                CustomerService.ModifyCustomer(customer, extendEntity);
            }

            Response.Redirect("/Customer/");
        } 







    }
}
