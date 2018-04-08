using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper.Configuration;
using System.Web;
using cPos.Model;

namespace cPos.Components.SqlMappers
{
    public class cSqlMapper
    {
        private static volatile ISqlMapper _mapper = null;

        private static string CustomerID = "--";

        protected static void Configure(object obj)
        {
            _mapper = null;
        }

        //protected static void InitMapper()
        //{
        //    ConfigureHandler handler = new ConfigureHandler(Configure);
        //    DomSqlMapBuilder builder = new DomSqlMapBuilder();
        //    _mapper = builder.ConfigureAndWatch("Sql2010SqlMap.config", handler);
        //}

        protected static void InitMapper()
        {
            ConfigureHandler handler = new ConfigureHandler(Configure);
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
//#if DEBUG
            _mapper = builder.ConfigureAndWatch("Sql2010SqlMap.config", handler);
//#else
            //string fileMap = HttpContext.Current.Server.MapPath("..\\mssqlmap.dat");
            //if (!File.Exists(fileMap))
            //{
            //    throw new FileNotFoundException("mssqlmap.dat");
            //}
            //string fileKey = HttpContext.Current.Server.MapPath("..\\keys\\apself.key");
            //if (!File.Exists(fileKey))
            //{
            //    throw new FileNotFoundException("apself.key");
            //}
            //byte[] keys, iv;
            //bool k_ret = CryptKeyManager.GetCryptKeyAndIV(fileKey, out keys, out iv);
            //if (!k_ret)
            //{
            //    throw new Exception("密钥文件错误!");
            //}
            //StreamReader tr = new StreamReader(fileMap, Encoding.UTF8);
            //string input = tr.ReadToEnd();
            //tr.Close();
            //string output = DecryptManager.Decrypt(input, keys, iv, CryptProviderType.TripleDES);
            //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //doc.LoadXml(output);
            //_mapper = builder.Configure(doc);
//#endif
            //取客户的专用连接串
//#if DEBUG

//#else
            //if (HttpContext.Current.Session[KEY_CUSTOMER_ID] != null)
            //{
            //string customer_id = HttpContext.Current.Session[SessionManager.KEY_CUSTOMER_ID].ToString();

            //string customer_connection_str = HttpContext.Current.Session[customer_id].ToString();
                //_mapper.DataSource.ConnectionString = customer_connection_str;
            //}

            RefreshMapperDataSource(null);
//#endif
        }


        private static void RefreshMapperDataSource(cPos.Model.LoggingManager loggingManager)
        {
            if (loggingManager == null)
            {
                SessionManager sm = new SessionManager();
                if (sm.loggingSessionInfo != null && sm.loggingSessionInfo.CurrentLoggingManager != null)
                {
                    if (sm.loggingSessionInfo.CurrentLoggingManager.Customer_Id != null)
                    {
                        if (sm.loggingSessionInfo.CurrentLoggingManager.Customer_Id != CustomerID)
                        {
                            CustomerID = sm.loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                            string customer_connection_str = sm.loggingSessionInfo.CurrentLoggingManager.Connection_String.ToString();
                            _mapper.DataSource.ConnectionString = customer_connection_str;
                        }
                    }
                }
            }
            else
            {
                if (loggingManager.Customer_Id != CustomerID)
                {
                    CustomerID = loggingManager.Customer_Id;
                    string customer_connection_str = loggingManager.Connection_String;
                    _mapper.DataSource.ConnectionString = customer_connection_str;
                }
            }
        }

        public static ISqlMapper Instance()
        {
            if (_mapper == null)
            {
                lock (typeof(SqlMapper))
                {
                    if (_mapper == null) // double-check
                    {
                        InitMapper();
                    }
                }
            }
            else
            {
                lock (typeof(SqlMapper))
                {
                    RefreshMapperDataSource(null);
                }
            }
            return _mapper;
        }


        public static ISqlMapper Instance(cPos.Model.LoggingManager loggingManager )
        {
            if (_mapper == null)
            {
                lock (typeof(SqlMapper))
                {
                    if (_mapper == null) // double-check
                    {
                        InitMapper(loggingManager);
                    }
                }
            }
            else
            {
                lock (typeof(SqlMapper))
                {
                    RefreshMapperDataSource(loggingManager);
                }
            }
            return _mapper;
        }

        protected static void InitMapper(cPos.Model.LoggingManager loggingManager)
        {
            ConfigureHandler handler = new ConfigureHandler(Configure);
            DomSqlMapBuilder builder = new DomSqlMapBuilder();

            _mapper = builder.ConfigureAndWatch("Sql2010SqlMap.config", handler);

            if (loggingManager.Customer_Id != null)
            {
                CustomerID = loggingManager.Customer_Id;
                string customer_connection_str = loggingManager.Connection_String.ToString();
                _mapper.DataSource.ConnectionString = customer_connection_str;
            }
        }

        public static ISqlMapper Get()
        {
            return Instance();
        }
    }
}
