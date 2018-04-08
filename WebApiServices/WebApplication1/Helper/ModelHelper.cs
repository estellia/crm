using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class ModelHelper
    {
        /// <summary>
        /// 用户枚举
        /// </summary>
        public enum UserEnum
        {
            /// <summary>
            /// 正念
            /// </summary>
            ZMIND,
            /// <summary>
            /// 新干线ERP
            /// </summary>
            ERP,
            /// <summary>
            /// 新干线PC商城
            /// </summary>
            PCShop,
        }

        /// <summary>
        /// 操作枚举
        /// </summary>
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
            /// 取消
            /// </summary>
            C,
            /// <summary>
            /// 删除
            /// </summary>
            D,
            /// <summary>
            /// 关闭
            /// </summary>
            L,
        }


        /// <summary>
        /// 生成消息
        /// </summary>
        /// <returns></returns>
        public static string GetOmsg()
        {
            OMSG model = new OMSG
            {             
                Timestamp = DateTime.Now,
                FromSystem = UserEnum.ZMIND.ToString(),
                FromCompany = UserEnum.ZMIND.ToString(),
                Flag = "1",
                ObjectType = "101",//101--116,详情产考对象清单
                TransType = OpeEnum.A.ToString(),
                FieldsInKey = 1,//主键字段数量
                FieldNames = "VipCardTypeID",
                FieldValues = "131",
                Status = 0,
                Comments = "Comments",
                UpDateTime = DateTime.Now,
            };

            #region flog
            //SELECT TOP { 0}
            //T0.SequenceID FROM IF_OMSG T0
            //                                JOIN IF_OMSG3 T1 ON T0.FromSystem = T1.FromSystem AND T0.FromCompany = T1.FromCompany AND T0.Flag = T1.Flag AND T0.ObjectType = T1.ObjectType AND T1.ToSystem = '{1}'
            //                                LEFT JOIN IF_MSG2 T2 ON T0.SequenceID = T2.SequenceID AND T2.targetsystem = '{1}'
            //                                WHERE(T0.Status <> -1) AND((T2.Status IS NULL) OR(T2.Status <> 0 AND T2.Status <= T0.Status))  ORDER BY T0.SequenceID ", top, systemCode);
            #endregion

            return JsonHelper.DataContractJsonSerialize<OMSG>(model);
        }

        /// <summary>
        /// 生成消息
        /// </summary>
        /// <returns></returns>
        public static OMSG GetOmsgModel()
        {
            OMSG model = new OMSG
            {               
                Timestamp = DateTime.Now,
                FromSystem = UserEnum.ZMIND.ToString(),
                FromCompany = UserEnum.ZMIND.ToString(),
                Flag = "1",
                ObjectType = "101",//101--116,详情产考对象清单
                TransType = OpeEnum.A.ToString(),
                FieldsInKey = 1,//主键字段数量
                FieldNames = "VipCardTypeID",
                FieldValues = "131",
                Status = 0,
                Comments = "Comments",
                UpDateTime = DateTime.Now,
            };

            #region flog
            //SELECT TOP { 0}
            //T0.SequenceID FROM IF_OMSG T0
            //                                JOIN IF_OMSG3 T1 ON T0.FromSystem = T1.FromSystem AND T0.FromCompany = T1.FromCompany AND T0.Flag = T1.Flag AND T0.ObjectType = T1.ObjectType AND T1.ToSystem = '{1}'
            //                                LEFT JOIN IF_MSG2 T2 ON T0.SequenceID = T2.SequenceID AND T2.targetsystem = '{1}'
            //                                WHERE(T0.Status <> -1) AND((T2.Status IS NULL) OR(T2.Status <> 0 AND T2.Status <= T0.Status))  ORDER BY T0.SequenceID ", top, systemCode);
            #endregion

            return model;
        }

        /// <summary>
        /// 生成的消息原始结构（未解析）
        /// </summary>
        /// <returns></returns>
        public static string GetMSG1()
        {
            string strContent = "JSON内容" + DateTime.Now.ToString("yyyY-MM-dd HH:mm:ss");
            var model = new MSG1
            {
                //SequenceID = 1,
                Content = strContent,
                iLength = strContent.Length,
            };

            return JsonHelper.DataContractJsonSerialize<MSG1>(model);
        }

        /// <summary>
        /// 生成的消息原始结构（未解析）
        /// </summary>
        /// <returns></returns>
        public static MSG1 GetMSG1Model()
        {
            string strContent = "JSON内容" + DateTime.Now.ToString("yyyY-MM-dd HH:mm:ss");
            var model = new MSG1
            {
                //SequenceID = 1,
                Content = strContent,
                iLength = strContent.Length,
            };
            
            return model;
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <returns></returns>

        public static string GetMsg2()
        {
            var model = new MSG2
            {
                SequenceID = 1,
                Status = 0,
                ErrorMSG = "错误内容",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                TargetSystem = UserEnum.ERP.ToString(),//消费方，谁来消费的
                TargetDB = null,//消费方数据库,暂时无用
                TargetType = "Response Message Model",
                TargetValue = "Response Message Model PK",
            };

            return JsonHelper.DataContractJsonSerialize<MSG2>(model);
        }

        public static MSG2 SetMsg2Model()
        {
            var model = new MSG2
            {
                SequenceID = 10001,//设定指定的消息消费结果
                Status = 0,
                ErrorMSG = "消费的结果",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                TargetSystem = UserEnum.ERP.ToString(),//消费方，谁来消费的
                TargetDB = null,//消费方数据库,暂时无用
                TargetType = "Response Message Model",
                TargetValue = "Response Message Model PK",
            };
            return model;
            //return JsonHelper.DataContractJsonSerialize<MSG2>(model);
        }
    }
}