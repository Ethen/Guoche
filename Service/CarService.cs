/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CarService
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/7/2018 3:56:26 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using DataRepository.DataAccess.Car;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using Common;

namespace Service
{
    public class CarService:BaseService
    {
        private static CarInfo TranslateCarInfo(CarEntity carEntity)
        {
            CarInfo carInfo = new CarInfo();
            if (carEntity != null)
            {
                carInfo.CarID=carEntity.CarID;
                carInfo.CarName = carEntity.CarName ?? "";
                carInfo.ModelCode = carEntity.ModelCode ?? "";
                carInfo.CarModel = carEntity.CarModel ?? "";
                carInfo.AttachmentIDs = carEntity.AttachmentIDs ?? "";
                carInfo.ContractCode = carEntity.ContractCode ?? "";
                carInfo.AppearanceSize = carEntity.AppearanceSize ?? "";
                carInfo.PlateSize = carEntity.PlateSize ?? "";
                carInfo.Capacity = carEntity.Capacity ?? "";
                carInfo.Slope = carEntity.Slope ?? "";
                carInfo.MaxWeight = carEntity.MaxWeight ?? "";
                carInfo.Wheelbase = carEntity.Wheelbase ?? "";
                carInfo.BatteryCapacity = carEntity.BatteryCapacity ?? "";
                carInfo.Quality = carEntity.Quality ?? "";
                carInfo.Braking = carEntity.Braking ?? "";
                carInfo.MaxSpeed = carEntity.MaxSpeed ?? "";
                carInfo.SiteNum = carEntity.SiteNum ?? "";
                carInfo.BatteryType = carEntity.BatteryType ?? "";
                carInfo.SafeConfigure = carEntity.SafeConfigure ?? "";
                carInfo.OuterConfigure = carEntity.OuterConfigure ?? "";
                carInfo.Renewal = carEntity.Renewal ?? "";
                carInfo.SupplierID = carEntity.SupplierID;
                carInfo.Status = carEntity.Status;
                carInfo.CarLicNumber = carEntity.CarLicNumber ?? "";
                carInfo.ModifyDate = carEntity.ModifyDate;
                carInfo.Operator = carEntity.Operator;
            }

            return carInfo;
        }

        private static CarEntity TranslateCarEntity(CarInfo carInfo)
        {
            CarEntity carEntity = new CarEntity();
            if (carInfo != null)
            {
                carEntity.CarID=carInfo.CarID;
                carEntity.CarName = carInfo.CarName ?? "";
                carEntity.ModelCode = carInfo.ModelCode ?? "";
                carEntity.CarModel = carInfo.CarModel ?? "";
                carEntity.AttachmentIDs = carInfo.AttachmentIDs ?? "";
                carEntity.ContractCode = carInfo.ContractCode ?? "";
                carEntity.AppearanceSize = carInfo.AppearanceSize ?? "";
                carEntity.PlateSize = carInfo.PlateSize ?? "";
                carEntity.Capacity = carInfo.Capacity ?? "";
                carEntity.Slope = carInfo.Slope ?? "";
                carEntity.MaxWeight = carInfo.MaxWeight ?? "";
                carEntity.Wheelbase = carInfo.Wheelbase ?? "";
                carEntity.BatteryCapacity = carInfo.BatteryCapacity ?? "";
                carEntity.Quality = carInfo.Quality ?? "";
                carEntity.Braking = carInfo.Braking ?? "";
                carEntity.MaxSpeed = carInfo.MaxSpeed ?? "";
                carEntity.SiteNum = carInfo.SiteNum ?? "";
                carEntity.BatteryType = carInfo.BatteryType ?? "";
                carEntity.SafeConfigure = carInfo.SafeConfigure ?? "";
                carEntity.OuterConfigure = carInfo.OuterConfigure ?? "";
                carEntity.Renewal = carInfo.Renewal ?? "";
                carEntity.SupplierID =carInfo.SupplierID;
                carEntity.Status =carInfo.Status;
                carEntity.CarLicNumber =carInfo.CarLicNumber;
                carEntity.ModifyDate =carInfo.ModifyDate;
                carEntity.Operator =carInfo.Operator;

                List<AttachmentEntity> attachments=new List<AttachmentEntity>();
                if (!string.IsNullOrEmpty(carEntity.AttachmentIDs))
                {
                    attachments = BaseDataService.GetAttachmentInfoByKyes(carEntity.AttachmentIDs);
                }
                carEntity.AttachmentsInfo = attachments;

                StoreEntity store=StoreService.GetStoreById(carEntity.SupplierID)??new StoreEntity();
                carEntity.Store=store;

                UserEntity user=UserService.GetUserById(carEntity.Operator)??new UserEntity();
                carEntity.OperatorInfo=user;


            }

            return carEntity;
        }

        public static List<CarEntity> GetAllCar()
        {
            List<CarEntity> all = new List<CarEntity>();
            CarRepository mr = new CarRepository();
            List<CarInfo> miList = Cache.Get<List<CarInfo>>("CarALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllCarInfo();
                Cache.Add("CarALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (CarInfo mInfo in miList)
                {
                    CarEntity carEntity = TranslateCarEntity(mInfo);
                    all.Add(carEntity);
                }
            }

            return all;
        }


        public static List<CarEntity> GetAllCar(string carid, string supplierid)
        {
            List<CarEntity> all = new List<CarEntity>();
            CarRepository mr = new CarRepository();
            List<CarInfo> miList = Cache.Get<List<CarInfo>>("CarALL" + carid + supplierid);
            if (miList.IsEmpty())
            {
                miList = mr.GetAllCarInfo(carid, supplierid);
                Cache.Add("CarALL" + carid + supplierid, miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (CarInfo mInfo in miList)
                {
                    CarEntity carEntity = TranslateCarEntity(mInfo);
                    all.Add(carEntity);
                }
            }

            return all;
        }



        public static bool ModifyCar(CarEntity car)
        {
            int result = 0;
            if (car != null)
            {
                CarRepository mr = new CarRepository();

                CarInfo carInfo = TranslateCarInfo(car);


                if (car.CarID > 0)
                {
                    carInfo.ModifyDate = DateTime.Now;
                    result = mr.ModifyCarInfo(carInfo);
                }
                else
                {
                    carInfo.CreateDate = DateTime.Now;
                    carInfo.ModifyDate = DateTime.Now;
                    result = mr.CreateNewCarInfo(carInfo);
                }

                List<CarInfo> miList = mr.GetAllCarInfo();//刷新缓存
                Cache.Add("CarALL", miList);
            }
            return result > 0;
        }

        public static CarEntity GetCarEntityById(long cid)
        {
            CarEntity result = new CarEntity();
            CarRepository mr = new CarRepository();
            CarInfo info = mr.GetCarInfoByKey(cid);
            result = TranslateCarEntity(info);
            return result;
        }

        public static List<CarEntity> GetCarInfoByRule(string name, string mcode, int status,PagerInfo pager)
        {
            List<CarEntity> all = new List<CarEntity>();
            CarRepository mr = new CarRepository();
            List<CarInfo> miList = mr.GetCarInfoByRule(name, mcode, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (CarInfo mInfo in miList)
                {
                    CarEntity storeEntity = TranslateCarEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }

        public static int GetCarCount(string name, string mcode, int status)
        {
            return new CarRepository().GetCarCount(name,mcode,status);
        }

        public static List<CarEntity> GetCarInfoPager(PagerInfo pager)
        {
            List<CarEntity> all = new List<CarEntity>();
            CarRepository mr = new CarRepository();
            List<CarInfo> miList = mr.GetAllCarInfoPager(pager);
            foreach (CarInfo mInfo in miList)
            {
                CarEntity carEntity = TranslateCarEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<CarEntity> GetCarInfoBySupplierID(int supplierID)
        {
            List<CarEntity> all = new List<CarEntity>();
            CarRepository mr = new CarRepository();
            List<CarInfo> miList = mr.GetCarInfoBySupplierID(supplierID);

            if (!miList.IsEmpty())
            {
                foreach (CarInfo mInfo in miList)
                {
                    CarEntity storeEntity = TranslateCarEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }

        public static void RemoveCar(long cid)
        {
            CarRepository mr = new CarRepository();
            mr.RemoveCarInfo(cid);
            List<CarInfo> miList = mr.GetAllCarInfo();
            Cache.Add("CarALL", miList);
        }

    }


}
