using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cPos.Admin.SSO
{
    public class SystemName
    {
        public static String SSO = "sso";
    }

    public class ModuleName
    {
        public static String AUTHENTICATION_SERVICE = "AuthService";
    }

    public class FunctionName
    {
        public static String WS_GET_LOGIN_USER_INFO = "GetLoginUserInfo";
        public static String WS_GET_CUSTOMER_DB_CONNECTION_STRING = "GetCustomerDBConnectionString";
    }

    public class MessageType
    {
        public static String PARAMATER = "Params";
        public static String RESULT = "Return";
    }
}