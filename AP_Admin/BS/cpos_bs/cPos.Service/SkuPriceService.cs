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
    /// sku价格
    /// </summary>
    public class SkuPriceService : BaseService
    {
        #region 下载

        /// <summary>
        /// 获取未打包的Sku零售价格数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="ItemPriceTypeId">价格类型</param>
        /// <returns></returns>
        public int GetSkuPriceNotPackagedCount(LoggingSessionInfo loggingSessionInfo,string ItemPriceTypeId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("ItemPriceTypeId", ItemPriceTypeId);
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("SkuPrice.SelectUnDownloadCount", _ht);
        }
        /// <summary>
        /// 需要打包的sku零售价格信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="ItemPriceTypeId">价格类型</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<SkuPriceInfo> GetSkuPriceListPackaged(LoggingSessionInfo loggingSessionInfo,string ItemPriceTypeId, int maxRowCount, int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("ItemPriceTypeId", ItemPriceTypeId);
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<SkuPriceInfo>("SkuPrice.SelectUnDownload", _ht);
        }

        /// <summary>
        /// 设置零售价格打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemPriceTypeId">价格类型</param>
        /// <param name="SkuPriceInfoList">SKU集合</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetSkuPriceBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id,string ItemPriceTypeId, IList<SkuPriceInfo> SkuPriceInfoList, out string strError)
        {
            SkuPriceInfo skuPriceInfo = new SkuPriceInfo();
            skuPriceInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
            skuPriceInfo.modify_time = GetCurrentDateTime();
            skuPriceInfo.bat_id = bat_id;
            skuPriceInfo.SkuPriceInfoList = SkuPriceInfoList;
            skuPriceInfo.item_price_type_id = ItemPriceTypeId;
            skuPriceInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("SkuPrice.UpdateUnDownloadBatId", skuPriceInfo);
            strError = "Success";
            return true;
        }
        /// <summary>
        /// 更新Item零售价格表打包标识方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetSkuPriceIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, out string strError)
        {
            SkuPriceInfo skuPriceInfo = new SkuPriceInfo();
            skuPriceInfo.bat_id = bat_id;
            skuPriceInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
            skuPriceInfo.modify_time = GetCurrentDateTime();
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("SkuPrice.UpdateUnDownloadIfFlag", skuPriceInfo);
            strError = "Success";
            return true;
        }

        #endregion
    }
}
