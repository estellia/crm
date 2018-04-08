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
    /// 配送单服务
    /// </summary>
    public class DistributionService
    {
        #region 获取未审批的配送单
        /// <summary>
        /// 根据状态与组织获取相关的配送单集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unit_id">组织</param>
        /// <param name="status">状态值</param>
        /// <returns></returns>
        public IList<InoutInfo> GetDistributionListByStatus(LoggingSessionInfo loggingSessionInfo, string unit_id, string status)
        {
            try
            {
                IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
                OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
                orderSearchInfo.unit_id = unit_id;
                //orderSearchInfo.sales_unit_id = unit_id;
                orderSearchInfo.status = status;
                orderSearchInfo.order_type_id = "6F4991A2F4A84CC3902BD880BF540DF1";
                orderSearchInfo.order_reason_id = "BAFA1B7A50914599BD7DC830B53203FA";
                orderSearchInfo.sales_unit_id_not_equal = unit_id;
                orderSearchInfo.purchase_unit_id = unit_id;
                orderSearchInfo.if_flag = "0";
                orderSearchInfo.StartRow = 0;
                orderSearchInfo.EndRow = 36500;

                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo = new InoutService().SearchInoutInfo(loggingSessionInfo, orderSearchInfo);
                inoutInfoList = inoutInfo.InoutInfoList;
                foreach (InoutInfo inout in inoutInfoList)
                {
                    inout.InoutDetailList = new InoutService().GetInoutDetailInfoByOrderId(loggingSessionInfo, inout.order_id);
                }
                return inoutInfoList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
    }
}
