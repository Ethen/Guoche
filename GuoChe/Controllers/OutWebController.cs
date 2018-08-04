using Common;
using Entity.ViewModel;
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
    public class OutWebController : Controller
    {
        //
        // GET: /OutWeb/

        public ActionResult Register(string Scode)
        {
            if (string.IsNullOrEmpty(Scode))
            {
                Scode = Request["Scode"];
            }
            ViewBag.SCode = Scode;
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="vcode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult RegisterNew(string telephone, string vcode, string password,string salesCode)
        {
            telephone = Request["phone"];
            vcode = Request["code"];
            password = Request["password"];
            ApiUserEntity viewE = new ApiUserEntity();
            CustomerEntity chkENtity = CustomerService.GetCustomerByTelephone(telephone);
            if (chkENtity == null)
            {
                //判断验证码是否正确、是否已经过期了
                VerificationCodeEntity VCode = BaseDataService.CheckVerificationCode(telephone, vcode);
                if (VCode != null)
                {

                    CustomerEntity entity = CustomerService.Register(telephone, EncryptHelper.MD5Encrypt(password), vcode, 4);
                    if (entity != null)
                    {
                        #region 注册成功与业务员建立关系
                        //绑定和业务员之间的关系
                        SalerRelationEntity sr = new SalerRelationEntity();
                        sr.SalerCode = salesCode;
                        //sr.SalerID = sid;
                        sr.CustomerID = entity.CustomerID;
                        sr.CustomerCode = entity.CustomerCode;
                        SalerService.CreateRelation(sr);  

                        #endregion

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
            return Json("ok");            
        }
    }
}
