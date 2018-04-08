/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/9 17:25:28
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

using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Export.DataFormatter
{
    /// <summary>
    /// 报表数据的格式化器 
    /// </summary>
    public interface IDataFormatter
    {
        /// <summary>
        /// 往导出后的容器中写数据
        /// </summary>
        /// <param name="pContainer">导出后的容器,如果导出为Excel,则为WookSheet</param>
        /// <param name="pData">数据值</param>
        /// <param name="pRowIndex">行</param>
        /// <param name="pColumnIndex">列</param>
        /// <param name="pColumnType">表格列的列类型</param>
        /// <param name="pGridColumn">ExtJS的表格列</param>
        void WriteData(object pContainer, object pData, int pRowIndex, int pColumnIndex, ColumnTypes pColumnType,Column pGridColumn);
    }
}
