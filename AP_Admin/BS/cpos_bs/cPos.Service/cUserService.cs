using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using cPos.Model;
using cPos.Model.User;
//using cPos.SqlHelper;
using IBatisNet.DataMapper;
using cPos.Components.SqlMappers;
using cPos.Components;
using cPos.Service;



namespace cPos.Service
{
    /// <summary>
    /// 用户管理类
    /// </summary>
    public class cUserService : BaseService
    {
        //#region 用户

        #region  用户查询
        /// <summary>
        /// 用户查询,根据条件返回用户查询信息与查询结果总记录数
        /// </summary>
        /// <param name="loggingSession">登录用户信息类</param>
        /// <param name="User_Name">用户名</param>
        /// <param name="User_Code">用户工号</param>
        /// <param name="CellPhone">手机</param>
        /// <param name="User_Status">状态</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns></returns>
        public UserInfo SearchUserList(LoggingSessionInfo loggingSession, string User_Name, string User_Code, string CellPhone, string User_Status, int maxRowCount, int startRowIndex)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("UserCode", User_Code);
            hashtable.Add("UserName", User_Name);
            hashtable.Add("CellPhone", CellPhone);
            hashtable.Add("UserStatus", User_Status);
            hashtable.Add("LoginUserId", loggingSession.CurrentUser.User_Id.ToString());
            hashtable.Add("StartRow", startRowIndex);
            hashtable.Add("EndRow", startRowIndex + maxRowCount);
            hashtable.Add("CustomerId", loggingSession.CurrentLoggingManager.Customer_Id);

            UserInfo userInfo = new UserInfo();
            int iCount = cSqlMapper.Instance().QueryForObject<int>("User.SelectUsersCount", hashtable);

            IList<UserInfo> userInfoList = new List<UserInfo>();
            userInfoList = cSqlMapper.Instance().QueryForList<UserInfo>("User.SelectUsers", hashtable);

            userInfo.ICount = iCount;
            userInfo.UserInfoList = userInfoList;

            return userInfo;
        }
        #endregion

        #region 根据用户的Id获取用户信息
        /// <summary>
        /// 根据用户的Id获取用户信息
        /// </summary>
        /// <param name="LoggingSessionInfo">用户登录信息类</param>
        /// <param name="user_id">用户标识</param>
        /// <returns></returns>
        public UserInfo GetUserById(LoggingSessionInfo LoggingSessionInfo, string user_id)
        {
            return (UserInfo)cSqlMapper.Instance(LoggingSessionInfo.CurrentLoggingManager).QueryForObject("User.SelectById", user_id);
        }
        #endregion

        #region 根据客户获取默认用户
        /// <summary>
        /// 根据客户获取默认用户
        /// </summary>
        /// <param name="loggingManager"></param>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public UserInfo GetUserDefaultByCustomerId(LoggingManager loggingManager, string customer_id)
        {
            return (UserInfo)cSqlMapper.Instance(loggingManager).QueryForObject("User.SelectUserDefaultByCustomerId", customer_id);
        }

        #endregion

        #region 获取用户在某种角色下的缺省的单位
        /// <summary>
        /// 获取用户在某种角色下的缺省的单位
        /// </summary>
        /// <param name="userId">用户的Id</param>
        /// <param name="roleId">角色的Id</param>
        /// <returns>单位Id</returns>
        public string GetDefaultUnitByUserIdAndRoleId(string userId, string roleId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("UserId", userId);
            hashTable.Add("RoleId", roleId);
            
            IList<UserRoleInfo> userRoleList = cSqlMapper.Instance().QueryForList<UserRoleInfo>("UserRole.SelectDefaultUnitByUserIdAndRoleId", hashTable);
            if (userRoleList == null || userRoleList.Count == 0)
            {
                return null;
            }
            else
            {
                return userRoleList[0].UnitId;
            }
        }

        #endregion

        #region 判断用户是否存在
        /// <summary>
        /// 判断用户标识是否存在
        /// </summary>
        /// <param name="loggingManager">登录model</param>
        /// <returns></returns>
        public bool IsExistUser(LoggingManager loggingManager)
        {
            int n = cSqlMapper.Instance(loggingManager).QueryForObject<int>("User.IsExsitUser", loggingManager.User_Id);
            return n > 0 ? true : false;
        }

        /// <summary>
        /// 判断用户号码是否存在
        /// </summary>
        /// <param name="loggingSessionInfo">用户登录信息</param>
        /// <param name="user_code">用户号码</param>
        /// <param name="user_id">用户标识</param>
        /// <returns></returns>
        public bool IsExistUserCode(LoggingSessionInfo loggingSessionInfo, string user_code, string user_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("User_Code", user_code);
                _ht.Add("User_Id", user_id);
                _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
                int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("User.IsExsitUserCode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex) {
                throw (ex); 
            }
        }

        #endregion

        #region 用户修改新建保存
        /// <summary>
        /// 用户保存
        /// </summary>
        /// <param name="loggingSessionInfo">登录用户信息</param>
        /// <param name="userInfo">处理的用户类</param>
        /// <param name="userRoleInfos">用户角色组织关系类集合</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public bool SetUserInfo(LoggingSessionInfo loggingSessionInfo, UserInfo userInfo, IList<UserRoleInfo> userRoleInfos,out string strError) 
        {
            string strResult = string.Empty;
            bool bReturn = true;
            //事物信息
            cSqlMapper.Instance().BeginTransaction();
            try
            {
                userInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                //处理是新建还是修改
                string strDo = string.Empty;
                if (userInfo.User_Id == null || userInfo.User_Id.Equals(""))
                {
                    userInfo.User_Id = NewGuid();
                    strDo = "Create";
                    userInfo.User_Password = EncryptManager.Hash(userInfo.User_Password, HashProviderType.MD5);
                }
                else {
                    strDo = "Modify";
                }
                //1 判断用户号码是否唯一
                if(!IsExistUserCode(loggingSessionInfo,userInfo.User_Code,userInfo.User_Id))
                {
                    strError = "用户号码已经存在。";
                    bReturn = false;
                    return bReturn;
                }
                //2.提交用户信息至表单
                if (strDo.Equals("Create"))
                {
                    if (!SetUserInsertBill(loggingSessionInfo,userInfo))
                    {
                        strError = "用户表单提交失败。";
                        bReturn = false;
                        return bReturn;
                    }
                }

                //3.获取用户表单信息,设置用户状态与状态描述
                BillModel billInfo = new cBillService().GetBillById(loggingSessionInfo, userInfo.User_Id);
                userInfo.User_Status = billInfo.Status;
                userInfo.User_Status_Desc = billInfo.BillStatusDescription;

                //4.提交用户信息
                if (!SetUserTableInfo(loggingSessionInfo, userInfo))
                {
                    strError = "提交用户表失败";
                    bReturn = false;
                    return bReturn;
                }
                //5.提交用户与角色与组织关系
                if (!SetUserRoleTableInfo(loggingSessionInfo, userRoleInfos, userInfo))
                {
                    strError = "提交用户与角色,组织表失败";
                    bReturn = false;
                    return bReturn;
                }
#if SYN_AP
                // 提交管理平台
                if (!SetManagerExchangeUserInfo(loggingSessionInfo, userInfo, strDo == "Create" ? 1 : 2))
                {
                    strError =  "提交管理平台失败";
                    bReturn = false;
                    return bReturn;
                }
#endif

#if SYN_DEX
                // 提交接口
                if (strDo.Equals("Create"))
                {
                    strResult = SetDexUserCertificate(loggingSessionInfo, userInfo);
                    if (!(strResult.Equals("True") || strResult.Equals("true")))
                    {
                        cSqlMapper.Instance().RollBackTransaction();
                        strError =  "提交接口失败";
                        bReturn = false;
                        return bReturn;
                    }
                }else{
                    bool bResult = SetDexUpdateUserCertificate(loggingSessionInfo, userInfo, out strError);
                    if (!bResult)
                    {
                        cSqlMapper.Instance().RollBackTransaction();
                        return false;
                    }
                }
#endif

                cSqlMapper.Instance().CommitTransaction();
                strError = "保存成功!";
                return bReturn; 
            }
            catch (Exception ex) {
                cSqlMapper.Instance().RollBackTransaction();
                strError = ex.ToString();
                throw (ex);    
            }
            
        }

        /// <summary>
        /// 用户表单提交
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="userInfo">用户model</param>
        /// <returns></returns>
        private bool SetUserInsertBill(LoggingSessionInfo loggingSessionInfo,UserInfo userInfo)
        {
            try
            {
                cPos.Model.BillModel bill = new BillModel();
                cPos.Service.cBillService bs = new cBillService();

                bill.Id = userInfo.User_Id;//order_id
                string order_type_id = bs.GetBillKindByCode(loggingSessionInfo, "USERMANAGER").Id.ToString(); //loggingSession, OrderType
                bill.Code = bs.GetBillNextCode(loggingSessionInfo,BillKindModel.CODE_USER_NEW); //BillKindCode
                bill.KindId = order_type_id;
                bill.UnitId = loggingSessionInfo.CurrentUserRole.UnitId;
                bill.AddUserId = loggingSessionInfo.CurrentUser.User_Id;

                BillOperateStateService state = bs.InsertBill(loggingSessionInfo, bill);

                if (state == BillOperateStateService.CreateSuccessful)
                {
                    return true;
                }
                else {
                    throw (new System.Exception(state.ToString()));
                }
                
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 提交用户表信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private bool SetUserTableInfo(LoggingSessionInfo loggingSessionInfo, UserInfo userInfo)
        {
            try
            {
                if (userInfo != null)
                {
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("User.InsertOrUpdate", userInfo);
                }
                return true;
            }
            catch (Exception ex) {
                return false;
                throw (ex);
            }
        }

        /// <summary>
        /// 提交用户表信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool SetUserTableInfo(LoggingManager loggingSessionInfo, UserInfo userInfo)
        {
            try
            {
                if (userInfo != null)
                {
                    cSqlMapper.Instance(loggingSessionInfo).Update("User.InsertOrUpdate", userInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }

        /// <summary>
        /// 设置用户与角色与组织关系
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userRoleInfos"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private bool SetUserRoleTableInfo(LoggingSessionInfo loggingSessionInfo, IList<UserRoleInfo> userRoleInfos,UserInfo userInfo)
        {
            try
            {
                
                foreach (UserRoleInfo userRoleInfo in userRoleInfos)
                {
                    if (userRoleInfo.UserId == null || userRoleInfo.UserId.Equals(""))
                        userRoleInfo.UserId = userInfo.User_Id;
                    if (userRoleInfo.Id == null || userRoleInfo.Id.Equals(""))
                        userRoleInfo.Id = NewGuid();
                }
                UserInfo us = new UserInfo();
                us.User_Id = loggingSessionInfo.CurrentUser.User_Id;
                us.Modify_Time = GetCurrentDateTime();
                us.Create_Time = GetCurrentDateTime();
                us.userRoleInfoList = userRoleInfos;
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("UserRole.InsertOrUpdate", us);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }
        /// <summary>
        /// 设置用户与角色与组织关系
        /// </summary>
        /// <param name="loggingManager">数据库连接对象</param>
        /// <param name="userRoleInfos">用户与角色与门店对象集合</param>
        /// <param name="userInfo">用户对象</param>
        /// <returns></returns>
        public bool SetUserRoleTableInfo(LoggingManager loggingManager, IList<UserRoleInfo> userRoleInfos, UserInfo userInfo)
        {
            try
            {

                foreach (UserRoleInfo userRoleInfo in userRoleInfos)
                {
                    if (userRoleInfo.UserId == null || userRoleInfo.UserId.Equals(""))
                        userRoleInfo.UserId = userInfo.User_Id;
                    if (userRoleInfo.Id == null || userRoleInfo.Id.Equals(""))
                        userRoleInfo.Id = NewGuid();
                }
                UserInfo us = new UserInfo();
                us.Modify_Time = GetCurrentDateTime();
                us.Create_Time = GetCurrentDateTime();
                us.userRoleInfoList = userRoleInfos;
                us.Create_User_Id = userInfo.User_Id;
                us.Modify_User_Id = userInfo.User_Id;
                us.User_Id = userInfo.User_Id;
                cSqlMapper.Instance(loggingManager).Update("UserRole.InsertOrUpdate", us);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }

        /// <summary>
        /// 用户上传（到管理平台）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userInfo"></param>
        /// <param name="TypeId">1:新建;2:修改;3:启用;4:停用;5:修改密码</param>
        /// <returns></returns>
        private bool SetManagerExchangeUserInfo(LoggingSessionInfo loggingSessionInfo, UserInfo userInfo,int TypeId)
        {
            try
            {
                UserInfo usInfo = new UserInfo();
                if (TypeId.ToString().Equals("1") || TypeId.ToString().Equals("2"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Code = userInfo.User_Code;
                    usInfo.User_Name = userInfo.User_Name;
                    usInfo.User_Password = userInfo.User_Password;
                    usInfo.Fail_Date = userInfo.Fail_Date;
                    usInfo.User_Status_Desc = userInfo.User_Status_Desc;
                }
                if (TypeId.ToString().Equals("3") || TypeId.ToString().Equals("4"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Status_Desc = userInfo.User_Status_Desc;
                }
                if (TypeId.ToString().Equals("5"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Password = userInfo.User_Password;
                }
                string strUserInfo = cXMLService.Serialiaze(usInfo);

                cPos.WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService  cdxService = new WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService();
                cdxService.Url = System.Configuration.ConfigurationManager.AppSettings["ap_url"] + "/customer/CustomerDataExchangeService.asmx";
                return cdxService.SynUser(loggingSessionInfo.CurrentLoggingManager.Customer_Id, TypeId, strUserInfo);
                
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 用户上传（到管理平台）
        /// </summary>
        /// <param name="loggingManager"></param>
        /// <param name="userInfo"></param>
        /// <param name="TypeId">1:新建;2:修改;3:启用;4:停用;5:修改密码</param>
        /// <returns></returns>
        public bool SetManagerExchangeUserInfo(LoggingManager loggingManager, UserInfo userInfo, int TypeId)
        {
            try
            {
                UserInfo usInfo = new UserInfo();
                if (TypeId.ToString().Equals("1") || TypeId.ToString().Equals("2"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Code = userInfo.User_Code;
                    usInfo.User_Name = userInfo.User_Name;
                    usInfo.User_Password = userInfo.User_Password;
                    usInfo.Fail_Date = userInfo.Fail_Date;
                    usInfo.User_Status_Desc = userInfo.User_Status_Desc;
                }
                if (TypeId.ToString().Equals("3") || TypeId.ToString().Equals("4"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Status_Desc = userInfo.User_Status_Desc;
                }
                if (TypeId.ToString().Equals("5"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Password = userInfo.User_Password;
                }

                if (usInfo.Fail_Date == null) usInfo.Fail_Date = "9999-12-31";
                string strUserInfo = cXMLService.Serialiaze(usInfo);

                cPos.WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService cdxService = new WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService();
                cdxService.Url = System.Configuration.ConfigurationManager.AppSettings["ap_url"] + "/customer/CustomerDataExchangeService.asmx";
                return cdxService.SynUser(loggingManager.Customer_Id, TypeId, strUserInfo);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region 与接口交互
        /// <summary>
        /// 申请用户凭证方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录用户信息</param>
        /// <param name="userInfo">用户集合</param>
        /// <returns></returns>
        private string SetDexUserCertificate(LoggingSessionInfo loggingSessionInfo, UserInfo userInfo)
        {
            try
            {
                //cPos.ExchangeService.DexAuthService dexAuthService = new cPos.ExchangeService.DexAuthService();
                //Hashtable _ht = dexAuthService.ApplyUserCertificate(loggingSessionInfo.CurrentUser.User_Id
                //                                                    , loggingSessionInfo.CurrentUser.User_Password
                //                                                    , userInfo.User_Id
                //                                                    , userInfo.User_Code
                //                                                    , loggingSessionInfo.CurrentLoggingManager.Customer_Id
                //                                                    , loggingSessionInfo.CurrentLoggingManager.Customer_Code
                //                                                    , userInfo.User_Password
                //                                                    , userInfo.userRoleInfoList);
                //if (!_ht["status"].ToString().Equals("True"))
                //{
                //    return _ht["error_code"].ToString();
                //}
                return "True";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool SetDexUserCertificate(string customer_code,UserInfo userInfo,out string strError)
        {
            try
            {
                cPos.ExchangeService.DexAuthService dexAuthService = new cPos.ExchangeService.DexAuthService();
                Hashtable _ht = dexAuthService.ApplyUserCertificate(userInfo.User_Id
                                                                    , userInfo.User_Password
                                                                    , userInfo.User_Id
                                                                    , userInfo.User_Code
                                                                    , userInfo.customer_id
                                                                    , customer_code
                                                                    , userInfo.User_Password
                                                                    , userInfo.userRoleInfoList);
                if (!_ht["status"].ToString().Equals("True"))
                {
                    strError = _ht["error_code"].ToString();
                    return false;
                }
                else
                {
                    strError = "插入中间层成功";
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 修改用户凭证
        /// </summary>
        /// <param name="loggingSessionInfo">用户登录信息</param>
        /// <param name="userInfo">修改用户model</param>
        /// <param name="strError">错误信息输出</param>
        /// <returns></returns>
        private bool SetDexUpdateUserCertificate(LoggingSessionInfo loggingSessionInfo, UserInfo userInfo,out string strError)
        {
            try
            {
                cPos.ExchangeService.DexAuthService dexAuthService = new cPos.ExchangeService.DexAuthService();
                Hashtable _ht = dexAuthService.UpdateUserCertificate(loggingSessionInfo.CurrentUser.User_Id
                                                                    , loggingSessionInfo.CurrentUser.User_Password
                                                                    , userInfo.User_Id
                                                                    , userInfo.User_Password
                                                                    , userInfo.User_Code
                                                                    , userInfo.User_Name
                                                                    , userInfo.userRoleInfoList);

                if (!_ht["status"].ToString().Equals("true"))
                {
                    strError = _ht["error_code"].ToString();
                    return false;
                }
                strError = "ok";                                   
                return true;
            }
            catch (Exception ex) { 
                strError = ex.ToString();
                return false;
            }
        }

        #endregion
        #endregion 用户修改新建保存

        //#endregion 用户

        #region 用户的状态处理
        /// <summary>
        /// 用户停用/启用方法
        /// </summary>
        /// <param name="user_id">用户标识</param>
        /// <param name="iStatus">用户状态</param>
        /// <param name="LoggingSessionInfo">登录用户Session类</param>
        /// <returns></returns>
        public bool SetUserStatus(string user_id, string iStatus, LoggingSessionInfo LoggingSessionInfo)
        {
            cSqlMapper.Instance().BeginTransaction();
            try
            {
                cPos.Service.cBillService bs = new cBillService();
                UserInfo userInfo = new UserInfo();
                userInfo.User_Id = user_id;
                
                BillActionType billActionType;
                if (iStatus.Equals("1"))
                {
                    billActionType = BillActionType.Open;
                    userInfo.User_Status_Desc = "正常";
                }
                else {
                    billActionType = BillActionType.Stop;
                    userInfo.User_Status_Desc = "停用";
                }
                BillOperateStateService state = bs.ApproveBill(LoggingSessionInfo, user_id, "", billActionType);
                if (state == BillOperateStateService.ApproveSuccessful)
                {
                    if (SetUserTableStatus(LoggingSessionInfo, user_id))
                    {
#if SYN_AP
                        // 提交管理平台
                        if (!SetManagerExchangeUserInfo(LoggingSessionInfo, userInfo, 3))
                        {
                            cSqlMapper.Instance().RollBackTransaction();
                            return false;
                        }
#endif
                        cSqlMapper.Instance().CommitTransaction();
                        return true;
                    }
                    else {
                        cSqlMapper.Instance().RollBackTransaction();
                        return false;
                    }
                }
                else {
                    cSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
            }
            catch (Exception ex)
            {
                cSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
        }

        /// <summary>
        /// 设置用户表的用户状态
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        private bool SetUserTableStatus(LoggingSessionInfo loggingSession, string user_id)
        {
            try
            {
                //获取要改变的表单信息
                BillModel billInfo = new cBillService().GetBillById(loggingSession, user_id);
                //设置要改变的用户信息
                UserInfo userInfo = new UserInfo();
                userInfo.User_Status = billInfo.Status;
                userInfo.User_Status_Desc = billInfo.BillStatusDescription;
                userInfo.User_Id = user_id;
                userInfo.Modify_User_Id = loggingSession.CurrentUser.User_Id;
                userInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                //提交
                cSqlMapper.Instance(loggingSession.CurrentLoggingManager).Update("User.UpdateUserStatus", userInfo);
                return true;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
        #region 用户的角色

        /// <summary>
        /// 获取用户在某种角色下能够登录的单位列表
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="userId">用户Id</param>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public IList<UnitInfo> GetUnitsByUserIdAndRoleId(LoggingSessionInfo loggingSession, string userId, string roleId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("UserId", userId);
            hashTable.Add("RoleId", roleId);

            return cSqlMapper.Instance().QueryForList<UnitInfo>("Unit.SelectByUserIdAndRoleId", hashTable);
        }

        /// <summary>
        /// 获取用户在某个应用系统下的角色和单位
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="userId">用户Id</param>
        /// <param name="applicationId">应用系统Id</param>
        /// <returns></returns>
        public IList<UserRoleInfo> GetUserRoles(LoggingSessionInfo loggingSession, string userId, string applicationId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("UserId", userId);
            hashTable.Add("ApplicationId", applicationId);

            return cSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForList<UserRoleInfo>("UserRole.SelectByUserIdAndApplicationId", hashTable);
        }

        /// <summary>
        /// 查询用户角色信息
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public bool CheckUserRole(UserRoleInfo userRole)
        {
            //将角色和单位的ID拼出来作为角色ID,这样按角色只能查询出一个单位
            userRole.RoleId = userRole.RoleId + "," + userRole.UnitId;
            userRole.DefaultFlag = 1;
            userRole.Id = NewGuid();

            bool existed = cSqlMapper.Instance().QueryForObject<int>("UserRole.CountByUserRoleUnit", userRole) == 1;
            if (!existed)
            {
                cSqlMapper.Instance().Insert("UserRole.Insert", userRole);
            }
            return existed;
        }
        #endregion

        #region 用户密码处理
        /// <summary>
        /// 判断用户输入的密码是否有效
        /// </summary>
        /// <param name="loggingSession">登录model</param>
        /// <param name="user">用户</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool IsValidPassword(LoggingSessionInfo loggingSession, UserInfo user, string pwd)
        {
            if (string.IsNullOrEmpty(user.User_Code))
                return false;
            if (string.IsNullOrEmpty(user.User_Code.Trim()))
                return false;

            if (string.IsNullOrEmpty(pwd))
                return false;
            if (string.IsNullOrEmpty(pwd.Trim()))
                return false;

            //保留
            if (pwd == "361-" + user.User_Code)
                return false;

            //新旧密码一样
            if (!string.IsNullOrEmpty(user.User_Password) && EncryptManager.Hash(pwd, HashProviderType.MD5) == user.User_Password)
                return false;

            string s = pwd;
            char[] s1 = s.ToCharArray();
            if (s.Length < 8)
                return false;
            if (s.Length != s1.Length)
                return false;
            bool b_char = false;
            bool b_number = false;
            bool b_other = false;
            foreach (char ch in s1)
            {
                if (ch >= '0' && ch <= '9')
                    b_number = true;
                else
                {
                    if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                        b_char = true;
                    else
                        b_other = true;
                }
            }

            if (!b_char || !b_number || !b_other)
                return false;
            return true;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="userId">用户Id</param>
        /// <param name="userPassword">用户密码</param>
        /// <returns></returns>
        public bool ModifyUserPassword(LoggingSessionInfo loggingSessionInfo,string userId, string userPassword)
        {
            cSqlMapper.Instance().BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userPassword))
                {
                    return false;
                }

                Hashtable hashTable = new Hashtable();
                hashTable.Add("UserId", userId);
                hashTable.Add("UserPassword", EncryptManager.Hash(userPassword, HashProviderType.MD5));
                int count = cSqlMapper.Instance().Update("User.ModifyPassword2", hashTable);
                //return count == 1;

                UserInfo userInfo = new UserInfo();
                userInfo.User_Id = userId;
                userInfo.User_Password = EncryptManager.Hash(userPassword, HashProviderType.MD5);
                userInfo.userRoleInfoList = new UserRoleService().GetUserRoleListByUserId(loggingSessionInfo, userId);
#if SYN_AP
                // 提交管理平台
                if (!SetManagerExchangeUserInfo(loggingSessionInfo, userInfo, 5))
                {
                    cSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
#endif

#if SYN_DEX
                // 提交接口
                string strError = string.Empty;
                bool bResult = SetDexUpdateUserCertificate(loggingSessionInfo, userInfo, out strError);
                if (!bResult)
                {
                    cSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
#endif
                cSqlMapper.Instance().CommitTransaction();
                return true;
            }
            catch (Exception ex) {
                cSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }

        }
        /// <summary>
        /// 从接口过来的修改用户密码（不需要加密）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userId"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public bool ModifyUserPassword_JK(LoggingSessionInfo loggingSessionInfo, string userId, string userPassword,out string strError)
        {
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userPassword))
                {
                    strError = "密码为空";
                    return false;
                }

                Hashtable hashTable = new Hashtable();
                hashTable.Add("UserId", userId);
                hashTable.Add("UserPassword", userPassword);
                int count = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("User.ModifyPassword2", hashTable);
                //return count == 1;

                UserInfo userInfo = new UserInfo();
                userInfo.User_Id = userId;
                userInfo.User_Password = userPassword;
                userInfo.userRoleInfoList = new UserRoleService().GetUserRoleListByUserId(loggingSessionInfo, userId);
#if SYN_AP
                // 提交管理平台
                if (!SetManagerExchangeUserInfo(loggingSessionInfo, userInfo, 5))
                {
                    cSqlMapper.Instance().RollBackTransaction();
                    strError = "提交管理平台失败";
                    return false;
                }
#endif

//#if SYN_DEX
//                // 提交接口
//                string strError = string.Empty;
//                bool bResult = SetDexUpdateUserCertificate(loggingSessionInfo, userInfo, out strError);
//                if (!bResult)
//                {
//                    cSqlMapper.Instance().RollBackTransaction();
//                    strError = "提交接口失败";
//                    return false;
//                }
//#endif
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                strError = "ok";
                return true;
            }
            catch (Exception ex)
            {
                cSqlMapper.Instance().RollBackTransaction();
                strError = ex.ToString();
                throw (ex);
            }

        }
        #endregion

        #region 根据组织获取用户集合
        /// <summary>
        /// 根据组织获取用户集合
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitId">组织标识</param>
        /// <returns></returns>
        public IList<UserInfo> GetUserListByUnitId(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unitId);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UserInfo>("User.SelectByUnitId", _ht);
        }
        #endregion

        #region 根据组织角色获取用户集合
        /// <summary>
        /// 根据组织与角色获取用户集合
        /// </summary>
        /// <param name="loggingSessionInfo">用户登录model</param>
        /// <param name="unitId">组织标识</param>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        public IList<UserInfo> GetUserListByUnitIdsAndRoleId(LoggingSessionInfo loggingSessionInfo, string unitId, string roleId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unitId);
            _ht.Add("RoleId", roleId);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UserInfo>("User.SelectByUnitIdRoleId", _ht);

        }
        #endregion

        /// <summary>
        /// 获取每个组织的开班人员
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unitId">组织标识</param>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        public IList<UserInfo> GetSalesUserByUnitIdsAndRoleId(LoggingSessionInfo loggingSessionInfo, string unitId, string roleId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unitId);
            _ht.Add("RoleId", roleId);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UserInfo>("User.SelectSalesUserByUnitIdRoleId", _ht);

        }
        
        #region 上传到中间层
        /// <summary>
        /// 根据用户,获取终端需要的基础信息
        /// </summary>
        /// <param name="User_id">用户标识</param>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">门店标识</param>
        /// <returns></returns>
        public BaseInfo GetUserBaseInfoByUserId(string User_id, string Customer_Id,string Unit_Id)
        {
            try
            {
                //根据客户标识,获取数据库连接字符串
                LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
                loggingSessionInfo = GetLoggingSessionInfoByCustomerId(Customer_Id);

                BaseInfo baseInfo = new BaseInfo();
                //1.人员集合
                baseInfo.CurrSalesUserInfoList = GetUserListByUnitId(loggingSessionInfo, Unit_Id);
                //2.角色集合
                baseInfo.CurrRoleInfoList = new RoleService().GetRoleListByUnitId(loggingSessionInfo, Unit_Id);
                //3.人员与角色关系
                baseInfo.CurrSalesUserRoleInfoList = new UserRoleService().GetUserRoleListByUnitId(loggingSessionInfo, Unit_Id);
                //4.菜单集合
                baseInfo.CurrMenuInfoList = new cAppSysServices().GetAllMenusByAppSysCode(loggingSessionInfo, "CT");
                //5.角色与菜单关系
                baseInfo.CurrRoleMenuInfoList = new RoleMenuService().GetRoleMenuListByUnitIdAndAppCode(loggingSessionInfo, Unit_Id, "CT");
                return baseInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
    }
}
