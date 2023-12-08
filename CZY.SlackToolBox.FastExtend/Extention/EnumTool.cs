using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CZY.SlackToolBox.FastExtend.Extention
{
    public static class EnumTool
    {

        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }
        /// <summary>
        /// 获取枚举的注释
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public static string GetDescription(this Enum value)
        {
            if (value == null) return "";
            System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            object[] attribArray = fieldInfo.GetCustomAttributes(false);
            if (attribArray.Length == 0)
                return value.ToString();
            else
                return (attribArray[0] as DescriptionAttribute).Description;
        }

        /// <summary>
        /// 将枚举类型转为选项列表
        /// 注：Value为值,Text为显示内容
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static List<object> ToOptionList(this Type enumType)
        {
            var values = Enum.GetValues(enumType);
            List<object> list = new List<object>();
            foreach (var aValue in values)
            {
                list.Add(new
                {
                    Value = (int)aValue,
                    Text = aValue.ToString()
                });
            }

            return list;
        }
    }
}
