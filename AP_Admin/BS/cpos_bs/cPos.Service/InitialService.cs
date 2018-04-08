using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;

using cPos.Model;
using cPos.Model.Exchange;
using cPos.Components.SqlMappers;
using System.Configuration;

namespace cPos.Service
{
    /// <summary>
    /// 初始化
    /// </summary>
    public class InitialService:BaseService
    {
        #region 终端初始化
        /// <summary>
        /// 根据客户号与门店号获取终端初始信息
        /// </summary>
        /// <param name="customer_code"></param>
        /// <param name="unit_code"></param>
        /// <returns></returns>
        public InitialInfo GetPCInitialInfo(string customer_code, string unit_code)
        {
            InitialInfo initialInfo = new InitialInfo();
            //获取客户信息
            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo = GetCustomerInfoByCustomerCode(customer_code);
            if (customerInfo == null || customerInfo.ID.Equals("")) { throw new Exception("客户不存在"); }
            //获取登录数据库
            cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            
            //获取门店
            UnitInfo unitInfo = new UnitInfo();
            if (!customerInfo.ID.Equals(""))
            {
                loggingSessionInfo = GetLoggingSessionInfoByCustomerId(customerInfo.ID);
                unitInfo = new UnitService().GetUnitInfoByCode(loggingSessionInfo, customerInfo.ID, unit_code);
            }
            else {
                throw new Exception("门店不存在");
            }
            //获取仓库
            IList<cPos.Model.Pos.WarehouseInfo> warehouseInfoList = new List<Model.Pos.WarehouseInfo>();
            //获取终端号码
            IList<cPos.Model.Pos.PosUnitInfo> posUnitInfoList = new List<cPos.Model.Pos.PosUnitInfo>(); 
            //获取用户集合
            IList<cPos.Model.User.UserInfo> userInfoList = new List<cPos.Model.User.UserInfo>();
            if (!unitInfo.Id.Equals(""))
            {
                warehouseInfoList = new cPos.Service.PosService().GetWarehouseByUnitID(loggingSessionInfo, unitInfo.Id);
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitID", unitInfo.Id);
                posUnitInfoList = new cPos.Service.PosService().SelectPosUnitList(loggingSessionInfo,_ht);

                cUserService userService = new cUserService();
                userInfoList = userService.GetUserListByUnitId(loggingSessionInfo, unitInfo.Id);
            }
            

            initialInfo.customerInfo = customerInfo;
            initialInfo.unitInfo = unitInfo;
            if (warehouseInfoList.Count > 0)
            {
                initialInfo.warehouseInfo = warehouseInfoList[0];
            }
            else {
                throw new Exception("仓库不存在");
            }
            if (posUnitInfoList.Count > 0)
            {
                initialInfo.posUnitInfo = posUnitInfoList[0];
            }
            else {
                throw new Exception("终端不存在");
            }
            if (userInfoList.Count > 0)
            {
                initialInfo.userInfo = userInfoList[0];
            }
            else {
                throw new Exception("用户不存在");
            }
            return initialInfo;
        }

        /// <summary>
        /// 根据客户号与门店号获取终端初始信息,同时系统自动设置终端号
        /// </summary>
        /// <param name="customer_code">客户号</param>
        /// <param name="unit_code">门店号</param>
        /// <param name="posSN">终端序列号</param>
        /// <returns></returns>
        public InitialInfo GetPCInitialInfo(string customer_code, string unit_code,string posSN)
        {
            InitialInfo initialInfo = new InitialInfo();
            //获取客户信息
            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo = GetCustomerInfoByCustomerCode(customer_code);
            if (customerInfo == null || customerInfo.ID.Equals("")) { throw new Exception("客户不存在"); }
            //获取登录数据库
            cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();

            //获取门店
            UnitInfo unitInfo = new UnitInfo();
            if (!customerInfo.ID.Equals(""))
            {
                loggingSessionInfo = GetLoggingSessionInfoByCustomerId(customerInfo.ID);
                unitInfo = new UnitService().GetUnitInfoByCode(loggingSessionInfo, customerInfo.ID, unit_code);
            }
            else
            {
                throw new Exception("门店不存在");
            }

            #region 添加pos终端以及终端与门店关系
            cPos.Model.Pos.PosInfo posInfo = new cPos.Model.Pos.PosInfo();
            string posNo = new PosService().GetPosNo(customerInfo.ID, unitInfo.Id, posSN, true);
            if (posNo == null || posNo.Equals("")) { throw new Exception("新建POS失败"); }
            #endregion

            //获取仓库
            IList<cPos.Model.Pos.WarehouseInfo> warehouseInfoList = new List<Model.Pos.WarehouseInfo>();
            //获取终端号码
            IList<cPos.Model.Pos.PosUnitInfo> posUnitInfoList = new List<cPos.Model.Pos.PosUnitInfo>();
            //获取用户集合
            IList<cPos.Model.User.UserInfo> userInfoList = new List<cPos.Model.User.UserInfo>();
            if (!unitInfo.Id.Equals(""))
            {
                warehouseInfoList = new cPos.Service.PosService().GetWarehouseByUnitID(loggingSessionInfo, unitInfo.Id);
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitID", unitInfo.Id);
                _ht.Add("PosSN", posSN);
                posUnitInfoList = new cPos.Service.PosService().SelectPosUnitList(loggingSessionInfo, _ht);

                cUserService userService = new cUserService();
                userInfoList = userService.GetUserListByUnitId(loggingSessionInfo, unitInfo.Id);
            }


            initialInfo.customerInfo = customerInfo;
            initialInfo.unitInfo = unitInfo;
            if (warehouseInfoList.Count > 0)
            {
                initialInfo.warehouseInfo = warehouseInfoList[0];
            }
            else
            {
                throw new Exception("仓库不存在");
            }
            if (posUnitInfoList.Count > 0)
            {
                initialInfo.posUnitInfo = posUnitInfoList[0];
            }
            else
            {
                throw new Exception("终端不存在");
            }
            if (userInfoList.Count > 0)
            {
                initialInfo.userInfo = userInfoList[0];
            }
            else
            {
                throw new Exception("用户不存在");
            }
            return initialInfo;
        }
        /// <summary>
        /// 根据客户号，获取客户信息
        /// </summary>
        /// <param name="customer_code">客户号码</param>
        /// <returns></returns>
        public CustomerInfo GetCustomerInfoByCustomerCode(string customer_code)
        {
            try
            {
                CustomerInfo customerInfo = new CustomerInfo();
                cPos.WebServices.AuthManagerWebServices.AuthService AuthWebService = new cPos.WebServices.AuthManagerWebServices.AuthService();
                AuthWebService.Url = ConfigurationManager.AppSettings["sso_url"].ToString() + "/AuthService.asmx";
                string str = AuthWebService.GetCustomerInfo(customer_code);

                customerInfo = (CustomerInfo)cXMLService.Deserialize(str, typeof(CustomerInfo));

                return customerInfo;
            }
            catch (Exception ex) {
                throw new Exception("客户号码不存在");
            }
        }

        #endregion

        #region 业务平台初始化
        /// <summary>
        /// 设置业务平台客户开户初始化
        /// </summary>
        /// <param name="strCustomerInfo">客户信息字符窜</param>
        /// <param name="strUnitInfo">门店信息字符窜</param>
        /// <param name="typeId">处理类型typeId=1(总部与门店一起处理);typeId=2（只处理总部，不处理门店）;typeId=3（只处理门店，不处理总部）</param>
        /// <returns></returns>
        public bool SetBSInitialInfo(string strCustomerInfo,string strUnitInfo,string strMenu,string typeId)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            #region 获取客户信息
            if (!strCustomerInfo.Equals(""))
            {
                customerInfo = (cPos.Model.CustomerInfo)cXMLService.Deserialize(strCustomerInfo, typeof(cPos.Model.CustomerInfo));
                if (customerInfo == null || customerInfo.ID.Equals(""))
                {
                    throw new Exception("客户不存在或者没有获取合法客户信息");

                }
            }
            else {
                throw new Exception("客户信息不存在.");
            }
            #endregion
            //获取连接数据库信息
            
                LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(customerInfo.ID);
           
            #region 判断客户是否已经建立总部
            UnitInfo unitHeadInfo = new UnitService().GetUnitTopByCustomerId(loggingManager, customerInfo.ID);
            if (unitHeadInfo == null || unitHeadInfo.Id.Equals(""))
            {
                typeId = "1";
            }else{
                typeId = "3";
            }
            #endregion

            UnitInfo unitShopInfo = new UnitInfo();//门店
            if (!strUnitInfo.Equals(""))
            {
                unitShopInfo = (cPos.Model.UnitInfo)cXMLService.Deserialize(strUnitInfo, typeof(cPos.Model.UnitInfo));
            }
            else {
                throw new Exception("门店信息不存在.");
            }

            #region 门店是否存在
            UnitInfo unitStoreInfo2 = new UnitInfo();
            try
            {
            unitStoreInfo2 = new UnitService().GetUnitById(loggingManager, unitShopInfo.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("用户插入管理平台失败：" + ex.ToString());
            }
            if (unitStoreInfo2 != null && string.IsNullOrEmpty(unitStoreInfo2.Id))
            {
                throw new Exception("门店信息已经存在.");
            }
            #endregion

            cSqlMapper.Instance(loggingManager).BeginTransaction();
            //try
            //{
                bool bReturn = false;
                #region 处理用户信息
                cPos.Model.User.UserInfo userInfo = new Model.User.UserInfo();
                if (typeId.Equals("1"))
                {
                    userInfo.User_Id = NewGuid(); //用户标识
                    userInfo.customer_id = customerInfo.ID;//客户标识
                    userInfo.User_Code = "admin";
                    userInfo.User_Name = "管理员";
                    userInfo.User_Gender = "1";
                    userInfo.User_Password = "81dc9bdb52d04dc20036dbd8313ed055";
                    userInfo.User_Status = "1";
                    userInfo.User_Status_Desc = "正常";
                    bReturn = new cUserService().SetUserTableInfo(loggingManager, userInfo);
                    if (!bReturn) { throw new Exception("保存用户失败"); }
                }
                else {
                    userInfo = new cUserService().GetUserDefaultByCustomerId(loggingManager,customerInfo.ID);
                }
                #endregion

                #region 处理新建客户总部
                cPos.Model.UnitInfo unitInfo = new Model.UnitInfo();
                if (typeId.Equals("1") || typeId.Equals("2"))
                {
                    unitInfo.Id = NewGuid();
                    unitInfo.TypeId = "2F35F85CF7FF4DF087188A7FB05DED1D";
                    unitInfo.Code = customerInfo.Code + "总部";
                    unitInfo.Name = customerInfo.Name + "总部";
                    unitInfo.CityId = customerInfo.city_id;
                    unitInfo.Status = "1";
                    unitInfo.Status_Desc = "正常";
                    unitInfo.CustomerLevel = 1;
                    unitInfo.customer_id = customerInfo.ID;
                    unitInfo.Parent_Unit_Id = "-99";
                    bReturn = new UnitService().SetUnitTableInfo(loggingManager, unitInfo, userInfo);
                    if (!bReturn) { throw new Exception("新建客户总部失败"); }
                }
                else
                {
                    unitInfo = new UnitService().GetUnitTopByCustomerId(loggingManager, customerInfo.ID);
                }
                #endregion

                #region 处理门店
                cPos.Model.UnitInfo storeInfo = new UnitInfo();
                storeInfo.Id = unitShopInfo.Id;
                storeInfo.TypeId = "EB58F1B053694283B2B7610C9AAD2742";
                storeInfo.Code = customerInfo.Code;
                storeInfo.Name = customerInfo.Name;
                storeInfo.CityId = customerInfo.city_id;
                storeInfo.Status = "1";
                storeInfo.Status_Desc = "正常";
                storeInfo.CustomerLevel = 1;
                storeInfo.customer_id = customerInfo.ID;
                storeInfo.Parent_Unit_Id = unitInfo.Id;
                bReturn = new UnitService().SetUnitTableInfo(loggingManager, storeInfo, userInfo);
                if (!bReturn) { throw new Exception("新建门店失败"); }
                #endregion

                #region 新建角色
                
                cPos.Model.RoleModel roleInfo = new RoleModel();
                if (typeId.Equals("1"))
                {
                    roleInfo.Role_Id = NewGuid();
                    roleInfo.Def_App_Id = "D8C5FF6041AA4EA19D83F924DBF56F93";
                    roleInfo.Role_Code = "Admin";
                    roleInfo.Role_Name = "管理员";
                    roleInfo.Role_Eng_Name = "administrator";
                    roleInfo.Is_Sys = 1;
                    roleInfo.Status = 1;
                    roleInfo.customer_id = customerInfo.ID;
                    roleInfo.Create_User_Id = userInfo.User_Id;
                    roleInfo.Create_Time = GetCurrentDateTime();
                    bReturn = new RoleService().SetRoleInfo(loggingManager, roleInfo);
                    if (!bReturn) { throw new Exception("新建角色失败"); }
                }
                else {
                    roleInfo = new RoleService().GetRoleDefaultByCustomerId(loggingManager, customerInfo.ID);
                }
                #endregion

                #region 插入用户与角色与客户总部关系
                IList<cPos.Model.User.UserRoleInfo> userRoleInfoList = new List<cPos.Model.User.UserRoleInfo>();
                if (typeId.Equals("1") || typeId.Equals("2"))
                {
                    cPos.Model.User.UserRoleInfo userRoleInfo = new Model.User.UserRoleInfo();
                    userRoleInfo.Id = NewGuid();
                    userRoleInfo.UserId = userInfo.User_Id;
                    userRoleInfo.RoleId = roleInfo.Role_Id;
                    userRoleInfo.UnitId = unitInfo.Id;
                    userRoleInfo.Status = "1";
                    userRoleInfo.DefaultFlag = 1;
                    userRoleInfoList.Add(userRoleInfo);
                }
                cPos.Model.User.UserRoleInfo userRoleInfo1 = new Model.User.UserRoleInfo();
                userRoleInfo1.Id = NewGuid();
                userRoleInfo1.UserId = userInfo.User_Id;
                userRoleInfo1.RoleId = roleInfo.Role_Id;
                userRoleInfo1.UnitId = storeInfo.Id;
                userRoleInfo1.Status = "1";
                userRoleInfo1.DefaultFlag = 1;
                userRoleInfoList.Add(userRoleInfo1);
                bReturn = new cUserService().SetUserRoleTableInfo(loggingManager, userRoleInfoList, userInfo);
                if (!bReturn) { throw new Exception("新建角色用户门店关系失败"); }
                #endregion

                #region 添加仓库以及仓库与门店关系
                cPos.Model.Pos.WarehouseInfo warehouseInfo = new cPos.Model.Pos.WarehouseInfo();
                warehouseInfo.ID = NewGuid();
                warehouseInfo.Code = storeInfo.Code + "_wh";
                warehouseInfo.Name = storeInfo.Name + "仓库";
                warehouseInfo.IsDefault = 1;
                warehouseInfo.Status = 1;
                warehouseInfo.CreateUserID = userInfo.User_Id;
                warehouseInfo.CreateTime = Convert.ToDateTime(GetCurrentDateTime());
                warehouseInfo.CreateUserName = userInfo.User_Name;
                warehouseInfo.Unit = storeInfo;

                cPos.Service.PosService posService = new PosService();
                bReturn = posService.InsertWarehouse(loggingManager, warehouseInfo, false);
                if (!bReturn) { throw new Exception("新建仓库失败"); }
                #endregion


                #region 设置菜单信息
                if (typeId.Equals("1") || typeId.Equals("2"))
                {
                    if (!strMenu.Equals(""))
                    {
                        bReturn = new cMenuService().SetMenuInfo(strMenu, customerInfo.ID, false);
                        if (!bReturn) { throw new Exception("新建菜单失败"); }
                    }
                }
                #endregion

                #region 角色与流程关系
                if (typeId.Equals("1"))
                {
                    Hashtable _ht = new Hashtable();
                    _ht.Add("RoleId", roleInfo.Role_Id);
                    bReturn = new cBillService().SetBatBillActionRole(_ht, loggingManager);
                    if (!bReturn) { throw new Exception("创建角色与流程关系失败"); }
                }
                #endregion 

                #region 管理平台--插入客户下的用户信息
                //if (typeId.Equals("1"))
                //{
                //    try
                //    {
                //        bReturn = new cUserService().SetManagerExchangeUserInfo(loggingManager, userInfo, 1);
                //        if (!bReturn) { throw new Exception("用户插入管理平台失败"); }
                //    }
                //    catch (Exception ex) {
                //        cSqlMapper.Instance(loggingManager).RollBackTransaction();
                //        throw new Exception("用户插入管理平台失败：" + ex.ToString());
                //    }
                //}
                #endregion

                #region 管理平台--插入客户下的门店信息

                //bReturn = new UnitService().SetManagerExchangeUnitInfo(loggingManager, storeInfo, 1);
                //if (!bReturn) { throw new Exception("门店插入管理平台失败"); }
                #endregion

                #region 中间层--插入用户，客户关系
                //if (typeId.Equals("1"))
                //{
                //    try
                //    {
                //        string strError = string.Empty;
                //        userInfo.customer_id = customerInfo.ID;
                //        bReturn = new cUserService().SetDexUserCertificate(customerInfo.Code, userInfo, out strError);
                //        if (!bReturn) { throw new Exception(strError); }
                //    }
                //    catch (Exception ex)
                //    {
                //        cSqlMapper.Instance(loggingManager).RollBackTransaction();
                //        throw new Exception("插入中间层失败：" + ex.ToString());
                //    }
                //}
                #endregion

                cSqlMapper.Instance(loggingManager).CommitTransaction();
                return bReturn;

        }

        

        #endregion
    }
}
