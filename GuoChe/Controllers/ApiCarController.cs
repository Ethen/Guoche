using Common;
using Entity.ViewModel;
using Service;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class ApiCarController : Controller
    {
        //
        // GET: /ApiCar/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 1、 不传 返回所有 2、 车辆信息ID carid 3、 供应商ID supplierid

        /// </summary>
        /// <returns></returns>
        public JsonResult CarInfo(string carid, string supplierid)
        {
            List<CarEntity> lstCar = CarService.GetAllCar(carid, supplierid);
            return Json(JsonHelper.ToJson<List<CarEntity>>(lstCar));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public JsonResult SupplierInfo(string cityid)
        {
            List<StoreEntity> lstStore = StoreService.GetStoreAll(cityid);
            return Json(JsonHelper.ToJson<List<StoreEntity>>(lstStore));
        }

        /// <summary>
        ///  用户预约车辆信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="carid"></param>
        /// <returns></returns>
        public JsonResult Reservation(string userid,  string carid)
        {
            return Json("");
        }
    }
}
