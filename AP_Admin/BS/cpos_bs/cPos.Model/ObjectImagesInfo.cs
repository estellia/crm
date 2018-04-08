using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Model
{
    public class ObjectImagesInfo
    {
        /// <summary>
        /// 图片标识【保存必须】
        /// </summary>
        public string ImageId { get; set; }
        /// <summary>
        /// 商品标识
        /// </summary>
        public string ObjectId { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayIndex { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastUpdateBy { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 是否已经删除
        /// </summary>
        public int IsDelete { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int IfFlag { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string BatId { get; set; }


        /// <summary>
        /// 商品与属性关系集合
        /// </summary>
        [XmlIgnore()]
        public IList<ObjectImagesInfo> ObjectImagesInfoList { get; set; }
    }
}
