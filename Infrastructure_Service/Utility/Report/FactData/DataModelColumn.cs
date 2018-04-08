/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 9:53:48
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

namespace JIT.Utility.Report.FactData
{
    /// <summary>
    /// 最明细的数据列
    /// </summary>
    public class DataModelColumn
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DataModelColumn()
        {
        }
        #endregion

        /// <summary>
        /// 属性名或列名
        /// <remarks>
        /// <para>如果SQL语句中有多张表中都包含相同的字段,则可以用表别名来限定属性名.例如:tableA.fieldA</para>
        /// </remarks>
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 数据列的类型(维度列或事实列)
        /// </summary>
        public DataModelColumnTypes ColumnType { get; set; }

        /// <summary>
        /// 数据列如果是维度列时，所关联的维度
        /// </summary>
        public IDimension RelatedDim { get; set; }
    }
}
