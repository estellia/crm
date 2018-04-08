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
    /// sku服务
    /// </summary>
    public class SkuService:BaseService
    {
        #region sku基本操作
        /// <summary>
        /// 根据商品获取Sku
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuListByItemId(LoggingSessionInfo loggingSessionInfo, string itemId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("ItemId", itemId);
            return MSSqlMapper.Instance().QueryForList<SkuInfo>("Sku.SelectByItemId", _ht);
        }
        /// <summary>
        /// 获取所有sku
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuList(LoggingSessionInfo loggingSessionInfo)
        {
            return MSSqlMapper.Instance().QueryForList<SkuInfo>("Sku.Select", "");
        }
        /// <summary>
        /// 根据sku标识获取sku明细
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="skuId"></param>
        /// <returns></returns>
        public SkuInfo GetSkuInfoById(LoggingSessionInfo loggingSessionInfo, string skuId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("SkuId", skuId);
            return (SkuInfo)MSSqlMapper.Instance().QueryForObject("Sku.SelectById", _ht);
        }

        /// <summary>
        /// 设置Sku
        /// </summary>
        /// <param name="itemInfo">item info</param>
       /// <param name="strError">输出错误信息</param>
       /// <returns></returns>
        public bool SetSkuInfo(ItemInfo itemInfo,out string strError)
        {
            try
            {
                if (itemInfo.SkuList != null)
                {
                    foreach (SkuInfo skuInfo in itemInfo.SkuList)
                    {
                        if (skuInfo.sku_id == null || skuInfo.sku_id.Equals(""))
                        {
                            skuInfo.sku_id = NewGuid();
                        }
                        if (!IsExistBarcode(skuInfo.barcode, skuInfo.sku_id))
                        {
                            strError = "Barcode号码已经存在。";
                            return false;
                        }
                    }
                    MSSqlMapper.Instance().Update("Sku.InsertOrUpdate", itemInfo);
                }
                else {
                    MSSqlMapper.Instance().Update("Sku.InsertOrUpdate", itemInfo);
                }
                strError = "处理sku信息成功";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 判断barcode是否重复
        /// </summary>
        /// <param name="loggingSessionInfo">login model</param>
        /// <param name="barcode">条形码</param>
        /// <param name="sku_id">sku标识</param>
        /// <returns></returns>
        public bool IsExistBarcode(string barcode, string sku_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("Barcode", barcode);
                _ht.Add("SkuId", sku_id);
                int n = MSSqlMapper.Instance().QueryForObject<int>("Item.IsExsitBarcode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 查询barcode是否存在
        /// </summary>
        public bool CheckBarcode(string barcode, string sku_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("Barcode", barcode);
                _ht.Add("SkuId", sku_id);
                int n = MSSqlMapper.Instance().QueryForObject<int>("Item.IsExsitBarcode", _ht);
                return n > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 查询sku
        /// <summary>
        /// 模糊查询sku集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuInfoByLike(LoggingSessionInfo loggingSessionInfo, string condition)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("Condition", condition);
                return MSSqlMapper.Instance().QueryForList<SkuInfo>("Sku.SelectByCondition", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 通过条码查询SKU
        /// </summary>
        public SkuInfo GetSkuByBarcode(string barcode)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("barcode", barcode);
                return MSSqlMapper.Instance().QueryForObject<SkuInfo>("Sku.GetSkuByBarcode", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 中间层
        #region 商品数据处理
        /// <summary>
        /// 获取未打包的SKU数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public int GetSkuNotPackagedCount()
        {
            return MSSqlMapper.Instance().QueryForObject<int>("Sku.SelectUnDownloadCount", "");
        }
        /// <summary>
        /// 需要打包的Sku信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuListPackaged(int maxRowCount, int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            return MSSqlMapper.Instance().QueryForList<SkuInfo>("Sku.SelectUnDownload", _ht);
        }

        /// <summary>
        /// 设置打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="SkuInfoList">商品集合</param>
        /// <returns></returns>
        public bool SetSkuBatInfo(string bat_id, IList<SkuInfo> SkuInfoList, string user_id)
        {
            SkuInfo skuInfo = new SkuInfo();
            skuInfo.modify_user_id = user_id;
            skuInfo.modify_time = GetCurrentDateTime();
            skuInfo.bat_id = bat_id;
            skuInfo.SkuInfoList = SkuInfoList;
            MSSqlMapper.Instance().Update("Sku.UpdateUnDownloadBatId", skuInfo);
            return true;
        }
        /// <summary>
        /// 更新Sku表打包标识方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <returns></returns>
        public bool SetSkuIfFlagInfo(string bat_id, string user_id)
        {
            SkuInfo skuInfo = new SkuInfo();
            skuInfo.bat_id = bat_id;
            skuInfo.modify_user_id = user_id;
            skuInfo.modify_time = GetCurrentDateTime();
            MSSqlMapper.Instance().Update("Sku.UpdateUnDownloadIfFlag", skuInfo);
            return true;
        }
        #endregion
        #endregion
    }
}
