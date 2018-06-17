using Common;
using Entity.ViewModel;
using Service;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Data;
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
        public JsonResult CarInfo(string carid, string supplierid, string cityid)
        {
            if (string.IsNullOrEmpty(cityid))
            {
                List<CarEntity> lstCar = CarService.GetAllCar(carid, supplierid, true);
                return Json(JsonHelper.ToJson<List<CarEntity>>(lstCar));
            }
            else
            {
                DataTable dt=CarService.MyCarInfoByCityID(cityid);
                return Json(JsonHelper.ToJson<DataTable>(dt));
            }
            return null;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public JsonResult SupplierInfo(string cityid)
        {
            List<StoreEntity> lstStore =string.IsNullOrEmpty(cityid)? StoreService.GetStoreAll(): StoreService.GetStoreAll(cityid);
            return Json(JsonHelper.ToJson<List<StoreEntity>>(lstStore));
        }

        /// <summary>
        ///  用户预约车辆试驾信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="carid"></param>
        /// <returns></returns>
        public JsonResult Reservation(string userid,  string carid)
        {
            ReservationsEntity entity = new ReservationsEntity();
            entity.CustomerID = Convert.ToInt64(userid);
            entity.CarID = Convert.ToInt32(carid);
            entity.RType = "SJ";//SJ:销售试驾 2：ZL：租车预约 DZ：电桩预约
            entity.Status = 0;//0未处理 1：已处理
            bool result = ReservationsService.AddReservations(entity);
            ApiReservationsEntity apiE = new ApiReservationsEntity();
            if (result)
            {
                apiE.code = "200";
                apiE.codeinfo = "汽车试驾预约成功！稍后客服联系您！";
            }
            else
            {
                apiE.code = "201";
                apiE.codeinfo = "预约失败，请稍后再试！";
            }
            return Json(JsonHelper.ToJson(apiE));
        }

        /// <summary>
        ///  用户预约车辆租赁信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="carid"></param>
        /// <returns></returns>
        public JsonResult LeaseReservation(string userid, string carid)
        {
            ReservationsEntity entity = new ReservationsEntity();
            entity.CustomerID = Convert.ToInt64(userid);
            entity.CarID = Convert.ToInt32(carid);
            entity.RType = "ZL";//SJ:销售试驾 2：ZL：租车预约 DZ：电桩预约
            entity.Status = 0;//0未处理 1：已处理
            bool result = ReservationsService.AddReservations(entity);
            ApiReservationsEntity apiE = new ApiReservationsEntity();
            if (result)
            {
                apiE.code = "200";
                apiE.codeinfo = "汽车租赁预约成功！稍后客服联系您！";
            }
            else
            {
                apiE.code = "201";
                apiE.codeinfo = "预约失败，请稍后再试！";
            }
            return Json(JsonHelper.ToJson(apiE));
        }
    }
}
