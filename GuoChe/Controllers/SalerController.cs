﻿using Common;
using Entity.ViewModel;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class SalerController : BaseController
    {
        public int PAGESIZE = 20;
        public ActionResult Index(string name,int status = -1, int p = 1)
        {
            List <SalerEntity> mList = null;

            int count = SalerService.GetSalerCount(name, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "/Saler";

            mList = SalerService.GetSalerInfoByRule(name??"", status, pager);

            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.Cars = mList;
            ViewBag.Pager = pager;
            return View();
        }

        public ActionResult Edit(string sid)
        {
            if (!string.IsNullOrEmpty(sid))
            {
                ViewBag.Saler = SalerService.GetSalerById(sid.ToLong(0));
            }
            else
            {
                ViewBag.Saler = new SalerEntity();
            }

            return View();
        }

        public void Modify(SalerEntity saler)
        {
            SalerService.ModifySaler(saler);
            Response.Redirect("/Saler/");
        }

        public void Remove(string sid)
        {
            SalerService.RemoveSaler(sid.ToLong(0));
            Response.Redirect("/Saler/");
        }

    }
}
