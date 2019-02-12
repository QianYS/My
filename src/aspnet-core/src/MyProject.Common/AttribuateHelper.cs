using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common
{
    /// <summary>
    /// 虽然特性是静态定义的，但是 .net通过反射获取特性时，会以反复创建实例的方式获取，可能存在一定的性能问题
    /// 此处对特性进行缓存处理
    /// </summary>
    public class AttribuateHelper
    {
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object _lockObj = new object();

        /// <summary>
        /// 静态缓存
        /// </summary>
        private static Dictionary<string, Attribute> _AttributeCacheList = new Dictionary<string, Attribute>();

        /// <summary>
        /// 获取枚举上定义的特性
        /// 这里会使用缓存
        /// </summary>
        /// <typeparam name="TAttribuate"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TAttribuate GetAttribuate<TAttribuate>(Enum value) where TAttribuate : Attribute
        {
            if (value != null)
            {
                Type enumType = value.GetType();//枚举类型
                Type attrType = typeof(TAttribuate);//特性类型
                string enumFieldName = Enum.GetName(enumType, value);//字段名

                if (string.IsNullOrEmpty(enumFieldName))
                {
                    return null;
                }

                string key = $"{enumType.FullName}@{enumFieldName}@{attrType.FullName}";
                Attribute attr = null;
                lock (_lockObj)
                {
                    //缓存中获取特性
                    if (!_AttributeCacheList.TryGetValue(key, out attr))
                    {
                        attr = null;
                        //缓存中不存在，取字段上的自定义特性
                        FieldInfo propInfo = enumType.GetField(enumFieldName);
                        if (propInfo != null)
                        {
                            if (propInfo.IsDefined(attrType, true))
                            {
                                attr = propInfo.GetCustomAttributes(attrType, true)[0] as TAttribuate;
                            }
                        }
                        _AttributeCacheList.Add(key, attr);//加入缓存队列
                    }
                }
                return attr as TAttribuate;
            }
            else
            {
                return null;
            }
        }
    }
}
