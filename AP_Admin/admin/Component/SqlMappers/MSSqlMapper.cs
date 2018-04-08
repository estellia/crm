using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper.Configuration;
using System.IO;
using System.Web;
using cPos.Admin.DataCrypt;

namespace cPos.Admin.Component.SqlMappers
{
    public class MSSqlMapper
    {
        private static volatile ISqlMapper _Mapper = null;

        protected static void Configure(object obj)
        {
            _Mapper = null;
        }

        protected static void InitMapper()
        {
            ConfigureHandler handler = new ConfigureHandler(Configure);
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
#if SQLMAP_ENCRYPT
            string encrypt_sqlmap_file = System.Configuration.ConfigurationManager.AppSettings["encrypt_sqlmap_file"];
            string fileMap = HttpContext.Current.Server.MapPath(string.Format("~\\{0}", encrypt_sqlmap_file));
            if (!File.Exists(fileMap))
            {
                throw new FileNotFoundException(encrypt_sqlmap_file);
            }
            StreamReader tr = new StreamReader(fileMap, Encoding.UTF8);
            string input = tr.ReadToEnd();
            tr.Close();
            string site_key = System.Configuration.ConfigurationManager.AppSettings["site_key"] + ".key";
            string output = CryptManager.DecryptString(site_key, input);
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(output);
            _Mapper = builder.Configure(doc);
#else
            _Mapper = builder.ConfigureAndWatch("MSSqlMap.config", handler);
#endif
        }

        public static ISqlMapper Instance()
        {
            if (_Mapper == null)
            {
                lock (typeof(SqlMapper))
                {
                    if (_Mapper == null) // double-check
                    {
                        InitMapper();   //返回单例模式实例
                    }
                }
            }
            return _Mapper;
        }

        public static ISqlMapper Instance(cPos.Model.LoggingManager loggingManager)
        {
            if (_Mapper == null)
            {
                lock (typeof(SqlMapper))
                {
                    if (_Mapper == null) // double-check
                    {
                        InitMapper();
                    }
                }
            }
            else
            {
                lock (typeof(SqlMapper))
                {
                    if (string.IsNullOrEmpty(loggingManager.Connection_String))
                    {
                        InitMapper();
                    }
                    else
                    {
                        RefreshMapperDataSource(loggingManager);
                    }
                    
                }
            }
            return _Mapper;
        }

        public static ISqlMapper Get()
        {
            return Instance();
        }

        private static void RefreshMapperDataSource(cPos.Model.LoggingManager loggingManager)
        {
            if (loggingManager == null)
            {
                //SessionManager sm = new SessionManager();
                //if (sm.loggingSessionInfo != null && sm.loggingSessionInfo.CurrentLoggingManager != null)
                //{
                //    if (sm.loggingSessionInfo.CurrentLoggingManager.Customer_Id != null)
                //    {
                //        if (sm.loggingSessionInfo.CurrentLoggingManager.Customer_Id != CustomerID)
                //        {
                //            CustomerID = sm.loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                //            string customer_connection_str = sm.loggingSessionInfo.CurrentLoggingManager.Connection_String.ToString();
                //            _mapper.DataSource.ConnectionString = customer_connection_str;
                //        }
                //    }
                //}
            }
            else
            {
                //if (loggingManager.Customer_Id != CustomerID)
                //{
                //    CustomerID = loggingManager.Customer_Id;
                //    string customer_connection_str = loggingManager.Connection_String;
                //    _Mapper.DataSource.ConnectionString = customer_connection_str;
                //}
                _Mapper.DataSource.ConnectionString = loggingManager.Connection_String;
            }
        }
    }
}
