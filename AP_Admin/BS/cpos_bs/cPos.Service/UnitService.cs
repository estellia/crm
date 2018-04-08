using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using cPos.Model;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// 单位服务
    /// </summary>
    public class UnitService:BaseService
    {
        #region 单位

        /// <summary>
        /// 根据单位的Id获取单位信息
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录用户的Session信息</param>
        /// <param name="unitId">单位的Id</param>
        /// <returns></returns>
        public UnitInfo GetUnitById(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSessionInfo.CurrentLanguageKindId);
            hashTable.Add("UnitId", unitId);

            UnitInfo unitInfo = new UnitInfo();
            unitInfo = (UnitInfo)cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject("Unit.SelectById", hashTable);
            unitInfo.PropertyList = new PropertyUnitService().GetUnitPropertyListByUnit(loggingSessionInfo, unitId);
            return unitInfo;
        }

        public UnitInfo GetUnitById(LoggingManager loggingManager, string unitId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("UnitId", unitId);

            UnitInfo unitInfo = new UnitInfo();
            unitInfo = (UnitInfo)cSqlMapper.Instance(loggingManager).QueryForObject("Unit.SelectById", hashTable);
            return unitInfo;
        }
        /// <summary>
        /// 根据客户获取单位最高节点
        /// </summary>
        /// <param name="loggingManager"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public UnitInfo GetUnitTopByCustomerId(LoggingManager loggingManager, string customerId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("CustomerId", customerId);
            return (UnitInfo)cSqlMapper.Instance(loggingManager).QueryForObject("Unit.SelectUnitTopByCustomerId", hashTable);
        }

        /// <summary>
        /// 保存组织信息（新建修改）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public string SetUnitInfo(LoggingSessionInfo loggingSessionInfo, UnitInfo unitInfo)
        {
            string strResult = string.Empty;
            int iType;
            //事物信息
            cSqlMapper.Instance().BeginTransaction();

            try
            {
                unitInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                 //处理是新建还是修改
                if (unitInfo.Id == null || unitInfo.Id.Equals(""))
                {
                    unitInfo.Id = NewGuid();
                    iType = 1;    
                }
                else {
                    iType = 2;
                }

                //1 判断组织号码是否唯一
                if(!IsExistUnitCode(loggingSessionInfo,unitInfo.Code,unitInfo.Id))
                {
                    strResult = "组织号码已经存在。";
                    return strResult;
                }

                 //2.提交用户信息至表单
                if (iType.ToString().Equals("1"))
                {
                    if (!SetUnitInsertBill(loggingSessionInfo,unitInfo))
                    {
                        strResult = "组织表单提交失败。";
                        return strResult;
                    }
                }

                //3.获取用户表单信息,设置用户状态与状态描述
                BillModel billInfo = new cBillService().GetBillById(loggingSessionInfo, unitInfo.Id);
                unitInfo.Status = billInfo.Status;
                unitInfo.Status_Desc = billInfo.BillStatusDescription;

                //4.提交用户信息
                if (!SetUnitTableInfo(loggingSessionInfo, unitInfo))
                {
                    strResult = "提交用户表失败";
                    return strResult;
                }
//#if SYN_AP
                 // 单位类型是门店，提交管理平台
                if (unitInfo.TypeId.Equals("EB58F1B053694283B2B7610C9AAD2742"))
                {
                    if (!SetManagerExchangeUnitInfo(loggingSessionInfo, unitInfo, iType))
                    {
                        return "提交管理平台失败";
                    }
                }
//#endif
                ////刷新单位级别数据
                //cSqlMapper.Instance().QueryForObject("Unit.RefreshUnitLevel", null);
                cSqlMapper.Instance().CommitTransaction();
                strResult = "保存成功!";

                return strResult;
            }
            catch (Exception ex) {
                cSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
        }
        /// <summary>
        /// 判断组织号码是否唯一
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="UnitCode">组织号码</param>
        /// <param name="UnitId">组织标识</param>
        /// <returns></returns>
        private bool IsExistUnitCode(LoggingSessionInfo loggingSessionInfo, string UnitCode, string UnitId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("Unit_Code", UnitCode);
                _ht.Add("Unit_Id", UnitId);
                _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
                   
                int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Unit.IsExsitUnitCode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 提交组织表单单据
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        private bool SetUnitInsertBill(LoggingSessionInfo loggingSessionInfo, UnitInfo unitInfo)
        {
            try
            {
                cPos.Model.BillModel bill = new BillModel();
                cPos.Service.cBillService bs = new cBillService();

                bill.Id = unitInfo.Id;//order_id
                string order_type_id = bs.GetBillKindByCode(loggingSessionInfo, "UNITMANAGER").Id.ToString(); //loggingSession, OrderType
                bill.Code = bs.GetBillNextCode(loggingSessionInfo,"CreateUnit"); //BillKindCode
                bill.KindId = order_type_id;
                bill.UnitId = loggingSessionInfo.CurrentUserRole.UnitId;
                bill.AddUserId = loggingSessionInfo.CurrentUser.User_Id;

                BillOperateStateService state = bs.InsertBill(loggingSessionInfo, bill);

                if (state == BillOperateStateService.CreateSuccessful)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        /// <summary>
        /// 提交组织单据
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unitInfo">组织类</param>
        /// <returns></returns>
        private bool SetUnitTableInfo(LoggingSessionInfo loggingSessionInfo, UnitInfo unitInfo)
        {
            try
            {
                if (unitInfo != null)
                {
                    if (unitInfo.Create_User_Id == null || unitInfo.Create_User_Id.Equals(""))
                    {
                        unitInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        unitInfo.Create_Time = GetCurrentDateTime();
                    }
                    if (unitInfo.Modify_User_Id == null || unitInfo.Modify_User_Id.Equals(""))
                    {
                        unitInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        unitInfo.Modify_Time = GetCurrentDateTime();
                    }
                    if (unitInfo.PropertyList != null)
                    {
                        foreach (UnitPropertyInfo unitPropInfo in unitInfo.PropertyList)
                        {
                            if (unitPropInfo.UnitId == null || unitPropInfo.UnitId.Equals(""))
                                unitPropInfo.UnitId = unitInfo.Id;
                            if (unitPropInfo.Id == null || unitPropInfo.Id.Equals(""))
                                unitPropInfo.Id = NewGuid();

                            unitPropInfo.Create_User_id = unitInfo.Create_User_Id;
                            unitPropInfo.Create_Time = unitInfo.Create_Time;
                            
                        }
                    }
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Unit.InsertOrUpdate", unitInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggingManager"></param>
        /// <param name="unitInfo"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool SetUnitTableInfo(LoggingManager loggingManager, UnitInfo unitInfo, cPos.Model.User.UserInfo userInfo)
        {
            try
            {
                if (unitInfo != null)
                {
                    if (unitInfo.Create_User_Id == null || unitInfo.Create_User_Id.Equals(""))
                    {
                        unitInfo.Create_User_Id = userInfo.User_Id;
                        unitInfo.Create_Time = GetCurrentDateTime();
                    }
                    if (unitInfo.Modify_User_Id == null || unitInfo.Modify_User_Id.Equals(""))
                    {
                        unitInfo.Modify_User_Id = userInfo.User_Id;
                        unitInfo.Modify_Time = GetCurrentDateTime();
                    }
                    if (unitInfo.PropertyList != null)
                    {
                        foreach (UnitPropertyInfo unitPropInfo in unitInfo.PropertyList)
                        {
                            if (unitPropInfo.UnitId == null || unitPropInfo.UnitId.Equals(""))
                                unitPropInfo.UnitId = unitInfo.Id;
                            if (unitPropInfo.Id == null || unitPropInfo.Id.Equals(""))
                                unitPropInfo.Id = NewGuid();

                            unitPropInfo.Create_User_id = unitInfo.Create_User_Id;
                            unitPropInfo.Create_Time = unitInfo.Create_Time;

                        }
                    }
                    cSqlMapper.Instance(loggingManager).Update("Unit.InsertOrUpdate", unitInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 组织提交管理平台（只是提交门店级别的）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitInfo"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        private bool SetManagerExchangeUnitInfo(LoggingSessionInfo loggingSessionInfo, UnitInfo unitInfo, int TypeId)
        {
            try
            {
                UnitInfo utInfo = new UnitInfo();
                if (TypeId.ToString().Equals("1") || TypeId.ToString().Equals("2"))
                {
                    if (unitInfo.CityId != null || (!unitInfo.CityId.Equals("")))
                    {
                        //获取城市
                        CityService cityService = new CityService();
                        CityInfo cityInfo = new CityInfo();
                        cityInfo = cityService.GetCityById(loggingSessionInfo, unitInfo.CityId);
                        utInfo.ProvinceName = cityInfo.City1_Name;
                        utInfo.CityName = cityInfo.City3_Name;
                        utInfo.StateName = cityInfo.City2_Name;

                    }
                    utInfo.Id = unitInfo.Id;
                    utInfo.Code = unitInfo.Code;
                    utInfo.Name = unitInfo.Name;
                    utInfo.EnglishName = unitInfo.EnglishName;
                    utInfo.ShortName = unitInfo.ShortName;
                   
                    utInfo.Address = unitInfo.Address;
                    utInfo.Postcode = unitInfo.Postcode;
                    utInfo.Contact = unitInfo.Contact;
                    utInfo.Telephone = unitInfo.Telephone;
                    utInfo.Fax = unitInfo.Fax;
                    utInfo.Email = unitInfo.Email;
                    utInfo.Remark = unitInfo.Remark;
                    utInfo.Status_Desc = unitInfo.Status_Desc;
                    utInfo.longitude = unitInfo.longitude;
                    utInfo.dimension = unitInfo.dimension;
                }
                if (TypeId.ToString().Equals("3") || TypeId.ToString().Equals("4"))
                {
                    utInfo.Id = unitInfo.Id;
                    utInfo.Status_Desc = unitInfo.Status_Desc;
                }
                string strUnitInfo = cXMLService.Serialiaze(utInfo);

                cPos.WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService cdxService = new WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService();
                cdxService.Url = System.Configuration.ConfigurationManager.AppSettings["ap_url"] + "/customer/CustomerDataExchangeService.asmx";

                return cdxService.SynUnit(loggingSessionInfo.CurrentLoggingManager.Customer_Id, TypeId, strUnitInfo);

            }
            catch (Exception ex)
            {
                throw (ex);
                
            }
        }

        public bool SetManagerExchangeUnitInfo(LoggingManager loggingManager, UnitInfo unitInfo, int TypeId)
        {
            try
            {
                UnitInfo utInfo = new UnitInfo();
                if (TypeId.ToString().Equals("1") || TypeId.ToString().Equals("2"))
                {
                    if (unitInfo.CityId != null && (!unitInfo.CityId.Equals("")))
                    {
                        //获取城市
                        CityService cityService = new CityService();
                        CityInfo cityInfo = new CityInfo();
                        cityInfo = cityService.GetCityById(loggingManager, unitInfo.CityId);
                        utInfo.ProvinceName = cityInfo.City1_Name;
                        utInfo.CityName = cityInfo.City3_Name;
                        utInfo.StateName = cityInfo.City2_Name;

                    }
                    utInfo.Id = unitInfo.Id;
                    utInfo.Code = unitInfo.Code;
                    utInfo.Name = unitInfo.Name;
                    utInfo.EnglishName = unitInfo.EnglishName;
                    utInfo.ShortName = unitInfo.ShortName;

                    utInfo.Address = unitInfo.Address;
                    utInfo.Postcode = unitInfo.Postcode;
                    utInfo.Contact = unitInfo.Contact;
                    utInfo.Telephone = unitInfo.Telephone;
                    utInfo.Fax = unitInfo.Fax;
                    utInfo.Email = unitInfo.Email;
                    utInfo.Remark = unitInfo.Remark;
                    utInfo.Status_Desc = unitInfo.Status_Desc;
                    utInfo.longitude = unitInfo.longitude;
                    utInfo.dimension = unitInfo.dimension;
                }
                if (TypeId.ToString().Equals("3") || TypeId.ToString().Equals("4"))
                {
                    utInfo.Id = unitInfo.Id;
                    utInfo.Status_Desc = unitInfo.Status_Desc;
                }
                string strUnitInfo = cXMLService.Serialiaze(utInfo);

                cPos.WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService cdxService = new WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService();
                cdxService.Url = System.Configuration.ConfigurationManager.AppSettings["ap_url"] + "/customer/CustomerDataExchangeService.asmx";

                return cdxService.SynUnit(loggingManager.Customer_Id, TypeId, strUnitInfo);

            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }
        /// <summary>
        /// 根据客户标识与门店号码获取门店信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="customer_id"></param>
        /// <param name="unit_code"></param>
        /// <returns></returns>
        public UnitInfo GetUnitInfoByCode(LoggingSessionInfo loggingSessionInfo, string customer_id, string unit_code)
        {
            UnitInfo unitInfo = new UnitInfo();
            string unit_id = string.Empty;
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitCode", unit_code);
            _ht.Add("CustomerId", customer_id);
            unit_id = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<string>("Unit.GetUnitIdByCode", _ht);
            if (unit_id==null || unit_id.Equals("")) { 
                throw new Exception("门店不存在。"); 
            }
           
            unitInfo = GetUnitById(loggingSessionInfo, unit_id);

            return unitInfo;
        }
        
        #endregion 单位

        #region 单位树
        /// <summary>
        /// 获取缺省关联模式下的根单位列表(只含已启用的)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <returns></returns>
        public IList<UnitInfo> GetRootUnitsByDefaultRelationMode(LoggingSessionInfo loggingSession)
        {
            return GetRootUnitsByUnitRelationMode(loggingSession, "");//GetDefaultUnitRelationMode(loggingSession).Id
        }

        /// <summary>
        /// 获取某种关联模式下的根单位列表(只含已启用的)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="unitRelationModeId">关联模式Id</param>
        /// <returns></returns>
        public IList<UnitInfo> GetRootUnitsByUnitRelationMode(LoggingSessionInfo loggingSession, string unitRelationModeId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("UserId", loggingSession.CurrentUser.User_Id);
            hashTable.Add("RoleId", loggingSession.CurrentUserRole.RoleId);
            hashTable.Add("UnitRelationModeId", unitRelationModeId);
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("UnitStatus", 1);

            return cSqlMapper.Instance().QueryForList<UnitInfo>("Unit.SelectRootUnits", hashTable);
        }

        /// <summary>
        /// 获取缺省的单位与单位之间的关联模式
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <returns></returns>
        public UnitRelationModeInfo GetDefaultUnitRelationMode(LoggingSessionInfo loggingSession)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);

            return (UnitRelationModeInfo)cSqlMapper.Instance().QueryForObject("UnitRelationMode.SelectDefault", hashTable);
        }

        /// <summary>
        /// 获取单位的下一级子单位列表(只含已启用的)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="unitId">单位Id</param>
        /// <returns></returns>
        public IList<UnitInfo> GetSubUnitsByDefaultRelationMode(LoggingSessionInfo loggingSession, string unitId)
        {
            return GetSubUnitsByUnitRelationMode(loggingSession, "", unitId);//GetDefaultUnitRelationMode(loggingSession).Id
        }

        /// <summary>
        /// 获取某种关联模式下的下一级子单位列表(只含已启用的)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="unitRelationModeId">关联模式Id</param>
        /// <param name="unitId">单位Id</param>
        /// <returns></returns>
        public IList<UnitInfo> GetSubUnitsByUnitRelationMode(LoggingSessionInfo loggingSession, string unitRelationModeId, string unitId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("UnitId", unitId);
            hashTable.Add("UnitRelationModeId", unitRelationModeId);
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("UnitStatus", 1);
            hashTable.Add("UserId", loggingSession.CurrentUser.User_Id);
            hashTable.Add("RoleId", loggingSession.CurrentUserRole.RoleId);

            return cSqlMapper.Instance().QueryForList<UnitInfo>("Unit.SelectSubUnitsById", hashTable);
        }

        #endregion

        #region 单位查询
        /// <summary>
        /// 单位查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="unit_code">组织号码</param>
        /// <param name="unit_name">组织名称</param>
        /// <param name="unit_type_id">组织类型</param>
        /// <param name="unit_tel">电话</param>
        /// <param name="unit_city_id">城市标识</param>
        /// <param name="unit_status">状态</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public UnitInfo SearchUnitInfo(LoggingSessionInfo loggingSessionInfo
                                             , string unit_code
                                             , string unit_name
                                             , string unit_type_id
                                             , string unit_tel
                                             , string unit_city_id
                                             , string unit_status
                                             , int maxRowCount
                                             , int startRowIndex
            )
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("unit_code", unit_code);
            _ht.Add("unit_name", unit_name);
            _ht.Add("unit_type_id", unit_type_id);
            _ht.Add("unit_tel", unit_tel);
            _ht.Add("unit_city_id", unit_city_id);
            _ht.Add("unit_status", unit_status);
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            UnitInfo unitInfo = new UnitInfo();
            
            int iCount = cSqlMapper.Instance().QueryForObject<int>("Unit.SearchUnitInfoCount", _ht);

            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            unitInfoList = cSqlMapper.Instance().QueryForList<UnitInfo>("Unit.SearchUnitInfo", _ht);

            unitInfo.ICount = iCount;
            unitInfo.UnitInfoList = unitInfoList;
            return unitInfo;
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UnitInfo>("Unit.SearchUnitInfo", _ht);
        }
        #endregion

        #region 单位停用启用
        /// <summary>
        /// 单位停用启用
        /// </summary>
        /// <param name="unit_id">组织标识</param>
        /// <param name="iStatus">状态</param>
        /// <param name="loggingSession">登录model</param>
        /// <returns></returns>
        public bool SetUnitStatus(string unit_id, string iStatus, LoggingSessionInfo loggingSession)
        {
            try
            {
                cPos.Service.cBillService bs = new cBillService();
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.Id = unit_id;
                int iType = 0;
                BillActionType billActionType;
                if (iStatus.Equals("1"))
                {
                    billActionType = BillActionType.Open;
                    unitInfo.Status_Desc = "正常";
                    iType = 3;
                }
                else
                {
                    billActionType = BillActionType.Stop;
                    unitInfo.Status_Desc = "停用";
                    iType = 4;
                }
                BillOperateStateService state = bs.ApproveBill(loggingSession, unit_id, "", billActionType);
  
                if (state == BillOperateStateService.ApproveSuccessful)
                {
                    if (SetUnitTableStatus(loggingSession, unit_id))
                    {
                        UnitInfo u2 = this.GetUnitById(loggingSession, unit_id);
                        if (u2 != null && u2.TypeId == "EB58F1B053694283B2B7610C9AAD2742")
                        {
#if SYN_AP
                            //单位类型是门店， 提交管理平台
                            if (!SetManagerExchangeUnitInfo(loggingSession, unitInfo, iType))
                            {
                                return false;
                            }
#endif
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 修改单位状态
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        private bool SetUnitTableStatus(LoggingSessionInfo loggingSession, string unit_id)
        {
            try
            {
                //获取要改变的表单信息
                BillModel billInfo = new cBillService().GetBillById(loggingSession, unit_id);
                //设置要改变的用户信息
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.Status = billInfo.Status;
                unitInfo.Status_Desc = billInfo.BillStatusDescription;
                unitInfo.Id = unit_id;
                unitInfo.Modify_User_Id = loggingSession.CurrentUser.User_Id;
                unitInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                //提交
                cSqlMapper.Instance(loggingSession.CurrentLoggingManager).Update("Unit.UpdateUnitStatus", unitInfo);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 根据用户获取该用户的所有门店
        /// <summary>
        /// 根据用户获取该用户的所有门店
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UnitInfo> GetUnitListByUserId(LoggingSessionInfo loggingSessionInfo, string userId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UserId", userId);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UnitInfo>("Unit.SelectByUserId", _ht);
        }


        #endregion


        #region 中间层
        #region 商品数据处理
        /// <summary>
        /// 获取未打包的Unit数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public int GetUnitNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        {
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Unit.SelectUnDownloadCount", "");
        }
        /// <summary>
        /// 需要打包的Unit信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<UnitInfo> GetUnitListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UnitInfo>("Unit.SelectUnDownload", _ht);
        }

        /// <summary>
        /// 设置打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="UnitInfoList">商品集合</param>
        /// <returns></returns>
        public bool SetUnitBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<UnitInfo> UnitInfoList)
        {
            UnitInfo unitInfo = new UnitInfo();
            unitInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            unitInfo.Modify_Time = GetCurrentDateTime();
            unitInfo.bat_id = bat_id;
            unitInfo.UnitInfoList = UnitInfoList;
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Unit.UpdateUnDownloadBatId", unitInfo);
            return true;
        }
        /// <summary>
        /// 更新Unit表打包标识方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <returns></returns>
        public bool SetUnitIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id)
        {
            UnitInfo unitInfo = new UnitInfo();
            unitInfo.bat_id = bat_id;
            unitInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            unitInfo.Modify_Time = GetCurrentDateTime();
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Unit.UpdateUnDownloadIfFlag", unitInfo);
            return true;
        }
        #endregion
        #endregion
    }
}
