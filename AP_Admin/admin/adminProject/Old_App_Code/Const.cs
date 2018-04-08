using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cPos.Admin.AP
{
    public class SystemName
    {
        public static String AP = "AP";
    }

    public class ModuleName
    {
        public static String CUSTOMER_DATA_EXCHANGE_SERVICE = "CustomerDataExchangeService";
    }

    public class FunctionName
    {
        public static String WS_SYN_USER = "SynUser";
        public static String WS_SYS_UNIT = "SynUnit";
        public static String WS_SYS_TERMINAL = "SynTerminal";
        public static String WS_CAN_ADD_USER = "CanAddUser";
        public static String WS_CAN_ADD_UNIT = "CanAddUnit";
        public static String WS_CAN_ADD_TERMINAL = "CanAddTerminal";
    }

    public class MessageType
    {
        public static String PARAMATER = "Params";
        public static String RESULT = "Return";
    }


    public static class HttpContentType
    {
        public const string X_WWW_FORM_URLENCODED = "application/x-www-form-urlencoded";
        public const string JSON = "application/json";
    }
}