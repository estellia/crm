using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Test
{
    public class Common
    {
        public enum UserEnum
        {
            /// <summary>
            /// 正念
            /// </summary>
            ZMIND = 0,
            /// <summary>
            /// 新干线ERP
            /// </summary>
            ERP = 1,
            /// <summary>
            /// 新干线PC商城
            /// </summary>
            PCShop = 2,
        }

        public enum OpeEnum
        {
            /// <summary>
            /// 新增
            /// </summary>
            A,
            /// <summary>
            /// 修改
            /// </summary>
            U,
            /// <summary>
            /// 删除
            /// </summary>
            D,
        }



    }

}