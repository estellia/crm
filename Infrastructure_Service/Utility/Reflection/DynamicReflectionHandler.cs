using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using JIT.Utility.ETCL.Checker;
using JIT.Utility.Log;
using JIT.Utility.ETCL.DataStructure;
using System.Data;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Reflection
{
    /// <summary>
    /// 运行期创建类，调用方法的反射处理器
    /// </summary>
    public class DynamicReflectionHandler
    {
        public static Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();
        /// <summary>
        /// 创建类的实例
        /// </summary>
        /// <param name="pAssemblyName">模块名（DLL）</param>
        /// <param name="pTypeName">待实例化的类型名称</param>
        /// <param name="pArgs">构造器参数</param>
        /// <returns>实例化后的对象</returns>
        public static object CreateInstance(string pAssemblyName, string pTypeName, object[] pArgs)
        {
            try
            {
                Assembly assembly;
                if (LoadedAssemblies.ContainsKey(pAssemblyName))
                {
                    assembly = LoadedAssemblies[pAssemblyName];
                }
                else
                {
                    assembly = System.Reflection.Assembly.LoadFrom(pAssemblyName);
                    LoadedAssemblies.Add(pAssemblyName, assembly);
                }
                Module[] allModules = assembly.GetModules();
                Type type = allModules[0].GetType(pTypeName);
                if (pArgs != null)
                {
                    ConstructorInfo conInfo = type.GetConstructor(new Type[] { pArgs[0].GetType() });
                    return conInfo.Invoke(pArgs);
                }
                return Activator.CreateInstance(type, pArgs, null);
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }

        public static object CreateInstance(Type pType, params object[] pArgs)
        {
            try
            {
                return Activator.CreateInstance(pType, pArgs);
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 反射调用对象的方法
        /// </summary>
        /// <param name="pInstance">对象实例</param>
        /// <param name="pFunctionName">待调用的方法名</param>
        /// <param name="pParam">方法所需要的参数</param>
        /// <returns>调用结果</returns>
        public static object CallFunction(object pInstance, string pFunctionName, object[] pParam)
        {
            //Type[] types ;
            //if (pParam == null)
            //{
            //    types = null;
            //}
            //else
            //{
            //    types = new Type[pParam.Length];
            //    for (int i = 0; i < pParam.Length; i++)
            //    {
            //        types[i] = pParam[i].GetType();
            //    }
            //}
            //MethodInfo method;
            //MethodInfo[] allMethods = pInstance.GetType().GetMethods();
            //List<MethodInfo> lstMethods = new List<MethodInfo>();
            //foreach (var item in allMethods)
            //{//查找同名方法
            //    if (item.Name == pFunctionName)
            //        lstMethods.Add(item);
            //}
            //if (lstMethods.Count == 0)
            //    throw new Exception("类型[" + pInstance.GetType().ToString() + "]中未找到方法[" + pFunctionName + "].");
            //if (lstMethods.Count == 1)
            //{
            //    method = lstMethods[0];
            //}
            //else
            //{//查找相同参数数量的方法

            //}
            //MethodInfo method = pInstance.GetType().GetMethod(pFunctionName, types);
            //if (method==null )
            //    method = pInstance.GetType().GetMethod(pFunctionName);
            try
            {
                MethodInfo method = TryGetMethodInfo(pInstance, pFunctionName, pParam);
                BindingFlags flag = BindingFlags.Public | BindingFlags.Instance;
                //object[] parameters = new object[] { "Hello!!!" };
                object returnValue = method.Invoke(pInstance, flag, Type.DefaultBinder, pParam, null);
                return returnValue;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }

        private static MethodInfo TryGetMethodInfo(object pInstance, string pFunctionName, object[] pParam)
        {
            try
            {
                MethodInfo method = null;
                try
                {
                    bool bGetTypeSuccess = true;
                    Type[] types;
                    if (pParam == null)
                    {
                        types = null;
                    }
                    else
                    {
                        types = new Type[pParam.Length];
                        for (int i = 0; i < pParam.Length; i++)
                        {
                            if (pParam[i] == null)
                            {
                                bGetTypeSuccess = false;
                                break;
                            }
                            types[i] = pParam[i].GetType();
                        }
                    }
                    if (bGetTypeSuccess)
                        method = pInstance.GetType().GetMethod(pFunctionName, types);
                }
                catch (Exception ex) { }
                if (method != null)
                    return method;

                MethodInfo[] allMethods = pInstance.GetType().GetMethods();
                List<MethodInfo> lstMethods = new List<MethodInfo>();
                foreach (var item in allMethods)
                {//查找同名方法
                    if (item.Name == pFunctionName)
                        lstMethods.Add(item);
                }
                if (lstMethods.Count == 0)
                    throw new Exception("类型[" + pInstance.GetType().ToString() + "]中未找到方法[" + pFunctionName + "].");
                if (lstMethods.Count == 1)
                {//只有一个同名方法
                    return lstMethods[0];
                }
                else
                {//查找相同参数数量的方法                     
                    foreach (var mtdItem in lstMethods)
                    {
                        bool bMached = true;
                        ParameterInfo[] param = mtdItem.GetParameters();
                        if (param.Length == pParam.Length)
                        {
                            method = mtdItem;
                            //检查类型
                            for (int i = 0; i < param.Length; i++)
                            {
                                ParameterInfo paramInfo = param[i];
                                if (pParam[i] != null)
                                {
                                    if (paramInfo.ParameterType != pParam[i].GetType())
                                    {
                                        bMached = false;
                                        break;
                                    }
                                }
                            }
                            if (bMached)
                                return method;
                        }
                    }
                }
                return method;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 获取对象实例的属性
        /// </summary>
        /// <param name="pInstance">对象实例</param>
        /// <param name="pPropertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static object GetProperty(object pInstance, string pPropertyName)
        {
            try
            {
                PropertyInfo propertyInfo = pInstance.GetType().GetProperty(pPropertyName);
                return propertyInfo.GetValue(pInstance, null);
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 设置对象实例的属性
        /// </summary>
        /// <param name="pInstance">对象实例</param>
        /// <param name="pPropertyName">属性名称</param>
        /// <param name="pValue">属性值</param>
        /// <returns>TRUE：设置成功，FALSE：设置失败。</returns>
        public static bool SetProperty(object pInstance, string pPropertyName, object pValue)
        {
            try
            {
                PropertyInfo propertyInfo = pInstance.GetType().GetProperty(pPropertyName);
                if (propertyInfo == null)
                    return true;
                try
                {
                    propertyInfo.SetValue(pInstance, pValue, null);
                    return true;
                }
                catch (Exception ex) { }
                try
                {
                    //类型转换
                    object value = null;
                    value = pValue.ChangeTypeTo(propertyInfo.PropertyType);

                    propertyInfo.SetValue(pInstance, value, null);
                    return true;
                }
                catch (Exception ex)
                {
                }
                return false;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 创建Entity到数据库表
        /// </summary>
        /// <param name="pTableInfo">表信息</param>
        /// <param name="pDaoConstructorParams">Dao构造器参数</param>
        /// <param name="object">实体对象</param>
        /// <param name="pTran">数据库事务</param>
        /// <returns>创建结果</returns>
        public static object CreateEntity(ETCLTable pTableInfo, object[] pDaoConstructorParams, object pEntity, IDbTransaction pTran)
        {
            try
            {
                string pDaoDLL = pTableInfo.DAOAssemblyName;
                string pDaoClassFullName = pTableInfo.DAOName;
                string pEntityDLL = pTableInfo.EntityAssemblyName;
                string pEntityFullName = pTableInfo.EntityName;
                if (string.IsNullOrWhiteSpace(pDaoDLL))
                    pDaoDLL = "TenantPlatform.DataAccess.dll";
                if (string.IsNullOrEmpty(pDaoClassFullName))
                    pDaoClassFullName = "JIT.TenantPlatform.DataAccess." + pTableInfo.TableName + "DAO";
                if (string.IsNullOrEmpty(pEntityDLL))
                    pEntityDLL = "TenantPlatform.Entity.dll";
                if (string.IsNullOrEmpty(pEntityFullName))
                    pEntityFullName = "JIT.TenantPlatform.Entity." + pTableInfo.TableName + "Entity";

                string assemblyPath;
                if (System.Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory)
                {//Winform
                    assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {//Web
                    assemblyPath = AppDomain.CurrentDomain.BaseDirectory + "Bin/";
                }
                object oDAO = JIT.Utility.Reflection.DynamicReflectionHandler.CreateInstance(assemblyPath + pDaoDLL, pDaoClassFullName, pDaoConstructorParams);
                return JIT.Utility.Reflection.DynamicReflectionHandler.CallFunction(oDAO, "Create", new object[] { pEntity, pTran });
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 根据实体属性查询实体
        /// </summary>
        /// <param name="pTableInfo">表信息</param>
        /// <param name="pDaoConstructorParams">Dao构造器参数</param>
        /// <param name="pEntityProperty">实体属性字典</param>
        /// <returns>查询结果</returns>
        public static object[] QueryByEntity(ETCLTable pTableInfo, object[] pDaoConstructorParams, Dictionary<string, object> pEntityProperty)
        {
            try
            {
                string pDaoDLL = pTableInfo.DAOAssemblyName;
                string pDaoClassFullName = pTableInfo.DAOName;
                string pEntityDLL = pTableInfo.EntityAssemblyName;
                string pEntityFullName = pTableInfo.EntityName;
                if (string.IsNullOrWhiteSpace(pDaoDLL))
                    pDaoDLL = "TenantPlatform.DataAccess.dll";
                if (pDaoClassFullName == null)
                    pDaoClassFullName = "JIT.TenantPlatform.DataAccess." + pTableInfo.TableName + "DAO";
                if (string.IsNullOrWhiteSpace(pEntityDLL))
                    pEntityDLL = "TenantPlatform.Entity.dll";
                if (pEntityFullName == null)
                    pEntityFullName = "JIT.TenantPlatform.Entity." + pTableInfo.TableName + "Entity";

                string assemblyPath;
                if (System.Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory)
                {//Winform
                    assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {//Web
                    assemblyPath = AppDomain.CurrentDomain.BaseDirectory + "Bin/";
                }

                object oDAO = JIT.Utility.Reflection.DynamicReflectionHandler.CreateInstance(assemblyPath + pDaoDLL, pDaoClassFullName, pDaoConstructorParams);
                object oEntity = JIT.Utility.Reflection.DynamicReflectionHandler.CreateInstance(assemblyPath + pEntityDLL, pEntityFullName, null);
                foreach (var item in pEntityProperty)
                {
                    JIT.Utility.Reflection.DynamicReflectionHandler.SetProperty(oEntity, item.Key, item.Value);
                }
                object oResult = JIT.Utility.Reflection.DynamicReflectionHandler.CallFunction(oDAO, "QueryByEntity", new object[] { oEntity, null });
                if (oResult == null)
                    return null;
                object[] oResults = oResult as object[];
                return oResults;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="pTableInfo">表信息</param>
        /// <param name="pDaoConstructorParams">Dao构造器参数</param>
        /// <returns>查询结果</returns>
        public static object[] QueryAll(ETCLTable pTableInfo, object[] pDaoConstructorParams)
        {
            try
            {
                string pDaoDLL = pTableInfo.DAOAssemblyName;
                string pDaoClassFullName = pTableInfo.DAOName;
                string pEntityDLL = pTableInfo.EntityAssemblyName;
                string pEntityFullName = pTableInfo.EntityName;
                if (string.IsNullOrWhiteSpace(pDaoDLL))
                    pDaoDLL = "TenantPlatform.DataAccess.dll";
                if (string.IsNullOrWhiteSpace(pDaoClassFullName))
                    pDaoClassFullName = "JIT.TenantPlatform.DataAccess." + pTableInfo.TableName + "DAO";
                if (string.IsNullOrWhiteSpace(pEntityDLL))
                    pEntityDLL = "TenantPlatform.Entity.dll";
                if (string.IsNullOrWhiteSpace(pEntityFullName))
                    pEntityFullName = "JIT.TenantPlatform.Entity." + pTableInfo.TableName + "Entity";

                string assemblyPath;
                if (System.Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory)
                {//Winform
                    assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {//Web
                    assemblyPath = AppDomain.CurrentDomain.BaseDirectory + "Bin/";
                }
                object oDAO = JIT.Utility.Reflection.DynamicReflectionHandler.CreateInstance(assemblyPath + pDaoDLL, pDaoClassFullName, pDaoConstructorParams);
                object oEntity = JIT.Utility.Reflection.DynamicReflectionHandler.CreateInstance(assemblyPath + pEntityDLL, pEntityFullName, null);
                object oResult = JIT.Utility.Reflection.DynamicReflectionHandler.CallFunction(oDAO, "GetAll", null);
                if (oResult == null)
                    return null;
                object[] oResults = oResult as object[];
                return oResults;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex.InnerException));
                throw ex.InnerException;
            }
        }
    }
}
