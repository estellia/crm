/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/1/6 13:18:14
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

namespace JIT.Utility.Notification
{
    /// <summary>
    /// 邮件发送方设置 
    /// </summary>
    public class FromSetting
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public FromSetting()
        {
        }
        #endregion

        /// <summary>
        /// FTP服务器
        /// </summary>
        public string SMTPServer { get; set; }

        /// <summary>
        /// 邮件的发送方
        /// </summary>
        public string SendFrom { get; set; }

        /// <summary>
        /// 邮件发送方的账户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮件发送方的密码
        /// </summary>
        public string Password { get; set; }
    }
}
