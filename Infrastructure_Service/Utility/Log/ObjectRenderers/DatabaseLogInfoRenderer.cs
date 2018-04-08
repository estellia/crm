/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 17:19:38
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
using System.IO;
using System.Text;

using log4net;
using log4net.ObjectRenderer;

namespace JIT.Utility.Log
{
    /// <summary>
    /// 数据库日志信息的呈现器 
    /// </summary>
    public class DatabaseLogInfoRenderer:IObjectRenderer
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DatabaseLogInfoRenderer()
        {
        }
        #endregion

        #region IObjectRenderer 成员

        public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
        {
            var info = obj as DatabaseLogInfo;
            if (info != null && info.TSQL !=null)
            {
                StringBuilder sb = new StringBuilder();
                if (info.StackTrances != null && info.StackTrances.Length > 0)
                {
                    sb.AppendFormat("<Stack_Trances>{0}", Environment.NewLine);
                    foreach (var item in info.StackTrances)
                    {
                        sb.AppendFormat("\t{1}{0}", Environment.NewLine, item.ToString());
                    }
                    sb.AppendFormat("</Stack_Trances>{0}", Environment.NewLine);
                }
                sb.AppendFormat("<Content>{0}",Environment.NewLine);
                sb.AppendFormat("\t<Database_Server>{1}</Database_Server>{0}", Environment.NewLine, info.TSQL.ServerName);
                sb.AppendFormat("\t<Database_Name>{1}</Database_Name>{0}", Environment.NewLine, info.TSQL.DatabaseName);
                sb.AppendFormat("\t<Execution_Time>{1}</Execution_Time>{0}", Environment.NewLine, info.TSQL.ExecutionTime);
                if (!string.IsNullOrEmpty(info.TSQL.CommandText))
                {
                    var sqlText = info.TSQL.CommandText;
                    if (!sqlText.StartsWith(Environment.NewLine))
                    {
                        sqlText =sqlText.Insert(0, Environment.NewLine);
                    }
                    sqlText = sqlText.Replace(Environment.NewLine, string.Format("{0}\t\t", Environment.NewLine));
                    sb.AppendFormat("\t<Command_Text>{1}{0}\t</Command_Text>{0}", Environment.NewLine,sqlText);
                }
                else
                {
                    sb.AppendFormat("\t<Command_Text></Command_Text>{0}", Environment.NewLine);
                }
                sb.AppendFormat("</Content>");
                //
                writer.Write(sb.ToString());
            }
        }

        #endregion
    }
}
