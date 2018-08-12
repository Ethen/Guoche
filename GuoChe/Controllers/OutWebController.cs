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

        public ActionResult Register(string Scode, string SalerSource)
        {
            if (string.IsNullOrEmpty(Scode))
            {
                Scode = Request["Scode"];
            }
            if (string.IsNullOrEmpty(SalerSource))
            {
                SalerSource = Request["SalerSource"];
            }
            ViewBag.SourceType = SalerSource;
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
        /// <param name="salerSource">销售源头：门店:Store 业务员:Saler</param>
        /// <returns></returns>
        public JsonResult RegisterNew(string telephone, string vcode, string SourceType, string salesCode)
        {
            telephone = Request["telephone"];
            vcode = Request["vcode"];
            SourceType = Request["SourceType"];
            salesCode = Request["Scode"];
            string password = "123456";
            ApiUserEntity viewE = new ApiUserEntity();
            CustomerEntity chkENtity = CustomerService.GetCustomerByTelephone(telephone);
            if (chkENtity == null)
            {
                //判断验证码是否正确、是否已经过期了
                VerificationCodeEntity VCode = BaseDataService.CheckVerificationCode(telephone, vcode);
                if (string.IsNullOrEmpty(VCode.Mobile))
                {

                    CustomerEntity entity = CustomerService.Register(telephone, EncryptHelper.MD5Encrypt(password), vcode, 4);
                    if (entity != null)
                    {
                        #region 注册成功与业务员建立关系


                      List<SalerRelationEntity> listSaler= SalerService.GetSalerCustomerByTelephone(telephone);
                      if (listSaler != null && listSaler.Count > 0)
                      {
                      }
                      else
                      {

                          //绑定和业务员之间的关系
                          SalerRelationEntity sr = new SalerRelationEntity();
                          sr.SalerCode = salesCode;
                          //sr.SalerID = sid;
                          sr.CustomerID = entity.CustomerID;
                          sr.CustomerCode = entity.CustomerCode;
                          sr.SalerSource = SourceType;
                          SalerService.CreateRelation(sr);

                        #endregion

                          viewE.code = "200";
                          viewE.codeinfo = "注册成功！";
                          viewE.customerEntity = entity;
                      }

                        #region 给客户发送短信
                        SendSMSService.SendRegisterMess(telephone, password);
                        #endregion
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
