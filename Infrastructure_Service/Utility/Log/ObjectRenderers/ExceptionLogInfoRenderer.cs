/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 18:16:39
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
    /// 异常日志信息的呈现器 
    /// </summary>
    public class ExceptionLogInfoRenderer:IObjectRenderer
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ExceptionLogInfoRenderer()
        {
        }
        #endregion

        #region IObjectRenderer 成员

        public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
        {
            var info = obj as ExceptionLogInfo;
            if (info != null)
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
                sb.AppendFormat("<Content>{0}", Environment.NewLine);
                sb.AppendFormat("\t<Is_JIT_Exception>{1}</Is_JIT_Exception>{0}", Environment.NewLine, info.IsJITException);
                sb.AppendFormat("\t<Is_Catched_By_Outer_Framework_Code>{1}</Is_Catched_By_Outer_Framework_Code>{0}", Environment.NewLine, info.IsCatchedByOuterFrameworkCode);
                sb.AppendFormat("\t<Message>{1}</Message>{0}", Environment.NewLine, info.ErrorMessage);
                if (info.Last20ExecutedTSQLs != null && info.Last20ExecutedTSQLs.Length > 0)
                {
                    sb.AppendFormat("\t<T-SQLs>{0}", Environment.NewLine);
                    for (int i = 0; i < info.Last20ExecutedTSQLs.Length; i++)
                    {
                        var item = info.Last20ExecutedTSQLs[i];
                        if (item != null)
                        {
                            sb.AppendFormat("\t\t<T-SQL NO.={1}>{0}", Environment.NewLine, i + 1);
                            sb.AppendFormat("\t\t\t<Server_Name>{1}</Server_Name>{0}", Environment.NewLine, item.ServerName);
                            sb.AppendFormat("\t\t\t<Database_Name>{1}</Database_Name>{0}", Environment.NewLine, item.DatabaseName);
                            sb.AppendFormat("\t\t\t<Execution_Time>{1}</Execution_Time>{0}", Environment.NewLine, item.ExecutionTime);
                            if (!string.IsNullOrEmpty(item.CommandText))
                            {
                                sb.AppendFormat("\t\t\t<Command_Text>{0}\t\t\t\t{1}{0}\t\t\t</Command_Text>{0}", Environment.NewLine, item.CommandText.Replace(Environment.NewLine, string.Format("\t\t\t\t{0}", Environment.NewLine)));
                            }
                            else
                            {
                                sb.AppendFormat("\t\t\t<Command_Text></Command_Text>{0}", Environment.NewLine);
                            }
                            sb.AppendFormat("\t\t</T-SQL>{0}", Environment.NewLine);
                        }
                    }
                    sb.AppendFormat("\t<T-SQLs>{0}", Environment.NewLine);
                }
                sb.AppendFormat("</Content>");
                //
                writer.Write(sb.ToString());
            }
        }

        #endregion
    }
}
