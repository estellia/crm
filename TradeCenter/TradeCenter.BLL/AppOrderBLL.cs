/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/8 15:45:54
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.TradeCenter.Entity;

namespace JIT.TradeCenter.BLL
{
    /// <summary>
    /// 业务处理： 应用订单表 
    /// </summary>
    public partial class AppOrderBLL
    {
        #region 创建事务
        /// <summary>
        /// 创建事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction CreateTran()
        {
            return this._currentDAO.CreateTran();
        }
        #endregion

        #region 查询交易中心交易订单信息
        /// <summary>
        /// 查询交易中心交易订单信息
        /// </summary>
        /// <param name="pAppID"></param>
        /// <param name="pAppOrderID"></param>
        /// <param name="pAppClientID"></param>
        /// <returns></returns>
        public AppOrderEntity GetByAppInfo(int pAppID, string pAppOrderID, string pAppClientID)
        {
            List<IWhereCondition> conds = new List<IWhereCondition>{
                new EqualsCondition(){ FieldName="AppID",Value=pAppID},
                new EqualsCondition(){ FieldName="AppOrderID",Value=pAppOrderID},
                new EqualsCondition(){ FieldName="AppClientID", Value=pAppClientID}};
            var list = this.Query(conds.ToArray(), null);
            if (list.Length > 0)
                return list[0];
            else
                throw new Exception(string.Format("未找到此订单,订单号:{0}  客户号:{1}  APPID:{2}", pAppOrderID, pAppClientID, pAppID));
        }
        #endregion

        #region 删除交易中心订单 更新为不可用
        /// <summary>
        /// 删除交易中心订单 更新为不可用
        /// </summary>
        /// <param name="pAppClientID"></param>
        /// <param name="pAppOrderID"></param>
        /// <param name="pAppID"></param>
        /// <param name="pTran"></param>
        public void DeleteByAppInfo(string pAppClientID, string pAppOrderID, int pAppID, SqlTransaction pTran)
        {
            this._currentDAO.DeleteByAppInfo(pAppClientID, pAppOrderID, pAppID, pTran);
        }
        #endregion

        #region 获取已支付未通知交易中心订单信息
        /// <summary>
        /// 获取已支付未通知交易中心订单信息
        /// </summary>
        /// <returns></returns>
        public AppOrderEntity[] GetNotNodify()
        {
            return this._currentDAO.GetNotNodify();
        }
        #endregion

        #region 获取当天未支付成功的交易中心订单
        /// <summary>
        /// 获取当天未支付成功的交易中心订单
        /// </summary>
        /// <returns></returns>
        public AppOrderEntity[] GetUnpaidOrder()
        {
            return _currentDAO.GetUnpaidOrder();
        }
        #endregion

        #region 通过AppOrderId查询交易中心订单
        /// <summary>
        /// 通过AppOrderId查询交易中心订单
        /// </summary>
        /// <param name="appOrderId"></param>
        /// <returns></returns>
        public AppOrderEntity GetAppOrderByAppOrderId(string appOrderId)
        {
            return this._currentDAO.GetAppOrderByAppOrderId(appOrderId);
        }
        #endregion
    }
}