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
    /// pos小票类
    /// </summary>
    public class PosInoutService:BaseService
    {
        #region POS 小票查询
        /// <summary>
        /// pos小票查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">订单号</param>
        /// <param name="unit_code">门店</param>
        /// <param name="item_name">商品</param>
        /// <param name="order_date_begin">开始日期</param>
        /// <param name="order_date_end">结束日期</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public InoutInfo SearchPosInfo(LoggingSessionInfo loggingSessionInfo
                                            , string order_no
                                            , string unit_code
                                            , string item_name
                                            , string order_date_begin
                                            , string order_date_end
                                            , int maxRowCount
                                            , int startRowIndex
                                            )
        {
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.order_no = order_no;
            orderSearchInfo.unit_code = unit_code;
            orderSearchInfo.item_name = item_name;
            orderSearchInfo.order_date_begin = order_date_begin;
            orderSearchInfo.order_date_end = order_date_end;
            orderSearchInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            orderSearchInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            
            return new InoutService().SearchInoutInfo(loggingSessionInfo, orderSearchInfo);
        }
        #endregion

    }
}
