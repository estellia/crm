/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/27 14:05:14
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;

using JIT.Const;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Web.ComponentModel;

namespace JIT.Utility.Web
{
    /// <summary>
    /// 前端树控件的数据请求的一般处理程序 
    /// </summary>
    public abstract class JITTreeHandler<T>:JITAjaxHandler<T> where T:BasicUserInfo
    {
        #region 处理前端ajax的请求
        /// <summary>
        /// 本方法实现请求处理逻辑
        /// </summary>
        protected override void ProcessAjaxRequest(HttpContext pContext)
        {
            this.InitParams(pContext);
            var childrenNodes = this.GetNodes(this.ParentNodeID);
            if (this.IsAddPleaseSelectItem && this.IsFirstRequest(this.ParentNodeID))
            {
                childrenNodes.Insert(0, new TreeNode() { ID=this.PleaseSelectID, Text =this.PleaseSelectText, ParentID =this.ParentNodeID, IsLeaf =true, IsPleaseSelectItem=true});
            }
            pContext.Response.Write(childrenNodes.ToTreeStoreJSON(this.IsMultiSelect,this.IsSelectLeafOnly,this.InitValues));
            pContext.Response.End();
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 父节点ID
        /// </summary>
        protected string ParentNodeID { get;private set; }

        /// <summary>
        /// 是否为多选,默认为单选
        /// </summary>
        protected bool IsMultiSelect { get; private set; }

        /// <summary>
        /// 是否只能选择叶子节点
        /// </summary>
        protected bool IsSelectLeafOnly { get; private set; }

        /// <summary>
        /// 是否添加"--请选择--"项
        /// </summary>
        protected bool IsAddPleaseSelectItem { get; private set; }

        /// <summary>
        /// 初始值
        /// </summary>
        protected TreeNode[] InitValues { get; private set; }

        /// <summary>
        /// "--请选择--"项的文本
        /// <remarks>
        /// <para>默认的文本为：--请选择--</para>
        /// </remarks>
        /// </summary>
        protected string PleaseSelectText { get; set; }

        /// <summary>
        /// "--请选择--"项的ID
        /// <remarks>
        /// <para>默认的ID值为：-2</para>
        /// </remarks>
        /// </summary>
        protected string PleaseSelectID { get; set; }
        #endregion

        #region 初始化参数
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="pContext"></param>
        protected void InitParams(HttpContext pContext)
        {
            //父节点ID
            this.ParentNodeID = pContext.Request.QueryString["node"];
            //是否为多选
            string strMultiSelect = pContext.Request.QueryString["multiSelect"];
            if (!string.IsNullOrWhiteSpace(strMultiSelect))
            {
                bool temp = false;
                if (bool.TryParse(strMultiSelect, out temp))
                {
                    this.IsMultiSelect = temp;
                }
            }
            //是否只能选择叶子节点
            string strIsSelectLeafOnly = pContext.Request.QueryString["isSelectLeafOnly"];
            if (!string.IsNullOrWhiteSpace(strIsSelectLeafOnly))
            {
                bool temp = false;
                if (bool.TryParse(strIsSelectLeafOnly, out temp))
                {
                    this.IsSelectLeafOnly = temp;
                }
            }
            //初始值
            string strInitValues = pContext.Request.QueryString["initValues"];
            if (!string.IsNullOrWhiteSpace(strInitValues))
            {
                this.InitValues = strInitValues.DeserializeJSONTo<TreeNode[]>();
            }
            //是否添加请选择项
            string strIsAddPleaseSelectItem = pContext.Request.QueryString["isAddPleaseSelectItem"];
            if (!string.IsNullOrWhiteSpace(strIsAddPleaseSelectItem))
            {
                bool temp = false;
                if (bool.TryParse(strIsAddPleaseSelectItem,out temp))
                {
                    this.IsAddPleaseSelectItem = temp;
                }
            }
            //请选择项文本
            string strPleaseSelectText = pContext.Request.QueryString["pleaseSelectText"];
            if (!string.IsNullOrWhiteSpace(strPleaseSelectText))
            {
                this.PleaseSelectText = strPleaseSelectText;
            }
            else
            {
                this.PleaseSelectText = "--请选择--";
            }
            //请选择项ID
            string strPleaseSelectID = pContext.Request.QueryString["pleaseSelectID"];
            if (!string.IsNullOrWhiteSpace(strPleaseSelectID))
            {
                this.PleaseSelectID = strPleaseSelectID;
            }
            else
            {
                this.PleaseSelectID = "-2";
            }
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 获得子节点
        /// </summary>
        /// <param name="pParentNodeID">父节点的ID</param>
        /// <returns>返回该父节点下的所有子节点</returns>
        protected abstract TreeNodes GetNodes(string pParentNodeID);
        #endregion

        #region 虚方法
        /// <summary>
        /// 是否为第一次请求
        /// <remarks>
        /// <para>当前的逻辑是：如果父节点的ID为空或null或者是-1或者是root,则表示当前是第一次请求.</para>
        /// <para>如果该逻辑与你的业务不符,请重写本方法,实现自身的逻辑.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pParentNodeID">父节点</param>
        /// <returns></returns>
        protected virtual bool IsFirstRequest(string pParentNodeID)
        {
            if (string.IsNullOrWhiteSpace(pParentNodeID) || pParentNodeID.Trim() == "-1" || pParentNodeID.Trim()=="root")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
