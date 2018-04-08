/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/15 11:20:10
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
using System.Data;
using System.Text;

namespace JIT.Utility.ETCL2.DataItem
{
    /// <summary>
    /// 数据行项 
    /// </summary>
    public class DataRowItem:IETCLDataItem
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DataRowItem()
        {
        }
        #endregion

        #region IETCLDataItem 成员

        /// <summary>
        /// 数据条目是数据集中的索引
        /// <remarks>
        /// <para>索引从0开始</para>
        /// </remarks>
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 数据条目数据
        /// </summary>
        public DataRow OriginalRow { get; set; }

        /// <summary>
        /// 数据条目
        /// <remarks>
        /// <para>隐式接口实现</para>
        /// </remarks>
        /// </summary>
        object IETCLDataItem.OriginalRow
        {
            get
            {
                return this.OriginalRow;
            }
            set
            {
                var val = value as DataRow;
                this.OriginalRow = val;
            }
        }

        #endregion
    }
}
