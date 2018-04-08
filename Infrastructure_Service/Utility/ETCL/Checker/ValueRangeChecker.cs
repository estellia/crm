using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Checker
{
    /// <summary>
    /// 取值范围检查器
    /// </summary>
    public class ValueRangeChecker : IChecker
    {
        /// <summary>
        /// 待检查的ETCL列信息
        /// </summary>
        private ETCLColumn _etclColumn;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="pETCLColumn">ETCL列信息</param>
        public ValueRangeChecker(ETCLColumn pETCLColumn)
        {
            this._etclColumn = pETCLColumn;
        }

        /// <summary>
        /// 执行取值范围检查
        /// </summary>
        /// <param name="pPropertyValue">待检查的数据值</param>
        /// <param name="pPropertyText">此数据的属性名称（列描述）</param>
        /// <param name="pRowData">当前数据项的信息</param>
        /// <param name="oResult">检查结果</param>
        /// <returns>TRUE：通过检查，FALSE：未通过检查。</returns>
        public bool Process(object pPropertyValue, string pPropertyText, IETCLDataItem pRowData, ref IETCLResultItem oResult)
        {
            bool isPass = true;
            if (pPropertyValue == null)
                return isPass;
            switch (pPropertyValue.GetType().ToString())
            {
                case "System.DateTime":
                case "System.DateTime?"://校验时间有效性
                    if (!string.IsNullOrWhiteSpace(this._etclColumn.MaxValue))
                    {
                        DateTime maxDate = Convert.ToDateTime(this._etclColumn.MaxValue);
                        if (((DateTime)pPropertyValue) > maxDate)
                        {
                            isPass = false;
                            oResult.Message = string.Format("列【" + this._etclColumn.ColumnText + "】中的值超过了允许的最大值:[" + this._etclColumn.MaxValue + "]");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this._etclColumn.MinValue))
                    {
                        DateTime minDate = Convert.ToDateTime(this._etclColumn.MinValue);
                        if (((DateTime)pPropertyValue) < minDate)
                        {
                            isPass = false;
                            oResult.Message = string.Format("列【" + this._etclColumn.ColumnText + "】中的值低于了允许的最小值:[" + this._etclColumn.MinValue + "]");
                        }
                    }
                    break;

                case "System.Int32"://校验数值有效性
                case "System.Int32?":
                case "System.Decimal":
                case "System.Decimal?":
                case "System.Single":
                case "System.Single?":
                case "System.Double":
                case "System.Double?":
                    if (!string.IsNullOrWhiteSpace(this._etclColumn.MaxValue))
                    {
                        Double maxValue = Convert.ToDouble(this._etclColumn.MaxValue);
                        if (Convert.ToDouble(pPropertyValue) > maxValue)
                        {
                            isPass = false;
                            oResult.Message = string.Format("列【" + this._etclColumn.ColumnText + "】中的值超过了允许的最大值:[" + this._etclColumn.MaxValue + "]");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this._etclColumn.MinValue))
                    {
                        Double minValue = Convert.ToDouble(this._etclColumn.MinValue);
                        if (Convert.ToDouble(pPropertyValue) < minValue)
                        {
                            isPass = false;
                            oResult.Message = string.Format("列【" + this._etclColumn.ColumnText + "】中的值低于了允许的最小值:[" + this._etclColumn.MinValue + "]");
                        }
                    }
                    break;

                case "System.String"://字符串的长度
                case "string":
                    if (!string.IsNullOrWhiteSpace(this._etclColumn.MaxValue))
                    {
                        int maxValue = Convert.ToInt32(this._etclColumn.MaxValue);
                        if (pPropertyValue.ToString().Length > maxValue)
                        {
                            isPass = false;
                            oResult.Message = string.Format("列【" + this._etclColumn.ColumnText + "】中的值长度超过了允许的最大长度:[" + this._etclColumn.MaxValue + "]");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this._etclColumn.MinValue))
                    {
                        int minValue = Convert.ToInt32(this._etclColumn.MinValue);
                        if (pPropertyValue.ToString().Length < minValue)
                        {
                            isPass = false;
                            oResult.Message = string.Format("列【" + this._etclColumn.ColumnText + "】中的值长度低于了允许的最小长度:[" + this._etclColumn.MinValue + "]");
                        }
                    }
                    break;
                case "System.DBNull":
                    isPass = true;
                    break;
                default:
                    throw new ETCLException(400, string.Format("未处理的数据类型:{0}", pPropertyValue.GetType().ToString()));
            }
            return isPass;
        }

    }
}
