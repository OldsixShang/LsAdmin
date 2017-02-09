using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ls.Reflection {
    /// <summary>
    /// 反射帮助类。
    /// </summary>
    public static class ReflectionHelper {
        /// <summary>
        /// 判断<paramref name="givenType"/>是否为<paramref name="genericType"/>的泛型。
        /// </summary>
        /// <param name="givenType">给定类型</param>
        /// <param name="genericType">泛型参数类型</param>
        /// <returns>返回结果。</returns>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType) {
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType) {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces()) {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType) {
                    return true;
                }
            }

            if (givenType.BaseType == null) {
                return false;
            }

            return IsAssignableToGenericType(givenType.BaseType, genericType);
        }

        /// <summary>
        /// 获取方法的指定特性声明。
        /// </summary>
        /// <typeparam name="TAttribute">特性类型参数</typeparam>
        /// <param name="memberInfo">方法信息</param>
        /// <returns>返回特性声明列表。</returns>
        public static List<TAttribute> GetAttributesOfMemberAndDeclaringType<TAttribute>(MemberInfo memberInfo)
            where TAttribute : Attribute {
            List<TAttribute> attributeList = new List<TAttribute>();

            if (memberInfo.IsDefined(typeof(TAttribute), true)) {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.IsDefined(typeof(TAttribute), true)) {
                attributeList.AddRange(memberInfo.DeclaringType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            return attributeList;
        }
    }
}
