/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/21 10:21:12
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
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.SessionState;
using System.IO;
using System.Threading;

namespace JIT.Utility.Web
{
    /// <summary>
    /// JIT的MVC模式的Ajax请求处理者  
    /// </summary>
    public abstract class JITMVCAjaxHandler<T>:JITAjaxHandler<T> where T:BasicUserInfo
    {
        #region 属性集
        /// <summary>
        /// 发送请求的视图
        /// </summary>
        protected View CurrentView { get; set; }

        /// <summary>
        /// 当前请求的操作
        /// </summary>
        protected Action CurrentAction { get; set; }
        #endregion

        #region 处理Ajax请求
        /// <summary>
        /// 处理Ajax请求
        /// </summary>
        /// <param name="pContext"></param>
        protected override void ProcessAjaxRequest(HttpContext pContext)
        {
            this.InitParams(pContext);
            this.ProccssAction(this.CurrentView, this.CurrentAction, pContext);
        }
        #endregion

        #region 初始化参数
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="pContext"></param>
        protected virtual void InitParams(HttpContext pContext)
        {
            string strView = string.IsNullOrEmpty(pContext.Request.QueryString["view"]) ? pContext.Request.Form["view"] : pContext.Request.QueryString["view"];
            if (!string.IsNullOrEmpty(strView))
            {
                this.CurrentView = new View(strView);
            }

            string strAction = string.IsNullOrEmpty(pContext.Request.QueryString["action"]) ? pContext.Request.Form["action"] : pContext.Request.QueryString["action"];
            if (!string.IsNullOrEmpty(strAction))
            {
                this.CurrentAction = new Action(strAction);
            }
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 处理Action请求
        /// </summary>
        /// <param name="pView">发送操作请求的视图</param>
        /// <param name="pAction">所请求的操作</param>
        /// <param name="pContext">请求上下文</param>
        protected abstract void ProccssAction(View pView, Action pAction, HttpContext pContext);
        #endregion
    }
}
