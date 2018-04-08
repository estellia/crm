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
    /// 商品服务
    /// </summary>
    public class ItemService:BaseService
    {
        #region 查询商品
        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="item_code">商品号码</param>
        /// <param name="item_name">商品名称</param>
        /// <param name="item_category_id">类型标识</param>
        /// <param name="status">状态</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public ItemInfo SearchItemList(LoggingSessionInfo loggingSessionInfo
                                                            , string item_code
                                                            , string item_name
                                                            , string item_category_id
                                                            , string status
                                                            , int maxRowCount
                                                            , int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("item_code", item_code);
            _ht.Add("item_name", item_name);
            _ht.Add("item_category_id", item_category_id);
            _ht.Add("status", status);

            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);

            ItemInfo itemInfo = new ItemInfo();
            int iCount = cSqlMapper.Instance().QueryForObject<int>("Item.SearchCount", _ht);

            IList<ItemInfo> ItemInfoList = new List<ItemInfo>();
            ItemInfoList = cSqlMapper.Instance().QueryForList<ItemInfo>("Item.Search", _ht);

            itemInfo.ICount = iCount;
            itemInfo.ItemInfoList = ItemInfoList;
            return itemInfo;

            //return cSqlMapper.Instance().QueryForList<ItemInfo>("Item.Search", _ht);
        }
        /// <summary>
        /// 获取所有的商品
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<ItemInfo> GetItemAllList(LoggingSessionInfo loggingSessionInfo)
        {
            return cSqlMapper.Instance().QueryForList<ItemInfo>("Item.SelectAll", "");
        }
        /// <summary>
        /// 获取单个商品信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public ItemInfo GetItemInfoById(LoggingSessionInfo loggingSessionInfo, string item_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("Item_Id", item_id);
            ItemInfo itemInfo = new ItemInfo();
            //获取基础信息
            itemInfo = (ItemInfo)cSqlMapper.Instance().QueryForObject("Item.SelectById", _ht);
            //获取商品与属性关系信息
            itemInfo.ItemPropList = new ItemPropService().GetItemPropListByItemId(loggingSessionInfo, item_id);
            //获取商品与商品价格类型信息
            itemInfo.ItemPriceList = new ItemPriceService().GetItemPriceListByItemId(loggingSessionInfo, item_id);
            //获取商品与sku关系信息
            itemInfo.SkuList = new SkuService().GetSkuListByItemId(loggingSessionInfo, item_id);
            return itemInfo;
        }
        #endregion

        #region 处理商品状态
        /// <summary>
        /// 停用启用商品
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="Item_Id">商品标识</param>
        /// <param name="Status">状态（停用-1,启用1）</param>
        /// <returns></returns>
        public string SetItemStatus(LoggingSessionInfo loggingSessionInfo, string Item_Id, string Status)
        {
            string strResult = string.Empty;
            try
            {
                //设置要改变的用户信息
                ItemInfo itenInfo = new ItemInfo();
                itenInfo.Status = Status;
                itenInfo.Item_Id = Item_Id;
                itenInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                itenInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                //提交
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Item.UpdateStatus", itenInfo);
                strResult = "状态修改成功.";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return strResult;
        }
        #endregion

        #region 商品保存
        /// <summary>
        /// 设置商品信息（修改,新建）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemInfo">商品对象</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public bool SetItemInfo(LoggingSessionInfo loggingSessionInfo, ItemInfo itemInfo,out string strError)
        {
            string strResult = string.Empty;
            //事物信息
            cSqlMapper.Instance().BeginTransaction();
            try
            {
                if (itemInfo.Item_Id == null || itemInfo.Item_Id.Equals(""))
                {
                    itemInfo.Item_Id = NewGuid();
                }
                else
                {
                }
                //1.判断号码唯一
                if (!IsExistItemCode(loggingSessionInfo, itemInfo.Item_Code, itemInfo.Item_Id))
                {
                    strError = "商品类别号码已经存在。";
                    cSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
                
                
                //2.处理基础信息
                if(!SetItemTableInfo(loggingSessionInfo,itemInfo)){
                    strError = "处理商品信息表失败。";
                    cSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
                //3.处理商品属性
                if (!new ItemPropService().SetItemPropInfo(loggingSessionInfo, itemInfo))
                {
                    strError = "处理商品与商品属性信息表失败。";
                    cSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
                //4.处理商品价格
                if (!new ItemPriceService().SetItemPriceInfo(loggingSessionInfo, itemInfo))
                {
                    strError = "处理商品与商品价格类型以及价格信息表失败。";
                    cSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
                //5.处理sku
                if (!new SkuService().SetSkuInfo(loggingSessionInfo, itemInfo,out strResult))
                {
                    strError = strResult;
                    cSqlMapper.Instance().RollBackTransaction();
                    return false;
                }
                cSqlMapper.Instance().CommitTransaction();
                strError = "保存成功!";
                return true;
            }
            catch (Exception ex) {
                cSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
            
        }

        /// <summary>
        /// 判断商品号码是否重复
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="item_code"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public bool IsExistItemCode(LoggingSessionInfo loggingSessionInfo, string item_code, string item_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ItemCode", item_code);
                _ht.Add("ItemId", item_id);
                int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Item.IsExsitItemCode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        

        /// <summary>
        /// 设置商品主表信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemTableInfo(LoggingSessionInfo loggingSessionInfo, ItemInfo itemInfo)
        {
            try
            {
                if (itemInfo != null)
                {
                    itemInfo.Status = "1";
                    itemInfo.Status_Desc = "正常";
                    if (itemInfo.Create_User_Id == null || itemInfo.Create_User_Id.Equals(""))
                    {
                        itemInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemInfo.Create_Time = GetCurrentDateTime();
                    }
                    if (itemInfo.Modify_User_Id == null || itemInfo.Modify_User_Id.Equals(""))
                    {
                        itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemInfo.Modify_Time = GetCurrentDateTime();
                    }
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Item.InsertOrUpdate", itemInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        


        #endregion

        #region 根据商品号码或者名称模糊查询商品
        /// <summary>
        /// 模糊查询商品信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public IList<ItemInfo> GetItemListLikeItemName(LoggingSessionInfo loggingSessionInfo, string itemName)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ItemName", itemName);
                return cSqlMapper.Instance().QueryForList<ItemInfo>("Item.SelectLikeItemName", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 中间层
        #region 商品数据处理
        /// <summary>
        /// 获取未打包的商品数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public int GetItemNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("customerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString().Trim());
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Item.SelectUnDownloadCount", _ht);
        }
        /// <summary>
        /// 需要打包的商品信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<ItemInfo> GetItemListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("customerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString().Trim());
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<ItemInfo>("Item.SelectUnDownload", _ht);
        }

        /// <summary>
        /// 设置打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemInfoList">商品集合</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetItemBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<ItemInfo> ItemInfoList,out string strError)
        {
                ItemInfo itemInfo = new ItemInfo();
                itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                itemInfo.Modify_Time = GetCurrentDateTime();
                itemInfo.bat_id = bat_id;
                itemInfo.ItemInfoList = ItemInfoList;
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Item.UpdateUnDownloadBatId", itemInfo);
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
        public bool SetItemIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, out string strError)
        {
                ItemInfo itemInfo = new ItemInfo();
                itemInfo.bat_id = bat_id;
                itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                itemInfo.Modify_Time = GetCurrentDateTime();
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Item.UpdateUnDownloadIfFlag", itemInfo);
                strError = "Success";
                return true;
        }
        #endregion
        #endregion
    }
}
