using Common;
using DataRepository.DataAccess.Order;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.ApiBiz;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService
    {
        private static OrderInfo TranslateOrderInfo(OrderEntity orderEntity)
        {
            OrderInfo info = new OrderInfo();

            if (orderEntity != null)
            {
                info.OrderInnerID = orderEntity.OrderInnerID;
                info.OrderID = orderEntity.OrderID;
                info.PreID = orderEntity.PreID;
                info.CustomerID = orderEntity.CustomerID;
                info.CustomerName = orderEntity.CustomerName;
                info.Mobile = orderEntity.Mobile;
                info.EID = orderEntity.EID;
                info.UseType = orderEntity.UseType;
                info.PayType = orderEntity.PayType;
                info.SupplierID = orderEntity.SupplierID;
                info.SupplierCode = orderEntity.SupplierCode;
                info.LeaseTime = orderEntity.LeaseTime;
                info.Price = orderEntity.Price;
                info.Amount = orderEntity.Amount;
                info.Remark = orderEntity.Remark;
                info.AttachmentIDs = orderEntity.AttachmentIDs;
                info.Status = orderEntity.Status;
                info.ModifyDate = orderEntity.ModifyDate;
                info.Operator = orderEntity.Operator;
            }

            return info;
        }

        private static OrderEntity TranslateOrderEntity(OrderInfo info)
        {
            OrderEntity orderEntity = new OrderEntity();

            if (info != null)
            {
                orderEntity.OrderInnerID = info.OrderInnerID;
                orderEntity.OrderID = info.OrderID;
                orderEntity.PreID = info.PreID;
                orderEntity.CustomerID = info.CustomerID;
                orderEntity.CustomerName = info.CustomerName;
                orderEntity.Mobile = info.Mobile;
                orderEntity.EID = info.EID;
                orderEntity.UseType = info.UseType;
                orderEntity.PayType = info.PayType;
                orderEntity.SupplierID = info.SupplierID;
                orderEntity.SupplierCode = info.SupplierCode;
                orderEntity.LeaseTime = info.LeaseTime;
                orderEntity.Price = info.Price;
                orderEntity.Amount = info.Amount;
                orderEntity.Remark = info.Remark;
                orderEntity.AttachmentIDs = info.AttachmentIDs;
                orderEntity.Status = info.Status;
                orderEntity.ModifyDate = info.ModifyDate;
                orderEntity.Operator = info.Operator;

                CustomerEntity customer = CustomerService.GetCustomerByID(info.CustomerID);
                orderEntity.Customer = customer;

                if ((info.UseType??"").ToLower() == "car")//汽车预约
                {
                    CarEntity car = CarService.GetCarEntityById(info.EID);
                    orderEntity.Car = car;
                }
                else if ((info.UseType ?? "").ToLower() == "chargepile")//充电桩预约
                {
                    ChargingPileEntity chargingPile = ChargeService.GetChargingPileEntityById(info.EID);
                    orderEntity.ChargePile = chargingPile;
                }

                BaseDataEntity pay = BaseDataService.GetBaseDataByPCode("P00").FirstOrDefault(t => t.TypeCode == info.PayType) ?? new BaseDataEntity();
                orderEntity.PayTypeInfo = pay;//支付信息

                StoreEntity store = StoreService.GetStoreById(info.SupplierID);
                orderEntity.Store = store;

                BaseDataEntity use = BaseDataService.GetBaseDataByPCode("U00").FirstOrDefault(t => t.TypeCode == info.UseType) ?? new BaseDataEntity();
                orderEntity.UseTypeDesc = use;//使用方式信息


            }

            return orderEntity;
        }

        public static OrderEntity GetOrderByInnerID(long rid)
        {
            OrderRepository rr = new OrderRepository();
            OrderInfo info = rr.GetOrderInfoByID(rid);
            return TranslateOrderEntity(info);
        }

        public static OrderEntity GetOrderByID(string orderId)
        {
            OrderRepository rr = new OrderRepository();
            OrderInfo info = rr.GetOrderInfoByID(orderId);
            return TranslateOrderEntity(info);
        }

        public static List<OrderEntity> GetOrderByRule(OrderSearchEntity search, PagerInfo pager)
        {
            List<OrderEntity> all = new List<OrderEntity>();
            OrderRepository mr = new OrderRepository();
            List<OrderInfo> miList = mr.GetOrderPagerByRule(search, pager);
            foreach (OrderInfo mInfo in miList)
            {
                OrderEntity entity = TranslateOrderEntity(mInfo);
                all.Add(entity);
            }
            return all;
        }

        public static int GetOrderCount(OrderSearchEntity search)
        {
            return new OrderRepository().GetOrderCount(search);

        }

        public static int EditOrderStatus(long innerid, int status)
        {
            return new OrderRepository().EditOrderStatus(innerid, status);
        }


    }
}
