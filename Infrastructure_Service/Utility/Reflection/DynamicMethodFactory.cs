/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/20 10:39:49
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
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace JIT.Utility.Reflection
{   
    /// <summary>
    /// 动态方法工厂 
    /// </summary>
    public static class DynamicMethodFactory
    {
        /// <summary>
        /// 创建对象属性值的设置器
        /// <remarks>
        /// <para>1.属性的设置器即使是非公有的，也可以进行赋值.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pProperty">属性值</param>
        /// <returns>设置属性值的委托</returns>
        public static SetValueDelegate CreatePropertySetter(PropertyInfo pProperty)
        {
            //参数检查
            if (pProperty == null)
                throw new ArgumentNullException("pProperty");
            if (!pProperty.CanWrite)
                throw new InvalidOperationException(string.Format("属性{0}不支持写操作.",pProperty.Name));
            //获取属性的设置器方法
            MethodInfo setMethod = pProperty.GetSetMethod(true);
            //使用EMIT创建动态方法
            DynamicMethod dm = new DynamicMethod("PropertySetter", null, new Type[] { typeof(object), typeof(object) }, pProperty.DeclaringType, true);
            ILGenerator il = dm.GetILGenerator();
            if (!setMethod.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
            }
            il.Emit(OpCodes.Ldarg_1);
            EmitCastToReference(il, pProperty.PropertyType);
            if (!setMethod.IsStatic && !pProperty.DeclaringType.IsValueType)
            {
                il.EmitCall(OpCodes.Callvirt, setMethod, null);
            }
            else
            {
                il.EmitCall(OpCodes.Call, setMethod, null);
            }
            il.Emit(OpCodes.Ret);
            //返回委托
            return (SetValueDelegate)dm.CreateDelegate(typeof(SetValueDelegate));
        }

        /// <summary>
        /// 引用类型和值类型的处理
        /// </summary>
        /// <param name="pIL"></param>
        /// <param name="pType"></param>
        private static void EmitCastToReference(ILGenerator pIL, Type pType)
        {
            if (pType.IsValueType)
                pIL.Emit(OpCodes.Unbox_Any, pType);
            else
                pIL.Emit(OpCodes.Castclass, pType);
        }
    }
}
