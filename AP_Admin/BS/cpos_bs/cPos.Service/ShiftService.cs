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
    /// 班次服务
    /// </summary>
    public class ShiftService
    {
        /// <summary>
        /// 设置班次信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录信息</param>
        /// <param name="shiftInfoList">班次集合</param>
        /// <param name="strError">错误信息输出</param>
        /// <returns></returns>
        public bool SetShiftInfoList(LoggingSessionInfo loggingSessionInfo, IList<ShiftInfo> shiftInfoList, out string strError)
        {
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                ShiftInfo shiftInfo = new ShiftInfo();
                shiftInfo.ShiftListInfo = shiftInfoList;
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Shift.InsertOrUpdate", shiftInfo);
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                strError = "";
                return true;
            }
            catch (Exception ex) {
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                strError = ex.ToString();
                throw (ex);
            }

        }
    }
}
