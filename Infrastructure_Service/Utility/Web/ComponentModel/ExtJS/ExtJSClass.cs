/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/8 15:27:08
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
using System.Text;

using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Web.ComponentModel.ExtJS
{
    /// <summary>
    /// Ext JS的类 
    /// </summary>
    public class ExtJSClass:IJavascriptObject
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ExtJSClass()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pInitConfigs">配置项</param>
        public ExtJSClass(Dictionary<string, object> pInitConfigs)
        {
            this.InitConfigs = pInitConfigs;
        }
        #endregion

        #region 属性集

        /// <summary>
        /// ExtJS类的类全名
        /// </summary>
        public string ClassFullName { get; set; }

        /// <summary>
        /// 类实例的配置属性,用于创建一个类实例
        /// </summary>
        protected Dictionary<string, object> InitConfigs { get; set; }

        /// <summary>
        /// 标签信息（该字段仅用于在程序内部，用于对对象的分组识别）
        /// <remarks>
        /// <para>该字段不属于Ext JS</para>
        /// </remarks>
        /// </summary>
        public object Tags { get; set; }
        #endregion

        #region 生成创建类实例的脚本
        /// <summary>
        /// 生成创建类实例的脚本
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns>Ext JS的创建类实例的语句</returns>
        public virtual string ToCreationScript(int pPrevTabs)
        {
            //检查
            if (string.IsNullOrEmpty(this.ClassFullName))
            {
                throw new ArgumentNullException("ClassFullName");
            }
            //
            var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
            //
            StringBuilder script = new StringBuilder();
            script.AppendFormat("{1}Ext.create({2}{3}{2},{0}",Environment.NewLine,tabs,JSONConst.STRING_WRAPPER,this.ClassFullName);
            var configs = ExtJSClass.ToConfigScript(pPrevTabs, this.InitConfigs);
            //configs = configs.TrimStart(Keyboard.SPACE);
            script.Append(configs);
            script.AppendFormat("{0});",tabs);
            //
            return script.ToString();
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 获取指定的初始化配置项的值
        /// </summary>
        /// <typeparam name="T">T为配置项值的类型</typeparam>
        /// <param name="pConfigItemKey">初始化配置项的键</param>
        /// <returns>初始化配置项的值</returns>
        public T GetInitConfigValue<T>(string pConfigItemKey)
        {
            var val = default(T);
            if (this.InitConfigs != null)
            {
                if (this.InitConfigs.ContainsKey(pConfigItemKey))
                {
                    var temp =this.InitConfigs[pConfigItemKey];
                    if (temp != null)
                    {
                        val = (T)temp;
                    }
                }
            }
            return val;
        }

        /// <summary>
        /// 设置指定的初始化配置项的值
        /// </summary>
        /// <param name="pConfigItemKey">初始化配置项的键</param>
        /// <param name="pConfigItemValue">初始化配置项的值</param>
        public void SetInitConfigValue(string pConfigItemKey, object pConfigItemValue)
        {
            if (this.InitConfigs == null)
                this.InitConfigs = new Dictionary<string, object>();
            if (this.InitConfigs.ContainsKey(pConfigItemKey))
            {
                this.InitConfigs[pConfigItemKey] = pConfigItemValue;
            }
            else
            {
                this.InitConfigs.Add(pConfigItemKey, pConfigItemValue);
            }
        }
        #endregion

        #region 类行为

        #region 生成定义的语句

        /// <summary>
        /// 生成定义的语句
        /// </summary>
        /// <param name="pClassFullName">类全名</param>
        /// <param name="pDefinitionConfigs">类定义的配置项</param>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns>Ext JS类的定义语句</returns>
        public static string ToDefineScript(string pClassFullName,Dictionary<string,object> pDefinitionConfigs,int pPrevTabs =1)
        {
            //检查
            if (string.IsNullOrEmpty(pClassFullName))
            {
                throw new ArgumentNullException("pClassFullName");
            }
            //
            var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
            //
            StringBuilder script = new StringBuilder();
            script.AppendFormat("{1}Ext.define({3}{2}{3},{0}", Environment.NewLine, tabs, pClassFullName,JSONConst.STRING_WRAPPER);
            var configs = ExtJSClass.ToConfigScript(pPrevTabs, pDefinitionConfigs);
            script.Append(configs);
            script.AppendFormat("{1});{0}", Environment.NewLine, tabs);
            //
            return script.ToString();
        }
        #endregion

        #region 生成配置项的语句
        /// <summary>
        /// 生成配置项的语句
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <param name="pConfigs">配置项</param>
        /// <returns></returns>
        public static string ToConfigScript(int pPrevTabs, Dictionary<string,object> pConfigs)
        {
            var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
            //
            var script = new StringBuilder();
            script.AppendFormat("{1}{{{0}", Environment.NewLine, tabs);
            if (pConfigs != null)
            {
                bool isFrist = true;
                foreach (var config in pConfigs)
                {
                    if (!string.IsNullOrEmpty(config.Key) && config.Value !=null)
                    {
                        if (isFrist)
                        {
                            script.AppendFormat("{0}{1}", tabs, Keyboard.TAB);
                            isFrist = false;
                        }
                        else
                        {
                            script.AppendFormat("{0}{1},", tabs, Keyboard.TAB);
                        }
                        var configValue =string.Empty;
                        var valType = config.Value.GetType();
                        if (config.Value is IJavascriptObject)  //实现IJavascriptObject接口的类
                        {
                            configValue = ((IJavascriptObject)config.Value).ToScriptBlock(pPrevTabs + 1);
                            configValue = configValue.TrimStart(Keyboard.TAB.ToCharArray());
                        }
                        else if (config.Value is IEnumerable<IJavascriptObject>)//元素实现IJavascriptObject接口的集合类
                        {
                            var items = config.Value as IEnumerable<IJavascriptObject>;
                            StringBuilder temp = new StringBuilder();
                            temp.AppendFormat("{0}{1}{2}[{0}", Environment.NewLine, tabs, Keyboard.TAB);
                            bool isFirst = true;
                            foreach (var item in items)
                            {
                                if (!isFirst)
                                {
                                    temp.AppendFormat("{0}{1},",tabs,Keyboard.TAB);
                                }
                                else
                                {
                                    temp.AppendFormat("{0}{1}", tabs, Keyboard.TAB);
                                    isFirst = false;
                                }
                                var itemScript = item.ToScriptBlock(pPrevTabs + 1);
                                itemScript = itemScript.TrimStart(Keyboard.SPACE);
                                temp.Append(itemScript);
                            }
                            temp.AppendFormat("{1}{2}]{0}", Environment.NewLine, tabs, Keyboard.TAB);
                            //
                            configValue = temp.ToString();
                        }
                        else if (valType.IsEnum)    //枚举类
                        {
                            configValue = string.Format("{1}{0}{1}",((Enum)config.Value).GetDescription(),JSONConst.STRING_WRAPPER);
                        }
                        else if (valType.IsNullableValueType() && valType.GetNullableUnderlyingType().IsEnum) //可空枚举
                        {
                            var val = (Enum)ReflectionUtils.GetPropertyValue(config.Value, "Value");
                            configValue = string.Format("{1}{0}{1}", val.GetDescription(), JSONConst.STRING_WRAPPER);
                        }
                        else
                        {
                            configValue = config.Value.ToJSON();
                        }
                        script.AppendFormat("{0}:{1}{2}", config.Key, configValue,Environment.NewLine);
                    }
                }
            }
            script.AppendFormat("{1}}}{0}", Environment.NewLine, tabs);
            //
            return script.ToString();
        }
        #endregion

        #endregion

        #region IJavascriptObject 成员

        /// <summary>
        /// Javascript对象转换成对象的脚本块
        /// <remarks>
        /// <para>对于Ext JS的类,生成脚本块都采用配置项的方式来生成.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns>Javascript脚本块</returns>
        public virtual string ToScriptBlock(int pPrevTabs)
        {
            return ExtJSClass.ToConfigScript(pPrevTabs, this.InitConfigs);
        }

        #endregion
    }
}
