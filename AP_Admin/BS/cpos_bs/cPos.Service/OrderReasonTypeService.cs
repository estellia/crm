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
    /// 订单类型2服务
    /// </summary>
    public class OrderReasonTypeService
    {
        #region 根据单据类型获取单据明细类型集合
        /// <summary>
        /// 根据单据类型获取单据明细类型集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="parent_reason_type_code">单据类型号码</param>
        /// <returns></returns>
        public IList<OrderReasonTypeInfo> GetOrderReasonTypeListByOrderTypeCode(LoggingSessionInfo loggingSessionInfo, string parent_reason_type_code)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ParentCode", parent_reason_type_code);
                return cSqlMapper.Instance().QueryForList<OrderReasonTypeInfo>("OrderReasonType.SelectByParentReasonTypeCode", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion
    }
}
