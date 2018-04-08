using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace cPos.Dex.Common
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

        #region AppendPropNode
        public static XmlNode AppendPropNode(XmlDocument doc, XmlNode parent, string name, object value)
        {
            if (value == null)
                value = string.Empty;
            else if (value.GetType() == typeof(DateTime))
                value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");

            XmlNode propNode = doc.CreateElement(name);
            propNode.InnerText = value.ToString();
            parent.AppendChild(propNode);
            return propNode;
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

        #region GetChineseCaps
        /// <summary>
        /// 拼音助记码
        /// </summary>
        public static string GetChineseCaps(string chineseStr)
        {
            if (chineseStr.Trim().Length == 0) return string.Empty;
            byte[] ZW = new byte[2];
            long ChineseStr_int;
            string Capstr = "", CharStr, ChinaStr = "";
            for (int i = 0; i <= chineseStr.Length - 1; i++)
            {
                CharStr = chineseStr.Substring(i, 1).ToString();
                ZW = System.Text.Encoding.Default.GetBytes(CharStr);
                // 得到汉字符的字节数组   
                if (ZW.Length == 2)
                {
                    int i1 = (short)(ZW[0]);
                    int i2 = (short)(ZW[1]);
                    ChineseStr_int = i1 * 256 + i2;
                    //table   of   the   constant   list   
                    //   'A';   //45217..45252   
                    //   'B';   //45253..45760   
                    //   'C';   //45761..46317   
                    //   'D';   //46318..46825   
                    //   'E';   //46826..47009   
                    //   'F';   //47010..47296   
                    //   'G';   //47297..47613   

                    //   'H';   //47614..48118   
                    //   'J';   //48119..49061   
                    //   'K';   //49062..49323   
                    //   'L';   //49324..49895   
                    //   'M';   //49896..50370   
                    //   'N';   //50371..50613   
                    //   'O';   //50614..50621   
                    //   'P';   //50622..50905   
                    //   'Q';   //50906..51386   

                    //   'R';   //51387..51445   
                    //   'S';   //51446..52217   
                    //   'T';   //52218..52697   
                    //没有U,V   
                    //   'W';   //52698..52979   
                    //   'X';   //52980..53640   
                    //   'Y';   //53689..54480   
                    //   'Z';   //54481..55289   

                    if ((ChineseStr_int >= 45217) && (ChineseStr_int <= 45252))
                    {
                        ChinaStr = "A";
                    }
                    else if ((ChineseStr_int >= 45253) && (ChineseStr_int <= 45760))
                    {
                        ChinaStr = "B";
                    }
                    else if ((ChineseStr_int >= 45761) && (ChineseStr_int <= 46317))
                    {
                        ChinaStr = "C";
                    }
                    else if ((ChineseStr_int >= 46318) && (ChineseStr_int <= 46825))
                    {
                        ChinaStr = "D";
                    }
                    else if ((ChineseStr_int >= 46826) && (ChineseStr_int <= 47009))
                    {
                        ChinaStr = "E";
                    }
                    else if ((ChineseStr_int >= 47010) && (ChineseStr_int <= 47296))
                    {
                        ChinaStr = "F";
                    }
                    else if ((ChineseStr_int >= 47297) && (ChineseStr_int <= 47613))
                    {
                        ChinaStr = "G";
                    }
                    else if ((ChineseStr_int >= 47614) && (ChineseStr_int <= 48118))
                    {
                        ChinaStr = "H";
                    }
                    else if ((ChineseStr_int >= 48119) && (ChineseStr_int <= 49061))
                    {
                        ChinaStr = "J";
                    }
                    else if ((ChineseStr_int >= 49062) && (ChineseStr_int <= 49323))
                    {
                        ChinaStr = "K";
                    }
                    else if ((ChineseStr_int >= 49324) && (ChineseStr_int <= 49895))
                    {
                        ChinaStr = "L";
                    }
                    else if ((ChineseStr_int >= 49896) && (ChineseStr_int <= 50370))
                    {
                        ChinaStr = "M";
                    }
                    else if ((ChineseStr_int >= 50371) && (ChineseStr_int <= 50613))
                    {
                        ChinaStr = "N";
                    }
                    else if ((ChineseStr_int >= 50614) && (ChineseStr_int <= 50621))
                    {
                        ChinaStr = "O";
                    }
                    else if ((ChineseStr_int >= 50622) && (ChineseStr_int <= 50905))
                    {
                        ChinaStr = "P";
                    }
                    else if ((ChineseStr_int >= 50906) && (ChineseStr_int <= 51386))
                    {
                        ChinaStr = "Q";
                    }
                    else if ((ChineseStr_int >= 51387) && (ChineseStr_int <= 51445))
                    {
                        ChinaStr = "R";
                    }
                    else if ((ChineseStr_int >= 51446) && (ChineseStr_int <= 52217))
                    {
                        ChinaStr = "S";
                    }
                    else if ((ChineseStr_int >= 52218) && (ChineseStr_int <= 52697))
                    {
                        ChinaStr = "T";
                    }
                    else if ((ChineseStr_int >= 52698) && (ChineseStr_int <= 52979))
                    {
                        ChinaStr = "W";
                    }
                    else if ((ChineseStr_int >= 52980) && (ChineseStr_int <= 53640))
                    {
                        ChinaStr = "X";
                    }
                    else if ((ChineseStr_int >= 53689) && (ChineseStr_int <= 54480))
                    {
                        ChinaStr = "Y";
                    }
                    else if ((ChineseStr_int >= 54481) && (ChineseStr_int <= 55289))
                    {
                        ChinaStr = "Z";
                    }
                }
                else if (CharStr == "1")
                {
                    ChinaStr = "Y";
                }
                else if (CharStr == "2")
                {
                    ChinaStr = "E";
                }
                else if (CharStr == "3")
                {
                    ChinaStr = "S";
                }
                else if (CharStr == "4")
                {
                    ChinaStr = "S";
                }
                else if (CharStr == "5")
                {
                    ChinaStr = "W";
                }
                else if (CharStr == "6")
                {
                    ChinaStr = "L";
                }
                else if (CharStr == "7")
                {
                    ChinaStr = "Q";
                }
                else if (CharStr == "8")
                {
                    ChinaStr = "B";
                }
                else if (CharStr == "9")
                {
                    ChinaStr = "J";
                }
                else if (CharStr == "0")
                {
                    ChinaStr = "L";
                }
                else
                {
                    ChinaStr = CharStr;
                }
                //else
                //{   
                //    Capstr = ChineseStr;
                //    break;
                //}
                Capstr = Capstr + ChinaStr;
            }
            return Capstr.Trim();
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

        #region GetIP
        public static string GetIP(string url)
        {
            string url2 = url;
            int index = url2.LastIndexOf(":");
            if (index > 6) url2 = url2.Substring(0, index);
            return url2.Replace("https://", string.Empty)
                .Replace("http://", string.Empty)
                .Replace("/", string.Empty)
                .Replace(":", string.Empty);
        }
        #endregion

        #region GetObjectVal
        //public static string GetStringVal(string obj, bool enableEmpty)
        //{
        //    if (!enableEmpty && (obj == null || obj.ToString().Trim().Length == 0))
        //        Dex.Services.ErrorService.Throw("值不能为空");
        //    return obj == null ? string.Empty : obj.ToString();
        //}

        //public static decimal GetDecimalVal(string obj, bool enableEmpty)
        //{
        //    if (!enableEmpty && (obj == null || obj.ToString().Trim().Length == 0))
        //        ErrorHelper.Throw("值不能为空");
        //    return obj == null ? 0 : decimal.Parse(obj.ToString());
        //}
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
        /// <summary>
        /// 获取受限制长度字符串
        /// </summary>
        public static string GetLimitedStr(string src, int maxLength)
        {
            if (src != null && src.Length > maxLength) return src.Substring(0, maxLength);
            return src;
        }
        #endregion
    }
}
