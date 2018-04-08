using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mail;
using System.Configuration;

namespace JIT.Utility.Notification
{
    public static class Mail
    {

        /// <summary>
        /// 邮件发送方法(带附件)
        /// </summary>
        /// <param name="mailto">收件人地址。如：receiver@163.com</param>
        /// <param name="mailsubject">邮件标题</param>
        /// <param name="mailbody">邮件正文</param>
        /// <param name="mailFrom">邮件发送人地址。如：sender@163.com</param>
        /// <param name="list">附件路径</param>
        /// <returns></returns>
        public static bool SendMail(string mailto, string mailsubject, string mailbody)
        {
            return SendMail(mailto, mailsubject, mailbody, null);
        }

        /// <summary>
        /// 邮件发送方法(带附件)
        /// </summary>
        /// <param name="pMailto">收件人地址。如：receiver@163.com</param>
        /// <param name="pMailsubject">邮件标题</param>
        /// <param name="pMailbody">邮件正文</param>
        /// <param name="pAttachments">附件的文件路径</param>
        /// <returns></returns>
        public static bool SendMail(string pMailto, string pMailsubject, string pMailbody, string[] pAttachments)
        {
            try
            {
                string smtpServer = "smtp.exmail.qq.com";
                string sendFrom = "support@retailone.com.cn";
                string username = "support@retailone.com.cn";
                string password = "jit123456";

                #region 新邮件的发送信息

                smtpServer = "smtp.exmail.qq.com";
                sendFrom = "aldsend@aladingyidong.com";
                username = "aldsend@aladingyidong.com";
                password = "ald.12345";

                #endregion

                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SmtpServer"]))
                    smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["MailSendFrom"]))
                    sendFrom = ConfigurationManager.AppSettings["MailSendFrom"];
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["MailUserName"]))
                    username = ConfigurationManager.AppSettings["MailUserName"];
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["MailUserPassword"]))
                    password = ConfigurationManager.AppSettings["MailUserPassword"];
                MailMessage mailObj = new MailMessage();
                mailObj.BodyEncoding = System.Text.UTF8Encoding.UTF8;
                SmtpMail.SmtpServer = smtpServer;

                mailObj.From = sendFrom;
                mailObj.To = pMailto;
                mailObj.Priority = MailPriority.High;
                mailObj.BodyFormat = MailFormat.Html;
                mailObj.Subject = pMailsubject;
                mailObj.Body = pMailbody;
                mailObj.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                mailObj.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/sendusername", username);
                mailObj.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
                //添加附件
                if (pAttachments != null)
                {
                    foreach (string pAttachment in pAttachments)
                    {
                        if (System.IO.File.Exists(pAttachment))
                        {
                            var attachment = new System.Web.Mail.MailAttachment(pAttachment);
                            mailObj.Attachments.Add(attachment);
                        }
                        else
                        {
                            Log.Loggers.Debug(new Log.DebugLogInfo() { Message = "指定的附件不存在:【" + pAttachment + "】" });
                        }
                    }
                }
                SmtpMail.Send(mailObj);
                return true;
            }
            catch (Exception ex)
            {
                //Log.Loggers.Exception(new Log.ExceptionLogInfo(ex));
                return false;
            }
        }

        /// <summary>
        /// 邮件发送方法(带附件)
        /// </summary>
        /// <param name="pFrom">发送方</param>
        /// <param name="pMailTo">收件人地址。如：receiver@163.com</param>
        /// <param name="pMailSubject">邮件标题</param>
        /// <param name="pMailBody">邮件正文</param>
        /// <param name="pAttachments">附件的文件路径</param>
        /// <returns></returns>
        public static bool SendMail(FromSetting pFrom, string pMailTo, string pMailSubject, string pMailBody, string[] pAttachments)
        {
            //参数检查
            if (pFrom == null)
                throw new ArgumentNullException("pFrom");
            if(string.IsNullOrWhiteSpace(pFrom.Password))
                throw new ArgumentNullException("pFrom.Password");
            if (string.IsNullOrWhiteSpace(pFrom.SendFrom))
                throw new ArgumentNullException("pFrom.SendFrom");
            if (string.IsNullOrWhiteSpace(pFrom.SMTPServer))
                throw new ArgumentNullException("pFrom.SMTPServer");
            if (string.IsNullOrWhiteSpace(pFrom.UserName))
                throw new ArgumentNullException("pFrom.UserName");
            //
            try
            {
                MailMessage mailObj = new MailMessage();
                mailObj.BodyEncoding = System.Text.UTF8Encoding.UTF8;
                SmtpMail.SmtpServer = pFrom.SMTPServer;

                mailObj.From = pFrom.SendFrom;
                mailObj.To = pMailTo;
                mailObj.Priority = MailPriority.High;
                mailObj.BodyFormat = MailFormat.Html;
                mailObj.Subject = pMailSubject;
                mailObj.Body = pMailBody;
                mailObj.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                mailObj.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/sendusername", pFrom.UserName);
                mailObj.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/sendpassword", pFrom.Password);
                //添加附件
                if (pAttachments != null)
                {
                    foreach (string pAttachment in pAttachments)
                    {
                        if (System.IO.File.Exists(pAttachment))
                        {
                            var attachment = new System.Web.Mail.MailAttachment(pAttachment);
                            mailObj.Attachments.Add(attachment);
                        }
                        else
                        {
                            Log.Loggers.Debug(new Log.DebugLogInfo() { Message = "指定的附件不存在:【" + pAttachment + "】" });
                        }
                    }
                }
                SmtpMail.Send(mailObj);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
