using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    public class ObjectImagesService : BaseService
    {
        #region 中间层
        #region 图片数据处理
        /// <summary>
        /// 获取未打包的商品数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public int GetItemNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString().Trim());
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("ObjectImages.SelectUnDownloadCount", _ht);//目前问题
        }
        /// <summary>
        /// 需要打包的商品信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<ObjectImagesInfo> GetItemListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex)
        {
            //用的hashtable
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString().Trim());
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<ObjectImagesInfo>("ObjectImages.SelectUnDownload", _ht);
        }

        /// <summary>
        /// 设置打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemInfoList">商品集合</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetItemBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<ObjectImagesInfo> ObjectImagesInfoList, out string strError)
        {
            
            ObjectImagesInfo itemInfo = new ObjectImagesInfo();
            itemInfo.LastUpdateBy = loggingSessionInfo.CurrentUser.User_Id;
            itemInfo.LastUpdateTime = GetCurrentDateTime2();
            itemInfo.BatId = bat_id;
            itemInfo.ObjectImagesInfoList = ObjectImagesInfoList;
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ObjectImages.UpdateUnDownloadBatId", itemInfo);//目前问题
            strError = "Success";
            return true;
        }
        /// <summary>
        /// 更新Item表打包标识方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetItemIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string BatId, out string strError)
        {
            ObjectImagesInfo itemInfo = new ObjectImagesInfo();
            itemInfo.BatId = BatId;
            itemInfo.LastUpdateBy = loggingSessionInfo.CurrentUser.User_Id;
            itemInfo.LastUpdateTime = GetCurrentDateTime2();//重写了一个方法，原来是返回的字符串
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ObjectImages.UpdateUnDownloadIfFlag", itemInfo);
            strError = "Success";
            return true;
        }
        #endregion
        #endregion
    }
}
