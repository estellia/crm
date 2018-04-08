using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;
using RedisOpenAPIClient.Common;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RedisOpenAPIClient.MethodExtensions.ObjectExtensions
{
    public static class ObjectMethodExtensions
    {
        /// <summary>
        /// JSON(string) ---> Object(T)
        /// </summary>
        public static T JsonDeserialize<T>(this string jsonStr)
        {
            var result = default(T);
            try
            {
                if (jsonStr.IsNullStr())
                {
                    return result;
                }

                result = JsonHelper.JsonDeserialize<T>(jsonStr);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:JsonDeserialize<T>出错" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Object(T) ---> JSON(string)   
        /// </summary>
        public static string JsonSerialize<T>(this T jsonObj)
            where T : class,new()
        {
            var result = string.Empty;
            try
            {
                if (jsonObj == null)
                {
                    return result;
                }

                result = JsonHelper.JsonSerializer<T>(jsonObj);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:JsonSerialize<T>出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 深度复制 (值类型/包装类型/引用类型/序列化/非序列化/标识序列化/非标识序列化,皆可深度复制)
        /// </summary>
        public static T DeepClone<T>(this T obj)
        {
            var result = default(T);
            try
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.SurrogateSelector = new SurrogateSelector();
                formatter.SurrogateSelector.ChainSelector(new NonSerialiazableTypeSurrogateSelector());
                var ms = new MemoryStream();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                result = (T)formatter.Deserialize(ms);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:DeepClone<T>(this T obj)出错.", ex);
            }
            return result;
        }

    }
}
