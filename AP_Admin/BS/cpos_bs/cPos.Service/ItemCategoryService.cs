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
    /// 商品类别类
    /// </summary>
    public class ItemCategoryService:BaseService
    {
        #region 查询
        /// <summary>
        /// 查询商品类别信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录信息</param>
        /// <param name="item_category_code">号码</param>
        /// <param name="item_category_name">名称</param>
        /// <param name="pyzjm">拼音助记码</param>
        /// <param name="status">状态</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public ItemCategoryInfo SearchItemCategoryList(LoggingSessionInfo loggingSessionInfo
                                                            , string item_category_code
                                                            , string item_category_name
                                                            , string pyzjm
                                                            , string status
                                                            , int maxRowCount
                                                            , int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("item_category_code", item_category_code);
            _ht.Add("item_category_name", item_category_name);
            _ht.Add("pyzjm", pyzjm);
            _ht.Add("status", status);
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);

            ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
            int iCount = cSqlMapper.Instance().QueryForObject<int>("ItemCategory.SearchCount", _ht);

            IList<ItemCategoryInfo> itemCategoryInfoList = new List<ItemCategoryInfo>();
            itemCategoryInfoList = cSqlMapper.Instance().QueryForList<ItemCategoryInfo>("ItemCategory.Search", _ht);

            itemCategoryInfo.ICount = iCount;
            itemCategoryInfo.ItemCategoryInfoList = itemCategoryInfoList;
            return itemCategoryInfo;
            //return cSqlMapper.Instance().QueryForList<ItemCategoryInfo>("ItemCategory.Search", _ht);
        }

        #endregion

        #region 修改状态
        /// <summary>
        /// 设置商品类别状态
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="item_category_id">商品类别标识</param>
        /// <param name="status">修改值</param>
        /// <returns></returns>
        public string SetItemCategoryStatus(LoggingSessionInfo loggingSessionInfo, string item_category_id, string status)
        {
            string strResult = string.Empty;
            try
            {
                //设置要改变的用户信息
                ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
                itemCategoryInfo.Status = status;
                itemCategoryInfo.Item_Category_Id = item_category_id;
                itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                itemCategoryInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                //提交
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemCategory.UpdateStatus", itemCategoryInfo);
                strResult = "状态修改成功.";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return strResult;
        }

        #endregion
        /// <summary>
        /// 获取所有的商品类别
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<ItemCategoryInfo> GetItemCagegoryList(LoggingSessionInfo loggingSessionInfo)
        {
            try
            {
                return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<ItemCategoryInfo>("ItemCategory.SelectAll", "");
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取单个商品类别信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="Item_Category_Id"></param>
        /// <returns></returns>
        public ItemCategoryInfo GetItemCategoryById(LoggingSessionInfo loggingSessionInfo, string Item_Category_Id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("Item_Category_Id", Item_Category_Id);
                return (ItemCategoryInfo)cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject("ItemCategory.SelectById", _ht);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region 保存
        /// <summary>
        /// 保存商品类别（新建或者修改）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemCategoryInfo"></param>
        /// <returns></returns>
        public string SetItemCategoryInfo(LoggingSessionInfo loggingSessionInfo, ItemCategoryInfo itemCategoryInfo)
        {
            string strResult = string.Empty;
            
                //事物信息
                cSqlMapper.Instance().BeginTransaction();

                try
                {
                    //处理是新建还是修改
                    if (itemCategoryInfo.Item_Category_Id == null || itemCategoryInfo.Item_Category_Id.Equals(""))
                    {
                        itemCategoryInfo.Item_Category_Id = NewGuid();
                    }
                    else
                    {
                    }

                    //1 判断组织号码是否唯一
                    if (!IsExistItemCategoryCode(loggingSessionInfo, itemCategoryInfo.Item_Category_Code, itemCategoryInfo.Item_Category_Id))
                    {
                        strResult = "商品类别号码已经存在。";
                        return strResult;
                    }

                 

                    //2.提交用户信息
                    if (!SetItemCategoryTableInfo(loggingSessionInfo, itemCategoryInfo))
                    {
                        strResult = "提交用户表失败";
                        return strResult;
                    }


                    cSqlMapper.Instance().CommitTransaction();
                    strResult = "保存成功!";
                    return strResult;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
        
        
        /// <summary>
        /// 判断号码是否重复
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="item_category_code"></param>
        /// <param name="item_category_id"></param>
        /// <returns></returns>
 
        public bool IsExistItemCategoryCode(LoggingSessionInfo loggingSessionInfo, string item_category_code,string item_category_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("Item_Category_Code", item_category_code);
                _ht.Add("Item_Category_Id", item_category_id);
                int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("ItemCategory.IsExsitItemCategoryCode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex) {
                throw (ex); 
            }
        }
        /// <summary>
        /// 保存商品类别信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemCategoryInfo"></param>
        /// <returns></returns>
        public bool SetItemCategoryTableInfo(LoggingSessionInfo loggingSessionInfo,ItemCategoryInfo itemCategoryInfo)
        {
            try
            {
                if (itemCategoryInfo != null)
                {
                    itemCategoryInfo.Status = "1";
                    if (itemCategoryInfo.Create_User_Id == null || itemCategoryInfo.Create_User_Id.Equals(""))
                    {
                        itemCategoryInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemCategoryInfo.Create_Time = GetCurrentDateTime();
                    }
                    if (itemCategoryInfo.Modify_User_Id == null || itemCategoryInfo.Modify_User_Id.Equals(""))
                    {
                        itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemCategoryInfo.Modify_Time = GetCurrentDateTime();
                    }
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemCategory.InsertOrUpdate", itemCategoryInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 中间层
        #region 商品数据处理
        /// <summary>
        /// 获取未打包的商品类别数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public int GetItemCategoryNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("customerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString().Trim());
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("ItemCategory.SelectUnDownloadCount", _ht);
        }
        /// <summary>
        /// 需要打包的商品类别信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public IList<ItemCategoryInfo> GetItemCategoryListPackaged(LoggingSessionInfo loggingSessionInfo)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("customerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString().Trim());
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<ItemCategoryInfo>("ItemCategory.SelectUnDownload", _ht);
        }

        /// <summary>
        /// 设置打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="ItemCategoryInfoList">商品集合</param>
        /// <returns></returns>
        public bool SetItemCategoryBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<ItemCategoryInfo> ItemCategoryInfoList)
        {
            ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
            itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            itemCategoryInfo.Modify_Time = GetCurrentDateTime();
            itemCategoryInfo.bat_id = bat_id;
            itemCategoryInfo.ItemCategoryInfoList = ItemCategoryInfoList;
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemCategory.UpdateUnDownloadBatId", itemCategoryInfo);
            return true;
           
        }
        /// <summary>
        /// 更新Item Category表打包标识方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <returns></returns>
        public bool SetItemCategoryIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id)
        {
            ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
            itemCategoryInfo.bat_id = bat_id;
            itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            itemCategoryInfo.Modify_Time = GetCurrentDateTime();
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemCategory.UpdateUnDownloadIfFlag", itemCategoryInfo);
           
            return true;
           
        }
        #endregion
        #endregion
    }
}

