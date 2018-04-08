using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JIT.Utility.Notification;
using JIT.Utility.ExtensionMethod;
using System.Text.RegularExpressions;

namespace JIT.Utility.MobileDeviceManagement
{
    public static class MailHelper
    {
        static MailHelper()
        {
            MailUsers = GetMailUser();
            MailUserRegex = GetRegex();
        }
        public static readonly MailUser[] MailUsers = null;
        public static readonly Regex MailUserRegex = null;
        static MailUser[] GetMailUser()
        {
            List<MailUser> userlist = new List<MailUser> { };
            var configlist = ConfigurationManager.AppSettings;
            foreach (var item in configlist.AllKeys)
            {
                if (item.Contains("|"))
                {
                    var strs = item.Split('|');
                    if (strs.Length > 1)
                    {
                        if (strs[1] == "*")
                            userlist.Add(new MailUser() { Address = configlist[item], Type = 0, ClientID = strs[1], AppCode = strs[0] });
                        else
                            userlist.Add(new MailUser() { Address = configlist[item], Type = 1, ClientID = strs[1], AppCode = strs[0] });
                    }
                }
            }
            return userlist.ToArray();
        }

        static Regex GetRegex()
        {
            var regex = ConfigurationManager.AppSettings["Regex"];
            if (string.IsNullOrEmpty(regex))
                return null;
            else
                return new Regex(regex);
        }

        public static void SendMail(MailUser pMailUser, LogRequest[] pRequests)
        {
            if (pRequests.Length == 0)
                return;
            string title = "JIT手机异常日志";
            var message = pRequests.Aggregate("", (i, j) => i + j.ToJSON() + Environment.NewLine);
            Mail.SendMail(pMailUser.Address, title, message);
        }

        public static void SendMail(MailUser[] pUsers, LogRequest[] pRequest)
        {
            foreach (var item in pUsers)
            {
                SendMail(item, pRequest);
            }
        }
    }
}
