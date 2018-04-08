using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace cPos.Admin.Task
{
    public class Utils
    {
        #region GetDate/GetDateTime
        public static string GetDate(object date)
        {
            return GetDate(GetStrVal(date));
        }

        public static string GetDate(string date)
        {
            if (date == null || date.Trim().Length < 8)
                return string.Empty;
            else
                return Convert.ToDateTime(date.Trim()).ToString("yyyy-MM-dd");
        }

        public static string GetDateTime(string time)
        {
            if (time == null || time.Trim().Length < 8)
                return string.Empty;
            else
                return Convert.ToDateTime(time.Trim()).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTime(DateTime time)
        {
            if (time == null)
                return string.Empty;
            else
                return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTime(object time)
        {
            if (time == null || time.ToString().Trim().Length < 8)
                return string.Empty;
            else
                return Convert.ToDateTime(time.ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetTodayString()
        {
            DateTime today = DateTime.Now;
            return today.ToString("yyyyMMdd");
        }

        public static string GetNowString()
        {
            DateTime today = DateTime.Now;
            return today.ToString("yyyyMMddHHmmssfff");
        }
        #endregion

        #region NewGuid
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
        }
        #endregion

        #region GetNow
        public static string GetNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetNowWithMillisecond()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        #endregion

        #region GetFieldVal
        public static string GetFieldVal(object val)
        {
            if (val == null) return "null";
            return string.Format("'{0}'", val.ToString());
        }

        public static string GetFieldVal(object val, int maxLength)
        {
            if (val == null) return "null";
            if (val.ToString().Length > maxLength)
                return string.Format("'{0}'", val.ToString().Substring(0, maxLength));
            return string.Format("'{0}'", val.ToString());
        }
        #endregion

        #region SaveFile
        public static void SaveFile(string folderPath, string fileName, string content)
        {
            if (folderPath == null || folderPath.Length <= 0) return;
            if (fileName == null || fileName.Length <= 0) return;

            folderPath = folderPath.Trim();
            fileName = fileName.Trim().Replace("-", "_").Replace(" ", "_").Replace(":", "_");

            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            if (!folderPath.EndsWith(@"\"))
                folderPath = folderPath + @"\";

            string filePath = folderPath + fileName;
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine(content);
            }
        }
        #endregion

        #region GetFilePath
        public static string GetFilePath(string folder, string name)
        {
            if (!folder.EndsWith(@"\")) folder = folder + @"\";
            return folder + name;
        }

        public static string GetFilePath(string folder, string name, bool appendDate)
        {
            if (!folder.EndsWith(@"\")) folder = folder + @"\";
            return folder + GetTodayString() + @"\" + name;
        }
        #endregion

        #region CheckTimeScope
        public static bool CheckTimeScope(string time, double scope)
        {
            time = time.Trim();
            DateTime now = DateTime.Now;
            if (time.Length == 5)
            {
                int th = int.Parse(time.Substring(0, 2));
                int tm = int.Parse(time.Substring(3, 2));
                DateTime dtTime = new DateTime(now.Year, now.Month, now.Day, th, tm, 0);
                DateTime dtEnd = dtTime.AddSeconds(scope);
                if (dtTime <= now && now <= dtEnd)
                    return true;
            }
            return false;
        }
        #endregion

        #region IsEmpty
        public static bool IsEmpty(object obj)
        {
            if (obj == null) return true;
            if (obj.GetType() == typeof(string))
            {
                if (obj.ToString().Trim().Length == 0) return true;
            }
            else if (obj.GetType() == typeof(int))
            {
                if (obj.ToString().Trim().Length == 0) return true;
            }
            else
            {
                if (obj.ToString().Trim().Length == 0) return true;
            }
            return false;
        }
        #endregion

        #region CheckUrl
        public static bool CheckUrl(string url)
        {
            if (url == null) return false;
            if (url.Trim().Length <= 4) return false;
            if (!url.StartsWith("http")) return false;
            return true;
        }
        #endregion

        #region CheckIsNumber
        public static bool CheckIsNumber(string num)
        {
            try
            {
                int n = int.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ToAbs
        public static decimal ToAbs(object value)
        {
            return Math.Abs(decimal.Parse(value.ToString()));
        }
        #endregion

        #region GetStrVal
        public static string GetStrVal(object obj)
        {
            return obj == DBNull.Value || obj == null ? string.Empty : obj.ToString();
        }
        #endregion

        #region GetDecimalVal
        public static decimal GetDecimalVal(object obj)
        {
            return obj == DBNull.Value || obj == null ? 0 : decimal.Parse(obj.ToString());
        }
        #endregion

        #region GetIntVal
        public static int GetIntVal(object obj)
        {
            return obj == DBNull.Value || obj == null ? 0 : int.Parse(obj.ToString());
        }
        #endregion

        #region GetDateTimeVal
        public static DateTime GetDateTimeVal(object obj)
        {
            return obj == DBNull.Value || obj == null ? new DateTime(1900, 1, 1) : DateTime.Parse(obj.ToString());
        }
        #endregion

        #region GetStatus
        /// <summary>
        /// 获取状态
        /// </summary>
        public static string GetStatus(bool status)
        {
            return status.ToString().ToLower();
        }
        #endregion

        #region GetLimitedStr
        public static string GetLimitedStr(string src, int maxLength)
        {
            if (src != null && src.Length > maxLength) return src.Substring(0, maxLength);
            return src;
        }
        #endregion


        #region 序列化与反序列化
        /// <summary>
        /// 反序列化 从字符串
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <param name="type">要生成的对象类型</param>
        /// <returns>反序列化后的对象</returns>
        public static object Deserialize(string xml, Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            StringReader sr = new StringReader(xml);
            object obj = xs.Deserialize(sr);
            return obj;
        }

        /// <summary>
        /// 序列化成字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>序列化后的字符串</returns>
        public static string Serialiaze(object obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(ms, System.Text.Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xs.Serialize(xtw, obj);
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(ms);
            string str = sr.ReadToEnd();
            xtw.Close();
            ms.Close();
            return str;
        }
        #endregion
    }
}
