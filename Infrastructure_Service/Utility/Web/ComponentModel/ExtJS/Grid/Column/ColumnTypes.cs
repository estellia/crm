/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 13:31:55
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
using System.ComponentModel;
using System.Text;

namespace JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column
{
    /// <summary>
    /// 表格的表格列的类型 
    /// </summary>
    public enum ColumnTypes
    {
        /// <summary>
        /// 整数，数值靠右对齐
        /// </summary>
        [Description("int")]
        Int
        ,
        /// <summary>
        /// 定点小数，数值靠右对齐，小数点后只保留两位
        /// </summary>
        [Description("decimal")]
        Decimal
        ,
        /// <summary>
        /// 布尔，如果是true则为是，为false则为否，否则为空
        /// </summary>
        [Description("boolean")]
        Boolean
        ,
        /// <summary>
        /// 字符串，文本靠左对齐
        /// </summary>
        [Description("string")]
        String
        ,
        /// <summary>
        /// 日期，数据类型为Date。格式化为yyyy-MM-dd，即4位年+2位月+2位日
        /// </summary>
        [Description("date")]
        Date
        ,
        /// <summary>
        /// 日期时间，数据类型为Date。格式化为 yyyy-MM-dd HH:mi:ss，即4位年+2位月+2位日+2位24小时制的小时+2位分+2位秒
        /// </summary>
        [Description("datetime")]
        DateTime
        ,
        /// <summary>
        /// 时间，数据类型为Date。格式化为HH:mi:ss，即2位24小时制的小时+2位分+2位秒
        /// </summary>
        [Description("time")]
        Time
        ,
        /// <summary>
        /// 时间跨度，数据为int，值为时间跨度的秒数。按 xxx天xxx小时xxx分xxx秒的方式格式化，最后只保留2节。
        /// <remarks>
        /// <para>例如：</para>
        /// <para>1天14小时</para>
        /// <para>1小时14分</para>
        /// <para>58分38秒</para>
        /// <para>44秒</para>
        /// </remarks>
        /// </summary>
        [Description("timespan")]
        Timespan
        ,
        /// <summary>
        /// 坐标，数据为字符串，该字符串的值以 经度,纬度 的固定格式组成。
        /// </summary>
        [Description("coordinate")]
        Coordinate
        ,
        /// <summary>
        /// 照片，数据为字符串，该字符串的值以固定的格式来表示手机提交的照片
        /// </summary>
        [Description("photo")]
        Photo
        ,
        /// <summary>
        /// 百分比列,数据为定点小数。可以通过指定accuracy属性来设置百分值保留的小数位
        /// </summary>
        [Description("percent")]
        Percent
        ,
        /// <summary>
        /// 自定义列,由外部来设置表格列
        /// </summary>
        [Description("customize")]
        Customize
    }
}
