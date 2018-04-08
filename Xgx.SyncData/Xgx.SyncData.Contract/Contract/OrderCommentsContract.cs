using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract.Contract
{
    public class OrderCommentsContract : IZmindToXgx
    {
        /// <summary>
        /// 增删改标志
        /// </summary>
        public OptEnum Operation { get; set; }

        /// <summary>
        /// 增删改标志
        /// </summary>
        public int CommentType { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string SkuID { get; set; }

        /// <summary>
        /// 订单评价，描述相符
        /// </summary>
        public int StarLevel1 { get; set; }

        /// <summary>
        /// 服务态度
        /// </summary>
        public int StarLevel2 { get; set; }

        /// <summary>
        /// 发货速度
        /// </summary>
        public int StarLevel3 { get; set; }

        /// <summary>
        /// 商品评级
        /// </summary>
        public int ItemLevel { get; set; }

        /// <summary>
        /// 商品评价
        /// </summary>
        public string ItemComment { get; set; }

        /// <summary>
        /// 会员编号
        /// </summary>
        public string VipId { get; set; }

        /// <summary>
        /// 是否匿名
        /// </summary>
        public int IsAnonymity { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}
