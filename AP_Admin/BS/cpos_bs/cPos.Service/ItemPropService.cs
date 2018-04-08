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
    /// 商品属性关系
    /// </summary>
    public class ItemPropService:BaseService
    {
        /// <summary>
        /// 获取商品的属性集合
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public IList<ItemPropInfo> GetItemPropListByItemId(LoggingSessionInfo loggingSessionInfo, string itemId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("ItemId", itemId);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<ItemPropInfo>("ItemProp.SelectByItemId", _ht);
        }

        /// <summary>
        /// 设置商品与商品属性关系
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemPropInfo(LoggingSessionInfo loggingSessionInfo, ItemInfo itemInfo)
        {
            try
            {
                if (itemInfo.ItemPropList != null)
                {
                    foreach (ItemPropInfo itemPropInfo in itemInfo.ItemPropList)
                    {
                        if (itemPropInfo.Item_Property_Id == null || itemPropInfo.Item_Property_Id.Equals(""))
                        {
                            itemPropInfo.Item_Property_Id = NewGuid();
                        }
                    }
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemProp.InsertOrUpdate", itemInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取商品级别的属性集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public IList<PropByItemInfo> GetPropByItemList(LoggingSessionInfo loggingSessionInfo)
        {
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<PropByItemInfo>("ItemProp.SelectItemProp", "");

        }
    }
}
