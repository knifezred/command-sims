﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace KnifeZ.Unity.Extensions
{
    /// <summary>
    /// Type的扩展函数
    /// </summary>
    public static class TypeExtension
    {        
        /// <summary>
        /// 判断是否是泛型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <param name="innerType">泛型类型</param>
        /// <returns>判断结果</returns>
        public static bool IsGeneric(this Type self, Type innerType)
        {
            if (self.GetTypeInfo().IsGenericType && self.GetGenericTypeDefinition() == innerType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否为Nullable<>类型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsNullable(this Type self)
        {
            return self.IsGeneric(typeof(Nullable<>));
        }

        /// <summary>
        /// 判断是否为List<>类型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsList(this Type self)
        {
            return self.IsGeneric(typeof(List<>)) || self.IsGeneric(typeof(IEnumerable<>));
        }

        /// <summary>
        /// 判断是否为List<>类型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsListOf<T>(this Type self)
        {
            if (self.IsGeneric(typeof(List<>)) && typeof(T).IsAssignableFrom(self.GenericTypeArguments[0]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #region 判断是否为枚举

        /// <summary>
        /// 判断是否为枚举
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsEnum(this Type self)
        {
            return self.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// 判断是否为枚举或者可空枚举
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsEnumOrNullableEnum(this Type self)
        {
            if (self == null)
            {
                return false;
            }
            if (self.IsEnum)
            {
                return true;
            }
            else
            {
                if (self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Nullable<>) && self.GetGenericArguments()[0].IsEnum)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        /// <summary>
        /// 判断是否为值类型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsPrimitive(this Type self)
        {
            return self.GetTypeInfo().IsPrimitive || self == typeof(decimal);
        }

        public static bool IsNumber(this Type self)
        {
            Type checktype = self;
            if (self.IsNullable())
            {
                checktype = self.GetGenericArguments()[0];
            }
            if (checktype == typeof(int) || checktype == typeof(short) || checktype == typeof(long) || checktype == typeof(float) || checktype == typeof(decimal) || checktype == typeof(double))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #region 判断是否是Bool

        public static bool IsBool(this Type self)
        {
            return self == typeof(bool);
        }

        /// <summary>
        /// 判断是否是 bool or bool?类型
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsBoolOrNullableBool(this Type self)
        {
            if (self == null)
            {
                return false;
            }
            if (self == typeof(bool) || self == typeof(bool?))
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
