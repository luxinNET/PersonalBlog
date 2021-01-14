using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace PersonalBlogweb.Common
{
    public class EnumHelper
    {
        private static readonly ILogger _logger;

        static EnumHelper()
        {
            _logger = LogHelper.CreateLogger<EnumHelper>();
        }

        /// <summary>
        /// 获取枚举的说明信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetEnumDescription<T>()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                    dic.Add(field.Name, description);
                }
            }
            return dic;
        }

        /// <summary>     
        /// 获取对应的枚举描述（中文）
        /// </summary>     
        public static List<KeyValuePair<string, string>> GetEnumDescriptionList<T>()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                    result.Add(new KeyValuePair<string, string>(field.Name, description));
                }
            }

            return result;
        }

        /// <summary>
        /// 获取枚举的描述文本
        /// </summary>
        /// <param name="obj">枚举成员</param>
        /// <returns></returns>
        public static string GetEnumDescription(object obj)
        {
            System.Reflection.FieldInfo[] ms;
            try
            {
                //获取字段信息
                ms = obj.GetType().GetFields();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取枚举的描述文本异常");
                return "数据异常：" + ex.Message;
            }

            Type t = obj.GetType();
            foreach (System.Reflection.FieldInfo f in ms)
            {
                //判断名称是否相等
                if (f.Name != obj.ToString()) continue;

                //反射出自定义属性
                foreach (Attribute attr in f.GetCustomAttributes(true))
                {
                    //类型转换找到一个Description，用Description作为成员名称
                    System.ComponentModel.DescriptionAttribute dscript = attr as System.ComponentModel.DescriptionAttribute;
                    if (dscript != null)
                        return dscript.Description;
                }
            }

            //如果没有检测到合适的注释，则用默认名称
            return obj.ToString();
        }


        public static string GetEnumDescription<TEnum>(object value)
        {
            Type enumType = typeof(TEnum);
            string enumItem = Enum.GetName(enumType, Convert.ToInt32(value));
            if (enumItem == null) { return String.Empty; }

            object[] objs = enumType.GetField(enumItem).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)objs[0]).Description;
        }

        /// <summary>
        /// 取得枚举项的描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="enumValue">枚举数值</param>
        /// <returns></returns>
        public static string GetDescription(Type type, int enumValue)
        {
            string descriptions = string.Empty;
            var custAttr = type.GetCustomAttributes(typeof(FlagsAttribute), false);
            if (custAttr != null && custAttr.Length > 0)
            {
                Array enumValues = Enum.GetValues(type);
                String[] enumNames = Enum.GetNames(type);
                for (int i = 0; i < enumValues.Length; i++)
                {
                    if ((Convert.ToInt32(enumValues.GetValue(i)) & enumValue) == Convert.ToInt32(enumValues.GetValue(i)))
                    {
                        if (descriptions != string.Empty)
                            descriptions += ";";
                        descriptions += GetDescription(type, enumNames[i]);
                    }
                }
                return descriptions;
            }
            return GetDescription(type, Enum.GetName(type, enumValue));
        }

        /// <summary>
        /// 根据枚举字符取得Description特性的值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="enumStr"></param>
        /// <returns></returns>
        public static string GetDescription(Type type, string enumStr)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException("传入的值必须是枚举类型。");
            }
            MemberInfo[] memInfos = type.GetMembers();
            MemberInfo memInfo = null;
            string temp = string.Empty;
            for (int i = 0; i < memInfos.Length; i++)
            {
                memInfo = memInfos[i];
                if (string.Compare(enumStr, memInfo.Name, true) == 0)
                {
                    object[] attrs = memInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        temp = ((DescriptionAttribute)attrs[0]).Description;
                    }
                    return temp;
                }
            }
            return "";
            //throw new Exception("未知的枚举描述");
        }

        /// <summary>
        /// 校验输入是否与枚举匹配
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns>是否匹配</returns>
        public static bool CheckEnum(string input, Type enumType)
        {
            if (enumType.BaseType != typeof(Enum) || String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var underlyingType = Enum.GetUnderlyingType(enumType);
            foreach (var value in Enum.GetValues(enumType))
            {
                if (Convert.ChangeType(value, underlyingType).ToString() == input || value.ToString() == input)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 将枚举值转换为数据字典
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(Type enumType)
        {
            var underlyingType = Enum.GetUnderlyingType(enumType);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var value in Enum.GetValues(enumType))
            {
                var underlyingValue = Convert.ChangeType(value, underlyingType);
                var name = Enum.GetName(enumType, value);
                var desc = enumType.GetField(name).GetCustomAttribute(typeof(DescriptionAttribute), false);

                if (desc == null)
                {
                    dic.Add(underlyingValue.ToString(), name);
                }
                else
                {
                    dic.Add(underlyingValue.ToString(), ((DescriptionAttribute)desc).Description);
                }
            }
            return dic;
        }

        public static object ParseByDescription(Type type, string description)
        {
            if (String.IsNullOrWhiteSpace(description))
            {
                return null;
            }
            if (!type.IsEnum)
            {
                throw new ArgumentException("传入的类型必须是枚举。");
            }

            foreach (FieldInfo f in type.GetFields())
            {
                //反射出自定义属性
                foreach (Attribute attr in f.GetCustomAttributes(true))
                {
                    //类型转换找到一个Description，用Description作为成员名称
                    DescriptionAttribute dscript = attr as DescriptionAttribute;
                    if (dscript == null || String.IsNullOrWhiteSpace(dscript.Description))
                    {
                        continue;
                    }
                    if (dscript.Description.Equals(description))
                        return Enum.Parse(type, f.Name);
                }
            }

            //如果没有匹配到合适的注释，返回空值
            return null;
        }
    }
}
