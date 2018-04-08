/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 18:43:21
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

using JIT.Utility.Locale;

namespace JIT.Utility.Report.FactData
{
    /// <summary>
    /// 直接的维度 
    /// </summary>
    public class DirectlyDimension:IDimension
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DirectlyDimension()
        {
        }
        #endregion

        #region IDimension 成员
        
        /// <summary>
        /// 获取维度数据的文本
        /// </summary>
        /// <param name="pIDs">维度数据ID数组</param>
        /// <param name="pLanguage">用户的语言选择</param>
        /// <returns>维度数据的文本</returns>
        public Dictionary<string,string> GetTexts(string[] pIDs, Languages pLanguage)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (pIDs != null && pIDs.Length>0)
            {
                foreach (var id in pIDs)
                {
                    if (id != null)
                    {
                        result.Add(id, id.ToString());
                    }
                }
            }
            //
            return result;
        }

        #endregion
    }
}
