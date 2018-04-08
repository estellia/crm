using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace JIT.Utility.ObjectParse
{
    /// <summary>
    ///TypeParse 数值转换
    /// </summary>
    public class TypeParse
    {
        public TypeParse()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            if (Expression != null && Expression != DBNull.Value)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;

        }


        public static bool IsDouble(object Expression)
        {
            if (Expression != null && Expression != DBNull.Value)
            {
                return Regex.IsMatch(Expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object Expression, bool defValue)
        {
            if (Expression != null && Expression != DBNull.Value)
            {
                if (string.Compare(Expression.ToString(), "true", true) == 0)
                {
                    return true;
                }
                else if (string.Compare(Expression.ToString(), "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object expression, int defValue)
        {
            if (expression != null && expression != DBNull.Value)
            {
                return StrToInt(expression.ToString(), defValue);
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;
            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;
            return Convert.ToInt32(StrToFloat(str, defValue));
        }
        /// <summary>
        /// 将对象转换为Int64类型
        /// </summary>
        /// <param name="Expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int64类型结果</returns>
        public static long StrToInt64(object Expression, int defValue)
        {
            if (Expression != null && Expression != DBNull.Value)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 22 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
                {
                    if ((str.Length < 20) || (str.Length == 20 && str[0] == '1') || (str.Length == 22 && str[0] == '-' && str[1] == '1'))
                    {
                        return Convert.ToInt64(str);
                    }
                }
            }
            return defValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null || strValue == DBNull.Value))
            {
                return defValue;
            }

            return StrToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    float.TryParse(strValue, out intValue);
                }
            }
            return intValue;
        }
        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static decimal StrToDecimal(string strValue, decimal defValue)
        {
            if (string.IsNullOrWhiteSpace(strValue) || strValue.Length > 16)
            {
                return defValue;
            }
            try
            {
                decimal result = defValue;
                decimal.TryParse(strValue, out result);
                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("string型转换为decimal型发生异常，传入值:" + strValue, ex);
            }
        }


        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumeric(id))
                {
                    return false;
                }
            }
            return true;

        }


        /// <summary>
        /// 计算两个时间的间隔
        /// 根据printType的值有不同的输出,已满足不通显示要求
        /// </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="printState">输出类型(目前只支持0)</param>
        /// <returns></returns>
        public static string GetTwoDateSpace(string dateBegin, string dateEnd, int printType)
        {
            string dateDiff = string.Empty;
            try
            {
                TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(dateEnd).Ticks);
                TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(dateBegin).Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                if (printType == 0) //显示天/小时/分钟
                {
                    if (ts.Days > 0)
                    {
                        dateDiff = string.Format("{0}天{1}时{2}分", ts.Days, ts.Hours, ts.Minutes);
                    }
                    else
                    {
                        dateDiff = string.Format("{0}时{1}分", ts.Hours, ts.Minutes);
                    }
                }
            }
            catch
            {
                dateDiff = "日期格式错误";
            }
            return dateDiff;
        }

        #region 过滤XML中换行和空白字符
        /// <summary>
        /// 过滤XML中换行和空白字符
        /// </summary>
        /// <param name="xml">要过滤的字符串</param>
        /// <returns></returns>
        public static string GetXMLClearSpace(string xml)
        {
            string body = Regex.Replace(xml, @"[\n\r\s]{2,}", "");
            return body.Trim();
        }
        #endregion

        /// <summary>
        /// string转换成时间格式
        /// </summary>
        /// <param name="date"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static DateTime StrToDateTime(string date, DateTime defValue)
        {
            DateTime outResult = DateTime.MinValue;

            if (DateTime.TryParse(date, out outResult))
            {
                return outResult;
            }
            return defValue;
        }

        #region BASE64 处理
        /// <summary>
        /// BASE64编码
        /// </summary>
        /// <param name="s">需要编码的字符串</param>
        /// <returns></returns>
        public static string ToBase64(string s)
        {
            if (String.IsNullOrEmpty(s)) return "";
            return Convert.ToBase64String(Encoding.GetEncoding(20936).GetBytes(s));
        }
        /// <summary>
        /// BASE64解码
        /// </summary>
        /// <param name="s">需要解码的字符串</param>
        /// <returns></returns>
        public static string FromBase64(string s)
        {
            if (String.IsNullOrEmpty(s)) return "";
            return Encoding.GetEncoding(20936).GetString(Convert.FromBase64String(s));
        }
        #endregion

        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        /// <summary>
        /// 将泛类型集合List类转换成DataTable，并指定列的类型
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTableWithColumnType<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        #region 转全角的函数(SBC case)
        /// <summary>
        /// 转全角的函数(SBC case)
        /// 任意字符串
        /// 全角字符串
        /// 全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToSbc(String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }
        #endregion


        #region 转半角的函数(DBC case)
        /// <summary>
        /// 转半角的函数(DBC case)
        /// 任意字符串
        /// 半角字符串
        /// 全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToDbc(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }
        #endregion
    }
}
