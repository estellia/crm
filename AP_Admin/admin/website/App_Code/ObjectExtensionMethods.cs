using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
///ObjectExtensionMethods 的摘要说明
/// </summary>
public static class ObjectExtensionMethods
{
    #region 扩展方法：将对象序列化为JSON
    /// <summary>
    /// 扩展方法：将对象序列化为JSON
    /// </summary>
    /// <param name="pCaller">调用者</param>
    /// <returns>对象序列化为JSON的字符串</returns>
    public static string ToJSON(this object pCaller)
    {
        if (pCaller == null) { return "null"; }

        if (pCaller.GetType().Name == "DataTable" || pCaller.GetType().Name == "DataSet")
        {
            //DataTable序列化
            return JsonConvert.SerializeObject(pCaller, Formatting.Indented);
        }
        else
        {
            //其它直接序列化
            return JsonConvert.SerializeObject(pCaller);
        }
    }

    #endregion

    /// <summary>
    /// 对Object对象进行类型转换
    /// </summary>
    /// <param name="pCaller">调用者</param>
    /// <param name="pTargetType">要转换成的类型</param>
    /// <returns></returns>
    public static object ChangeTypeTo(this object pCaller, Type pTargetType)
    {
        if (pCaller == null)
            return null;
        if (pCaller.GetType() == typeof(string))
            return pCaller.ToString().ChangeTypeTo(pTargetType);
        if (pCaller.GetType() == typeof(DBNull))
            return null;
        if (pTargetType.IsNullableCRTValueType())
        {
            return Convert.ChangeType(pCaller, pTargetType.GetNullableUnderlyingType());
        }
        else
        {
            return Convert.ChangeType(pCaller, pTargetType);
        }
    }

    /// <summary>
    /// 获取可序列化对像的二进制数据
    /// </summary>
    /// <param name="pValue">可序列化对象</param>
    /// <returns></returns>
    public static byte[] GetBytes(this object pValue)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, pValue);
            return ms.ToArray();
        }
    }
}




/// <summary>
/// Type的扩展方法
/// </summary>
public static class TypeExtensionMethods
{
    /// <summary>
    /// 扩展方法：判断类型是否为公共语言运行时类型
    /// </summary>
    /// <param name="pCaller">调用者</param>
    /// <returns></returns>
    public static bool IsCRT(this Type pCaller)
    {
        if (pCaller == null)
            throw new NullReferenceException();
        if (pCaller == typeof(bool)
            || pCaller == typeof(byte)
            || pCaller == typeof(char)
            || pCaller == typeof(DateTime)
            || pCaller == typeof(decimal)
            || pCaller == typeof(double)
            || pCaller == typeof(Int16)
            || pCaller == typeof(Int32)
            || pCaller == typeof(Int64)
            || pCaller == typeof(sbyte)
            || pCaller == typeof(Single)
            || pCaller == typeof(string)
            || pCaller == typeof(UInt16)
            || pCaller == typeof(UInt32)
            || pCaller == typeof(UInt64)
        )
            return true;
        else
            return false;
    }

    /// <summary>
    /// 扩展方法：判断类型是否为可空值类型
    /// </summary>
    /// <param name="pCaller">调用者</param>
    /// <returns></returns>
    public static bool IsNullableValueType(this Type pCaller)
    {
        if (pCaller == null)
            throw new NullReferenceException();
        if (pCaller.IsGenericType && pCaller.GetGenericTypeDefinition() == typeof(Nullable<>))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 扩展方法：判断类型是否为基础类型为公共语言运行时类型的可空值类型
    /// </summary>
    /// <param name="pCaller">调用者</param>
    /// <returns></returns>
    public static bool IsNullableCRTValueType(this Type pCaller)
    {
        if (pCaller == null)
            throw new NullReferenceException();
        if (pCaller == typeof(bool?)
            || pCaller == typeof(byte?)
            || pCaller == typeof(char?)
            || pCaller == typeof(DateTime?)
            || pCaller == typeof(decimal?)
            || pCaller == typeof(double?)
            || pCaller == typeof(Int16?)
            || pCaller == typeof(Int32?)
            || pCaller == typeof(Int64?)
            || pCaller == typeof(sbyte?)
            || pCaller == typeof(Single?)
            || pCaller == typeof(UInt16?)
            || pCaller == typeof(UInt32?)
            || pCaller == typeof(UInt64?)
        )
            return true;
        else
            return false;
    }

    /// <summary>
    /// 扩展方法：获取可空值类型的基本类型
    /// </summary>
    /// <param name="pCaller"></param>
    /// <returns></returns>
    public static Type GetNullableUnderlyingType(this Type pCaller)
    {
        if (pCaller.IsNullableValueType())
            return pCaller.GetGenericArguments()[0];
        else
            return null;
    }
}