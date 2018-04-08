/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/2 11:33:12
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

namespace JIT.Utility.Report
{
    /// <summary>
    /// 事实数据接口
    /// <remarks>
    /// <para>1.空接口，表名类是事实数据</para>
    /// </remarks>
    /// </summary>
    public interface IFactData
    {
        /// <summary>
        /// 根据属性名获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pPropertyName"></param>
        /// <returns></returns>
        object GetData(string pPropertyName);
    }
}
