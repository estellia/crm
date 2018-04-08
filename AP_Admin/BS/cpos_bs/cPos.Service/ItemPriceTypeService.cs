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
    /// 商品价格类型
    /// </summary>
    public class ItemPriceTypeService
    {
        /// <summary>
        /// 获取所有价格类型
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<ItemPriceTypeInfo> GetItemPriceTypeList(LoggingSessionInfo loggingSessionInfo)
        {
            return cSqlMapper.Instance().QueryForList<ItemPriceTypeInfo>("ItemPriceType.SelectAll", "");
        }
        /// <summary>
        /// 根据商品价格类型标识获取商品价格类型信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="item_price_type_id"></param>
        /// <returns></returns>
        public ItemPriceTypeInfo GetItemPriceTypeById(LoggingSessionInfo loggingSessionInfo, string item_price_type_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("ItemPriceTypeId", item_price_type_id);
            return (ItemPriceTypeInfo)cSqlMapper.Instance().QueryForObject("ItemPriceType.SelectById", _ht);
        }
    }
}
