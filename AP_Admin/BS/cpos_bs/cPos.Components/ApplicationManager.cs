using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using cPos.Components.Exceptions;

namespace cPos.Components
{
    public class ApplicationManager
    {
        public static string KEY_OBJECT_KINDS = "Language.ObjectKinds";

        /// <summary>
        /// 根据对象的种类的编码获取对象的种类的Id
        /// </summary>
        /// <param name="objectKindCode">对象的种类的编码</param>
        /// <returns></returns>
        public static string GetObjectKindId(string objectKindCode)
        {
            Hashtable hashTable = (Hashtable)HttpContext.Current.Application[KEY_OBJECT_KINDS];

            if (hashTable == null)
            {
                throw new DrpException("Not contain object kinds: " + objectKindCode);
            }
            else
            {
                return (string)hashTable[objectKindCode];
            }
        }

        //得到本项目的ApplicationId
        public static string GetApplicationId()
        {
            return "A2BF354A4E5E4DE7907DCD25200A0879";
        }

        //得到POS项目的ApplicationId
        public static string GetPosApplicationId()
        {
            return "FE1ED74EB63A442CB55C91E1F0A46788";
        }
    }
}
