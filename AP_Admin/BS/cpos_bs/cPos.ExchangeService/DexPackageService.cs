using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Model;
using cPos.Dex.Common;

namespace cPos.ExchangeService
{
    public class DexPackageService : IDexPackageService
    {
        #region D002-创建数据包方法
        /// <summary>
        /// D002-创建数据包方法
        /// </summary>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_type_code">包类型代码</param>
        /// <param name="customer_id">客户ID</param>
        /// <param name="unit_id">门店ID</param>
        /// <param name="create_user_id">创建人ID</param>
        /// <param name="package_gen_type">包生成方式，包括：MANUAL（手工）、AUTO_TASK（自动任务）。</param>
        /// <param name="package_name">包名称</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreatePackage(string bat_id, string package_type_code,
            string customer_id, string unit_id, string create_user_id, 
            string package_gen_type, string package_name)
        {
            var service = new Dex.Services.PackageService();
            return service.CreatePackage(AppType.BS, bat_id, package_type_code,
                customer_id, unit_id, create_user_id,
                package_gen_type, package_name);
        }
        #endregion

        #region D003-发布数据包方法
        /// <summary>
        /// D003-发布数据包方法
        /// </summary>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable PublishPackage(string bat_id, string package_id, string user_id)
        {
            var service = new Dex.Services.PackageService();
            return service.PublishPackage(AppType.BS, bat_id, package_id, user_id);
        }
        #endregion

        #region D006-创建门店用户（多个）配置信息数据包文件方法
        /// <summary>
        /// D006-创建门店用户（多个）配置信息数据包文件方法
        /// </summary>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="menus">菜单集合</param>
        /// <param name="roles">角色集合</param>
        /// <param name="role_menus">角色菜单关系集合</param>
        /// <param name="users">用户集合</param>
        /// <param name="user_roles">用户角色关系集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateUsersProfilePackageFile(string bat_id, string package_id, string user_id,
            string file_name, IList<MenuModel> menus, IList<RoleModel> roles, IList<RoleMenuModel> role_menus,
            IList<cPos.Model.User.UserInfo> users, IList<cPos.Model.User.UserRoleInfo> user_roles)
        {
            var service = new Dex.Services.PackageService();
            return service.CreateUsersProfilePackageFile(AppType.BS, bat_id, package_id, user_id,
                file_name, menus, roles, role_menus,
                users, user_roles);
        }
        #endregion

        #region D010-创建商品数据包文件方法
        /// <summary>
        /// 创建商品数据包文件方法
        /// </summary>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="items">商品集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateItemsPackageFile(string bat_id, string package_id, string user_id,
            string file_name, IList<ItemInfo> items)
        {
            var service = new Dex.Services.PackageService();
            return service.CreateItemsPackageFile(AppType.BS, bat_id, package_id, user_id,
                file_name, items);
        }
        #endregion

        #region D011-创建Sku数据包文件方法
        /// <summary>
        /// 创建Sku数据包文件方法
        /// </summary>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="skus">Sku集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateSkusPackageFile(string bat_id, string package_id, string user_id,
            string file_name, IList<SkuInfo> skus)
        {
            var service = new Dex.Services.PackageService();
            return service.CreateSkusPackageFile(AppType.BS, bat_id, package_id, user_id,
                file_name, skus);
        }
        #endregion

        #region D012-创建Unit数据包文件方法
        /// <summary>
        /// 创建Unit数据包文件方法
        /// </summary>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="units">门店集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateUnitsPackageFile(string bat_id, string package_id, string user_id,
            string file_name, IList<UnitInfo> units)
        {
            var service = new Dex.Services.PackageService();
            return service.CreateUnitsPackageFile(AppType.BS, bat_id, package_id, user_id,
                file_name, units);
        }
        #endregion

        #region D013-创建ItemCategory数据包文件方法
        /// <summary>
        /// 创建ItemCategory数据包文件方法
        /// </summary>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="itemCategorys">商品分类集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateItemCategorysPackageFile(string bat_id, string package_id, string user_id,
            string file_name, IList<ItemCategoryInfo> itemCategorys)
        {
            var service = new Dex.Services.PackageService();
            return service.CreateItemCategorysPackageFile(AppType.BS, bat_id, package_id, user_id,
                file_name, itemCategorys);
        }
        #endregion
    }
}
