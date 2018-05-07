/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CarStatement
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/7/2018 3:09:41 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Car
{
    public class CarStatement
    {
        public static string GetAllCarInfo = @"SELECT * FROM [CarInfo](NOLOCK) ORDER BY Status DESC ";

        public static string GetAllCarInfoByID = @"SELECT * FROM [CarInfo](NOLOCK) WHERE CarID=@CarID";

        public static string GetCarInfoBySupplierID = @"SELECT * FROM [CarInfo](NOLOCK) WHERE SupplierID=@SupplierID";

        public static string CreateNewCarInfo = @"INSERT INTO [CarInfo]([CarName],[ModelCode],[CarModel],[AttachmentIDs],[ContractCode],[AppearanceSize],[PlateSize],[Capacity],[Slope],[MaxWeight],[Wheelbase],[BatteryCapacity],[Quality],[Braking],[MaxSpeed],[SiteNum],[BatteryType],[SafeConfigure],[OuterConfigure],[Renewal],[SupplierID],[Status],[CarLicNumber],[CreateDate],[ModifyDate],[Operator]) 
                                                     VALUES (@CarName,@Modelcodel,@CarModel,@AttachmentIDs,@ContractCode,@AppearanceSize,@PlateSize,@Capacity,@Slope,@MaxWeight,@Wheelbase,@BatteryCapacity,@Quality,@Braking,@MaxSpeed,@SiteNum,@BatteryType,@SafeConfigure,@OuterConfigure,@Renewal,@SupplierID,@Status,@CarLicNumber,@CreateDate,@ModifyDate,@Operator)";

        public static string ModifyCarInfo = @"UPDATE [CarInfo] SET CarName=@CarName,ModelCode=@ModelCode,CarModel=@CarModel,AttachmentIDs=@AttachmentIDs,ContractCode=@ContractCode,AppearanceSize=@AppearanceSize,PlateSize=@PlateSize,Capacity=@Capacity,Slope=@Slope,
                                                  MaxWeight=@MaxWeight,Wheelbase=@Wheelbase,BatteryCapacity=@BatteryCapacity,Quality=@Quality,Braking=@Braking,MaxSpeed=@MaxSpeed,SiteNum=@SiteNum,BatteryType=@BatteryType,SafeConfigure=@SafeConfigure,OuterConfigure=@OuterConfigure,Renewal=@Renewal,SupplierID=@SupplierID,Status=@Status,CarLicNumber=@CarLicNumber,ModifyDate=@ModifyDate,Operator=@Operator
                                                   WHERE CarID=@CarID";

        public static string RemoveCarInfo = @"UPDATE [CarInfo] SET Status=0 WHERE CarID=@CarID";

        public static string GetAllCarInfoByRule = @"SELECT * FROM [CarInfo](NOLOCK) WHERE 1=1 ";


    }
}
