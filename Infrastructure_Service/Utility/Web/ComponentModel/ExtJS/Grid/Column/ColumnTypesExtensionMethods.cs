/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/2 11:48:07
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column
{
    /// <summary>
    /// GridColumnTypes的扩展方法
    /// </summary>
    public static class ColumnTypesExtensionMethods
    {
        /// <summary>
        /// 扩展方法：根据表格列(Ext JS)的数据类型获得相应的.net的类型
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static Type GetDotNetType(this ColumnTypes pCaller)
        {
            switch (pCaller)
            {
                case ColumnTypes.Int:
                    return typeof(int);
                case ColumnTypes.Decimal:
                    return typeof(decimal);
                case ColumnTypes.Boolean:
                    return typeof(bool);
                case ColumnTypes.String:
                    return typeof(string);
                case ColumnTypes.Date:
                    return typeof(DateTime);
                case ColumnTypes.DateTime:
                    return typeof(DateTime);
                case ColumnTypes.Timespan:
                    return typeof(int);
                case ColumnTypes.Coordinate:
                    return typeof(string);
                case ColumnTypes.Photo:
                    return typeof(string);
                case ColumnTypes.Percent:
                    return typeof(decimal);
                case ColumnTypes.Customize:
                    return typeof(string);
                default:
                    throw new NotImplementedException(string.Format("未实现枚举值[{0}]的转换处理.",pCaller));
            }
        }

        /// <summary>
        /// 扩展方法：根据表格列(Ext JS)的数据类型获得相应的javascript的类型
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string GetJavascriptType(this ColumnTypes pCaller)
        {
            switch (pCaller)
            {
                case ColumnTypes.Int:
                case ColumnTypes.Timespan:
                    return "int";
                case ColumnTypes.Decimal:
                case ColumnTypes.Percent:
                    return "float";
                case ColumnTypes.Boolean:
                    return "boolean";
                case ColumnTypes.String:
                case ColumnTypes.Coordinate:
                case ColumnTypes.Photo:
                case ColumnTypes.Customize:
                    return "string";
                case ColumnTypes.DateTime:
                case ColumnTypes.Date:
                    return "date";
                default:
                    throw new NotImplementedException(string.Format("未实现枚举值[{0}]的转换处理.", pCaller));
            }
        }
    }
}
