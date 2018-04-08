/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/4 11:29:24
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
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// Page的扩展方法 
    /// </summary>
    public static class PageExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取页面的文件名
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static string GetFileName(this Page pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            var filePath =pCaller.Request.PhysicalPath;
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// 扩展方法：获取页面所属的文件夹的名称
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static string GetFolderName(this Page pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            var filePath = pCaller.Request.PhysicalPath;
            var directoryPath = Path.GetDirectoryName(filePath);
            var folderStartIndex = directoryPath.LastIndexOf('/');
            //
            var temp =directoryPath.LastIndexOf('\\');
            if (temp > folderStartIndex)
            {
                folderStartIndex = temp;
            }
            return directoryPath.Substring(folderStartIndex+1);
        }

        /// <summary>
        /// 扩展方法：获取目录的路径
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string GetDirectory(this Page pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            var path = pCaller.Request.Path;
            return Path.GetDirectoryName(path).Replace("\\","/");
        }

        /// <summary>
        /// 扩展方法：获取页面的绝对路径的url
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static string GetAbsoluteUrl(this Page pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            //
            return pCaller.Request.Path;
        }
    }
}
