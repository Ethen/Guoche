using Common;
using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Order
{
    public class OrderRepository : DataAccessBase
    {
        public OrderInfo GetOrderInfoByID(long innerId)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.GetOrderByInnerID, "Text"));
            command.AddInputParameter("@OrderInnerID", DbType.Int64, innerId);
            return command.ExecuteEntity<OrderInfo>();
        }

        public OrderInfo GetOrderInfoByID(string orderId)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.GetOrderByID, "Text"));
            command.AddInputParameter("@OrderID", DbType.String, orderId);
            return command.ExecuteEntity<OrderInfo>();
        }


        public List<OrderInfo> GetOrderPagerByRule(OrderSearchEntity serach, PagerInfo pager)
        {
            List<OrderInfo> result = new List<OrderInfo>();


            StringBuilder builder = new StringBuilder();

            if (serach.CustomerID>0)
            {
                builder.Append(" AND CustomerID=@CustomerID ");
            }
            if (serach.SupplierID>0)
            {
                builder.Append(" AND SupplierID =@SupplierID ");
            }
            if (!string.IsNullOrEmpty(serach.OrderID))
            {
                builder.Append(" AND OrderID=@OrderID ");
            }
            if (serach.Status > -2)
            {
                builder.Append(" AND Status =@Status ");
            }

            string sql = OrderStatement.GetOrderPagerHeader + builder.ToString() + OrderStatement.GetOrderPagerFooter;

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sql, "Text"));

            if (serach.CustomerID > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.Int64, serach.CustomerID);
            }
            if (serach.SupplierID > 0)
            {
                command.AddInputParameter("@SupplierID", DbType.Int64, serach.SupplierID);
            }
            if (!string.IsNullOrEmpty(serach.OrderID))
            {
                command.AddInputParameter("@OrderID", DbType.String, serach.OrderID);
            }
            if (serach.Status > -2)
            {
                command.AddInputParameter("@Status", DbType.Int32, serach.Status);
            }
            command.AddInputParameter("@PageIndex", DbType.Int32, pager.PageIndex);
            command.AddInputParameter("@PageSize", DbType.Int32, pager.PageSize);
            command.AddInputParameter("@recordCount", DbType.Int32, pager.SumCount);

            result = command.ExecuteEntityList<OrderInfo>();


            return result;
        }


        public int GetOrderCount(OrderSearchEntity serach)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(OrderStatement.GetOrderCount);
            if (serach.CustomerID > 0)
            {
                builder.Append(" AND CustomerID=@CustomerID ");
            }
            if (serach.SupplierID > 0)
            {
                builder.Append(" AND SupplierID =@SupplierID ");
            }
            if (!string.IsNullOrEmpty(serach.OrderID))
            {
                builder.Append(" AND OrderID=@OrderID ");
            }
            if (serach.Status > -2)
            {
                builder.Append(" AND Status =@Status ");
            }

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(builder.ToString(), "Text"));

            if (serach.CustomerID > 0)
            {
                command.AddInputParameter("@CustomerID", DbType.Int64, serach.CustomerID);
            }
            if (serach.SupplierID > 0)
            {
                command.AddInputParameter("@SupplierID", DbType.Int64, serach.SupplierID);
            }
            if (!string.IsNullOrEmpty(serach.OrderID))
            {
                command.AddInputParameter("@OrderID", DbType.String, serach.OrderID);
            }
            if (serach.Status > -2)
            {
                command.AddInputParameter("@Status", DbType.Int32, serach.Status);
            }


            var o = command.ExecuteScalar<object>();
            return Convert.ToInt32(o);
        }

        public int EditOrderStatus(long oid,int status)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(OrderStatement.EditOrderStatus, "Text"));
            command.AddInputParameter("@OrderInnerID", DbType.Int64, oid);
            command.AddInputParameter("@Status", DbType.Int32, status);
            return command.ExecuteNonQuery();
        }
    }
}
