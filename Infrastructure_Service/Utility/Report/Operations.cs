/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/16 10:52:55
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

namespace JIT.Utility.Report
{
    /// <summary>
    /// 报表的操作 
    /// </summary>
    public enum Operations
    {
        /// <summary>
        /// 查询
        /// </summary>
        Query=1
        ,
        /// <summary>
        /// 钻入
        /// </summary>
        DrillIn=2
        ,
        /// <summary>
        /// 跳转到指定的钻取剖面
        /// </summary>
        Goto=3
        ,
        /// <summary>
        /// 改变数据透视
        /// </summary>
        ChangePivot=4
        ,
        /// <summary>
        /// 改变行列转换
        /// </summary>
        ChangeCRConversion=5
        ,
        /// <summary>
        /// 导出到Excel
        /// </summary>
        ExportToExcel=6
        ,
        /// <summary>
        /// 行列互换
        /// </summary>
        CRExchange =7
        ,
        /// <summary>
        /// 查看明细
        /// </summary>
        ViewDetail =8
    }
}
