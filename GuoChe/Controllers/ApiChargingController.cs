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
    public class ApiChargingController : Controller
    {
        //
        // GET: /ApiCharging/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有充电桩信息
        /// 1、 不传 返回所有2、 充电桩ID CPid3、 供应点信息ID ChargeBaseID
        /// </summary>
        /// <param name="CPid"></param>
        /// <param name="ChargeBaseID"></param>
        /// <returns></returns>
        public JsonResult ChargingPileInfo(string CPid, string ChargeBaseID)
        {
            List<ChargingPileEntity> lstEntity = ChargeService.GetChargingPileInfo(CPid, ChargeBaseID);

            return Json(JsonHelper.ToJson<List<ChargingPileEntity>>(lstEntity));
        }

        /// <summary>
        /// 获取所有供电站信息
        /// 1、 不传 返回所有2、 充电桩ID CPid3、 供应点信息ID ChargeBaseID
        /// </summary>
        /// <param name="CPid"></param>
        /// <param name="ChargeBaseID"></param>
        /// <returns></returns>
        public JsonResult ChargingBaseInfo(string cityid, string ChargeBaseID)
        {
            List<ChargingBaseEntity> lstEntity = ChargeService.GetChargingBaseInfo(cityid, ChargeBaseID);

            return Json(JsonHelper.ToJson<List<ChargingBaseEntity>>(lstEntity));
        }

    }
}
