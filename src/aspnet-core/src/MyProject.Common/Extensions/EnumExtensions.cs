using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string GetName(this Enum value)
        {
            if (value != null)
            {
                DisplayAttribute displayAttr = AttribuateHelper.GetAttribuate<DisplayAttribute>(value);
                if (displayAttr == null)
                {
                    return Enum.GetName(value.GetType(), value);
                }
                else
                {
                    return displayAttr.Name;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetValue(object e)
        {
            var type = Enum.GetUnderlyingType(e.GetType());
            switch (type.FullName)
            {
                case "System.Boolean":
                    return ((bool)e).ToString();
                case "System.Byte":
                    return ((byte)e).ToString();
                case "System.SByte":
                    return ((sbyte)e).ToString();
                case "System.Char":
                    return ((char)e).ToString();
                case "System.Decimal":
                    return ((decimal)e).ToString();
                case "System.Double":
                    return ((double)e).ToString();
                case "System.Single":
                    return ((float)e).ToString();
                case "System.Int32":
                    return ((int)e).ToString();
                case "System.UInt32":
                    return ((uint)e).ToString();
                case "System.Int64":
                    return ((long)e).ToString();
                case "System.UInt64":
                    return ((ulong)e).ToString();
                case "System.Object":
                    return ((object)e).ToString();
                case "System.Int16":
                    return ((short)e).ToString();
                case "System.UInt16":
                    return ((ushort)e).ToString();
                case "System.String":
                    return ((string)e).ToString();
                case "System.DateTime":
                    return ((DateTime)e).ToString();
                case "System.Guid":
                    return ((Guid)e).ToString();
                default:
                    return ((object)e).ToString();
            }
        }

        /// <summary>
        /// 根据EnumName获取Value
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static int GetEnumValue(Type enumType, string enumName)
        {
            try
            {
                if (!enumType.IsEnum)
                    throw new ArgumentException("enumType必须是枚举类型");
                var values = Enum.GetValues(enumType);
                var ht = new Hashtable();
                foreach (var val in values)
                {
                    ht.Add(Enum.GetName(enumType, val), val);
                }
                return (int)ht[enumName];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
