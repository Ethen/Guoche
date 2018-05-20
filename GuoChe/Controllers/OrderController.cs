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
    public class OrderController:BaseController
    {
        public int PAGESIZE = 20;

        public ActionResult Index(string orderid, string customerid,string supplierid,int status=-2,int p=1)
        {
            OrderSearchEntity search = new OrderSearchEntity();
            search.OrderID = orderid ?? "";
            search.CustomerID = customerid.ToLong(0);
            search.SupplierID = supplierid.ToLong(0);
            search.Status = status;


            int count = OrderService.GetOrderCount(search);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Order";

            List<StoreEntity> stores = StoreService.GetStoreAll().Where(t => t.Status == 1).ToList();
            List<CustomerEntity> customers = CustomerService.GetCustomerList();

            ViewBag.Search = search;
            List<OrderEntity> mList = OrderService.GetOrderByRule(search,pager);
            ViewBag.Orders = mList;
            ViewBag.Pager = pager;
            ViewBag.Customers = customers;
            ViewBag.Stores = stores;

            return View();
        }

        public JsonResult Edit(long oid, int status)
        {
            var result = new
            {
                result = OrderService.EditOrderStatus(oid, status)
            };

            return Json(result);
        }




    }
}
