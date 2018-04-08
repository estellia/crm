/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 17:48:14
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
    /// 调试日志信息的呈现器 
    /// </summary>
    public class DebugLogInfoRenderer:IObjectRenderer
    {
        #region IObjectRenderer 成员
        public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
        {
            var info = obj as DebugLogInfo;
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
                    sb.AppendFormat("</Stack_Trances>", Environment.NewLine);
                }
                sb.AppendFormat("{0}<Content>{0}", Environment.NewLine);
                sb.AppendFormat("\t<Message>{0}\t\t{1}{0}\t</Message>{0}", Environment.NewLine, info.Message);
                sb.AppendFormat("</Content>");
                //
                writer.Write(sb.ToString());
            }
        }

        #endregion
    }
}
