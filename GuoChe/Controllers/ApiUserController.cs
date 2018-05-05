using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity.ViewModel;
using Common;
using Service.ApiBiz;

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

            CustomerEntity entity= CustomerService.Login(telephone, password);
            if (entity != null)
            {
                viewE.result = true;
                viewE.message = "登录成功！";
                viewE.customerEntity = entity;
                viewE.token = Guid.NewGuid().ToString();
            }
            else
            {
                viewE.result = false;
                viewE.message = "登录失败！";
                viewE.customerEntity = entity;
                viewE.token = Guid.NewGuid().ToString();
            }


            return Json(JsonHelper.ToJson(viewE));
        }


        /// <summary>
        /// 手机注册返回验证码
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public JsonResult RegisterVCode(string telephone)
        {
            ApiUserEntity viewE = new ApiUserEntity();
            viewE.result = true;
            viewE.message = "验证码返回成功！";
            viewE.vcode = "898929";
            return Json(JsonHelper.ToJson(viewE));
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
                CustomerEntity entity = CustomerService.Register(telephone, password, vcode);
                if (entity != null)
                {
                    viewE.result = true;
                    viewE.message = "注册成功！";
                    viewE.customerEntity = entity;
                }
                else
                {
                    viewE.result = false;
                    viewE.message = "注册失败！";
                }
            }
            else
            {
                viewE.result = false;
                viewE.message = "手机号已经注册！";
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
                viewE.result = true;
                viewE.message = "未使用！";
                viewE.customerEntity = chkENtity;
            }
            else
            {
                viewE.result = false;
                viewE.message = "已使用！";
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
        public JsonResult Forget(string userid, string telephone, string vcode, string newpassword)
        {
            ApiUserEntity viewE = new ApiUserEntity();
            CustomerEntity chkENtity = CustomerService.UpdatePassword(telephone, newpassword, vcode);
            if (chkENtity != null)
            {
                viewE.customerEntity = chkENtity;
                viewE.result = true;
                viewE.message = "密码修改成功！";
            }
            else
            {
                viewE.result = false;
                viewE.message = "密码修改失败！";
            }

            return Json(JsonHelper.ToJson(viewE));
        }
    }
}
