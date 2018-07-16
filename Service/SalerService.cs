using Infrastructure.Helper;
using DataRepository.DataAccess.Saler;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Service
{
    public class SalerService:BaseService
    {
        private static SalerInfo TranslateSalerEntity(SalerEntity entity)
        {
            SalerInfo salerInfo = new SalerInfo();
            if (entity != null)
            {
                 salerInfo.SID =entity.SID;
                 salerInfo.SCode =entity.SCode;
                 salerInfo.Name =entity.Name;
                 salerInfo.Sex =entity.Sex;
                 salerInfo.Birthday =entity.Birthday;
                 salerInfo.CertificateType =entity.CertificateType;
                 salerInfo.CertificateNo =entity.CertificateNo;
                 salerInfo.WXCode =entity.WXCode;
                 salerInfo.Mobile =entity.Mobile;
                 salerInfo.Status =entity.Status;
                 salerInfo.ImageURL = entity.ImageURL;
                 salerInfo.CreateDate = entity.CreateDate;
            }

            return salerInfo;
        }

        private static SalerEntity TranslateSalerInfo(SalerInfo salerInfo)
        {
            SalerEntity entity = new SalerEntity();
            if (salerInfo != null)
            {
                entity.SID = salerInfo.SID;
                entity.SCode = salerInfo.SCode;
                entity.Name = salerInfo.Name;
                entity.Sex = salerInfo.Sex;
                entity.Birthday = salerInfo.Birthday;
                entity.CertificateType = salerInfo.CertificateType;
                entity.CertificateNo = salerInfo.CertificateNo;
                entity.WXCode = salerInfo.WXCode;
                entity.Mobile = salerInfo.Mobile;
                entity.Status = salerInfo.Status;
                entity.CreateDate = salerInfo.CreateDate;
                entity.ImageURL = salerInfo.ImageURL;

                if (!string.IsNullOrEmpty(entity.CertificateType))//证件描述
                {
                    BaseDataEntity cardType = BaseDataService.GetBaseDataAll().First(t => t.TypeCode == entity.CertificateType) ?? new BaseDataEntity();
                    entity.CertificateTypeDesc = cardType.Description ?? "";
                }

                AttachmentEntity attachment = new AttachmentEntity();
                if (!string.IsNullOrEmpty(entity.ImageURL))
                {
                    attachment = BaseDataService.GetAttachmentInfoByKyes(entity.ImageURL).FirstOrDefault() ?? new AttachmentEntity();
                }
                entity.Attachment = attachment;

                //todo: 处理关联的用户信息

            }

            return entity;
        }


        public static List<SalerEntity> GetAllSalerEntity()
        {
            List<SalerEntity> all = new List<SalerEntity>();
            SalerRepository mr = new SalerRepository();
            List<SalerInfo> miList = Cache.Get<List<SalerInfo>>("SalerALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetSalerAll();
                Cache.Add("SalerALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (SalerInfo mInfo in miList)
                {
                    SalerEntity salerEntity = TranslateSalerInfo(mInfo);
                    all.Add(salerEntity);
                }
            }

            return all;
        }


        public static SalerEntity GetSalerById(long sid)
        {
            SalerEntity entity = new SalerEntity();
            SalerInfo info = new SalerRepository().GetSalerByID(sid);
            if (info != null)
            {
                entity = TranslateSalerInfo(info);
            }

            return entity;
        }


        public static bool ModifySaler(SalerEntity entity)
        {
            int result = 0;
            if (entity != null)
            {
                SalerRepository mr = new SalerRepository();

                SalerInfo info = TranslateSalerEntity(entity);


                if (entity.SID > 0)
                {
                    result = mr.ModifySaler(info);
                }
                else
                {
                    info.CreateDate = DateTime.Now;
                    result = mr.CreateSaler(info);

                }

                List<SalerInfo> miList = mr.GetSalerAll();//刷新缓存
                Cache.Add("SalerALL", miList);
            }
            return result > 0;
        }


        public static int RemoveSaler(long sid)
        {
            SalerRepository mr = new SalerRepository();
            int r = mr.RemoveSaler(sid);
            List<SalerInfo> miList = mr.GetSalerAll();//刷新缓存
            Cache.Add("ChargingBaseALL", miList);
            
            return r;
        }

        public static int GetSalerCount(string name,int status)
        {
            return new SalerRepository().GetSalerCount(name,status);
        }

        public static List<SalerEntity> GetSalerInfoByRule(string name,int status,PagerInfo pager)
        {
            List<SalerEntity> all = new List<SalerEntity>();
            SalerRepository mr = new SalerRepository();
            List<SalerInfo> miList = mr.GetSaler(name,status, pager);

            if (!miList.IsEmpty())
            {
                foreach (SalerInfo mInfo in miList)
                {
                    SalerEntity storeEntity = TranslateSalerInfo(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }

        public static void CreateRelation(SalerRelationEntity sr)
        {
            if (sr != null)
            {
                SalerRelation sRelation = new SalerRelation();
                sRelation.CustomerCode = sr.CustomerCode;
                sRelation.CustomerID = sr.CustomerID;
                sRelation.SalerCode = sr.SalerCode;
                sRelation.SalerID = sr.SalerID;
                sRelation.Status = 1;
                SalerRepository mr = new SalerRepository();
                mr.CreateSalerRelation(sRelation);
            }
        }


    }
}
