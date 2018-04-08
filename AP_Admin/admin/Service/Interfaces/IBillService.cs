using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cPos.Admin.Model.Bill;

namespace cPos.Admin.Service.Interfaces
{
    /// <summary>
    /// 表单接口
    /// </summary>
    public interface IBillService
    {
        #region 表单的种类

        #endregion


        #region 表单的状态

        #endregion

        #region 表单的动作

        #endregion

        #region 表单的动作与角色的关系

        #endregion

        #region 表单

        #endregion

        #region 表单的日志
        /// <summary>
        /// 对表单进行操作
        /// </summary>
        /// <param name="loggingSession">当前登录用户的信息</param>
        /// <param name="billKindCode">表单类型编码</param>
        /// <param name="billIdList">表单ID列表(以逗号分隔表单ID)</param>
        /// <param name="haveFlow">表单是否走流程</param>
        /// <param name="actionFlagType">表单操作的标志的类型</param>
        /// <param name="actionFlagValue">表单操作的标志的值</param>
        /// <param name="comment">备注</param>
        void ActionBills(Component.LoggingSessionInfo loggingSession, string billKindCode, string billIdList,
             bool haveFlow, Model.Bill.BillActionFlagType actionFlagType, int actionFlagValue, string comment);

        /// <summary>
        /// 获取某个表单的操作历史
        /// </summary>
        /// <param name="billID">表单ID</param>
        /// <returns></returns>
        IList<BillActionLogInfo> GetBillActionLogHistoryList(string billID);
        #endregion

    }
}
