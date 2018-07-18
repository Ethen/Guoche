using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity.ViewModel;
using Common;
using Service.ApiBiz;
using Service.BaseBiz;
using Service;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace GuoChe.Controllers
{
    public class ApiUserController : Controller
    {
        //
        // GET: /ApiUser/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult Login(string telephone, string password)
        {
            ApiUserEntity viewE = new ApiUserEntity();

            CustomerEntity entity = CustomerService.Login(telephone, EncryptHelper.MD5Encrypt(password));
            if (entity != null)
            {
                viewE.code = "200";
                viewE.codeinfo = "登录成功！";
                viewE.customerEntity = entity;
                viewE.token = Guid.NewGuid().ToString();
            }
            else
            {
                viewE.code = "201";
                viewE.codeinfo = "登录失败！";
                viewE.customerEntity = entity;
                viewE.token = Guid.NewGuid().ToString();
            }


            return Json(JsonHelper.ToJson(viewE));
        }


        /// <summary>
        /// 手机注册返回验证码
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="timeout">默认一分钟</param>
        /// <returns></returns>
        public JsonResult RegisterVCode(string telephone,string timeout="1")
        {
            ApiUserEntity viewE = new ApiUserEntity();
            string vcode= SendSMSService.SendSMSMess(telephone, timeout.ToInt(0));
            if (!string.IsNullOrEmpty(vcode))
            {
                viewE.vcode = vcode;
                viewE.code = "200";
                viewE.codeinfo = "验证码返回成功！";
            }
            else
            {
                viewE.vcode = "";
                viewE.code = "201";
                viewE.codeinfo = "验证码返回失败！";
            }
            
            return Json(JsonHelper.ToJson(viewE));
        }

        [HttpGet]
        public ActionResult Detail(string ID)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                ViewBag.News = NewsService.GetNewsByID(ID.ToInt(0));
            }
            else
            {
                ViewBag.News = new NewsEntity();
            }
            return View();
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="vcode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult Register(string telephone, string vcode, string password)
        {
            ApiUserEntity viewE = new ApiUserEntity();
            CustomerEntity chkENtity = CustomerService.GetCustomerByTelephone(telephone);
            if (chkENtity == null)
            {
                //判断验证码是否正确、是否已经过期了
                VerificationCodeEntity VCode= BaseDataService.CheckVerificationCode(telephone,vcode);
                if (VCode != null)
                {

                    CustomerEntity entity = CustomerService.Register(telephone, EncryptHelper.MD5Encrypt(password), vcode);
                    if (entity != null)
                    {
                        viewE.code = "200";
                        viewE.codeinfo = "注册成功！";
                        viewE.customerEntity = entity;
                    }
                    else
                    {
                        viewE.code = "201";
                        viewE.codeinfo = "注册失败！";
                    }
                }
                else
                {
                    viewE.code = "202";
                    viewE.codeinfo = "验证码已经过期！";
                }
            }
            else
            {
                viewE.code = "203";
                viewE.codeinfo = "手机号已经注册！";
            }

            return Json(JsonHelper.ToJson(viewE));
        }


        /// <summary>
        /// 判断手机号是否已经使用
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public JsonResult CheckTelephone(string telephone)
        {
            ApiUserEntity viewE = new ApiUserEntity();
            CustomerEntity chkENtity = CustomerService.GetCustomerByTelephone(telephone);
            if (chkENtity == null)
            {
                viewE.code = "200";
                viewE.codeinfo = "未使用！";
                viewE.customerEntity = chkENtity;
            }
            else
            {
                viewE.code = "201";
                viewE.codeinfo = "已使用！";
            }
            return Json(JsonHelper.ToJson(viewE));
        }



        /// <summary>
        /// 忘记密码  直接更新数据库中密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="telephone"></param>
        /// <param name="vcode"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public JsonResult Forget(string telephone, string vcode, string newpassword)
        {
            ApiUserEntity viewE = new ApiUserEntity();
            CustomerEntity chkENtity = CustomerService.UpdatePassword(telephone, EncryptHelper.MD5Encrypt(newpassword), vcode);
            if (chkENtity != null)
            {
                viewE.customerEntity = chkENtity;
                viewE.code = "200";
                viewE.codeinfo = "密码修改成功！";
            }
            else
            {
                viewE.code = "201";
                viewE.codeinfo = "密码修改失败！";
            }

            return Json(JsonHelper.ToJson(viewE));
        }

        public JsonResult GetNews(int count, string newsid)
        {
            NewsEntity news = new NewsEntity();
            string jsonstr = string.Empty;
            if (count > 0)
            {
                List<NewsEntity> lstNews = NewsService.GetCountNews(count,true);
                jsonstr = JsonHelper.ToJson<List<NewsEntity>>(lstNews);
            }
            if (!string.IsNullOrEmpty(newsid))
            {
                NewsEntity newe = NewsService.GetNewsByID(newsid.ToInt(0));
                jsonstr = JsonHelper.ToJson(newe);
            }
            return Json(jsonstr);
        }

        /// <summary>
        /// 获取所有有汽车、有充电桩的城市
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllHasCity()
        {
            List<City> lstCity = new List<City>();
            lstCity = BaseDataService.GetAllHasCity();
            return Json(JsonHelper.ToJson<List<City>>(lstCity));
        }

        /// <summary>
        /// 我的预约信息
        /// </summary>
        /// <returns></returns>
        public JsonResult MyReservationOrder(string userid)
        {
            DataTable dt = CarService.MyReservation(userid, "SJ", "MyReservationOrder");
            return Json(JsonHelper.ToJson<DataTable>(dt));
        }

        /// <summary>
        /// 我的租赁信息
        /// </summary>
        /// <returns></returns>
        public JsonResult MyLeaseOrder(string userid)
        {
            DataTable dt = CarService.MyReservation(userid, "ZL", "MyLeaseOrder");
            return Json(JsonHelper.ToJson<DataTable>(dt));
        }

        /// <summary>
        /// 首页图片显示
        /// </summary>
        /// <returns></returns>
        public JsonResult ListImage()
        {
            List<string> lst = new List<string>();
            lst.Add("http://118.31.13.37/Images/app/image1.png");
            lst.Add("http://118.31.13.37/Images/app/image2.png");
            lst.Add("http://118.31.13.37/Images/app/image3.png");
            return Json(JsonHelper.ToJson<List<string>>(lst));
        }


        /// <summary>
        /// 实名认证接口  证件照
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JsonResult CheckCardID(string userid, string data)
        {
            LogHelper.WriteTextLog("HeadImage", "userid:" + userid + "data:" + data, DateTime.Now);
            try
            {
                CustomerExtendEntity entity = new CustomerExtendEntity();
                CustomerExtendEntity customer = CustomerService.AddFile(long.Parse(userid), data, "CD02");//证件照
                return Json(JsonHelper.ToJson(customer));
            }
            catch (Exception ex)
            {
                LogHelper.WriteTextLog("CheckCardID", ex.ToString(), DateTime.Now);    
            }
            return Json("");
            
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JsonResult HeadImage(string userid, string data)
        {
            //LogHelper.WriteTextLog("HeadImage", "userid:" + userid + "data:" + data, DateTime.Now);
            try
            {
                CustomerExtendEntity entity = new CustomerExtendEntity();
                CustomerExtendEntity customer = CustomerService.AddFile(long.Parse(userid), data, "CD05");//用户头像
                return Json(JsonHelper.ToJson(customer));
            }
            catch (Exception ex)
            {
                LogHelper.WriteTextLog("HeadImage", ex.ToString(), DateTime.Now);
            }
            return Json("");
        }

        /// <summary>
        /// 意见反馈
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public JsonResult Feedback(string userid,string remark)
        {
            AdviseEntity aentity=new AdviseEntity();
            aentity.CustomerID=long.Parse(userid);
            aentity.Summary = remark;
            aentity.AdviseTitle = "用户意见反馈";
            AdviseService.CreateNewAdvise(aentity);
            return Json(JsonHelper.ToJson("意见反馈成功！"));
        }

        public void CreateRelation(long sid, string code, string wxcode)
        {
            if (sid > 0 && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(wxcode))
            {
                CustomerEntity exist=CustomerService.GetCustomerByWXCode(wxcode);
                if (exist == null || exist.CustomerID==0)//微信号不存在
                {

                    //注册成为新的用户
                    CustomerExtendEntity extendEntity = new CustomerExtendEntity();
                    extendEntity.CustomerCode = DateTime.Now.ToString("yyyyMMddHHmmss");
                    extendEntity.RegisterTime = DateTime.Now;
                    extendEntity.AuditTime = DateTime.Parse("1753-01-01");
                    extendEntity.WXCode = wxcode;
                    extendEntity.Channel = 4;

                    CustomerEntity customer = new CustomerEntity();
                    customer.CustomerID = extendEntity.CustomerID;
                    customer.CustomerName = "扫码注册客户";
                    customer.CustomerCode = extendEntity.CustomerCode;
                    customer.Channel = extendEntity.Channel;
                    customer.Mobile = extendEntity.Mobile;
                    customer.WXCode = extendEntity.WXCode;
                    customer.Name = "";
                    if (customer.CustomerID < 1)
                    {
                        customer.Password = EncryptHelper.MD5Encrypt("123456");
                    }

                    long customerID=CustomerService.CreateNewCustomer(customer, extendEntity);
                    string customerCode = extendEntity.CustomerCode;


                    //绑定和业务员之间的关系
                    SalerRelationEntity sr = new SalerRelationEntity();
                    sr.SalerCode = code;
                    sr.SalerID = sid;
                    sr.CustomerID = customerID;
                    sr.CustomerCode = customerCode;
                    SalerService.CreateRelation(sr);                  
                }

            }
        }
    }
}
