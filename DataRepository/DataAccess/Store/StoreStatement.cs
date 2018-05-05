using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Store
{
    public class StoreStatement
    {
        public static string GetAllStores = @"SELECT * FROM [StoreInfo](NOLOCK)";

        public static string GetAllStoresByRule = @"SELECT * FROM StoreInfo(NOLOCK) WHERE 1=1 ";

        public static string GetStoreByKey = @"SELECT * FROM StoreInfo(NOLOCK) WHERE SupplierID=@SupplierID";

        public static string Remove = @"UPDATE StoreInfo SET Status=0 WHERE SupplierID=@SupplierID";

        public static string GetStoreByKeys = @"SELECT * FROM StoreInfo(NOLOCK) WHERE SupplierID IN (#ids#)";

        public static string CreateNewStore = @"INSERT INTO [StoreInfo]([SupplierName],[SupplierCode],[SupplierType],[CityID],[Address],[Telephone],[Mobile],[StartTime],[EndTime],[Coordinate],[Status],[CreateDate],[ModifyDate],[Operator],[AttachmentIDs]) 
                                                          VALUES (@SupplierName,@SupplierCode,@SupplierType,@CityID,@Address,@Telephone,@Mobile,@StartTime,@EndTime,@Coordinate,@Status,@CreateDate,@ModifyDate,@Operator,@AttachmentIDs)";

        public static string ModifyStore = @"UPDATE [StoreInfo]
                                               SET SupplierName=@SupplierName,SupplierCode=@SupplierCode,SupplierType=@SupplierType,CityID=@CityID,Address=@Address,
                                                   Telephone=@Telephone,Mobile=@Mobile,StartTime=@StartTime,EndTime=@EndTime,Coordinate=@Coordinate,Status=@Status,
                                                   ModifyDate=@ModifyDate,Operator=@Operator,AttachmentIDs=@AttachmentIDs
                                             WHERE [SupplierID]=@SupplierID";
    }
}
