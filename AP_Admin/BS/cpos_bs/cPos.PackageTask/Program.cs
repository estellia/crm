using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;

namespace cPos.PackageTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string logFolderPath = @"C:\cpos_log\task\";   //日志文件地址
            try
            {
                int cycleTime = 60000;
                string userId = "0ed1a737a178491c86278b001a059a15";
                string unitId = null; // "1"
                string batId = string.Empty;
                PackageGenTypeMethod packageGenType = PackageGenTypeMethod.AUTO_TASK;
                var exPackageService = new cPos.ExchangeService.DexPackageService();
                var apCustomerService = new cPos.Admin.Service.Implements.CustomerService();
                var apUserService = new cPos.Admin.Service.Implements.UserService();

                while (true)
                {
                    Console.WriteLine(string.Format("[{0}]任务批次开始...", Utils.GetNow()));
                    Common.PackageIds.Clear();

                    // 获取客户列表
                    Hashtable htCustomer = new Hashtable();
                    htCustomer["Status"] = "1";
                    IList<cPos.Admin.Model.Customer.CustomerInfo> customers =
                        apCustomerService.GetAllCustomerList(htCustomer);
                    WorkerType workerType;
                    BaseContract result = new BaseContract();
                    string taskBeginFormat = "[{0}] {2} {1}任务开始...";
                    string taskEndFormat = "[{0}] {2} {1}任务结束";
                    foreach (var customer in customers)
                    {
                        if (!customer.IsPad.Equals(1))
                        {
                            Console.WriteLine(string.Format("[{0} {1}]该客户暂未开放同步功能.", Utils.GetNow(),customer.Code));
                            continue;
                        }
                        //if (customer.Code.Equals("nestle"))//测试单个客户
                        //{

                            // Package - ObjectImages  新增图片
                            if (true)
                            {

                                workerType = WorkerType.ObjectImages;
                                batId = Utils.NewGuid();
                                Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                                var worker = new PackageObjectImages();
                                result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                                Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                            }
                        //}
                   
                            // Package - SkuPrices
                            if (true)
                            {
                                unitId = null;
                                workerType = WorkerType.SkuPrices;
                                batId = Utils.NewGuid();
                                Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                                var worker = new PackageSkuPrices();
                                result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                                Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                            }                          
                          
                            // Package - ItemProps
                            if (true)
                            {
                                workerType = WorkerType.ItemProps;
                                batId = Utils.NewGuid();
                                Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                                var worker = new PackageItemProps();
                                result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                                Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                            }
                            // Package - SkuProps
                            if (true)
                            {
                                workerType = WorkerType.SkuProps;
                                batId = Utils.NewGuid();
                                Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                                var worker = new PackageSkuProps();
                                result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                                Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                            }
                            // Package - Items
                            if (true)
                            {
                                workerType = WorkerType.Items;
                                batId = Utils.NewGuid();
                                Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                                var worker = new PackageItems();
                                result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                                Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                            }
                            // Package - ItemsCategory
                            if (true)
                            {
                                workerType = WorkerType.ItemCategorys;
                                batId = Utils.NewGuid();
                                Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                                var worker = new PackageItemCategorys();
                                result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                                Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                            }

                            // Package - Skus
                            if (true)
                            {
                                workerType = WorkerType.Skus;
                                batId = Utils.NewGuid();
                                Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                                var worker = new PackageSkus();
                                result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                                Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                            }
                            // Package - Units
                            if (true)
                            {
                                workerType = WorkerType.Units;
                                batId = Utils.NewGuid();
                                Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                                var worker = new PackageUnits();
                                result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                                Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                            }
                     

                     

                        //// Package - Users
                        //if (true)
                        //{
                        //    workerType = WorkerType.Users;
                        //    batId = Utils.NewGuid();
                        //    Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                        //    var worker = new PackageUsers();
                        //    result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                        //    Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                        //}

                        #region
                        // Package - ItemPrices
                        //if (false)
                        //{
                        //    workerType = WorkerType.SkuProps;
                        //    batId = Utils.NewGuid();
                        //    Console.WriteLine(string.Format("[{0}] {1}任务开始...", Utils.GetNow(), workerType));
                        //    var worker = new PackageItemPrices();
                        //    result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                        //    Console.WriteLine(string.Format("[{0}] {1}任务结束", Utils.GetNow(), workerType));
                        //}
                        //// Package - Users
                        //if (false)
                        //{
                        //    workerType = WorkerType.Users;
                        //    batId = Utils.NewGuid();
                        //    Console.WriteLine(string.Format("[{0}] {1}任务开始...", Utils.GetNow(), workerType));
                        //    var worker = new PackageUsers();
                        //    result = worker.Run(packageGenType, batId, customer.ID, unitId, userId);
                        //    Console.WriteLine(string.Format("[{0}] {1}任务结束", Utils.GetNow(), workerType));
                        //}

                        //// 获取门店列表
                        //Hashtable htUnit = new Hashtable();
                        //htUnit["CustomerID"] = customer.ID;
                        //htUnit["ShopStatus"] = "1";
                        //IList<MVS.cPos.Model.Customer.CustomerShopInfo> units =
                        //    apCustomerService.GetAllShopList(htUnit);
                        //foreach (var unit in units)
                        //{
                        //    if (true)
                        //    {
                        //        workerType = WorkerType.ItemPrices;
                        //        batId = Utils.NewGuid();
                        //        Console.WriteLine(string.Format("[{0}] {1} {2}任务开始...", 
                        //            Utils.GetNow(), workerType, unit.Code));
                        //        var worker = new PackageItemPrices();
                        //        result = worker.Run(packageGenType, batId, customer.ID, unit.ID, userId);
                        //        Console.WriteLine(string.Format("[{0}] {1} {2}任务结束:{3}.", 
                        //            Utils.GetNow(), workerType, unit.Code, result.status));
                        //    }
                        //} 
                        #endregion
                    }

                    // 导出mobile人员(客户、门店)相关数据
                    if (false)
                    {
                        IList<cPos.Admin.Model.User.UserInfo> mobileUserList = null;
                        Hashtable htMoblieUser = new Hashtable();
                        htMoblieUser["RoleCode"] = "MobileSales";
                        mobileUserList = apUserService.GetUserList(htMoblieUser);
                        if (mobileUserList != null && mobileUserList.Count > 0)
                        {
                            foreach (var mobileUser in mobileUserList)
                            {
                                // Package - Mobile-customers,units
                                if (true)
                                {
                                    unitId = null;
                                    workerType = WorkerType.Mobile;
                                    batId = Utils.NewGuid();
                                    Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType,
                                        "MOBILE-" + mobileUser.Account));
                                    var worker = new PackageMobileUnit();
                                    result = worker.Run(packageGenType, batId, null, null, mobileUser.ID);
                                    Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType,
                                        "MOBILE-" + mobileUser.Account));
                                }
                            }
                        }
                    }

                    // Package - Mobile基础数据
                    if (false)
                    {
                        unitId = null;
                        workerType = WorkerType.Mobile;
                        batId = Utils.NewGuid();
                        Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, "MOBILE"));
                        var worker = new PackageMobile();
                        result = worker.Run(packageGenType, batId, null, unitId, userId);
                        Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, "MOBILE"));
                    }

                    // 发布数据包
                    Console.WriteLine(string.Format("[{0}]数据包发布开始...", Utils.GetNow()));
                    foreach (var packageId in Common.PackageIds)
                    {
                        exPackageService.PublishPackage(batId, packageId, userId);
                    }
                    Console.WriteLine(string.Format("[{0}]数据包发布完成", Utils.GetNow()));

                    Console.WriteLine(string.Format("[{0}]任务批次结束", Utils.GetNow()));
                    Console.WriteLine(string.Format("".PadLeft(50, '=')));
                    Thread.Sleep(cycleTime);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveFile(logFolderPath, Utils.GetNowString() + ".log", ex.ToString());
                Console.Write(ex.ToString());
                Console.Read();
            }
        }
    }

    public enum WorkerType
    {
        Items, Skus, Units, Users, ItemCategorys,
        SkuProps, ItemProps, SkuPrices, ItemPrices,
        Mobile
            , ObjectImages
    }

    public static class Common
    {
        public static IList<string> PackageIds = new List<string>();
    }
}
