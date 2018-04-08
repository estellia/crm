/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/16 9:45:40
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
using System.ComponentModel;
using System.Text;

namespace JIT.Utility.Locale
{
    /// <summary>
    /// 系统支持的语言 
    /// </summary>
    public enum Languages
    {
        /// <summary>
        /// 英文(美国)
        /// </summary>
        [Description("en-US")]
        en_US
        ,
        /// <summary>
        /// 中文简体
        /// </summary>
        [Description("zh-CN")]
        zh_CN
    }

    /// <summary>
    /// Languages的扩展方法
    /// </summary>
    public static class LanguageExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取语言所对应的语言区域ID
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static int GetLCID(this Languages pCaller)
        {
            switch (pCaller)
            {
                case Languages.en_US:
                    return 1033;
                case Languages.zh_CN:
                    return 2052;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
