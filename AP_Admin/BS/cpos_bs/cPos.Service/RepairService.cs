using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// 一键修复
    /// </summary>
    public class RepairService:BaseService
    {
        #region 保存
        /// <summary>
        /// 一键报修上传保存
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="repairInfos">一键修复内容集合</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public bool SetRepairInfo(LoggingSessionInfo loggingSessionInfo, IList<RepairInfo> repairInfos, out string strError)
        {
            cSqlMapper.Instance().BeginTransaction();
            try
            {
                RepairInfo ri = new RepairInfo();
                if (repairInfos != null)
                {
                    //foreach (RepairInfo repairInfo in repairInfos)
                    //{ 
                        
                    //}
                    ri.modify_time = GetCurrentDateTime();
                    ri.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    ri.repairInfoList = repairInfos;
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Repair.InsertOrUpdate", ri);
                }
                cSqlMapper.Instance().CommitTransaction();
                strError = "ok";
                return true;
            }
            catch (Exception ex) {
                cSqlMapper.Instance().RollBackTransaction();
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 一键修复信息查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unit_id">组织标识</param>
        /// <param name="status">状态</param>
        /// <param name="repair_date_begin">报修日期起</param>
        /// <param name="repair_date_end">报修日期止</param>
        /// <param name="maxRowCount">每页行数</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public RepairInfo SearchRepairInfo(LoggingSessionInfo loggingSessionInfo
                                        , string unit_id
                                        , string status
                                        , string repair_date_begin
                                        , string repair_date_end
                                        , int maxRowCount, int startRowIndex)
        {
            try
            {
                RepairInfo repairInfo = new RepairInfo();
                Hashtable hashtable = new Hashtable();
                hashtable.Add("unit_id", unit_id);
                hashtable.Add("status", status);
                hashtable.Add("repair_date_begin", repair_date_begin);
                hashtable.Add("repair_date_end", repair_date_end);
                hashtable.Add("StartRow", startRowIndex);
                hashtable.Add("EndRow", startRowIndex + maxRowCount);

                int iCount = cSqlMapper.Instance().QueryForObject<int>("Repair.SearchCount", hashtable);

                IList<RepairInfo> repairInfoList = new List<RepairInfo>();
                repairInfoList = cSqlMapper.Instance().QueryForList<RepairInfo>("Repair.Search", hashtable);

                repairInfo.icount = iCount;
                repairInfo.repairInfoList = repairInfoList;

                return repairInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 修改状态（响应）
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="repairInfo">修改报修内容状态信息</param>
        /// <param name="strMessage">提示信息</param>
        /// <returns></returns>
        public bool SetRepairStatus(LoggingSessionInfo loggingSessionInfo, RepairInfo repairInfo, out string strMessage)
        {
            try
            {
                repairInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                repairInfo.modify_time = GetCurrentDateTime(); //获取当前时间
                //提交
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("User.UpdateStatus", repairInfo);
                strMessage = "ok";
                return true;
            }
            catch (Exception ex) {
                strMessage = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 获取单个报修信息
        /// <summary>
        /// 获取单个报修信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="repair_id">标识</param>
        /// <returns></returns>
        public RepairInfo GetRepairInfoById(LoggingSessionInfo loggingSessionInfo, string repair_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("repair_id", repair_id);
                return (RepairInfo)cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject("Repair.SelectById", _ht);       
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
    }
}
