/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/12 10:03:37
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

using JIT.Utility.ETCL2.ValueObject;

namespace JIT.Utility.ETCL2
{
    /// <summary>
    /// ETCL过程接口
    /// <remarks>
    /// <para>E=Extraction,从数据源抽取数据</para>
    /// <para>T=Transfer,将数据源的数据进行清洗转换,最后获得一个强类型的数据结构</para>
    /// <para>C=Check,对转换后的数据进行检查,主要的检查有三类：数据有效性、数据重复性、数据完整性（外键依赖）</para>
    /// <para>L=Load,检查通过后,将数据写入到目标数据源中</para>
    /// </remarks>
    /// </summary>
    public interface IETCL
    {
        /// <summary>
        /// 执行ETCL处理
        /// </summary>
        /// <returns>处理结果</returns>
        ETCLProcessResult Process();
    }
}
