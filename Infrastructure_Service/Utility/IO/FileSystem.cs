/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/26 15:03:52
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
using System.Reflection;
using System.Text;

namespace JIT.Utility.IO
{
    /// <summary>
    /// 文件系统 
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// 获取当前应用程序域的入口程序集的文件位置
        /// </summary>
        public static string EntryAssemblyLocation
        {
            get
            {
                var ass = Assembly.GetEntryAssembly();
                return ass != null && !ass.IsDynamic ? ass.Location : null;
            }
        }

        /// <summary>
        /// 当前应用程序域的根目录
        /// </summary>
        public static string AppDomainBaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// 从指定的文件中读取所有文本
        /// </summary>
        /// <param name="pPath">指定文件的路径</param>
        /// <returns>文件内容</returns>
        public static string ReadAllText(string pPath)
        {
            return File.ReadAllText(pPath);
        }

        /// <summary>
        /// 将文本保存到文件中去
        /// </summary>
        /// <param name="pFilePath">文件路径</param>
        /// <param name="pFileContent">文件内容</param>
        public static void SaveTextToFile(string pFilePath, string pFileContent)
        {
            File.WriteAllText(pFilePath, pFileContent);
        }

        /// <summary>
        /// 将多个文件路径合并
        /// </summary>
        /// <param name="pPaths">待合并的文件路径</param>
        /// <returns></returns>
        public static string Combine(params string[] pPaths)
        {
            return Path.Combine(pPaths);
        }

        /// <summary>
        /// 获取文件路径的全路径
        /// </summary>
        /// <param name="pPath">文件路径（文件路径中的相对路径会被转换掉）</param>
        /// <returns></returns>
        public static string GetFullPath(string pPath)
        {
            return Path.GetFullPath(pPath);
        }

        /// <summary>
        /// 判断是否有该文件
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static bool IsExists(string pPath)
        {
            return File.Exists(pPath);
        }

        /// <summary>
        /// 判断文件路径是否包含根路径
        /// </summary>
        /// <param name="pPath">文件路径</param>
        /// <returns></returns>
        public static bool IsPathRooted(string pPath)
        {
            return Path.IsPathRooted(pPath);
        }

        /// <summary>
        /// 根据文件路径获取文件所属的目录的路径
        /// </summary>
        /// <param name="pPath">文件路径</param>
        /// <returns></returns>
        public static string GetDirectoryName(string pPath)
        {
            return Path.GetDirectoryName(pPath);
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="pPath">文件路径</param>
        /// <returns></returns>
        public static string GetFileName(string pPath)
        {
            return Path.GetFileName(pPath);
        }

        /// <summary>
        /// 如果没有目录则创建目录
        /// </summary>
        /// <param name="pDir">目录，或文件</param>
        public static void CreateDirectoryIfNotExists(string pDir)
        {
            var dir = Path.GetDirectoryName(pDir);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
