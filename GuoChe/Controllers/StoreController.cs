﻿using Common;
using Infrastructure.Helper;
using Entity.ViewModel;
using Infrastructure.Cache;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace GuoChe.Controllers
{
    public class StoreController : BaseController
    {
        public ActionResult Index(string name,int status=-1)
        {
            List<StoreEntity> mList = null;
            if (!string.IsNullOrEmpty(name) || status > -1)
            {
                mList = StoreService.GetStoreByRule(name, status);
            }
            else
            {
                mList = StoreService.GetStoreAll();
            }
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.Stores = mList; 
            return View();
        }

        public ActionResult Edit(string sid)
        {
            ViewBag.Province = BaseDataService.GetAllProvince();
            ViewBag.MaxPicCount = ConfigurationManager.AppSettings["MaxPicCount"].ToString();
            if (!string.IsNullOrEmpty(sid))
            {
                ViewBag.Store = StoreService.GetStoreById(sid.ToInt(0));
            }
            else
            {
                ViewBag.Store = new StoreEntity();
            }
            
            return View();
        }

        public void Modify(StoreEntity store)
        {
            StoreService.ModifyStore(store);
            Response.Redirect("/Store/");
        }

        public void Remove(string sid)
        {
            StoreService.Remove(sid.ToInt(0));

            Response.Redirect("/Store/");
        } 




    }
}
