/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/2 17:45:50
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

using JIT.Utility.Message.ValueObject;

namespace JIT.Utility.Message
{
    /// <summary>
    /// 消息推送的渠道的基类 
    /// </summary>
    public abstract class BaseChannel
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseChannel()
        {
            this.Settings = new ParameterDictionary();
        }
        #endregion

        /// <summary>
        /// 渠道对应的手机端平台
        /// </summary>
        public MobilePlatforms? Platform { get; set; }

        /// <summary>
        /// 渠道的设置
        /// </summary>
        public ParameterDictionary Settings { get; set; }
    }
}
