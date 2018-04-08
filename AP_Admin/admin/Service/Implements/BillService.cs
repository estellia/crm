using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Admin.Component.SqlMappers;

namespace cPos.Admin.Service
{
    /// <summary>
    /// 表单类
    /// </summary>
    public class BillService : BaseService
    {
        #region 表单的日志

        public void ActionBills(Component.LoggingSessionInfo loggingSession, string billKindCode, string billIdList,
            bool haveFlow, Model.Bill.BillActionFlagType actionFlagType, int actionFlagValue, string comment)
        {
            string[] arr_BillID = billIdList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Hashtable ht = new Hashtable();
            try
            {
                Component.SqlMappers.MSSqlMapper.Instance().BeginTransaction();
                for (int i = 0; i < arr_BillID.Length; i++)
                {
                    if (!haveFlow)
                    {
                        if (i == 0)
                        {
                            ht.Add("bill_kind_code", billKindCode);
                            ht.Add("bill_id", arr_BillID[i]);
                            ht.Add("user_id", loggingSession.UserID);
                            ht.Add("bill_action_flag_type", (int)actionFlagType);
                            ht.Add("bill_action_flag_value", actionFlagValue);
                            ht.Add("comment", comment);
                        }
                        else
                        {
                            ht["bill_id"] = arr_BillID[i];
                        }

                        Component.SqlMappers.MSSqlMapper.Instance().QueryForObject("Bill.ActionLog.ProcInsertBillActionLogWithoutFlow", ht);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            ht.Add("bill_id", arr_BillID[i]);
                            ht.Add("role_id", loggingSession.RoleID);
                            ht.Add("user_id", loggingSession.UserID);
                            ht.Add("bill_action_flag_type", (int)actionFlagType);
                            ht.Add("bill_action_flag_value", actionFlagValue);
                            ht.Add("comment", comment);
                        }
                        else
                        {
                            ht["bill_id"] = arr_BillID[i];
                        }
                        Component.SqlMappers.MSSqlMapper.Instance().QueryForObject("Bill.ActionLog.ProcInsertBillActionLogWithFlow", ht);
                    }
                }
                Component.SqlMappers.MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                Component.SqlMappers.MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        public IList<cPos.Admin.Model.Bill.BillActionLogInfo> GetBillActionLogHistoryList(string billID)
        {
            return Component.SqlMappers.MSSqlMapper.Instance().QueryForList<cPos.Admin.Model.Bill.BillActionLogInfo>("Bill.ActionLog.SelectByBillID", billID);
        }
        #endregion

        #region 表单种类
        /// <summary>
        /// 获取某种表单种类的下一个表单编码
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="billKindCode">表单种类的编码</param>
        /// <returns></returns>
        public string GetBillNextCode(LoggingSessionInfo loggingSessionInfo,string billKindCode)
        {
            switch (billKindCode)
            {
                case BillKindModel.CODE_USER_NEW:
                    return GetNo(loggingSessionInfo,"UN");
                case BillKindModel.CODE_USER_MODIFY:
                    return GetNo(loggingSessionInfo,"UM");
                case BillKindModel.CODE_USER_DISABLE:
                    return GetNo(loggingSessionInfo,"UD");
                case BillKindModel.CODE_USER_ENABLE:
                    return GetNo(loggingSessionInfo,"UA");
                case "CreateUnit": //组织新建
                    return GetNo(loggingSessionInfo,"UNIT");
                case "CreateAdjustmentPrice"://调价单
                    return GetNo(loggingSessionInfo,"AOP");
                case "POSINOUT": //pos小票
                    return GetNo(loggingSessionInfo,"POS");
                default:
                    return GetNo(loggingSessionInfo,"BI");
            }
        }
        /// <summary>
        /// 获取所有的Bill种类
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <returns></returns>
        public IList<BillKindModel> GetAllBillKinds(LoggingSessionInfo loggingSession)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);

            return MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForList<BillKindModel>("BillKind.Select", hashTable);
        }

        /// <summary>
        /// 根据Bill种类的Id获取Bill种类
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billKindId">Bill种类的Id</param>
        /// <returns></returns>
        public BillKindModel GetBillKindById(LoggingSessionInfo loggingSession, string billKindId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("BillKindId", billKindId);

            return MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillKindModel>("BillKind.SelectById", hashTable);
        }

        /// <summary>
        /// 根据Bill种类的编码获取Bill种类
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billKindCode">Bill种类的编码</param>
        /// <returns></returns>
        public BillKindModel GetBillKindByCode(LoggingSessionInfo loggingSession, string billKindCode)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("BillKindCode", billKindCode);

            return MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillKindModel>("BillKind.SelectByCode", hashTable);
        }
        #endregion

        #region 表单状态
        /// <summary>
        /// 获取某种表单的起始状态
        /// </summary>
        /// <param name="loggingSession">登录model</param>
        /// <param name="billKindId">Bill的种类</param>
        /// <returns></returns>
        private BillStatusModel GetBillBeginStatus(LoggingSessionInfo loggingSession, string billKindId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("BillKindId", billKindId);

            return MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillStatusModel>("BillStatus.GetBeginStatusByKindId", hashTable);
        }

        /// <summary>
        /// 获取某种Bill的某种状态
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billKindId">Bill的种类</param>
        /// <param name="billStatus">Bill的某种状态</param>
        /// <returns></returns>
        public BillStatusModel GetBillStatusByKindIdAndStatus(LoggingSessionInfo loggingSession, string billKindId, string billStatus)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("BillKindId", billKindId);
            hashTable.Add("BillStatus", billStatus);

            return MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillStatusModel>("BillStatus.SelectByKindIdAndStatus", hashTable);
        }

        /// <summary>
        /// 获取某种表单的删除状态
        /// </summary>
        /// <param name="loggingSession">语言</param>
        /// <param name="billKindId">Bill的种类</param>
        /// <returns></returns>
        private BillStatusModel GetBillDeleteStatus(LoggingSessionInfo loggingSession, string billKindId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("BillKindId", billKindId);

            return MSSqlMapper.Instance().QueryForObject<BillStatusModel>("BillStatus.GetDeleteStatusByKindId", hashTable);
        }

        /// <summary>
        /// 获取某种单据的所有状态
        /// </summary>
        /// <param name="loggingSession">登录信息</param>
        /// <param name="bill_kind_code">单据种类</param>
        /// <returns></returns>
        public IList<BillStatusModel> GetBillStatusByKindCode(LoggingSessionInfo loggingSession, string bill_kind_code)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("bill_kind_code", bill_kind_code);
            return MSSqlMapper.Instance().QueryForList<BillStatusModel>("BillStatus.SelectByKindCode", hashTable);
        }

        /// <summary>
        /// 获取某种表单类型的所有状态
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billKindID">表单类型ID</param>
        /// <returns></returns>
        public IList<BillStatusModel> GetAllBillStatusesByBillKindID(LoggingSessionInfo loggingSession, string billKindID)
        {
            return MSSqlMapper.Instance().QueryForList<BillStatusModel>("BillStatus.SelectByKindId", billKindID);
        }

        /// <summary>
        /// 判断能否添加一个表单状态
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billStatus">要添加的表单状态</param>
        /// <returns></returns>
        public BillStatusCheckState CheckAddBillStatus(LoggingSessionInfo loggingSession, BillStatusModel billStatus)
        {
            //获取表单类型下的所有的表单状态
            IList<BillStatusModel> bill_stauts_lst = this.GetAllBillStatusesByBillKindID(loggingSession, billStatus.KindId);
            //为空，可以添加
            if (bill_stauts_lst == null || bill_stauts_lst.Count == 0)
            {
                return BillStatusCheckState.Successful;
            }

            //检查状态
            foreach (BillStatusModel m in bill_stauts_lst)
            {
                if (m.Status == billStatus.Status)
                {
                    return BillStatusCheckState.ExistBillStatus;
                }
            }

            //一个表单类型下，只能有一个开始标志为1的表单状态
            if (billStatus.BeginFlag == 1)
            {
                foreach (BillStatusModel m in bill_stauts_lst)
                {
                    if (m.BeginFlag==1)
                    {
                        return BillStatusCheckState.ExistBegin;
                    }
                }
                return BillStatusCheckState.Successful;
            }

            //一个表单类型下，只能有一个结束标志为1的表单状态
            if (billStatus.EndFlag == 1)
            {
                foreach (BillStatusModel m in bill_stauts_lst)
                {
                    if (m.EndFlag == 1)
                    {
                        return BillStatusCheckState.ExistEnd;
                    }
                }
                return BillStatusCheckState.Successful;
            }
            //一个表单类型下，只能有一个删除标志为1的表单状态
            if (billStatus.DeleteFlag == 1)
            {
                foreach (BillStatusModel m in bill_stauts_lst)
                {
                    if (m.DeleteFlag == 1)
                    {
                        return BillStatusCheckState.ExistDelete;
                    }
                }
                return BillStatusCheckState.Successful;
            }
            //一个表单类型下，只能有一个自定义标志的表单状态
            foreach (BillStatusModel m in bill_stauts_lst)
            {
                if (m.CustomFlag == billStatus.CustomFlag)
                {
                    return BillStatusCheckState.ExistCustom;
                }
            }
            return BillStatusCheckState.Successful;
        }

        /// <summary>
        /// 添加一个表单的状态
        /// </summary>
        /// <param name="loggingSession">当前登录信息</param>
        /// <param name="billStatus">要添加的表单状态的信息</param>
        /// <returns></returns>
        public bool InsertBillStatus(LoggingSessionInfo loggingSession, BillStatusModel billStatus)
        {
            if (string.IsNullOrEmpty(billStatus.Id))
                billStatus.Id = this.NewGuid();

            billStatus.CreateUserID = loggingSession.CurrentUser.User_Id;
            billStatus.CreateTime = this.GetCurrentDateTime();

            MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).Insert("BillStatus.Insert", billStatus);
            return true;
        }

        /// <summary>
        /// 检查是否能够删除某个表单状态
        /// </summary>
        /// <param name="loggingSession">当前登录信息</param>
        /// <param name="billStatusID">要删除的表单状态ID</param>
        /// <returns></returns>
        public bool CanDeleteBillStatus(LoggingSessionInfo loggingSession, string billStatusID)
        {
            //检查是否存在表单的状态=要删除的表单状态
            int ret = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<int>("BillStatus.CountUsingBillsById", billStatusID);
            if (ret > 0)
                return false;

            //检查是否存在角色与表单操作的关系中的前置状态或者当前状态=要删除的表单状态
            ret = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<int>("BillStatus.CountUsingBillActionRolesById", billStatusID);
            return ret == 0;
        }

        /// <summary>
        /// 删除一个表单状态
        /// </summary>
        /// <param name="loggingSession">当前登录信息</param>
        /// <param name="billStatusID">要删除的表单状态ID</param>
        /// <returns></returns>
        public bool DeleteBillStatus(LoggingSessionInfo loggingSession, string billStatusID)
        {
            int ret = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).Delete("BillStatus.DeleteById", billStatusID);
            return ret == 1;
        }

        #endregion

        #region 表单操作
        /// <summary>
        /// 查询某种表单类型下的所有的表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billKindID">表单类型ID</param>
        /// <returns></returns>
        public IList<BillActionModel> GetAllBillActionsByBillKindId(LoggingSessionInfo loggingSession, string billKindID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("BillKindId", billKindID);
            return MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForList<BillActionModel>("BillAction.SelectByKindId", ht);
        }
        /// <summary>
        /// 获取表单动作
        /// </summary>
        /// <param name="loggingSession">登录信息</param>
        /// <param name="billKindId">类型标识</param>
        /// <param name="billActionType">动作标识</param>
        /// <returns></returns>
        public BillActionModel GetBillAction(LoggingSessionInfo loggingSession, string billKindId, BillActionType billActionType)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("BillKindId", billKindId);
            hashTable.Add("BillActionCode", billActionType.ToString());

            BillActionModel billAction = null;
            switch (billActionType)
            {
                case BillActionType.Create:
                    billAction = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionModel>("BillAction.GetCreateActionByKindId", hashTable);
                    break;
                case BillActionType.Modify:
                    billAction = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionModel>("BillAction.GetModifyActionByKindId", hashTable);
                    break;
                case BillActionType.Approve:
                    billAction = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionModel>("BillAction.GetApproveActionByKindId", hashTable);
                    break;
                case BillActionType.Reject:
                    billAction = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionModel>("BillAction.GetRejectActionByKindId", hashTable);
                    break;
                case BillActionType.Cancel:
                    billAction = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionModel>("BillAction.GetCancelActionByKindId", hashTable);
                    break;
                case BillActionType.Open:
                    billAction = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionModel>("BillAction.GetOpenActionByKindId", hashTable);
                    break;
                case BillActionType.Stop:
                    billAction = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionModel>("BillAction.GetStopActionByKindId", hashTable);
                    break;
                default:
                    return null;
            }
            return billAction;
        }

        /// <summary>
        /// 根据表单标识，获取表单的当前按钮（操作）
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录的信息</param>
        /// <param name="billId">表单标识</param>
        /// <returns></returns>
        public BillActionInfo GetBillActionByBillId(LoggingSessionInfo loggingSessionInfo, string billId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("BillId", billId);
            _ht.Add("RoleId", GetBasicRoleId(loggingSessionInfo));
            return (BillActionInfo)MSSqlMapper.Instance().QueryForObject("BillActionInfo.GetBillActionByBillId", _ht);
        }

        /// <summary>
        /// 判断能否添加一个表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billAction">要添加的表单操作</param>
        /// <returns></returns>
        public BillActionCheckState CheckAddBillAction(LoggingSessionInfo loggingSession, BillActionModel billAction)
        {
            //获取表单类型下的所有的表单状态
            IList<BillActionModel> bill_action_lst = this.GetAllBillActionsByBillKindId(loggingSession, billAction.KindId);
            //为空，可以添加
            if (bill_action_lst == null || bill_action_lst.Count == 0)
            {
                return BillActionCheckState.Successful;
            }

            //检查编码
            foreach (BillActionModel m in bill_action_lst)
            {
                if (m.Code == billAction.Code)
                {
                    return BillActionCheckState.ExistCode;
                }
            }

            //一个表单类型下，只能有一个新建标志为1的表单操作
            if (billAction.CreateFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.CreateFlag == 1)
                    {
                        return BillActionCheckState.ExistCreateAction;
                    }
                }
                return BillActionCheckState.Successful;
            }

            //一个表单类型下，只能有一个修改标志为1的表单操作
            if (billAction.ModifyFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.ModifyFlag == 1)
                    {
                        return BillActionCheckState.ExistModifyAction;
                    }
                }
                return BillActionCheckState.Successful;
            }
            //一个表单类型下，只能有一个审核标志为1的表单操作
            if (billAction.ApproveFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.ApproveFlag == 1)
                    {
                        return BillActionCheckState.ExistApproveAction;
                    }
                }
            }
            //一个表单类型下，只能有一个退回标志为1的表单操作
            if (billAction.RejectFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.RejectFlag == 1)
                    {
                        return BillActionCheckState.ExistRejectAction;
                    }
                }
            }
            //一个表单类型下，只能有一个删除标志为1的表单操作
            if (billAction.CancelFlag == 1)
            {
                foreach (BillActionModel m in bill_action_lst)
                {
                    if (m.CancelFlag == 1)
                    {
                        return BillActionCheckState.ExistCancelAction;
                    }
                }
                return BillActionCheckState.Successful;
            }

            return BillActionCheckState.Successful;
        }


        /// <summary>
        /// 添加一个表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billAction"></param>
        /// <returns></returns>
        public bool InsertBillAction(LoggingSessionInfo loggingSession, BillActionModel billAction)
        {
            if (string.IsNullOrEmpty(billAction.Id))
                billAction.Id = this.NewGuid();

            billAction.CreateUserID = loggingSession.CurrentUser.User_Id;
            billAction.CreateTime = this.GetCurrentDateTime();

            MSSqlMapper.Instance().Insert("BillAction.Insert", billAction);
            return true;
        }

        /// <summary>
        /// 判断能否删除某个表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billActionID">表单操作ID</param>
        /// <returns></returns>
        public bool CanDeleteBillAction(LoggingSessionInfo loggingSession, string billActionID)
        {
            int ret = MSSqlMapper.Instance().QueryForObject<int>("BillAction.CountUsingBillAction", billActionID);
            return ret == 0;
        }

        /// <summary>
        /// 删除一个表单操作
        /// </summary>
        /// <param name="loggingSession">当前登录信息</param>
        /// <param name="billActionID">要删除的表单操作ID</param>
        /// <returns></returns>
        public bool DeleteBillAction(LoggingSessionInfo loggingSession, string billActionID)
        {
            int ret = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).Delete("BillAction.DeleteByID", billActionID);
            return ret == 1;
        }
        #endregion

        #region 表单
        /// <summary>
        /// 插入一个Bill的动作日志
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="bill">Bill信息</param>
        /// <param name="billAction">Bill的动作信息</param>
        /// <param name="billActionRole">Bill在当前角色下与Bill的动作的关联信息</param>
        /// <param name="remark">批注</param>
        private void InsertBillActionLog(LoggingSessionInfo loggingSession
            , BillModel bill
            , BillActionModel billAction,
            BillActionRoleModel billActionRole, string remark)
        {
            //BillActionLogInfo billActionLog = new BillActionLogInfo();
            //billActionLog.Id = NewGuid();
            //billActionLog.ActionUserId = loggingSession.CurrentUser.Id;
            //billActionLog.ActionDate = GetCurrentDateTime();
            //billActionLog.BillId = bill.Id;
            //billActionLog.BillActionId = billAction.Id;
            //billActionLog.PreviousStatus = billActionRole.PreviousStatus;
            //billActionLog.CurrentStatus = billActionRole.CurrentStatus;
            //billActionLog.ActionComment = remark;
            //OraMapper.Instance().Insert("BillActionLog.Insert", billActionLog);
        }

        /// <summary>
        /// 新建一个Bill
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="bill">Bill</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService InsertBill(LoggingSessionInfo loggingSession, BillModel bill)
        {
            //获取表单类别
            BillKindModel billKind = GetBillKindById(loggingSession, bill.KindId);
            if (billKind == null)
            {
                return BillOperateStateService.NotExistKind;
            }
            //获取表单状态
            BillStatusModel beginBillStatus = GetBillBeginStatus(loggingSession, bill.KindId);
            if (beginBillStatus == null)
            {
                return BillOperateStateService.NotSetBeginStatus;
            }
            BillActionModel billCreateAction = GetBillAction(loggingSession, bill.KindId, BillActionType.Create);
            if (billCreateAction == null)
            {
                return BillOperateStateService.NotSetCreateAction;
            }
            Hashtable hashTable = new Hashtable();

            hashTable.Add("RoleId", GetBasicRoleId(loggingSession.CurrentUserRole.RoleId));
            hashTable.Add("BillKindId", bill.KindId);
            hashTable.Add("BillActionId", billCreateAction.Id);
            hashTable.Add("PreviousBillStatus", beginBillStatus.Status);
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            BillActionRoleModel billCreateActionRole = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionRoleModel>("BillActionRole.SelectByKindIdAndActionIdAndRoleIdAndPreviousStatus", hashTable);
            if (billCreateActionRole == null)
            {
                return BillOperateStateService.NotAllowCreate;
            }
            ////判断是否超出系统设置时间
            //if (billCreateActionRole.ValidateDate == 0)
            //{
            //    return BillOperateStateService.OutDateTime;
            //}
            if (billKind.MoneyFlag == 1)
            {
                if ((bill.Money < billCreateActionRole.MinMoney) || (bill.Money > billCreateActionRole.MaxMoney))
                {
                    return BillOperateStateService.OutOfMoneyScope;
                }
            }

            bill.AddUserId = loggingSession.CurrentUser.User_Id;
            bill.AddDate = GetCurrentDateTime();                
            bill.Status = billCreateActionRole.CurrentStatus;
            MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).Insert("Bill.Insert", bill);
            InsertBillActionLog(loggingSession, bill, billCreateAction, billCreateActionRole, null);
            return BillOperateStateService.CreateSuccessful;
        }


        /// <summary>
        /// 根据Bill的Id查询Bill
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billId">Bill的Id</param>
        /// <returns></returns>
        public BillModel GetBillById(LoggingSessionInfo loggingSession, string billId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("BillId", billId);

            return MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillModel>("Bill.SelectById", hashTable);
        }

        #region delete bill
        /// <summary>
        /// 删除一个Bill(事务由调用方负责)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billId">Bill</param>
        /// <param name="remark">批注</param>
        /// <returns>操作的状态</returns>
        private BillOperateStateService DeleteBill(LoggingSessionInfo loggingSession, string billId, string remark)
        {
            BillModel bill = GetBillById(loggingSession, billId);
            if (bill == null)
            {
                return BillOperateStateService.NotExist;
            }

            BillKindModel billKind = GetBillKindById(loggingSession, bill.KindId);
            if (billKind == null)
            {
                return BillOperateStateService.NotExistKind;
            }
            BillStatusModel billStatus = GetBillStatusByKindIdAndStatus(loggingSession, billKind.Id, bill.Status);
            if (billStatus == null)
            {
                return BillOperateStateService.NotExistStatus;
            }
            BillStatusModel billDeleteStatus = GetBillDeleteStatus(loggingSession, billKind.Id);
            if (billDeleteStatus == null)
            {
                return BillOperateStateService.NotSetDeleteStatus;
            }
            BillActionModel billDeleteAction = GetBillAction(loggingSession, bill.KindId, BillActionType.Cancel);
            if (billDeleteAction == null)
            {
                return BillOperateStateService.NotSetModifyAction;
            }
            Hashtable hashTable = new Hashtable();
            //hashTable.Add("RoleId", loggingSession.CurrentUserRole.RoleId);
            hashTable.Add("RoleId", GetBasicRoleId(loggingSession.CurrentUserRole.RoleId));
            hashTable.Add("BillKindId", bill.KindId);
            hashTable.Add("BillActionId", billDeleteAction.Id);
            hashTable.Add("PreviousBillStatus", billStatus.Status);
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            BillActionRoleModel billDeleteActionRole = MSSqlMapper.Instance().QueryForObject<BillActionRoleModel>("BillActionRole.SelectByKindIdAndActionIdAndRoleIdAndPreviousStatus", hashTable);
            if (billDeleteActionRole == null)
            {
                return BillOperateStateService.NotAllowCancel;
            }
            ////判断是否超出系统设置时间
            //if (billDeleteActionRole.ValidateDate == 0)
            //{
            //    return BillOperateStateService.OutDateTime;
            //}
            if (billKind.MoneyFlag == 1)
            {
                if ((bill.Money < billDeleteActionRole.MinMoney) || (bill.Money > billDeleteActionRole.MaxMoney))
                {
                    return BillOperateStateService.OutOfMoneyScope;
                }
            }

            bill.ModifyUserId = loggingSession.CurrentUser.User_Id;
            bill.ModifyDate = GetCurrentDateTime();
            bill.Status = billDeleteActionRole.CurrentStatus;
            MSSqlMapper.Instance().Update("Bill.Update", bill);
            InsertBillActionLog(loggingSession, bill, billDeleteAction, billDeleteActionRole, remark);

            return BillOperateStateService.CancelSuccessful;
        }
        /// <summary>
        /// 批量删除Bill(含事务)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billList">要删除的Bill的Id的列表</param>
        /// <param name="remark">批注</param>
        /// <param name="billId">输出标识</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService DeleteBills(LoggingSessionInfo loggingSession, IList<string> billList, string remark, out string billId)
        {
            billId = null;
            if (billList == null || billList.Count == 0)
            {
                return BillOperateStateService.CancelSuccessful;
            }
            MSSqlMapper.Instance().BeginTransaction();
            try
            {
                BillOperateStateService state;
                foreach (string deleteBillId in billList)
                {
                    state = DeleteBill(loggingSession, deleteBillId, remark);
                    if (state != BillOperateStateService.CancelSuccessful)
                    {
                        MSSqlMapper.Instance().RollBackTransaction();
                        billId = deleteBillId;
                        return state;
                    }
                }
                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
            return BillOperateStateService.CancelSuccessful;
        }
        #endregion delete bill

        #region approve bill
        /// <summary>
        /// 批准一个Bill(事务由调用方负责)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billId">Bill</param>
        /// <param name="remark">批注</param>
        /// <param name="strBillActionType">状态</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService ApproveBill(LoggingSessionInfo loggingSession, string billId, string remark, BillActionType strBillActionType)
        {
            if (strBillActionType.Equals(""))
            {
                strBillActionType = BillActionType.Approve;
            }
            BillModel bill = GetBillById(loggingSession, billId);
            if (bill == null)
            {
                return BillOperateStateService.NotExist;
            }

            BillKindModel billKind = GetBillKindById(loggingSession, bill.KindId);
            if (billKind == null)
            {
                return BillOperateStateService.NotExistKind;
            }
            BillStatusModel currentBillStatus = GetBillStatusByKindIdAndStatus(loggingSession, billKind.Id, bill.Status);
            if (currentBillStatus == null)
            {
                return BillOperateStateService.NotExistStatus;
            }
            BillActionModel billApproveAction = GetBillAction(loggingSession, bill.KindId, strBillActionType);
            if (billApproveAction == null)
            {
                return BillOperateStateService.NotSetApproveAction;
            }
            Hashtable hashTable = new Hashtable();
            //hashTable.Add("RoleId", loggingSession.CurrentUserRole.RoleId);
            hashTable.Add("RoleId", GetBasicRoleId(loggingSession.CurrentUserRole.RoleId));
            hashTable.Add("BillKindId", bill.KindId);
            hashTable.Add("BillActionId", billApproveAction.Id);
            hashTable.Add("PreviousBillStatus", currentBillStatus.Status);
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            BillActionRoleModel billApproveActionRole = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionRoleModel>("BillActionRole.SelectByKindIdAndActionIdAndRoleIdAndPreviousStatus", hashTable);
            if (billApproveActionRole == null)
            {
                return BillOperateStateService.NotAllowApprove;
            }
            //判断是否超出系统设置时间
            //if (billApproveActionRole.ValidateDate == 0)
            //{
            //    return BillOperateStateService.OutDateTime;
            //}
            if (billKind.MoneyFlag == 1)
            {
                if ((bill.Money < billApproveActionRole.MinMoney) || (bill.Money > billApproveActionRole.MaxMoney))
                {
                    return BillOperateStateService.OutOfMoneyScope;
                }
            }

            bill.ModifyUserId = loggingSession.CurrentUser.User_Id;
            bill.ModifyDate = GetCurrentDateTime();
            bill.Status = billApproveActionRole.CurrentStatus;

            BillStatusModel nextBillStatus = GetBillStatusByKindIdAndStatus(loggingSession, billKind.Id, billApproveActionRole.CurrentStatus);
            if (nextBillStatus == null)
            {
                return BillOperateStateService.NotExistStatus;
            }

            MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).Update("Bill.Update", bill);
            InsertBillActionLog(loggingSession, bill, billApproveAction, billApproveActionRole, remark);
        
            return BillOperateStateService.ApproveSuccessful;
        }


        /// <summary>
        /// 批准一个Bill(事务由调用方负责) Jermyn2012-09-17
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billId">Bill</param>
        /// <param name="remark">批注</param>
        /// <param name="strBillActionType">状态</param>
        /// <param name="strError">输出错误参数</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService ApproveBill(LoggingSessionInfo loggingSession, string billId, string remark, BillActionType strBillActionType, out string strError)
        {
            if (strBillActionType.Equals(""))
            {
                strError = "BillActionType.Approve";
                strBillActionType = BillActionType.Approve;
            }
            BillModel bill = GetBillById(loggingSession, billId);
            if (bill == null)
            {
                strError = "BillOperateStateService.NotExist";
                return BillOperateStateService.NotExist;
            }

            BillKindModel billKind = GetBillKindById(loggingSession, bill.KindId);
            if (billKind == null)
            {
                strError = "BillOperateStateService.NotExistKind";
                return BillOperateStateService.NotExistKind;
            }
            BillStatusModel currentBillStatus = GetBillStatusByKindIdAndStatus(loggingSession, billKind.Id, bill.Status);
            if (currentBillStatus == null)
            {
                strError = "BillOperateStateService.NotExistStatus";
                return BillOperateStateService.NotExistStatus;
            }
            BillActionModel billApproveAction = GetBillAction(loggingSession, bill.KindId, strBillActionType);
            if (billApproveAction == null)
            {
                strError = "BillOperateStateService.NotSetApproveAction";
                return BillOperateStateService.NotSetApproveAction;
            }
            Hashtable hashTable = new Hashtable();
            //hashTable.Add("RoleId", loggingSession.CurrentUserRole.RoleId);
            hashTable.Add("RoleId", GetBasicRoleId(loggingSession.CurrentUserRole.RoleId));
            hashTable.Add("BillKindId", bill.KindId);
            hashTable.Add("BillActionId", billApproveAction.Id);
            hashTable.Add("PreviousBillStatus", currentBillStatus.Status);
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            BillActionRoleModel billApproveActionRole = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionRoleModel>("BillActionRole.SelectByKindIdAndActionIdAndRoleIdAndPreviousStatus", hashTable);
            if (billApproveActionRole == null)
            {
                strError = "BillOperateStateService.NotAllowApprove--"
                    //+ "RoleId:" + GetBasicRoleId(loggingSession.CurrentUserRole.RoleId).ToString() + "----"
                    //+ "BillKindId:" + bill.KindId + "----"
                    //+ "BillActionId:" + billApproveAction.Id + "----"
                    //+ "PreviousBillStatus:" + currentBillStatus.Status + "----"
                         + "InoutStatus" + bill.Status + "--" + bill.BillStatusDescription + "--" + bill.Id
                          ;
                return BillOperateStateService.NotAllowApprove;
            }
            //判断是否超出系统设置时间
            //if (billApproveActionRole.ValidateDate == 0)
            //{
            //    return BillOperateStateService.OutDateTime;
            //}
            if (billKind.MoneyFlag == 1)
            {
                if ((bill.Money < billApproveActionRole.MinMoney) || (bill.Money > billApproveActionRole.MaxMoney))
                {
                    strError = "BillOperateStateService.OutOfMoneyScope";
                    return BillOperateStateService.OutOfMoneyScope;
                }
            }

            bill.ModifyUserId = loggingSession.CurrentUser.User_Id;
            bill.ModifyDate = GetCurrentDateTime();
            bill.Status = billApproveActionRole.CurrentStatus;

            BillStatusModel nextBillStatus = GetBillStatusByKindIdAndStatus(loggingSession, billKind.Id, billApproveActionRole.CurrentStatus);
            if (nextBillStatus == null)
            {
                strError = "BillOperateStateService.NotExistStatus";
                return BillOperateStateService.NotExistStatus;
            }

            MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).Update("Bill.Update", bill);
            InsertBillActionLog(loggingSession, bill, billApproveAction, billApproveActionRole, remark);
            strError = "BillOperateStateService.ApproveSuccessful";
            return BillOperateStateService.ApproveSuccessful;
        }
       

        /// <summary>
        /// 批量批准Bill(含事务)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billList">要批准的Bill的Id的列表</param>
        /// <param name="remark">批注</param>
        /// <param name="strBillActionType">第一个不能批准的Bill的Code</param>
        /// <param name="billId">输出标识</param>
        /// <returns>操作的状态</returns>
        public BillOperateStateService ApproveBills(LoggingSessionInfo loggingSession, IList<string> billList, string remark, BillActionType strBillActionType, out string billId)
        {
            billId = null;
            //如果没有审核的,则直接返回成功
            if (billList == null || billList.Count == 0)
            {
                return BillOperateStateService.ApproveSuccessful;
            }
            //查找审核后的状态是最终状态的表单的种类的编码列表, 主要是为了刷新物化视图用
            //IList<string> bill_kind_code_list = GetApprovedBillKindCodes(loggingSession, billList);

            MSSqlMapper.Instance().BeginTransaction();
            try
            {
                BillOperateStateService state;
                foreach (string approveBillId in billList)
                {
                    state = ApproveBill(loggingSession, approveBillId, remark, strBillActionType);
                    if (state != BillOperateStateService.ApproveSuccessful)
                    {
                        MSSqlMapper.Instance().RollBackTransaction();
                        billId = approveBillId;
                        return state;
                    }
                }
                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }

            //根据表单类型编码刷新对应的物化视图
            //if (bill_kind_code_list != null)
            //{
            //    foreach (string code in bill_kind_code_list)
            //    {
            //        RefreshMViewByBillKindCode(code);
            //    }
            //}
            return BillOperateStateService.ApproveSuccessful;
        }

        #endregion approve bill
        #endregion


        #region 判断是否有新建权限
        /// <summary>
        /// 查看当前用户能否新建一个Bill
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="unitRelationModeId">单位的关联模式</param>
        /// <param name="billKindId">新建的Bill的种类的Id</param>
        /// <returns></returns>
        public bool CanCreateBill(LoggingSessionInfo loggingSession, string unitRelationModeId, string billKindId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("UserId", loggingSession.CurrentUser.User_Id);
            hashTable.Add("RoleId", GetBasicRoleId(loggingSession));
            hashTable.Add("UnitRelationModeId", unitRelationModeId);
            hashTable.Add("BillKindId", billKindId);

            int count = MSSqlMapper.Instance().QueryForObject<int>("BillActionRole.CountCreateActionRoleByKindIdAndRoleId", hashTable);
            return count > 0;
        }

        /// <summary>
        /// 查看当前用户能否新建一个表单种类的表单
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="billKindCode">新建的Bill的种类的编码</param>
        /// <returns></returns>
        public bool CanCreateBillKind(LoggingSessionInfo loggingSession, string billKindCode)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("UserId", loggingSession.CurrentUser.User_Id);
            hashTable.Add("RoleId", GetBasicRoleId(loggingSession));
            hashTable.Add("BillKindCode", billKindCode);

            int count = MSSqlMapper.Instance().QueryForObject<int>("BillActionRole.CountCreateActionRoleByKindCodeAndRoleId", hashTable);
            return count > 0;
        }
        /// <summary>
        /// 根据标识判断Bill是否存在
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="billId"></param>
        /// <returns></returns>
        public bool CanHaveBill(LoggingSessionInfo loggingSessionInfo, string billId,out string strError)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("BillId", billId);

            int count = MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Bill.CanHaveBill", _ht);
            strError = count.ToString();
            return count > 0;   
        }

        /// <summary>
        /// 查看当前用户能否批准一个Bill
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录用户的Session信息</param>
        /// <param name="unitRelationModeId">单位的关联模式</param>
        /// <param name="billId">Bill的Id</param>
        /// <returns></returns>
        public bool CanApproveBill(LoggingSessionInfo loggingSessionInfo, string unitRelationModeId, string billId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("UserId", loggingSessionInfo.CurrentUser.User_Id);
            hashTable.Add("RoleId", GetBasicRoleId(loggingSessionInfo));
            hashTable.Add("UnitRelationModeId", unitRelationModeId);
            hashTable.Add("BillId", billId);

            int count = MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Bill.CountApproveBill", hashTable);
            return count > 0;
        }
        #endregion 

        #region 角色与表单操作

        /// <summary>
        /// 查询角色与表单操作的关系
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="condition">BillKindID:表单类型;RoleID:角色;</param>
        /// <returns></returns>
        public IList<SelectBillActionRoleInfo> SelectBillActionRoleList(LoggingSessionInfo loggingSession, Hashtable condition)
        {
            return MSSqlMapper.Instance().QueryForList<SelectBillActionRoleInfo>("BillActionRole.SelectByCondition", condition);
        }

        /// <summary>
        /// 添加一个角色与表单操作的关系
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billActionRole">角色与表单操作的关系</param>
        /// <returns></returns>
        public bool InsertBillActionRole(LoggingSessionInfo loggingSession, BillActionRoleModel billActionRole)
        {
            if (string.IsNullOrEmpty(billActionRole.Id))
                billActionRole.Id = this.NewGuid();

            billActionRole.CreateUserID = loggingSession.CurrentUser.User_Id;
            billActionRole.CreateTime = this.GetCurrentDateTime();

            MSSqlMapper.Instance().Insert("BillActionRole.Insert", billActionRole);
            return true;
        }

        /// <summary>
        /// 删除一个角色与表单操作的关系
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billActionRoleID">角色与表单操作的关系的ID</param>
        /// <returns></returns>
        public bool DeleteBillActionRole(LoggingSessionInfo loggingSession, string billActionRoleID)
        {
            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                int ret = MSSqlMapper.Instance().Delete("BillActionRole.Delete", billActionRoleID);
                if (ret != 1)
                {
                    MSSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
                else
                {
                    MSSqlMapper.Instance().CommitTransaction();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 判断能否添加一个角色与表单操作的关系
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="billActionRole">要添加的角色与表单操作的关系</param>
        /// <returns></returns>
        public BillActionRoleCheckState CheckAddBillActionRole(LoggingSessionInfo loggingSession, BillActionRoleModel billActionRole)
        {
            Hashtable ht = new Hashtable();
            //查询表单操作
            ht.Add("BillActionId", billActionRole.ActionId);
            BillActionModel bill_action = MSSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<BillActionModel>("BillAction.SelectById", ht);
            if (bill_action == null)
            {
                return BillActionRoleCheckState.NotExistAction;
            }
            
            //查询角色与表单的关系列表
            ht.Add("BillKindID", billActionRole.KindId);
            ht.Add("RoleID", billActionRole.RoleId);
            IList<SelectBillActionRoleInfo> bill_action_role_lst = this.SelectBillActionRoleList(loggingSession, ht);
            //角色与表单的关系为空，可以添加
            if (bill_action_role_lst == null || bill_action_role_lst.Count == 0)
            {
                return BillActionRoleCheckState.Successful;
            }

            //一个角色对一个表单，只能有一个新建操作
            if (bill_action.CreateFlag == 1)
            {
                foreach (BillActionRoleModel m in bill_action_role_lst)
                {
                    if (m.RoleId == billActionRole.RoleId && m.ActionId == billActionRole.ActionId)
                    {
                        return BillActionRoleCheckState.ExistCreate;
                    }
                }
                return BillActionRoleCheckState.Successful;
            }

            //一个角色对一个表单，对同一个前置状态，只能有一个修改、审核、退回操作
            foreach (BillActionRoleModel m in bill_action_role_lst)
            {
                if (m.RoleId == billActionRole.RoleId && m.ActionId == billActionRole.ActionId && m.PreviousStatus == billActionRole.PreviousStatus)
                {
                    //修改操作
                    if (bill_action.ModifyFlag == 1)
                    {
                        return BillActionRoleCheckState.ExistModify;
                    }
                    else
                    {
                        //审核操作
                        if (bill_action.ApproveFlag == 1)
                        {
                            return BillActionRoleCheckState.ExistApprove;
                        }
                        else
                        {
                            //退回操作
                            if (bill_action.RejectFlag == 1)
                            {
                                return BillActionRoleCheckState.ExistReject;
                            }
                            else
                            {
                                //删除操作
                                return BillActionRoleCheckState.ExistCancel;
                            }
                        }
                    }
                }
            }
            return BillActionRoleCheckState.Successful;
        }
        #endregion

    }
}
