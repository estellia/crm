using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageService.Interface;
using System.Configuration;

namespace JIT.MessageService.Base
{
    public abstract class BaseRequest : IGetRequestStr
    {
        protected BaseRequest(SmsCommandType cmd)
        {
            Spid = ConfigurationManager.AppSettings["SPID"];
            Sppassword = ConfigurationManager.AppSettings["SPPWD"];
            Command = cmd;
            DC = Convert.ToInt32(ConfigurationManager.AppSettings["DC"]);
        }
        /// <summary>
        /// 命令类型
        /// </summary>
        public SmsCommandType Command { get; set; }
        /// <summary>
        /// SP的ID
        /// </summary>
        public string Spid { get; set; }
        /// <summary>
        /// 消息编码格式
        /// </summary>
        public int DC { get; set; }
        /// <summary>
        /// SP密码
        /// </summary>
        public string Sppassword { get; set; }
        /// <summary>
        /// 服务代码
        /// </summary>
        public string Spsc { get; set; }
        /// <summary>
        /// 源地址
        /// </summary>
        public string Sa { get; set; }
        /// <summary>
        /// esm_class
        /// </summary>
        public string Ec { get; set; }
        /// <summary>
        /// protocol_id
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// 定时发送时间
        /// </summary>
        public string Attime { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public string Validtime { get; set; }

        public string GetCommandStr()
        {
            return GetBaseCommandStr() + (string.IsNullOrEmpty(GetStr()) ? "" : "&" + GetStr());
        }

        public byte[] GetCommandStrData()
        {
            byte[] bTemp = Encoding.ASCII.GetBytes(this.GetCommandStr());
            return bTemp;
        }

        protected abstract string GetStr();

        private string GetBaseCommandStr()
        {
            string str = "{0}={1}&{2}={3}&{4}={5}&{6}={7}".Fmt("command", Enum.GetName(typeof(SmsCommandType), Command).ToUpper(), "spid", Spid, "sppassword", Sppassword, "dc", DC);
            return str;
        }
    }
}
