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
        public JsonResult CarInfo(string carid, string supplierid, string cityid,string modelcode)
        {
            if (string.IsNullOrEmpty(cityid))
            {
                List<CarEntity> lstCar = CarService.GetAllCar(carid, supplierid, true);
                if (lstCar != null && lstCar.Count > 0)
                {
                    if (!string.IsNullOrEmpty(modelcode))
                    {
                        lstCar = lstCar.FindAll(p => p.ModelCode.Equals(modelcode));
                    }
                }
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
        /// supplierType:1 销售 2：租赁 3：租售一体
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult GetHotCarInfo(string supplierType)
        {
            List<CarEntity> lstCar = CarService.GetHotCarInfo(supplierType, true);
            return Json(JsonHelper.ToJson<List<CarEntity>>(lstCar));
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
        /// 获取所有车型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCarType()
        {
            List<BaseDataEntity> lstBaseData = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "C00" && t.Status == 1).ToList();
            return Json(JsonHelper.ToJson<List<BaseDataEntity>>(lstBaseData));
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


        /// <summary>
        /// 根据用户编号获取改用户下发展的客户信息
        /// </summary>
        /// <param name="CustomerCode"></param>
        /// <returns></returns>
        public JsonResult GetCustomerByCustomerCode(string CustomerCode)
        {
            List<SalerRelationEntity> listSalerCustomer = SalerService.GetSalerCustomerBySalerCode(CustomerCode);
            return Json(JsonHelper.ToJson<List<SalerRelationEntity>>(listSalerCustomer));
        }
    }
}
