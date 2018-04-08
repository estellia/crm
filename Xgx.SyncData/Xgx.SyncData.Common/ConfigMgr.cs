using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xgx.SyncData.Common
{
    public static class ConfigMgr
    {
        internal static string RabbitMqHost
        {
            get
            {
                if (string.IsNullOrEmpty(_rabbitMqHost))
                {
                    _rabbitMqHost = ConfigurationManager.AppSettings["RabbitMqHost"];
                }
                return _rabbitMqHost;
            }
        }
        public static string CustomerId
        {
            get
            {
                if (string.IsNullOrEmpty(_customerId))
                {
                    _customerId = ConfigurationManager.AppSettings["CustomerId"];
                }
                return _customerId;
            }
        }
        /// <summary>
        /// 微信原始ID
        /// </summary>
        public static string WeiXinID
        {
            get
            {
                if (string.IsNullOrEmpty(_weiinId))
                {
                    _weiinId = ConfigurationManager.AppSettings["WeiXinID"];
                }
                return _weiinId;
            }
        }
        public static string HeadUnitId
        {
            get
            {
                if (string.IsNullOrEmpty(_headUnitId))
                {
                    _headUnitId = ConfigurationManager.AppSettings["HeadUnitId"];
                }
                return _headUnitId;
            }
        }
        public static string XgxHeadUnitId
        {
            get
            {
                if (string.IsNullOrEmpty(_xgxHeadUnitId))
                {
                    _xgxHeadUnitId = ConfigurationManager.AppSettings["XgxHeadUnitId"];
                }
                return _xgxHeadUnitId;
            }
        }
        public static string HeadItemCategoryId
        {
            get
            {
                if (string.IsNullOrEmpty(_headItemCategoryId))
                {
                    _headItemCategoryId = ConfigurationManager.AppSettings["HeadItemCategoryId"];
                }
                return _headItemCategoryId;
            }
        }
        public static string ItemPriceTypeId_OriginalPrice
        {
            get
            {
                if (string.IsNullOrEmpty(_itemPriceTypeId_OriginalPrice))
                {
                    _itemPriceTypeId_OriginalPrice = ConfigurationManager.AppSettings["ItemPriceTypeId_OriginalPrice"];
                }
                return _itemPriceTypeId_OriginalPrice;
            }
        }
        public static string ItemPriceTypeId_RetailPrice
        {
            get
            {
                if (string.IsNullOrEmpty(_itemPriceTypeId_RetailPrice))
                {
                    _itemPriceTypeId_RetailPrice = ConfigurationManager.AppSettings["ItemPriceTypeId_RetailPrice"];
                }
                return _itemPriceTypeId_RetailPrice;
            }
        }
        public static string ItemPriceTypeId_Inventory
        {
            get
            {
                if (string.IsNullOrEmpty(_itemPriceTypeId_Inventory))
                {
                    _itemPriceTypeId_Inventory = ConfigurationManager.AppSettings["ItemPriceTypeId_Inventory"];
                }
                return _itemPriceTypeId_Inventory;
            }
        }
        public static string ItemPriceTypeId_SalesVolume
        {
            get
            {
                if (string.IsNullOrEmpty(_itemPriceTypeId_SalesVolume))
                {
                    _itemPriceTypeId_SalesVolume = ConfigurationManager.AppSettings["ItemPriceTypeId_SalesVolume"];
                }
                return _itemPriceTypeId_SalesVolume;
            }
        }
        public static string VirtualGoodsSkuId
        {
            get
            {
                if (string.IsNullOrEmpty(_virtualGoodsSkuId))
                {
                    _virtualGoodsSkuId = ConfigurationManager.AppSettings["VirtualGoodsSkuId"];
                }
                return _virtualGoodsSkuId;
            }
        }
        private static string _rabbitMqHost;
        private static string _customerId;
        private static string _headUnitId;
        private static string _xgxHeadUnitId;
        private static string _headItemCategoryId;
        private static string _itemPriceTypeId_OriginalPrice;
        private static string _itemPriceTypeId_RetailPrice;
        private static string _itemPriceTypeId_Inventory;
        private static string _itemPriceTypeId_SalesVolume;
        private static string _virtualGoodsSkuId;
        private static string _weiinId;
    }
}
