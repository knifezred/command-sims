﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using KnifeZ.Unity.Extensions;

namespace CommandSims.Utils
{
    /// <summary>
    /// 属性辅助类
    /// </summary>
    public static class PropertyUtils
    {
        /// <summary>
        /// 获取属性名
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="getAll">是否获取全部级别名称，比如a.b.c</param>
        /// <returns>属性名</returns>
        public static string GetPropertyName(this Expression expression, bool getAll = true)
        {
            if (expression == null)
            {
                return "";
            }
            MemberExpression me = null;
            if (expression is MemberExpression)
            {
                me = expression as MemberExpression;
            }
            if (expression is LambdaExpression)
            {
                LambdaExpression le = expression as LambdaExpression;
                if (le.Body is MemberExpression)
                {
                    me = le.Body as MemberExpression;
                }
                if (le.Body is UnaryExpression)
                {
                    me = (le.Body as UnaryExpression).Operand as MemberExpression;
                }
            }
            string rv = "";
            if (me != null)
            {
                rv = me.Member.Name;
            }
            while (me != null && getAll && me.NodeType == ExpressionType.MemberAccess)
            {
                Expression exp = me.Expression;
                if (exp is MemberExpression)
                {
                    rv = (exp as MemberExpression).Member.Name + "." + rv;
                    me = exp as MemberExpression;
                }
                else if (exp is MethodCallExpression)
                {
                    var mexp = exp as MethodCallExpression;
                    if (mexp.Method.Name == "get_Item")
                    {
                        object index;
                        if (mexp.Arguments[0] is MemberExpression)
                        {
                            var obj = ((mexp.Arguments[0] as MemberExpression).Expression as ConstantExpression).Value;
                            index = obj.GetType().GetField((mexp.Arguments[0] as MemberExpression).Member.Name).GetValue(obj);
                        }
                        else
                        {
                            index = (mexp.Arguments[0] as ConstantExpression).Value;
                        }
                        rv = (mexp.Object as MemberExpression).Member.Name + "[" + index + "]." + rv;
                        me = mexp.Object as MemberExpression;
                    }
                }
                else
                {
                    break;
                }
            }
            return rv;
        }

        public static Expression GetMemberExp(this ParameterExpression self, Expression member)
        {
            return self.GetMemberExp(member.GetPropertyName());
        }

        public static Expression GetMemberExp(this ParameterExpression self, string memberName)
        {
            var names = memberName.Split(',');
            Expression rv = Expression.PropertyOrField(self, names[0]); ;
            for (int i = 1; i < names.Length; i++)
            {
                rv = Expression.PropertyOrField(rv, names[i]);
            }
            return rv;
        }


        /// <summary>
        /// 获取属性名的Id形式，将属性名中的.转换为_，适合作为HTML中的Id使用
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="getAll">是否获取全部级别名称，比如a.b.c</param>
        /// <returns>属性Id</returns>
        public static string GetPropertyId(this Expression expression, bool getAll = true)
        {
            return expression.GetPropertyName(getAll).GetIdByName();
        }

        /// <summary>
        /// 获取正则表达式错误
        /// </summary>
        /// <param name="pi">属性信息</param>
        /// <returns>错误文本</returns>
        public static string GetRegexErrorMessage(this MemberInfo pi)
        {
            string rv = "";
            if (pi.GetCustomAttributes(typeof(RegularExpressionAttribute), false).FirstOrDefault() is RegularExpressionAttribute dis && !string.IsNullOrEmpty(dis.ErrorMessage))
            {
                rv = dis.ErrorMessage;
            }
            return rv;
        }

        /// <summary>
        /// 获取属性显示名称
        /// </summary>
        /// <param name="pi">属性信息</param>
        /// <returns>属性名称</returns>
        public static string GetPropertyDisplayName(this MemberInfo pi)
        {
            string rv;
            if (pi.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() is DisplayAttribute dis && !string.IsNullOrEmpty(dis.Name))
            {
                rv = dis.Name;
            }
            else
            {
                rv = pi.Name;
            }
            return rv;
        }

        /// <summary>
        /// 获取属性显示名称
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <returns>属性显示名称</returns>
        public static string GetPropertyDisplayName(this Expression expression)
        {
            return expression.GetPropertyInfo().GetPropertyDisplayName();
        }


        /// <summary>
        /// 获取枚举显示名称
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>枚举显示名称</returns>
        public static string GetEnumDisplayName(this Enum value)
        {
            return GetEnumDisplayName(value.GetType(), value.ToString());
        }

        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <returns>属性信息</returns>
        public static PropertyInfo GetPropertyInfo(this Expression expression)
        {
            MemberExpression me = null;
            LambdaExpression le = null;
            if (expression is MemberExpression)
            {
                me = expression as MemberExpression;
            }
            if (expression is LambdaExpression)
            {
                le = expression as LambdaExpression;
                if (le.Body is MemberExpression)
                {
                    me = le.Body as MemberExpression;
                }
                if (le.Body is UnaryExpression)
                {
                    me = (le.Body as UnaryExpression).Operand as MemberExpression;
                }
            }
            PropertyInfo rv = null;
            if (me != null)
            {
                rv = me.Member.DeclaringType.GetProperties().Where(x => x.Name == me.Member.Name).FirstOrDefault();
            }
            return rv;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="exp">属性表达式</param>
        /// <param name="obj">属性所在实例</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(this object obj, LambdaExpression exp)
        {
            //获取表达式的值，并过滤单引号
            try
            {
                var expValue = exp.Compile().DynamicInvoke(obj);
                object val = expValue;
                return val;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 判断属性是否必填
        /// </summary>
        /// <param name="pi">属性信息</param>
        /// <returns>是否必填</returns>
        public static bool IsPropertyRequired(this MemberInfo pi)
        {
            bool isRequired = false;
            //如果需要显示星号，则判断是否是必填项，如果是必填则在内容后面加上星号
            //所有int，float。。。这种Primitive类型的，肯定都是必填
            Type t = pi.GetMemberType();
            if (t != null && (t.IsPrimitive() || t.IsEnum() || t == typeof(decimal) || t == typeof(Guid)))
            {
                isRequired = true;
            }
            else
            {
                //对于其他类，检查是否有RequiredAttribute，如果有就是必填
                if (pi.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() is RequiredAttribute required && required.AllowEmptyStrings == false)
                {
                    isRequired = true;
                }
                else if (pi.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault() != null)
                {
                    isRequired = true;
                }
            }
            return isRequired;
        }

        /// <summary>
        /// 根据MemberInfo获取值
        /// </summary>
        /// <param name="mi">MemberInfo</param>
        /// <param name="obj">所在实例</param>
        /// <param name="index">如果是数组，指定数组下标。默认为null</param>
        /// <returns>MemberInfo的值</returns>
        public static object GetMemberValue(this MemberInfo mi, object obj, object[] index = null)
        {
            object rv = null;
            if (mi.MemberType == MemberTypes.Property)
            {
                rv = ((PropertyInfo)mi).GetValue(obj, index);
            }
            else if (mi.MemberType == MemberTypes.Field)
            {
                rv = ((FieldInfo)mi).GetValue(obj);
            }
            return rv;
        }

        /// <summary>
        /// 设定MemberInfo的值
        /// </summary>
        /// <param name="mi">MemberInfo</param>
        /// <param name="obj">所在实例</param>
        /// <param name="val">要赋的值</param>
        /// <param name="index">如果是数组，指定数组下标。默认为null</param>
        public static void SetMemberValue(this MemberInfo mi, object obj, object val, object[] index = null)
        {
            object newval = val;
            if (val is string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    val = null;
                }
            }
            if (val != null && val.GetType() != mi.GetMemberType())
            {
                newval = val.ConvertValue(mi.GetMemberType());
            }
            if (mi.MemberType == MemberTypes.Property)
            {
                ((PropertyInfo)mi).SetValue(obj, newval, index);
            }
            else if (mi.MemberType == MemberTypes.Field)
            {
                ((FieldInfo)mi).SetValue(obj, newval);
            }
        }

        /// <summary>
        /// 获取某个MemberInfo的类型
        /// </summary>
        /// <param name="mi">MemberInfo</param>
        /// <returns>类型</returns>
        public static Type GetMemberType(this MemberInfo mi)
        {
            Type rv = null;
            if (mi.MemberType == MemberTypes.Property)
            {
                rv = ((PropertyInfo)mi).PropertyType;
            }
            else if (mi.MemberType == MemberTypes.Field)
            {
                rv = ((FieldInfo)mi).FieldType;
            }
            return rv;
        }

        /// <summary>
        /// 获取枚举显示名称
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="value">枚举值</param>
        /// <returns>枚举显示名称</returns>
        public static string GetEnumDisplayName(Type enumType, string value)
        {
            string rv = "";
            FieldInfo field = null;

            if (enumType.IsEnum())
            {
                field = enumType.GetField(value);
            }
            //如果是nullable的枚举
            if (enumType.IsGeneric(typeof(Nullable<>)) && enumType.GetGenericArguments()[0].IsEnum())
            {
                field = enumType.GenericTypeArguments[0].GetField(value);
            }

            if (field != null)
            {

                var attribs = field.GetCustomAttributes(typeof(DisplayAttribute), true).ToList();
                if (attribs.Count > 0)
                {
                    rv = ((DisplayAttribute)attribs[0]).GetName();
                }
                else
                {
                    rv = value;
                }
            }
            return rv;
        }

        public static string GetEnumDisplayName(Type enumType, int value)
        {
            string rv = "";
            FieldInfo field = null;
            string ename = "";
            if (enumType.IsEnum())
            {
                ename = enumType.GetEnumName(value);
                if (string.IsNullOrEmpty(ename))
                {
                    return rv;
                }

                field = enumType.GetField(ename);
            }
            //如果是nullable的枚举
            if (enumType.IsGeneric(typeof(Nullable<>)) && enumType.GetGenericArguments()[0].IsEnum())
            {
                ename = enumType.GenericTypeArguments[0].GetEnumName(value);
                field = enumType.GenericTypeArguments[0].GetField(ename);
            }

            if (field != null)
            {

                var attribs = field.GetCustomAttributes(typeof(DisplayAttribute), true).ToList();
                if (attribs.Count > 0)
                {
                    rv = ((DisplayAttribute)attribs[0]).GetName();
                }
                else
                {
                    rv = ename;
                }
            }
            return rv;
        }

        /// <summary>
        /// 转化值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="propertyType">转换后的类型</param>
        /// <returns>转换后的值</returns>
        public static object ConvertValue(this object value, Type propertyType)
        {
            object val = null;
            if (propertyType.IsGeneric(typeof(Nullable<>)) == true)
            {
                var gs = propertyType.GenericTypeArguments;
                try
                {
                    val = value.ConvertValue(gs[0]);
                }
                catch { }
            }
            else if (propertyType.IsEnum())
            {
                val = Enum.Parse(propertyType, value.ToString());
            }
            else if (propertyType == typeof(string))
            {
                val = value?.ToString().Trim();
            }
            else if (propertyType == typeof(Guid))
            {
                bool suc = Guid.TryParse(value.ToString(), out Guid g);
                if (suc)
                {
                    val = g;
                }
                else
                {
                    val = Guid.Empty;
                }
            }
            //else if (propertyType == typeof(DateRange))
            //{
            //    if (DateRange.TryParse(value.ToString(), out var result))
            //    {
            //        val = result;
            //    }
            //    else
            //    {
            //        val = DateRange.Default;
            //    }
            //}
            else
            {
                try
                {
                    if (value.ToString().StartsWith("`") && value.ToString().EndsWith("`"))
                    {
                        string inner = value.ToString().Trim('`').TrimEnd(',');
                        if (!string.IsNullOrWhiteSpace(inner))
                        {
                            val = propertyType.GetConstructor(Type.EmptyTypes).Invoke(null);
                            string[] pair = inner.Split(',');
                            var gs = propertyType.GetGenericArguments();
                            foreach (var p in pair)
                            {
                                (val as IList).Add(Convert.ChangeType(p, gs[0]));
                            }
                        }
                    }
                    else
                    {
                        val = Convert.ChangeType(value.ToString(), propertyType);
                    }
                }
                catch
                {
                }
            }
            return val;
        }
    }
}
