using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model
{
    /// <summary>
    /// AdOrder
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class AdOrderInfo
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 单据号码
        /// </summary>
        public string order_code { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string order_date { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public string date_start { get; set; }
        /// <summary>
        /// 终止日期
        /// </summary>
        public string date_end { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public string time_start { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        public string time_end { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        public string playbace_no { get; set; }
        /// <summary>
        /// 广告文件地址
        /// </summary>
        public string url_address { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string status_desc { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        [XmlIgnore()]
        public IList<AdOrderInfo> List { get; set; }
    }
}
