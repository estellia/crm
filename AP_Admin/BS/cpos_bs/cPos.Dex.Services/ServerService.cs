using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;

namespace cPos.Dex.Services
{
    public class ServerService
    {
        #region Server
        /// <summary>
        /// 获取Server列表
        /// </summary>
        /// <param name="ht">ServerId, ServerCode, ServerName, ServerIp, 
        /// ServerStatus, SortFlag, CreateUserId, ModifyUserId</param>
        public IList<ServerInfo> GetServers(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<ServerInfo>("ServerInfo.GetServers", ht);
        }

        /// <summary>
        /// 通过Code获取Server
        /// </summary>
        public ServerInfo GetServerByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<ServerInfo>("ServerInfo.GetServerByCode", code);
        }

        /// <summary>
        /// 插入Server
        /// </summary>
        public bool InsertServer(ServerInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("ServerInfo.InsertServer", info);
            return true;
        }

        /// <summary>
        /// 更新Server
        /// </summary>
        public bool UpdateServer(ServerInfo info)
        {
            SqlMapper.Instance().Update("ServerInfo.UpdateServer", info);
            return true;
        }

        /// <summary>
        /// 删除Server
        /// </summary>
        public bool DeleteServer(string id)
        {
            SqlMapper.Instance().Update("ServerInfo.DeleteServer", id);
            return true;
        }
        #endregion

        #region ServerFtp
        /// <summary>
        /// 获取ServerFtp列表
        /// </summary>
        /// <param name="ht">FtpId, FtpCode, FtpName, ServerId, FtpStatus</param>
        public IList<ServerFtpInfo> GetServerFtps(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<ServerFtpInfo>("ServerInfo.GetServerFtps", ht);
        }

        /// <summary>
        /// 通过Code获取ServerFtp
        /// </summary>
        public ServerFtpInfo GetServerFtpByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<ServerFtpInfo>("ServerInfo.GetServerFtpByCode", code);
        }

        /// <summary>
        /// 获取一个可用的ServerFtp
        /// </summary>
        public ServerFtpInfo GetActiveServerFtp()
        {
            Hashtable ht = new Hashtable();
            ht.Add("FtpStatus", "0");
            IList<ServerFtpInfo> list = GetServerFtps(ht);
            if (list == null || list.Count == 0)
                throw new Exception("未找到FTP服务器");
            Random rnd = new Random();
            return list[rnd.Next(0, list.Count - 1)];
        }

        /// <summary>
        /// 插入ServerFtp
        /// </summary>
        public bool InsertServerFtp(ServerFtpInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("ServerInfo.InsertServerFtp", info);
            return true;
        }

        /// <summary>
        /// 更新ServerFtp
        /// </summary>
        public bool UpdateServerFtp(ServerFtpInfo info)
        {
            SqlMapper.Instance().Update("ServerInfo.UpdateServerFtp", info);
            return true;
        }

        /// <summary>
        /// 删除ServerFtp
        /// </summary>
        public bool DeleteServerFtp(string id)
        {
            SqlMapper.Instance().Update("ServerInfo.DeleteServerFtp", id);
            return true;
        }
        #endregion

        #region ServerAccount
        /// <summary>
        /// 获取ServerAccount列表
        /// </summary>
        /// <param name="ht">AccountId, AccountTypeId, ServerId, AccountName</param>
        public IList<ServerAccountInfo> GetServerAccounts(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<ServerAccountInfo>("ServerInfo.GetServerAccounts", ht);
        }

        /// <summary>
        /// 通过Code获取ServerAccount
        /// </summary>
        public ServerAccountInfo GetServerAccountByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<ServerAccountInfo>("ServerInfo.GetServerAccountByCode", code);
        }

        /// <summary>
        /// 获取一个可用的ServerAccount
        /// </summary>
        public ServerAccountInfo GetActiveServerAccount(string serverId, string accountTypeId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ServerId", serverId);
            ht.Add("AccountTypeId", accountTypeId);
            IList<ServerAccountInfo> list = GetServerAccounts(ht);
            if (list == null || list.Count == 0)
                throw new Exception("未找到匹配的帐号");
            return list[0];
        }

        /// <summary>
        /// 插入ServerAccount
        /// </summary>
        public bool InsertServerAccount(ServerAccountInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("ServerInfo.InsertServerAccount", info);
            return true;
        }

        /// <summary>
        /// 更新ServerAccount
        /// </summary>
        public bool UpdateServerAccount(ServerAccountInfo info)
        {
            SqlMapper.Instance().Update("ServerInfo.UpdateServerAccount", info);
            return true;
        }

        /// <summary>
        /// 删除ServerAccount
        /// </summary>
        public bool DeleteServerAccount(string id)
        {
            SqlMapper.Instance().Update("ServerInfo.DeleteServerAccount", id);
            return true;
        }
        #endregion

        #region ServerAccountType
        /// <summary>
        /// 获取ServerAccountType列表
        /// </summary>
        /// <param name="ht">AccountTypeId, TypeCode, TypeName, TypeStatus</param>
        public IList<ServerAccountTypeInfo> GetServerAccountTypes(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<ServerAccountTypeInfo>("ServerInfo.GetServerAccountTypes", ht);
        }

        /// <summary>
        /// 通过Code获取ServerAccountType
        /// </summary>
        public ServerAccountTypeInfo GetServerAccountTypeByCode(string code)
        {
            return SqlMapper.Instance().QueryForObject<ServerAccountTypeInfo>("ServerInfo.GetServerAccountTypeByCode", code);
        }

        /// <summary>
        /// 插入ServerAccountType
        /// </summary>
        public bool InsertServerAccountType(ServerAccountTypeInfo info)
        {
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("ServerInfo.InsertServerAccountType", info);
            return true;
        }

        /// <summary>
        /// 更新ServerAccountType
        /// </summary>
        public bool UpdateServerAccountType(ServerAccountTypeInfo info)
        {
            SqlMapper.Instance().Update("ServerInfo.UpdateServerAccountType", info);
            return true;
        }

        /// <summary>
        /// 删除ServerAccountType
        /// </summary>
        public bool DeleteServerAccountType(string id)
        {
            SqlMapper.Instance().Update("ServerInfo.DeleteServerAccountType", id);
            return true;
        }
        #endregion
    }
}
