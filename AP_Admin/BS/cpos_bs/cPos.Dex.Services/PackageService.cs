using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Model;
using cPos.Dex.ContractModel;

namespace cPos.Dex.Services
{
    public class PackageService
    {
        #region Package
        /// <summary>
        /// 获取Package列表
        /// </summary>
        /// <param name="ht">PkgId, PkgTypeId, PkgGenTypeId, UnitId, CustomerId, PkgName, PkgDesc, PkgSeq, PkgStatus, BatId, 
        /// CreateTimeBegin, CreateTimeEnd, CreateUserId, ModifyTimeBegin, ModifyTimeEnd, ModifyUserId, StartRow, EndRow
        /// </param>
        public IList<PackageInfo> GetPackages(Hashtable ht)
        {
            if (!ht.ContainsKey("StartRow") || ht["StartRow"] == null)
                ht["StartRow"] = 0;
            if (!ht.ContainsKey("RowsCount") || ht["RowsCount"] == null)
                ht["RowsCount"] = Dex.Common.Config.QueryDBMaxCount;
            if (!ht.ContainsKey("EndRow") || ht["EndRow"] == null)
                ht["EndRow"] = Convert.ToInt32(ht["StartRow"]) + Convert.ToInt32(ht["RowsCount"]);
            return SqlMapper.Instance().QueryForList<PackageInfo>("PackageInfo.GetPackages", ht);
        }

        // <summary>
        /// 获取Package数量
        /// </summary>
        /// <param name="ht">PkgId, PkgTypeId, PkgGenTypeId, UnitId, CustomerId, PkgName, PkgDesc, PkgSeq, PkgStatus, BatId, 
        /// CreateTimeBegin, CreateTimeEnd, CreateUserId, ModifyTimeBegin, ModifyTimeEnd, ModifyUserId
        /// </param>
        public int GetPackagesCount(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForObject<int>("PackageInfo.GetPackagesCount", ht);
        }

        // <summary>
        /// 查询某数据接口是否已经打过包
        /// </summary>
        public int CheckIsPackaged(string customerId, string pkgTypeCode)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CustomerId", customerId);
            ht.Add("PkgTypeCode", pkgTypeCode);
            return SqlMapper.Instance().QueryForObject<int>("PackageInfo.GetPackagesCount", ht);
        }

        /// <summary>
        /// 通过Code获取Package
        /// </summary>
        public PackageInfo GetPackageByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<PackageInfo>("PackageInfo.GetPackageByCode", code);
        }

        /// <summary>
        /// 通过Id获取Package
        /// </summary>
        public PackageInfo GetPackageById(string id)
        {
            return SqlMapper.Instance().QueryForObject<PackageInfo>("PackageInfo.GetPackageById", id);
        }

        /// <summary>
        /// 检查数据包批次号是否存在
        /// </summary>
        public bool CheckPackageBatId(string batId)
        {
            int count = SqlMapper.Instance().QueryForObject<int>("PackageInfo.GetPackageCountByBatId", batId);
            if (count > 0) return true;
            return false;
        }

        /// <summary>
        /// 检查数据包名称是否存在
        /// </summary>
        public bool CheckPackageName(string unitId, string packageName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("UnitId", unitId);
            ht.Add("PkgName", packageName);
            int count = SqlMapper.Instance().QueryForObject<int>("PackageInfo.GetPackageCountByUnitIdAndPackageName", ht);
            if (count > 0) return true;
            return false;
        }

        /// <summary>
        /// 插入Package
        /// </summary>
        public bool InsertPackage(PackageInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("PackageInfo.InsertPackage", info);
            return true;
        }

        /// <summary>
        /// 更新Package
        /// </summary>
        public bool UpdatePackage(PackageInfo info)
        {
            SqlMapper.Instance().Update("PackageInfo.UpdatePackage", info);
            return true;
        }

        /// <summary>
        /// 发布Package
        /// </summary>
        public bool PublishPackage(PackageInfo info)
        {
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();
            SqlMapper.Instance().Update("PackageInfo.PublishPackage", info);
            return true;
        }

        /// <summary>
        /// 删除Package
        /// </summary>
        public bool DeletePackage(string id)
        {
            SqlMapper.Instance().Update("PackageInfo.DeletePackage", id);
            return true;
        }
        #endregion

        #region PackageFile
        /// <summary>
        /// 获取PackageFile列表
        /// </summary>
        /// <param name="ht">TypeId, TypeCode, TypeName, TypeStatus, StartRow, EndRow</param>
        public IList<PackageFileInfo> GetPackageFiles(Hashtable ht)
        {
            if (!ht.ContainsKey("StartRow") || ht["StartRow"] == null)
                ht["StartRow"] = 0;
            if (!ht.ContainsKey("EndRow") || ht["EndRow"] == null)
                ht["EndRow"] = Dex.Common.Config.QueryDBMaxCount;
            return SqlMapper.Instance().QueryForList<PackageFileInfo>("PackageInfo.GetPackageFiles", ht);
        }

        /// <summary>
        /// 获取PackageFile数量
        /// </summary>
        /// <param name="ht">TypeId, TypeCode, TypeName, TypeStatus</param>
        public int GetPackageFilesCount(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForObject<int>("PackageInfo.GetPackageFilesCount", ht);
        }

        /// <summary>
        /// 通过Name获取PackageFile
        /// </summary>
        public PackageFileInfo GetPackageFileByName(string code)
        {
            return SqlMapper.Instance().QueryForObject<PackageFileInfo>("PackageInfo.GetPackageFileByName", code);
        }

        /// <summary>
        /// 插入PackageFile
        /// </summary>
        public bool InsertPackageFile(PackageFileInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("PackageInfo.InsertPackageFile", info);
            return true;
        }

        /// <summary>
        /// 更新PackageFile
        /// </summary>
        public bool UpdatePackageFile(PackageFileInfo info)
        {
            SqlMapper.Instance().Update("PackageInfo.UpdatePackageFile", info);
            return true;
        }

        /// <summary>
        /// 删除PackageFile
        /// </summary>
        public bool DeletePackageFile(string id)
        {
            SqlMapper.Instance().Update("PackageInfo.DeletePackageFile", id);
            return true;
        }
        #endregion

        #region PackageType
        /// <summary>
        /// 获取PackageType列表
        /// </summary>
        /// <param name="ht">TypeId, TypeCode, TypeName, TypeStatus</param>
        public IList<PackageTypeInfo> GetPackageTypes(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<PackageTypeInfo>("PackageInfo.GetPackageTypes", ht);
        }

        /// <summary>
        /// 通过Code获取PackageType
        /// </summary>
        public PackageTypeInfo GetPackageTypeByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<PackageTypeInfo>("PackageInfo.GetPackageTypeByCode", code);
        }

        /// <summary>
        /// 插入PackageType
        /// </summary>
        public bool InsertPackageType(PackageTypeInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("PackageInfo.InsertPackageType", info);
            return true;
        }

        /// <summary>
        /// 更新PackageType
        /// </summary>
        public bool UpdatePackageType(PackageTypeInfo info)
        {
            SqlMapper.Instance().Update("PackageInfo.UpdatePackageType", info);
            return true;
        }

        /// <summary>
        /// 删除PackageType
        /// </summary>
        public bool DeletePackageType(string id)
        {
            SqlMapper.Instance().Update("PackageInfo.DeletePackageType", id);
            return true;
        }
        #endregion

        #region PackageGenType
        /// <summary>
        /// 获取PackageGenType列表
        /// </summary>
        /// <param name="ht">TypeId, TypeCode, TypeName, TypeStatus</param>
        public IList<PackageGenTypeInfo> GetPackageGenTypes(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<PackageGenTypeInfo>("PackageInfo.GetPackageGenTypes", ht);
        }

        /// <summary>
        /// 通过Code获取PackageGenType
        /// </summary>
        public PackageGenTypeInfo GetPackageGenTypeByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<PackageGenTypeInfo>("PackageInfo.GetPackageGenTypeByCode", code);
        }

        /// <summary>
        /// 插入PackageGenType
        /// </summary>
        public bool InsertPackageGenType(PackageGenTypeInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("PackageInfo.InsertPackageGenType", info);
            return true;
        }

        /// <summary>
        /// 更新PackageGenType
        /// </summary>
        public bool UpdatePackageGenType(PackageGenTypeInfo info)
        {
            SqlMapper.Instance().Update("PackageInfo.UpdatePackageGenType", info);
            return true;
        }

        /// <summary>
        /// 删除PackageGenType
        /// </summary>
        public bool DeletePackageGenType(string id)
        {
            SqlMapper.Instance().Update("PackageInfo.DeletePackageGenType", id);
            return true;
        }
        #endregion

        #region AppType
        /// <summary>
        /// 获取AppType列表
        /// </summary>
        /// <param name="ht">TypeId, TypeCode, TypeName, TypeStatus</param>
        public IList<AppTypeInfo> GetAppTypes(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<AppTypeInfo>("PackageInfo.GetAppTypes", ht);
        }

        /// <summary>
        /// 通过Code获取AppType
        /// </summary>
        public AppTypeInfo GetAppTypeByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<AppTypeInfo>("PackageInfo.GetAppTypeByCode", code);
        }
        #endregion

        #region 创建数据包方法
        /// <summary>
        /// 创建数据包方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_type_code">包类型代码</param>
        /// <param name="customer_id">客户ID</param>
        /// <param name="unit_id">门店ID</param>
        /// <param name="create_user_id">创建人ID</param>
        /// <param name="package_gen_type">包生成方式，包括：MANUAL（手工）、AUTO_TASK（自动任务）。</param>
        /// <param name="package_name">包名称</param>
        /// <param name="user_id">用户ID</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreatePackage(AppType appType, string bat_id, string package_type_code,
            string customer_id, string unit_id, string create_user_id,
            string package_gen_type, string package_name, string user_id, string app_type_code)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageTypeCode", package_type_code);
            ht.Add("CustomerId", customer_id);
            ht.Add("UnitId", unit_id);
            ht.Add("CreateUserId", create_user_id);
            ht.Add("PackageGenType", package_gen_type);
            ht.Add("PackageName", package_name);
            ht.Add("UserId", user_id);

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包类型代码", package_type_code, 1, 30, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            if (package_type_code != PackageTypeMethod.MOBILE.ToString())
            {
                htResult = ErrorService.CheckLength("客户ID", customer_id, 1, 50, false, false, ref paramCheckFlag);
                if (!paramCheckFlag) return htResult;
            }
            htResult = ErrorService.CheckLength("门店ID", unit_id, 0, 50, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", create_user_id, 0, 50, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("数据包生成方式", package_gen_type, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包名称", package_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验包类型代码、包生成方式
            PackageTypeInfo packageTypeObj = null;
            PackageGenTypeInfo packageGenTypeObj = null;
            packageTypeObj = GetPackageTypeByCode(package_type_code);
            packageGenTypeObj = GetPackageGenTypeByCode(package_gen_type);
            if (packageTypeObj == null || packageTypeObj.PkgTypeId.Trim().Length == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包类型不存在";
                return htResult;
            }
            if (packageGenTypeObj == null || packageGenTypeObj.PkgTypeId.Trim().Length == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包生成方式不存在";
                return htResult;
            }
            #endregion

            package_name = CreatePackageName(package_name);

            #region 校验批次号、包名称是否存在
            if (CheckPackageBatId(bat_id))
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A010.ToString();
                htResult["error_desc"] = "批次号已存在";
                return htResult;
            }
            if (package_name != null && package_name.Trim().Length > 0)
            {
                if (CheckPackageName(unit_id, package_name))
                {
                    htResult["status"] = false;
                    htResult["error_code"] = ErrorCode.A010.ToString();
                    htResult["error_desc"] = "包名称已存在";
                    return htResult;
                }
            }
            #endregion

            // 创建数据包对象
            PackageInfo pkgObj = new PackageInfo();
            pkgObj.PkgId = Utils.NewGuid();//随机生成
            pkgObj.BatId = bat_id;
            pkgObj.PkgTypeId = packageTypeObj.PkgTypeId;
            pkgObj.CustomerId = customer_id;
            pkgObj.UnitId = unit_id;
            pkgObj.UserId = user_id;
            pkgObj.CreateUserId = create_user_id;
            pkgObj.PkgGenTypeId = packageGenTypeObj.PkgTypeId;
            pkgObj.PkgName = package_name;
            pkgObj.PkgSeq = GetPackageSeq();
            pkgObj.PkgStatus = "0";
            if (app_type_code != null && app_type_code.Trim().Length > 0)
            {
                AppTypeInfo appTypeInfo = GetAppTypeByCode(app_type_code);
                if (appTypeInfo == null)
                {
                    htResult["status"] = false;
                    htResult["error_code"] = ErrorCode.A006.ToString();
                    htResult["error_desc"] = "未查找到应用程序类型";
                    return htResult;
                }
                pkgObj.AppTypeId = appTypeInfo.AppTypeId;
            }
            bool flag = InsertPackage(pkgObj);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "插入数据包操作失败";
                return htResult;
            }
            htResult["package_id"] = pkgObj.PkgId;
            htResult["status"] = true;
            return htResult;
        }

        public Hashtable CreatePackage(AppType appType, string bat_id, string package_type_code,
            string customer_id, string unit_id, string create_user_id,
            string package_gen_type, string package_name)
        {
            return CreatePackage(appType, bat_id, package_type_code,
                customer_id, unit_id, create_user_id,
                package_gen_type, package_name, null, AppType.POS.ToString());
        }
        #endregion

        #region 发布数据包方法
        /// <summary>
        /// 发布数据包方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable PublishPackage(AppType appType, string bat_id, string package_id, string user_id)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);

            // 校验参数
            bool paramCheckFlag = false;
            #region Check Length
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("发布人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            // 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }

            // 保存
            PackageInfo pubObj = new PackageInfo();
            pubObj.PkgId = package_id;
            if (user_id != null && user_id.Trim().Length > 0)
                pubObj.ModifyUserId = user_id;
            bool flag = PublishPackage(pubObj);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "发布数据包操作失败";
                return htResult;
            }
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建门店用户（多个）配置信息数据包文件方法
        /// <summary>
        /// 创建门店用户（多个）配置信息数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
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
        public Hashtable CreateUsersProfilePackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<MenuModel> menus, IList<RoleModel> roles, IList<RoleMenuModel> role_menus,
            IList<cPos.Model.User.UserInfo> users, IList<cPos.Model.User.UserRoleInfo> user_roles)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("Menus", menus);
            ht.Add("Roles", roles);
            ht.Add("RoleMenus", role_menus);
            ht.Add("Users", users);
            ht.Add("UserRoles", user_roles);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (menus == null || menus.Count == 0)
            {
                menus = new List<MenuModel>();
                //htResult["status"] = false;
                //htResult["error_code"] = ErrorCode.A016.ToString();
                //htResult["error_desc"] = "菜单集合不能为空";
                //return htResult;
            }
            if (roles == null || roles.Count == 0)
            {
                roles = new List<RoleModel>();
                //htResult["status"] = false;
                //htResult["error_code"] = ErrorCode.A016.ToString();
                //htResult["error_desc"] = "角色集合不能为空";
                //return htResult;
            }
            if (role_menus == null || role_menus.Count == 0)
            {
                role_menus = new List<RoleMenuModel>();
                //htResult["status"] = false;
                //htResult["error_code"] = ErrorCode.A016.ToString();
                //htResult["error_desc"] = "角色菜单关系集合不能为空";
                //return htResult;
            }
            if (users == null || users.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "用户集合不能为空";
                return htResult;
            }
            if (user_roles == null || user_roles.Count == 0)
            {
                user_roles = new List<cPos.Model.User.UserRoleInfo>();
                //htResult["status"] = false;
                //htResult["error_code"] = ErrorCode.A016.ToString();
                //htResult["error_desc"] = "用户角色关系集合不能为空";
                //return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new UsersProfileContract();
            // menus
            contract.menus = new List<Menu>();
            foreach (var menu in menus)
            {
                var obj = new Menu();
                obj.menu_id = menu.Menu_Id;
                obj.menu_code = menu.Menu_Code;
                obj.parent_menu_id = menu.Parent_Menu_Id;
                obj.menu_level = Utils.GetStrVal(menu.Menu_Level);
                obj.url_path = menu.Url_Path;
                obj.icon_path = menu.Icon_Path;
                obj.display_index =  Utils.GetStrVal(menu.Display_Index);
                obj.user_flag =  Utils.GetStrVal(menu.User_Flag);
                obj.menu_name = menu.Menu_Name;
                obj.menu_eng_name = menu.Menu_Eng_Name;
                obj.status =  Utils.GetStrVal(menu.Status);
                contract.menus.Add(obj);
            }
            // roles
            contract.roles = new List<Role>();
            foreach (var role in roles)
            {
                var obj = new Role();
                obj.role_id = role.Role_Id;
                obj.role_name = role.Role_Name;
                obj.role_code = role.Role_Code;
                obj.is_sys = Utils.GetStrVal(role.Is_Sys);
                obj.role_eng_name = role.Role_Eng_Name;
                contract.roles.Add(obj);
            }
            // role_menus
            contract.role_menus = new List<RoleMenu>();
            foreach (var role_menu in role_menus)
            {
                var obj = new RoleMenu();
                obj.role_menu_id = role_menu.Role_Menu_Id;
                obj.role_id = role_menu.Role_Id;
                obj.menu_id = role_menu.Menu_Id;
                obj.status = Utils.GetStrVal(role_menu.Status);
                contract.role_menus.Add(obj);
            }
            // users
            contract.users = new List<User>();
            foreach (var user in users)
            {
                var obj = new User();
                obj.user_id = user.User_Id;
                obj.user_code = user.User_Code;
                obj.user_name = user.User_Name;
                obj.user_name_en = user.User_Name_En;
                obj.user_password = user.User_Password;
                obj.user_email = user.User_Email;
                obj.user_mobile = user.User_Cellphone;
                obj.user_tel = user.User_Telephone;
                obj.user_status = user.User_Status;
                obj.user_gender = user.User_Gender;
                obj.user_birthday = user.User_Birthday;
                obj.user_identity = user.User_Identity;
                obj.user_address = user.User_Address;
                obj.user_postcode = user.User_Postcode;
                obj.qq = user.QQ;
                obj.msn = user.MSN;
                obj.blog = user.Blog;
                obj.user_remark = user.User_Remark;
                contract.users.Add(obj);
            }
            // user_role
            contract.user_roles = new List<UserRole>();
            foreach (var user_role in user_roles)
            {
                var obj = new UserRole();
                obj.user_role_id = user_role.Id;
                obj.user_id = user_role.UserId;
                obj.unit_id = user_role.UnitId;
                obj.role_id = user_role.RoleId;
                obj.status = user_role.Status;
                contract.user_roles.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion
            
            // 保存至文件
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}", 
                cfgService.GetUsersProfileFolder(), package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}", 
                fileServerFolder, cfgService.GetUsersProfileFolder(), package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建商品数据包文件方法
        /// <summary>
        /// 创建商品数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="items">商品集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateItemsPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<ItemInfo> items)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("Items", items);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (items == null || items.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "商品集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new ItemsContract();
            // items
            contract.items = new List<Item>();
            foreach (var item in items)
            {
                var obj = new Item();
                obj.item_id = item.Item_Id;
                obj.item_category_id = item.Item_Category_Id;
                obj.item_code = item.Item_Code;
                obj.item_name = item.Item_Name;
                obj.item_name_en = item.Item_Name_En;
                obj.item_name_short = item.Item_Name_Short;
                obj.item_status = item.Status;
                obj.item_remark = item.Item_Remark;
                obj.pyzjm = item.Pyzjm;
                obj.create_user_id = item.Create_User_Id;
                obj.create_time = item.Create_Time;
                obj.modify_user_id = item.Modify_User_Id;
                obj.modify_time = item.Modify_Time;
                obj.if_gifts = Utils.GetStrVal(item.ifgifts);
                obj.if_often = Utils.GetStrVal(item.ifoften);
                obj.if_service = Utils.GetStrVal(item.ifservice);
                obj.isgb = Utils.GetStrVal(item.isGB);
                obj.data_from = Utils.GetStrVal(item.data_from);
                obj.display_index = Utils.GetStrVal(item.display_index);
                obj.image_url = Utils.GetStrVal(item.imageUrl);
                obj.item_props = new List<ItemProp>();
                foreach (var itemProp in item.ItemPropList)
                {
                    var objProp = new ItemProp();
                    objProp.item_id = itemProp.Item_Id;
                    objProp.item_property_id = itemProp.Item_Property_Id;
                    objProp.property_code_group_id = itemProp.PropertyCodeGroupId;
                    objProp.property_code_id = itemProp.PropertyCodeId;
                    objProp.property_detail_id = itemProp.PropertyDetailId;
                    objProp.property_code_value = itemProp.PropertyCodeValue;
                    objProp.status = Utils.GetStrVal(itemProp.Status);
                    objProp.property_code_group_name = itemProp.PropertyCodeGroupName;
                    objProp.property_code_name = itemProp.PropertyCodeName;
                    objProp.create_user_id = itemProp.Create_User_id;
                    objProp.create_time = itemProp.Create_Time;
                    obj.item_props.Add(objProp);
                }
                contract.items.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetItemsFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建图片数据包文件方法
        /// <summary>
        /// 创建图片数据包文件方法新的
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="items">图片集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateObjectImagessPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<ObjectImagesInfo> ObjectImages)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("ObjectImages", ObjectImages);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (ObjectImages == null || ObjectImages.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "图片集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();//来自cPos.Dex.Model
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new ObjectImagesContract();//来自 cPos.Dex.ContractModel
            // ObjectImages
            contract.ObjectImages = new List<ObjectImages>();
            foreach (var item in ObjectImages)//数据转换
            {
                var obj = new ObjectImages();//来自 cPos.Dex.ContractModel
                obj.ImageId = item.ImageId;
                obj.ObjectId = item.ObjectId;
                obj.ImageURL = item.ImageURL;
                obj.DisplayIndex = item.DisplayIndex;
                obj.CreateTime = item.CreateTime.ToString();
                obj.CreateBy = item.CreateBy;
                obj.LastUpdateBy = item.LastUpdateBy;
                obj.LastUpdateTime = item.LastUpdateTime.ToString();
                obj.IsDelete = item.IsDelete;
                obj.CustomerId = item.CustomerId;
                obj.Title = item.Title;
                obj.Description = item.Description;
                obj.IfFlag = item.IfFlag;
               
                //foreach (var itemProp in item.ItemPropList)
                //{
                //    var objProp = new ItemProp();
                //    objProp.item_id = itemProp.Item_Id;
                //    objProp.item_property_id = itemProp.Item_Property_Id;
                //    objProp.property_code_group_id = itemProp.PropertyCodeGroupId;
                //    objProp.property_code_id = itemProp.PropertyCodeId;
                //    objProp.property_detail_id = itemProp.PropertyDetailId;
                //    objProp.property_code_value = itemProp.PropertyCodeValue;
                //    objProp.status = Utils.GetStrVal(itemProp.Status);
                //    objProp.property_code_group_name = itemProp.PropertyCodeGroupName;
                //    objProp.property_code_name = itemProp.PropertyCodeName;
                //    objProp.create_user_id = itemProp.Create_User_id;
                //    objProp.create_time = itemProp.Create_Time;
                //    obj.item_props.Add(objProp);
                //}
                contract.ObjectImages.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetObjectImagesFolder();//这个文件夹地址要 在数据库配置
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion





        #region 创建Sku数据包文件方法
        /// <summary>
        /// 创建Sku数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="skus">Sku集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateSkusPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<SkuInfo> skus)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("Skus", skus);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (skus == null || skus.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "Sku集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new SkusContract();
            // skus
            contract.skus = new List<Sku>();
            foreach (var sku in skus)
            {
                var obj = new Sku();
                obj.sku_id = sku.sku_id;
                obj.item_id = sku.item_id;
                obj.prop_1_detail_id = sku.prop_1_detail_id;
                obj.prop_1_detail_code = sku.prop_1_detail_code;
                obj.prop_1_detail_name = sku.prop_1_detail_name;
                obj.prop_2_detail_id = sku.prop_2_detail_id;
                obj.prop_2_detail_code = sku.prop_2_detail_code;
                obj.prop_2_detail_name = sku.prop_2_detail_name;
                obj.prop_3_detail_id = sku.prop_3_detail_id;
                obj.prop_3_detail_code = sku.prop_3_detail_code;
                obj.prop_3_detail_name = sku.prop_3_detail_name;
                obj.prop_4_detail_id = sku.prop_4_detail_id;
                obj.prop_4_detail_code = sku.prop_4_detail_code;
                obj.prop_4_detail_name = sku.prop_4_detail_name;
                obj.prop_5_detail_id = sku.prop_5_detail_id;
                obj.prop_5_detail_code = sku.prop_5_detail_code;
                obj.prop_5_detail_name = sku.prop_5_detail_name;
                obj.prop_1_id = sku.prop_1_id;
                obj.prop_1_code = sku.prop_1_code;
                obj.prop_1_name = sku.prop_1_name;
                obj.prop_2_id = sku.prop_2_id;
                obj.prop_2_code = sku.prop_2_code;
                obj.prop_2_name = sku.prop_2_name;
                obj.prop_3_id = sku.prop_3_id;
                obj.prop_3_code = sku.prop_3_code;
                obj.prop_3_name = sku.prop_3_name;
                obj.prop_4_id = sku.prop_4_id;
                obj.prop_4_code = sku.prop_4_code;
                obj.prop_4_name = sku.prop_4_name;
                obj.prop_5_id = sku.prop_5_id;
                obj.prop_5_code = sku.prop_5_code;
                obj.prop_5_name = sku.prop_5_name;
                obj.barcode = sku.barcode;
                obj.status = sku.status;
                obj.create_time = sku.create_time;
                obj.create_user_id = sku.create_user_id;
                obj.modify_time = sku.modify_time;
                obj.modify_user_id = sku.modify_user_id;
                
                contract.skus.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetSkusFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建门店数据包文件方法
        /// <summary>
        /// 创建门店数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="units">门店集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateUnitsPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<UnitInfo> units)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("Units", units);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (units == null || units.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "门店集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new UnitsContract();
            // units
            contract.units = new List<Unit>();
            foreach (var unit in units)
            {
                var obj = new Unit();
                obj.unit_id = unit.Id;
                obj.type_id = unit.TypeId;
                obj.unit_code = unit.Code;
                obj.unit_name = unit.Name;
                obj.unit_name_en = unit.EnglishName;
                obj.unit_name_short = unit.ShortName;
                obj.unit_city_id = unit.CityId;
                obj.unit_address = unit.Address;
                obj.unit_contact = unit.Contact;
                obj.unit_tel = unit.Telephone;
                obj.unit_fax = unit.Fax;
                obj.unit_email = unit.Email;
                obj.unit_postcode = unit.Postcode;
                obj.unit_remark = unit.Remark;
                obj.unit_status = unit.Status;
                obj.imager_url = unit.imageURL;
                if (unit.UnitPropertyInfoList != null)
                {
                    obj.unit_props = new List<UnitProp>();
                    foreach (var unitProp in unit.UnitPropertyInfoList)
                    {
                        var objProp = new UnitProp();
                        objProp.id = unitProp.Id;
                        objProp.unit_id = unitProp.UnitId;
                        objProp.property_code_group_id = unitProp.PropertyCodeGroupId;
                        objProp.property_code_id = unitProp.PropertyCodeId;
                        objProp.property_detail_id = unitProp.PropertyDetailId;
                        objProp.property_detail_code = unitProp.PropertyDetailCode;
                        objProp.property_detail_name = unitProp.PropertyDetailName;
                        objProp.status = Utils.GetStrVal(unitProp.Status);
                        objProp.property_code_group_name = unitProp.PropertyCodeGroupName;
                        objProp.property_code_group_code = unitProp.PropertyCodeGroupCode;
                        objProp.property_code_name = unitProp.PropertyCodeName;
                        objProp.property_code_code = unitProp.PropertyCodeCode;
                        objProp.create_user_id = unitProp.Create_User_id;
                        objProp.create_time = unitProp.Create_Time;
                        obj.unit_props.Add(objProp);
                    }
                }
                if (unit.WarehouseInfoList != null)
                {
                    obj.warehouses = new List<Warehouse>();
                    foreach (var warehouse in unit.WarehouseInfoList)
                    {
                        var objWarehouse = new Warehouse();
                        objWarehouse.warehouse_id = warehouse.ID;
                        objWarehouse.wh_code = warehouse.Code;
                        objWarehouse.wh_name = warehouse.Name;
                        objWarehouse.wh_name_en = warehouse.EnglishName;
                        objWarehouse.wh_address = warehouse.Address;
                        objWarehouse.wh_contacter = warehouse.Contacter;
                        objWarehouse.wh_tel = warehouse.Tel;
                        objWarehouse.wh_fax = warehouse.Fax;
                        objWarehouse.wh_status = Utils.GetStrVal(warehouse.Status);
                        objWarehouse.wh_remark = warehouse.Remark;
                        objWarehouse.is_default = Utils.GetStrVal(warehouse.IsDefault);
                        objWarehouse.create_time = Utils.GetStrVal(warehouse.CreateTime);
                        objWarehouse.create_user_id = warehouse.CreateUserID;
                        objWarehouse.modify_time = Utils.GetStrVal(warehouse.ModifyTime);
                        objWarehouse.modify_user_id = warehouse.ModifyUserID;
                        objWarehouse.unit_id = warehouse.Unit.Id;
                        obj.warehouses.Add(objWarehouse);
                    }
                }
                contract.units.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetUnitsFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建商品分类数据包文件方法
        /// <summary>
        /// 创建商品分类数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="items">商品分类集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateItemCategorysPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<ItemCategoryInfo> itemCategorys)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("ItemCategorys", itemCategorys);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (itemCategorys == null || itemCategorys.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new ItemCategorysContract();
            // items
            contract.item_categorys = new List<ItemCategory>();
            foreach (var item in itemCategorys)
            {
                var obj = new ItemCategory();
                obj.item_category_id = item.Item_Category_Id;
                obj.item_category_code = item.Item_Category_Code;
                obj.item_category_name = item.Item_Category_Name;
                obj.status = item.Status;
                obj.create_user_id = item.Create_User_Id;
                obj.create_time = item.Create_Time;
                obj.modify_user_id = item.Modify_User_Id;
                obj.modify_time = item.Modify_Time;
                obj.parent_id = item.Parent_Id;
                obj.pyzjm = item.Pyzjm;
                if (item.DisplayIndex == null || item.DisplayIndex.ToString().Equals(""))
                {
                    obj.display_index = "0";
                }
                else {
                    obj.display_index = item.DisplayIndex.ToString();
                }
                contract.item_categorys.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetItemCategorysFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建SkuProps数据包文件方法
        /// <summary>
        /// 创建SkuProps数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="props">属性集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateSkuPropsPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<SkuPropInfo> props)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("props", props);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (props == null || props.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new SkuPropsContract();
            // skus
            contract.sku_props = new List<SkuProp>();
            foreach (var prop in props)
            {
                var obj = new SkuProp();
                obj.sku_prop_id = prop.sku_prop_id;
                obj.prop_id = prop.prop_id;
                obj.display_index = Utils.GetStrVal(prop.display_index);
                obj.prop_code = prop.prop_code;
                obj.prop_name = prop.prop_name;
                obj.prop_input_flag = prop.prop_input_flag;
                contract.sku_props.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetSkuPropsFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建ItemProps数据包文件方法
        /// <summary>
        /// 创建ItemProps数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="props">属性集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateItemPropsPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<ItemPropInfo> props)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("props", props);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (props == null || props.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new ItemPropsContract();
            // item_props
            contract.item_props = new List<ItemProp>();
            foreach (var prop in props)
            {
                var obj = new ItemProp();
                obj.item_id = prop.Item_Id;
                obj.item_property_id = prop.Item_Property_Id;
                obj.property_code_group_id = prop.PropertyCodeGroupId;
                obj.property_code_id = prop.PropertyCodeId;
                obj.property_detail_id = prop.PropertyDetailId;
                obj.property_code_value = prop.PropertyCodeValue;
                obj.status = Utils.GetStrVal(prop.Status);
                obj.property_code_group_name = prop.PropertyCodeGroupName;
                obj.property_code_name = prop.PropertyCodeName;
                obj.create_user_id = prop.Create_User_id;
                obj.create_time = prop.Create_Time;
                contract.item_props.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetItemPropsFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建ItemPrices数据包文件方法
        /// <summary>
        /// 创建ItemPrices数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="prices">价格集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateItemPricesPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<ItemPriceInfo> prices)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("Prices", prices);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (prices == null || prices.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new ItemPricesContract();
            contract.item_prices = new List<ItemPrice>();
            foreach (var price in prices)
            {
                var obj = new ItemPrice();
                obj.item_price_id = price.item_price_id;
                obj.item_id = price.item_id;
                obj.item_price_type_id = price.item_price_type_id;
                obj.item_price_type_name = price.item_price_type_name;
                obj.item_price = Utils.GetStrVal(price.item_price);
                obj.status = price.status;
                obj.create_user_id = price.create_user_id;
                obj.create_time = price.create_time;
                obj.modify_user_id = price.modify_user_id;
                obj.modify_time = price.modify_time;
                contract.item_prices.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetItemPricesFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建SkuPrices数据包文件方法
        /// <summary>
        /// 创建SkuPrices数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="prices">价格集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateSkuPricesPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<SkuPriceInfo> prices)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("Prices", prices);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (prices == null || prices.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new SkuPricesContract();
            contract.sku_prices = new List<SkuPrice>();
            foreach (var price in prices)
            {
                var obj = new SkuPrice();
                obj.sku_id = price.sku_id;
                obj.item_id = price.item_id;
                obj.unit_id = price.unit_id;
                obj.item_price_type_id = price.item_price_type_id;
                obj.price = Utils.GetStrVal(price.price);
                obj.bat_id = price.bat_id;
                obj.create_user_id = null;
                obj.create_time = null;
                obj.modify_user_id = price.modify_user_id;
                obj.modify_time = price.modify_time;
                contract.sku_prices.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetSkuPricesFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建ItemPropDefs数据包文件方法
        /// <summary>
        /// 创建ItemPropDefs数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="props">属性集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateItemPropDefsPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<PropByItemInfo> props)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("props", props);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (props == null || props.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new ItemPropDefsContract();
            contract.item_prop_defs = new List<ItemPropDef>();
            foreach (var prop in props)
            {
                var obj = new ItemPropDef();
                obj.prop_id = prop.prop_id;
                obj.prop_code = prop.prop_code;
                obj.prop_name = prop.prop_name;
                obj.display_index = Utils.GetStrVal(prop.display_index);
                contract.item_prop_defs.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetItemPropsFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建Mobile客户与门店数据包文件方法
        /// <summary>
        /// 创建Mobile客户与门店数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="customers">客户集合</param>
        /// <param name="units">门店集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateMoblieCustomerAndUnitsPackageFile(
            AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<cPos.Admin.Model.Customer.CustomerInfo> customers,
            IList<cPos.Admin.Model.Customer.CustomerShopInfo> units)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("Customers", customers);
            ht.Add("Units", units);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if ((customers == null && units == null) ||
                (customers == null && units.Count == 0) ||
                (customers.Count == 0 && units == null) ||
                (customers.Count == 0 && units.Count == 0))
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "客户与门店集合不能同时为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new MobileBasicContract();
            // customers
            contract.customers = new List<Customer>();
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    var obj = new Customer();
                    obj.customer_id = customer.ID;
                    obj.customer_code = customer.Code;
                    obj.customer_name = customer.Name;
                    obj.customer_name_en = customer.EnglishName;
                    obj.address = customer.Address;
                    obj.post_code = customer.PostCode;
                    obj.contacter = customer.Contacter;
                    obj.tel = customer.Tel;
                    obj.fax = customer.Fax;
                    obj.email = customer.Email;
                    obj.cell = customer.Cell;
                    obj.memo = customer.Memo;
                    obj.start_date = customer.StartDate;
                    obj.status = customer.Status.ToString();
                    contract.customers.Add(obj);
                }
            }
            // units
            contract.units = new List<Unit>();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    var obj = new Unit();
                    obj.unit_id = unit.ID;
                    obj.unit_code = unit.Code;
                    obj.unit_name = unit.Name;
                    obj.unit_name_en = unit.EnglishName;
                    obj.unit_name_short = unit.ShortName;
                    obj.unit_city_id = unit.City;
                    obj.unit_address = unit.Address;
                    obj.unit_contact = unit.Contact;
                    obj.unit_tel = unit.Tel;
                    obj.unit_fax = unit.Fax;
                    obj.unit_email = unit.Email;
                    obj.unit_postcode = unit.PostCode;
                    obj.unit_remark = unit.Remark;
                    obj.unit_status = unit.Status.ToString();
                    contract.units.Add(obj);
                }
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetMobileFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        #region 创建Mobile Sku数据包文件方法
        /// <summary>
        /// 创建Mobile Sku数据包文件方法
        /// </summary>
        /// <param name="appType">平台类型</param>
        /// <param name="bat_id">批次ID</param>
        /// <param name="package_id">包ID</param>
        /// <param name="user_id">发布人ID</param>
        /// <param name="file_name">包文件名称</param>
        /// <param name="skus">Sku集合</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable CreateMobileSkusPackageFile(AppType appType, string bat_id, string package_id, string user_id,
            string file_name, IList<SkuInfo> skus)
        {
            Hashtable htResult = new Hashtable();
            Hashtable ht = new Hashtable();
            ht.Add("AppType", appType);
            ht.Add("BatId", bat_id);
            ht.Add("PackageId", package_id);
            ht.Add("UserId", user_id);
            ht.Add("FileName", file_name);
            ht.Add("Skus", skus);

            ConfigService cfgService = new ConfigService();

            #region 校验参数
            bool paramCheckFlag = false;
            htResult = ErrorService.CheckLength("批次ID", bat_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包ID", package_id, 1, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("创建人ID", user_id, 0, 50, false, false, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            htResult = ErrorService.CheckLength("包文件名称", file_name, 0, 100, false, true, ref paramCheckFlag);
            if (!paramCheckFlag) return htResult;
            #endregion

            #region 校验数据项
            if (skus == null || skus.Count == 0)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A016.ToString();
                htResult["error_desc"] = "Sku集合不能为空";
                return htResult;
            }
            #endregion

            #region 校验包状态
            PackageInfo pkgObj = new PackageInfo();
            pkgObj = GetPackageById(package_id);
            if (pkgObj == null)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A006.ToString();
                htResult["error_desc"] = "数据包不存在";
                return htResult;
            }
            if (pkgObj.PkgStatus == "1")
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A013.ToString();
                htResult["error_desc"] = "数据包已发布";
                return htResult;
            }
            #endregion

            #region 构建数据包对象
            var contract = new SkusContract();
            // skus
            contract.skus = new List<Sku>();
            foreach (var sku in skus)
            {
                var obj = new Sku();
                obj.sku_id = sku.sku_id;
                obj.item_id = sku.item_id;
                obj.prop_1_detail_id = sku.prop_1_detail_id;
                obj.prop_1_detail_code = sku.prop_1_detail_code;
                obj.prop_1_detail_name = sku.prop_1_detail_name;
                obj.prop_2_detail_id = sku.prop_2_detail_id;
                obj.prop_2_detail_code = sku.prop_2_detail_code;
                obj.prop_2_detail_name = sku.prop_2_detail_name;
                obj.prop_3_detail_id = sku.prop_3_detail_id;
                obj.prop_3_detail_code = sku.prop_3_detail_code;
                obj.prop_3_detail_name = sku.prop_3_detail_name;
                obj.prop_4_detail_id = sku.prop_4_detail_id;
                obj.prop_4_detail_code = sku.prop_4_detail_code;
                obj.prop_4_detail_name = sku.prop_4_detail_name;
                obj.prop_5_detail_id = sku.prop_5_detail_id;
                obj.prop_5_detail_code = sku.prop_5_detail_code;
                obj.prop_5_detail_name = sku.prop_5_detail_name;
                obj.prop_1_id = sku.prop_1_id;
                obj.prop_1_code = sku.prop_1_code;
                obj.prop_1_name = sku.prop_1_name;
                obj.prop_2_id = sku.prop_2_id;
                obj.prop_2_code = sku.prop_2_code;
                obj.prop_2_name = sku.prop_2_name;
                obj.prop_3_id = sku.prop_3_id;
                obj.prop_3_code = sku.prop_3_code;
                obj.prop_3_name = sku.prop_3_name;
                obj.prop_4_id = sku.prop_4_id;
                obj.prop_4_code = sku.prop_4_code;
                obj.prop_4_name = sku.prop_4_name;
                obj.prop_5_id = sku.prop_5_id;
                obj.prop_5_code = sku.prop_5_code;
                obj.prop_5_name = sku.prop_5_name;
                obj.barcode = sku.barcode;
                obj.status = sku.status;
                obj.create_time = sku.create_time;
                obj.create_user_id = sku.create_user_id;
                obj.modify_time = sku.modify_time;
                obj.modify_user_id = sku.modify_user_id;
                
                contract.skus.Add(obj);
            }
            contract.status = Utils.GetStatus(true);
            string contractContent = Json.GetJsonString(contract);
            #endregion

            // 保存至文件
            string selfFolder = cfgService.GetMobileFolder();
            string fileServerFolder = cfgService.GetFileServerFolder();
            string fileName = CreatePackageFileName(file_name);
            string fileFullName = fileName + cfgService.GetPackageFileExtension();
            string filePath = string.Format("/{0}/{1}/{2}",
                selfFolder, package_id, fileFullName);
            string fileFullFolderPath = string.Format("{0}{1}\\{2}",
                fileServerFolder, selfFolder, package_id);
            Utils.SaveFile(fileFullFolderPath, fileFullName, contractContent);

            // 保存
            PackageFileInfo pkgfInfo = new PackageFileInfo();
            pkgfInfo.PkgfId = Utils.NewGuid();
            pkgfInfo.PkgId = package_id;
            pkgfInfo.PkgfName = fileName;
            pkgfInfo.PkgfSeq = GetPackageFileSeq();
            pkgfInfo.PkgfPath = filePath;
            pkgfInfo.PkgfStatus = "0";
            pkgfInfo.CreateTime = Utils.GetNow();
            pkgfInfo.CreateUserId = user_id;
            bool flag = InsertPackageFile(pkgfInfo);
            if (!flag)
            {
                htResult["status"] = false;
                htResult["error_code"] = ErrorCode.A015.ToString();
                htResult["error_desc"] = "数据库插入数据包文件操作失败";
                return htResult;
            }
            htResult["package_file_id"] = pkgfInfo.PkgfId;
            htResult["status"] = true;
            return htResult;
        }
        #endregion

        /// <summary>
        /// 获取数据包名称
        /// </summary>
        public string CreatePackageName(string name)
        {
            if (name != null && name.Trim().Length > 0)
                return name.Trim();
            return Utils.NewGuid();
        }

        /// <summary>
        /// 获取数据包文件名称
        /// </summary>
        public string CreatePackageFileName(string name)
        {
            if (name != null && name.Trim().Length > 0)
                return name.Trim();
            return Utils.NewGuid();
        }

        /// <summary>
        /// 获取数据包序号
        /// </summary>
        public Int64 GetPackageSeq()
        {
            return SqlMapper.Instance().QueryForObject<Int64>("PackageInfo.GetPackageSeq", null);
        }

        /// <summary>
        /// 获取数据包文件序号
        /// </summary>
        public Int64 GetPackageFileSeq()
        {
            return SqlMapper.Instance().QueryForObject<Int64>("PackageInfo.GetPackageFileSeq", null);
        }
    }
}
