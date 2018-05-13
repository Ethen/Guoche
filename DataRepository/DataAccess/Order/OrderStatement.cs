using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Order
{
    public class OrderStatement
    {

        public static string GetOrderCount = @"SELECT * FROM [OrderInfo](NOLOCK) WHERE 1=1 ";

        public static string GetOrderByID = @"SELECT TOP 1 * FROM [OrderInfo](NOLOCK) WHERE [OrderID]=@OrderID ";

        public static string GetOrderByInnerID = @"SELECT * FROM [OrderInfo](NOLOCK) WHERE OrderInnerID=@OrderInnerID ";

        public static string GetOrderPagerHeader = @"DECLARE @UP INT
        
	                                                  ---------分页区间计算-------------最大页数
                                                      IF(@recordCount<@PageSize*(@PageIndex-1)) 
                                                      BEGIN
                                                        SET @PageIndex= @recordCount/@PageSize+1
                                                      END
                                                      --最小页数
	                                                  IF(@PageIndex<1)
	                                                     SET @PageIndex=1
                                                      --当前页起始游标值
	                                                  IF(@recordCount>@PageSize*(@PageIndex-1))
	                                                  BEGIN
		                                                  SET @UP=@PageSize*(@PageIndex-1);
		                                                  ---------分页查询-----------
		                                                  WITH order AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status) AS RowNumber FROM (SELECT * FROM OrderInfo WHERE 1=1";

        public static string GetOrderPagerFooter = @")as T ) 
		                                                  SELECT *  FROM order 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
    }
}
