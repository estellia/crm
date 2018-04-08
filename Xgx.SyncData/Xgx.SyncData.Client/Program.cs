using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DomainPublishService;
using Zmind.EventBus.Contract;
using OptEnum = Xgx.SyncData.Contract.OptEnum;
using Xgx.SyncData.Contract.Contract;

namespace Xgx.SyncData.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建门店
            //var unitId = Guid.NewGuid().ToString("N");
            //CreateUnit(unitId);
            //更新门店(unitId从数据库表t_unit查)
            //var unitId = "2874c4c6161343c3b0e76371f89e2551";
            //UpdateUnit(unitId);
            //删除门店(unitId从数据库表t_unit查)
            //var unitId = "2874c4c6161343c3b0e76371f89e2551";
            //DeleteUnit(unitId);
            //创建会员
            //var vipId = Guid.NewGuid().ToString("N");
            //CreateVip(vipId);
            //更新会员
            //var vipId = "1b594161c9ef4f30ae7ce593fabe3d2b";
            //UpdateVip(vipId);
            //删除会员
            //var vipId = "1b594161c9ef4f30ae7ce593fabe3d2b";
            //DeleteVip(vipId);
            //创建店员
            //var userId = Guid.NewGuid().ToString("N");
            //CreateUser(userId);
            //更新店员
            //var userId = "166a56cc16864190b3b4e0911771c9b3";
            //UpdateUser(userId);
            //删除店员
            //var userId = "166a56cc16864190b3b4e0911771c9b3";
            //DeleteUser(userId);
            //var service = new UnitDomainService();
            //var msg = new EventContract
            //{
            //    Operation = Zmind.EventBus.Contract.OptEnum.Create,
            //    EntityType = EntityTypeEnum.Unit,
            //    Id = "91c3c91f802b4c9894a43b5a7b4cdbaa"
            //};
            //service.Deal(msg);
            //var service = new UserDomainService();
            //var msg = new EventContract
            //{
            //    Operation = Zmind.EventBus.Contract.OptEnum.Create,
            //    EntityType = EntityTypeEnum.User,
            //    Id = "9551E59E4B8F41FA9BCB22A6A49D46EC"
            //};
            //service.Deal(msg);
            //var service = new VipDomainService();
            //var msg = new EventContract
            //{
            //    Operation = Zmind.EventBus.Contract.OptEnum.Create,
            //    EntityType = EntityTypeEnum.Vip,
            //    Id = "f68c3b5274df41b0a9e7ce3cf6aab230"
            //};
            //service.Deal(msg);
            //var itemContract = new ItemContract();
            //itemContract.Operation = OptEnum.Create;
            //itemContract.ItemId = Guid.NewGuid().ToString("N");
            //itemContract.ItemCategoryId = "000638e95784499e8b7aaf4d0f14d22a";//汽车配件 对应ItemCategoryContract的ItemCategoryId
            //itemContract.ItemCode = "Commodity001";
            //itemContract.ItemName = "轮胎";
            //itemContract.ItemNameEn = "tire";
            //itemContract.ItemNameShort = "轮胎";
            //itemContract.Pyzjm = "luntai";
            //itemContract.ItemRemark = "仅在某某活动期间享受折扣";
            //itemContract.SkuNameIdList = new List<string>
            //{
            //    "0003A9496D144203BE9D763AD2EF81C1",//颜色 对应SkuContract的skuid
            //    "00073c881b7b4f3e8a45a02fce7404b1"//直径 对应SkuContract的skuid
            //};
            //itemContract.ItemDetailList = new List<ItemDetail>();
            //var itemDetail1 = new ItemDetail
            //{
            //    ItemDetailId = Guid.NewGuid().ToString("N"),
            //    SkuValueIdList = new List<string> {
            //        "00105569fcc8c4c310946a7632631f11",//红色 对应SkuValue的SkuValueId
            //        "00123900F59C42E3A2C8494F844CE1B1",//20 对应SkuValue的SkuValueId
            //    },
            //    OriginalPrice = 500.00m,
            //    RetailPrice = 488.00m,
            //    BarCode = "358476"
            //};
            //itemContract.DeliveryList = new List<EnumDelivery> { EnumDelivery.HomeDelivery };
            var itemDetailAmountContract = new ItemDetailAmountContract
            {
                Operation = OptEnum.Update,
                ItemDetailId = "C_1000000000018535",
                Inventory = null,
                SalesVolume = 0,
                CreateTime = null,
                ModifyTime = new DateTime(2016, 11, 24, 18, 57, 0)
            };
            Publish(itemDetailAmountContract);
        }

        private static void CreateUnit(string unitId)
        {
            var msg = new UnitContract
            {
                Operation = OptEnum.Create,
                UnitId = unitId,
                UnitCode = "黄陂南路店(lou测试)",
                UnitName = "黄陂南路店(lou测试)",
                TypeCode = "门店",
                ParentUnitId = "870555a3b1fd4ad6b2f15e599c119c26",
                UnitNameEn = "HPNL(lou test)",
                UnitNameShort = "黄陂南路店(lou测试)",
                City1Name = "浙江省",
                City2Name = "丽水市",
                City3Name = "松阳县",
                UnitAddress = "中山北路1001号",
                UnitContact = "娄振宇",
                UnitTel = "15900695259",
                UnitFax = "021-53198321",
                UnitEmail = "zerglou@126.com",
                UnitPostcode = "200336",
                UnitRemark = "测试门店",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                StoreType = "DirectStore"
            };
            Publish1(msg);
        }

        private static void UpdateUnit(string unitId)
        {
            var msg = new Contract.UnitContract
            {
                Operation = OptEnum.Update,
                UnitId = unitId,
                UnitCode = "黄陂北路店(lou测试)",
                UnitName = "黄陂北路店(lou测试)",
                TypeCode = "门店",
                ParentUnitId = "870555a3b1fd4ad6b2f15e599c119c26",
                UnitNameEn = "HPNL(lou test)",
                UnitNameShort = "黄陂北路店(lou测试)",
                City1Name = "浙江省",
                City2Name = "丽水市",
                City3Name = "松阳县",
                UnitAddress = "中山北路1001号",
                UnitContact = "娄振宇",
                UnitTel = "15900695259",
                UnitFax = "021-53198321",
                UnitEmail = "zerglou@126.com",
                UnitPostcode = "200336",
                UnitRemark = "测试门店",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                StoreType = "DirectStore"
            };
            Publish(msg);
        }

        private static void DeleteUnit(string unitId)
        {
            var msg = new Contract.UnitContract
            {
                Operation = OptEnum.Delete,
                UnitId = unitId,
                UnitCode = "黄陂南路店(lou测试)",
                UnitName = "黄陂南路店(lou测试)",
                TypeCode = "门店",
                ParentUnitId = "870555a3b1fd4ad6b2f15e599c119c26",
                UnitNameEn = "HPNL(lou test)",
                UnitNameShort = "黄陂南路店(lou测试)",
                City1Name = "浙江省",
                City2Name = "丽水市",
                City3Name = "松阳县",
                UnitAddress = "中山北路1001号",
                UnitContact = "娄振宇",
                UnitTel = "15900695259",
                UnitFax = "021-53198321",
                UnitEmail = "zerglou@126.com",
                UnitPostcode = "200336",
                UnitRemark = "测试门店",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                StoreType = "DirectStore"
            };
            Publish(msg);
        }

        private static void CreateVip(string vipId)
        {
            var msg = new VipContract
            {
                Operation = OptEnum.Create,
                VipId = vipId,
                VipName = "娄振宇1",
                VipCode = "lzy",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                VipCardTypeID = 38,
                Phone = "15900695259",
                IdType = "身份证",
                IdNumber = "230231197812030011",
                Birthday = DateTime.Parse("1978-12-03"),
                Gender = 1,
                Email = "zerglou@126.com"
            };
            Publish(msg);
        }

        private static void UpdateVip(string vipId)
        {
            var msg = new VipContract
            {
                Operation = OptEnum.Update,
                VipId = vipId,
                VipName = "娄振宇1",
                VipCode = "lzy",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                //VipLevel = 1,
                Phone = "15900695259",
                IdType = "身份证",
                IdNumber = "230231197812030011",
                Birthday = DateTime.Parse("1978-12-03"),
                Gender = 1,
                Email = "zerglou@126.com"
            };
            Publish(msg);
        }

        private static void DeleteVip(string vipId)
        {
            var msg = new VipContract
            {
                Operation = OptEnum.Delete,
                VipId = vipId,
                VipName = "娄振宇1",
                VipCode = "lzy",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                //VipLevel = 1,
                Phone = "15900695259",
                IdType = "身份证",
                IdNumber = "230231197812030011",
                Birthday = DateTime.Parse("1978-12-03"),
                Gender = 1,
                Email = "zerglou@126.com"
            };
            Publish(msg);
        }

        private static void CreateUser(string userId)
        {
            var msg = new UserContract
            {
                Operation = OptEnum.Create,
                UserId = userId,
                UserCode = "lzy",
                UserName = "娄振宇",
                UserTelephone = "15900695259",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                UnitId = "91c3c91f802b4c9894a43b5a7b4cdbaa",
                RoleCode = new List<RoleEnum> { RoleEnum.clerkAPP }
            };
            Publish(msg);
        }

        private static void UpdateUser(string userId)
        {
            var msg = new UserContract
            {
                Operation = OptEnum.Update,
                UserId = userId,
                UserCode = "lzy",
                UserName = "娄振宇1",
                UserTelephone = "15900695259",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                UnitId = "91c3c91f802b4c9894a43b5a7b4cdbaa",
                RoleCode = new List<RoleEnum> { RoleEnum.clerkAPP }
            };
            Publish(msg);
        }

        private static void DeleteUser(string userId)
        {
            var msg = new UserContract
            {
                Operation = OptEnum.Delete,
                UserId = userId,
                UserCode = "lzy",
                UserName = "娄振宇",
                UserTelephone = "15900695259",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                UnitId = "91c3c91f802b4c9894a43b5a7b4cdbaa",
                RoleCode = new List<RoleEnum> { RoleEnum.clerkAPP }
            };
            Publish(msg);
        }

        private static void Publish<T>(T msg) where T : IXgxToZmind
        {
            var bus = MqBusMgr.GetInstance();
            bus.Publish<IXgxToZmind>(msg);
            bus.Dispose();
        }
        private static void Publish1<T>(T msg) where T : IZmindToXgx
        {
            var bus = MqBusMgr.GetInstance();
            bus.Publish<IZmindToXgx>(msg);
            bus.Dispose();
        }
    }
}
