using Common;
using Entity.ViewModel;
using Service;
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

        public JsonResult SupplierInfo(string cityid)
        {
            return Json("");
        }
    }
}
