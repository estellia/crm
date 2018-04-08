/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/11/1 10:18:27
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

namespace JIT.Const
{
    /// <summary>
    /// 表列数据类型
    /// <remarks>
    /// <para>作用:</para>
    /// <para>1.用于数据库元数据中声明列的数据类型</para>
    /// <para>注意:</para>
    /// <para>1.枚举的类型必须是byte,否则同步时进行序列化/反序列化时会出错</para>
    /// </remarks>
    /// </summary>
    public enum JITTableColumnDataTypes:byte
    {
        /// <summary>
        /// Null值,仅用于在数据通信层面的数据的序列化，在列的数据类型的申明中不用
        /// </summary>
        Null = 0
        ,
        /// <summary>
        /// 整数
        /// </summary>
        Int=1
        ,
        /// <summary>
        /// 浮点数
        /// </summary>
        Float=2
        ,
        /// <summary>
        /// 日期型
        /// </summary>
        DateTime=3
        ,
        /// <summary>
        /// 字符串
        /// </summary>
        String=4
        ,
        /// <summary>
        /// 布尔
        /// </summary>
        Boolean=5
        ,
        /// <summary>
        /// GUID
        /// </summary>
        GUID=6
        ,
        /// <summary>
        /// 定点小数
        /// </summary>
        Decimal = 7
        ,
        /// <summary>
        /// 图片(路径)
        /// </summary>
        Image = 8
    }
}
