using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface
{
    /// <summary>
    /// LogContent 的摘要说明
    /// </summary>
    public class LogContent
    {
        public LogContent(string interfaceName, string reqContent, string createBy, string customerId, string userId, string openId, string requestIP, string deviceNumber, string versionNumber, string webPage)
        {
            InterfaceName = interfaceName;
            ReqContent = reqContent;
            CreateBy = createBy;
            CustomerId = customerId;
            UserId = userId;
            OpenId = openId;
            RequestIP = requestIP;
            DeviceNumber = deviceNumber;
            VersionNumber = versionNumber;
            WebPage = webPage;
        }

        //接口方法名称
        public string InterfaceName { get; set; }
        //请求参数
        public string ReqContent { get; set; }

        public string CreateBy { get; set; }

        public string CustomerId { get; set; }

        public string UserId { get; set; }

        public string OpenId { get; set; }

        public string RequestIP { get; set; }

        //设备号
        public string DeviceNumber { get; set; }

        //程序版本号
        public string VersionNumber { get; set; }

        public string WebPage { get; set; }
    }
}