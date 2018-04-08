/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/25 17:43:45
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
using System.Text;

using Newtonsoft.Json;

namespace JIT.Utility.Web.ComponentModel
{
    /// <summary>
    /// 树节点 
    /// </summary>
    public class TreeNode : ICloneable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TreeNode()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 节点的文本
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// 节点的标识符
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        /// <summary>
        /// 父节点的标识符
        /// </summary>
        [JsonIgnore]
        public string ParentID { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [JsonProperty(PropertyName = "leaf")]
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 是否为 --请选择-- 项
        /// </summary>
        [JsonIgnore]
        public bool IsPleaseSelectItem { get; set; }

        /// 促销商品数量
        /// </summary>

        [JsonProperty(PropertyName = "promotionItemCount")]
        public int PromotionItemCount { get; set; }
        /// 创建时间
        /// </summary>
        [JsonProperty(PropertyName = "create_time")]
        public string create_time { get; set; }
        /// 分类图片
        /// </summary>
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
        public int NodeLevel { get; set; }
        public int DisplayIndex { get; set; }
        /// <summary>
        /// 佣金比例
        /// </summary>
        [JsonProperty(PropertyName = "CommissionRate")]
        public int CommissionRate { get; set; }
        [JsonProperty(PropertyName = "ItemGroupType")]
        public int ItemGroupType { get; set; }
        /// <summary>
        /// 分类颜色
        /// </summary>
        [JsonProperty(PropertyName = "Color")]
        public string Color { get; set; }

        /// <summary>
        /// 是否偏好
        /// </summary>
        [JsonProperty(PropertyName = "IsPreference")]
        public string IsPreference { get; set; }
        #endregion

        #region ICloneable 成员
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region 深拷贝
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        public TreeNode Clone()
        {
            return (TreeNode)this.MemberwiseClone();
        }
        #endregion

    }
}
